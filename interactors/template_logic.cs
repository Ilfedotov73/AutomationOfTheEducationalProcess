using contracts.binding_models;
using contracts.interactor_contracts;
using contracts.search_models;
using contracts.storage_contracts;
using contracts.storage_contracts.db_models;
using contracts.worker_contracts;
using Microsoft.Extensions.Logging;
using worker;

namespace interactors {
    public class template_logic : Itemplate_logic {

        private readonly Itemplate_storage _storage;
        private readonly Itemplate_worker _worker;
        private readonly ILogger _logger;
        
        public template_logic(Itemplate_storage storage, Itemplate_worker worker, ILogger<Itemplate_logic> logger) {
            _storage = storage;
            _worker = worker;
            _logger = logger;
        }

        public byte[] insert_template(template_binding_model model) {
            check_model(model);
            byte[] document = _worker.create_template_file(model);

            if (_storage.insert_template(model) == false) {
                throw new Exception("insert operation failed in database");
            }
            return document;
        }

        public void edit_template(template_binding_model model, byte[] rewire_data, string? new_name = null) {

            var editModel = get_template_info(new template_search_model { id = model.id });

            check_model(editModel, false,true);

            File.Exists(editModel.file_path);
            File.WriteAllBytes(editModel.file_path, rewire_data);

            if (!string.IsNullOrEmpty(new_name)) {
                string mote_to = new template_binding_model().file_path + $"{new_name}.xlsx";
                File.Move(editModel.file_path, mote_to);
                editModel.name = new_name;
                editModel.file_path = mote_to;
            }

            if (_storage.edit_tempalte(editModel) == false) {
                throw new Exception("edit operation failed in database");
            };
            _logger.LogInformation($"template:{editModel.id} is edited");
        }

        public void delete_template(template_binding_model model) {
            var delModel = get_template_info(new template_search_model { id = model.id });
            check_model(delModel, true);

            File.Exists(delModel.file_path);
            File.Delete(delModel.file_path);

            if (_storage.delete_template(delModel) == false) {
                throw new Exception("delete operation failed in database");
            }
            _logger.LogInformation($"template:{delModel.id} is deleted");
        }

        public void check_model(template_binding_model model, bool onDelete = false, bool onEdit = false) {
            if (string.IsNullOrEmpty(model.id.ToString())) {
                throw new ArgumentNullException("template id is missing", nameof(model.id));
            }
            if (string.IsNullOrEmpty(model.file_path)) {
                throw new ArgumentNullException("template file path is missing", nameof(model.file_path));
            }
            if (onDelete) {
                return;
            }

            if (string.IsNullOrEmpty(model.name)) {
                throw new ArgumentNullException("template name is missing", nameof(model.name));
            }
            if (onEdit) {
                return;
            }

            var template = get_template_info(new template_search_model { name = model.name });
            if (template != null) {
                throw new Exception("the template is already created");
            }
            
            char[] invalidPathChars = Path.GetInvalidFileNameChars();
            foreach (char i in invalidPathChars) {
                if (model.name.Contains(i)) {
                    throw new Exception("the filename is contains charactera are invalid in a path");
                }
            }
        }

        public List<template_binding_model> get_template_list() {
            var models = _storage.get_template_list();
            if (models.Count == 0) {
                _logger.LogWarning("get_template_list returned an empty list");
                return new();
            }
            _logger.LogInformation($"get_template_list:{models.Count} elements");
            List<template_binding_model> bindingModels = new();
            foreach (var model in models) {
                bindingModels.Add(getBindingModel(model));
            }
            return bindingModels;
        }

        public template_binding_model? get_template_info(template_search_model search_model) {
            if (search_model == null) {
                throw new ArgumentNullException(nameof(search_model));
            }
            var model = _storage.get_template_info(search_model);
            if (model == null) {
                _logger.LogWarning("get_template_info returned null");
                return null;
            }
            _logger.LogInformation($"get_template_info:{model.id}|name:{model.name}|file_path:{model.file_path}|" +
                                $"user_id:{model.UserId}|document_type:{model.document_type}");
            return getBindingModel(model);
        }

        public byte[] on_import_template(template_search_model search_model) {
            var model = get_template_info(search_model);
            if (model == null) {
                throw new Exception("Такого шаблона нет");
            }

            byte[] document = File.ReadAllBytes(model.file_path);
            return document;
        }

        public template_binding_model getBindingModel(Template model) {
            return new() {
                id = model.id,
                name = model.name,
                file_path = model.file_path,
                UserId = model.UserId,
                document_type = model.document_type,
                user = model.user
            };
        }
    }
}
