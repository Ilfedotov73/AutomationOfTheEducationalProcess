using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.view_moedels {
    public class direction_view_model {
        public int id { get; set; }
        public string full_name { get; set; } = string.Empty;
        public string alt_name { get; set; } = string.Empty; 
        public string department_name { get; set; } = string.Empty;
    }
}
