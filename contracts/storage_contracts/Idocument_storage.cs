using contracts.search_models;
using data_models.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.storage_contracts {
    public interface Idocument_storage {
        public List<Idocumnet> get_document_list();
        public List<Idocumnet> get_document_filltered_list(document_search_model search_model);
        public Idocumnet get_document_info(document_search_model search_model);
        public bool insert_document(Idocumnet model);
        public bool delete_docuemnt(Idocumnet model);
        public bool edit_docuemnt(Idocumnet model);
    }
}
