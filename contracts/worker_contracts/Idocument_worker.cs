using contracts.worker_contracts.helper_models;
using data_models.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace contracts.worker_contracts {
    public interface Idocument_worker {
        public void create_document(Idocument model);
        public List<int> read_temp_file(int template_id);
        public Idata_info prepare_data(Idocument model);
        public void save_doc_in_file(byte[] data, string file_path);
    }
}
