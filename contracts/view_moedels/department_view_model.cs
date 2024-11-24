using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.view_moedels {
    public class department_view_model {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string faculty_name { get; set; } = string.Empty;
    }
}
