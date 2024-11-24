using data_models.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.binding_models {
    public class faculty_binding_model : Ifaculty {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
    }
}
