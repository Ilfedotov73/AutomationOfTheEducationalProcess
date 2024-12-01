using data_models.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.binding_models {
    public class template_binding_model : Itemplate {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string file_path { get; set; } = string.Empty;
    }
}
