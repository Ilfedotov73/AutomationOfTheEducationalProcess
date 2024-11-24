using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.view_moedels {
    public class document_view_model {
        public int id { get; set; }
        public string date { get; set; } = string.Empty;
        public string file_format_type { get; set; } = string.Empty;
        public string author_name { get; set; } = string.Empty;
        public string template_name { get; set; } = string.Empty;
    }
}
