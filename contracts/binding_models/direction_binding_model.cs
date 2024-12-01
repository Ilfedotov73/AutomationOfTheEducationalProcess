using data_models.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.binding_models {
    public class direction_binding_model : Idirection {
        public int id { get; set; }
        public string full_name { get; set; } = string.Empty;
        public string alt_name { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
    }
}
