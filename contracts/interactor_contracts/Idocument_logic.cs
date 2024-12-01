﻿using contracts.search_models;
using data_models.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.interactor_contracts {
    public interface Idocument_logic {
        public List<Idocument> get_document_list();
        public List<Idocument> get_document_filltered_list(document_search_model search_model);
        public Idocument get_document_info (document_search_model search_model);
        public void insert_document(Idocument model);
        public void delete_document(Idocument model);
        public void edit_document(Idocument model);
        public bool check_model(Idocument model);
        public void save_doc_in_file(byte[] data, Idocument model);
        public byte[] on_export_docfile(document_search_model search_odel);
    }
}
