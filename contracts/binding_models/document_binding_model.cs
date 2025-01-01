using data_models.Enums;
using data_models.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.binding_models {
    public class document_binding_model : Idocument {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string file_path { get; set; } = string.Empty;
        public int UserId { get; set; }
        public enum_file_format_type file_format_type { get; set; }
        public enum_document_type document_type { get; set; }
        public int TemplateId { get; set; }
    }
}
