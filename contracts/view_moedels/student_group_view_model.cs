using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.view_moedels {
    public class student_group_view_model {
        public int id { get; set; }
        public string direction_name { get; set; } = string.Empty;
        public List<string> students { get; set; } = new();
    }
}
