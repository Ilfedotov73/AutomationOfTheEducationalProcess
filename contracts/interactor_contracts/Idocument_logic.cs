using contracts.binding_models;
using contracts.search_models;
using data_models.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.interactor_contracts {
    public interface Idocument_logic {
        public List<document_binding_model> get_document_list(document_search_model search_model);
        public document_binding_model? get_document_info (document_search_model search_model);
        public void insert_document(Idocument model);
        public void delete_document(Idocument model);
        public void edit_document(Idocument model, byte[] rewire_data);
        public void check_model(Idocument model, bool onDelete);
        public byte[] on_export_docfile(document_search_model search_odel);
    }
}
