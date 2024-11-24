using contracts.search_models;
using data_models.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.interactor_contracts {
    public interface Idocument_logic {
        public List<Idocumnet> get_document_list();
        public List<Idocumnet> get_document_filltered_list(document_search_model search_model);
        public Idocumnet get_document_info (document_search_model search_model);
        public void insert_document(Idocumnet model);
        public void delete_document(Idocumnet model);
        public void edit_document(Idocumnet model);
        public bool check_model(Idocumnet model);
        public void save_doc_in_file(byte[] data, Idocumnet model);
        public byte[] on_export_docfile(document_search_model search_odel);
    }
}
