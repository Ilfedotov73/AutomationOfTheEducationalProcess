using contracts.binding_models;
using contracts.search_models;
using data_models.IModels;

namespace contracts.interactor_contracts {
    public interface Idocument_logic {
        public List<document_binding_model> get_document_list(document_search_model? search_model);
        public document_binding_model? get_document_info (document_search_model search_model);
        public void insert_document(Idocument model);
        public void delete_document(Idocument model);
        public void edit_document(Idocument model, byte[] rewire_data);
        public void check_model(Idocument model, bool onDelete, bool onEdit);
        public byte[] on_export_docfile(document_search_model search_odel);
    }
}
