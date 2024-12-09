using contracts.binding_models;
using contracts.interactor_contracts;
using contracts.search_models;
using contracts.storage_contracts;
using contracts.storage_contracts.db_models;
using data_models.Enums;
using data_models.IModels;
using worker;

namespace interactors {
    public class document_logic : Idocument_logic {

        private readonly Idocument_storage _storage;
        private readonly Itemplate_logic _template_logic;

        public document_logic(Idocument_storage storage, Itemplate_logic template_logic) {
            _storage = storage;
            _template_logic = template_logic;
        }

        public void insert_document(Idocument model) {
            check_model(model);
            model.file_path += $"{model.name}" + $".{model.file_format_type}";

            var template_info = _template_logic.get_template_info(new template_search_model { id = model.TemplateId });

            switch(model.document_type) {
                case enum_document_type.individual_teacher_plan_document:
                    document_itp_facade.is_function itp_funcs = document_itp_facade.get_function(model.file_format_type);
                    itp_funcs(model, template_info);
                    break;
                case enum_document_type.statement_document:
                    document_st_facade.is_function st_funcs = document_st_facade.get_function(model.file_format_type);
                    st_funcs(model);
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (_storage.insert_document(model) == false) {
                throw new Exception("insert operation failed in database");
            }
        }

        public void edit_document(Idocument model, byte[] rewire_data) {
            check_model(model);

            File.Exists(model.file_path);
            File.WriteAllBytes(model.file_path, rewire_data);

            if (_storage.edit_docuemnt(model) == false) {
                throw new Exception("edit operation failed in database");
            }
        }

        public void delete_document(Idocument model) {
            check_model(model, true);
 
            File.Exists(model.file_path);
            File.Delete(model.file_path);

            if (_storage.delete_docuemnt(model) == false) {
                throw new Exception("delete operation failed in database");
            }
        }

        public void check_model(Idocument model, bool onDelete = false) {
            if (string.IsNullOrEmpty(model.id.ToString())) {
                throw new ArgumentNullException("document id is missing", nameof(model.id));
            }
            if (onDelete) {
                return;
            }
            if (string.IsNullOrEmpty(model.file_path)) {
                throw new ArgumentNullException("document file path is missing", nameof(model.file_path));
            }
            if (string.IsNullOrEmpty(model.UserId.ToString())) {
                throw new ArgumentNullException("document author is missing", nameof(model.UserId));
            }
            if (string.IsNullOrEmpty(model.file_format_type.ToString())) {
                throw new ArgumentNullException("document file format type is missing", nameof(model.file_format_type));
            }
            if (string.IsNullOrEmpty(model.document_type.ToString())) {
                throw new ArgumentNullException("document type is missing", nameof(model.document_type));
            }
        }

        public List<document_binding_model> get_document_list(document_search_model search_model) {
            var models = search_model == null ? _storage.get_document_list() : _storage.get_document_filltered_list(search_model);
            if  (models.Count == 0) {
                return new();
            }
            List<document_binding_model> bindingModels = new();
            foreach (var model in models) {
                bindingModels.Add(getBindingModel(model));
            }
            return bindingModels;
        }

        public document_binding_model? get_document_info(document_search_model search_model) {
            if (search_model == null) {
                throw new ArgumentNullException(nameof(search_model));
            }
            var model = _storage.get_document_info(search_model);
            if (model == null) {
                return null;                
            }
            return getBindingModel(model);
        }

        public byte[] on_export_docfile(document_search_model search_model) {
            var document = get_document_info(search_model);
            if (document == null) {
                throw new Exception("Такого документа нет");
            }

            byte[] file = File.ReadAllBytes(document.file_path);
            return file;
        }

        document_binding_model getBindingModel(Document model) {
            return new() {
                id = model.id,
                name = model.name,
                file_path = model.file_path,
                UserId = model.UserId,
                file_format_type = model.file_format_type,
                document_type = model.document_type,
                TemplateId = model.TemplateId
            };
        }
    }
}