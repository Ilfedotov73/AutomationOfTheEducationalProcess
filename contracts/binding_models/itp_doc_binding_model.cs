﻿using data_models.Enums;
using data_models.IModels.document_extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.binding_models {
    public class itp_doc_binding_model : Iitp_doc {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string file_path { get; set; } = string.Empty;
        public int author_id { get; set; }
        public enum_file_format_type file_format_type { get; set; }
        public int template_id { get; set; }
    }
}