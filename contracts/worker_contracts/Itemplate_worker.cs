using contracts.binding_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.worker_contracts {
    public interface Itemplate_worker {
        public byte[] create_template_file(template_binding_model model);
        public List<string> read_temp_file(template_binding_model model);
    }
}
