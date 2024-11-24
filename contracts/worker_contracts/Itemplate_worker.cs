using contracts.binding_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.worker_contracts {
    public interface Itemplate_worker {
        public void create_template_file(template_binding_model model);
        public void save_template_in_file(byte[] data, string file_path);
    }
}
