using data_models.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.binding_models {
    public class department_binding_model : Idepartment {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public int faculty_id { get; set; }
    }
}
