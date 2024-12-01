using data_models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.search_models {
    public class document_search_model {
        public int? id { get; set; }
        public string? name { get; set; }
        public enum_file_format_type? file_format_type { get; set; }
        public enum_document_type? document_type { get; set; }
        public int? author_id { get; set; }
        public int? template_id { get; set; }
    }
}
