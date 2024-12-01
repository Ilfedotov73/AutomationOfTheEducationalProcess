using contracts.search_models;
using contracts.storage_contracts.db_models;
using data_models.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.storage_contracts {
    public interface Idocument_storage {
        public List<Document> get_document_list();
        public List<Document> get_document_filltered_list(document_search_model search_model);
        public Document? get_document_info(document_search_model search_model);
        public bool insert_document(Idocument model);
        public bool delete_docuemnt(Idocument model);
        public bool edit_docuemnt(Idocument model);
    }
}
