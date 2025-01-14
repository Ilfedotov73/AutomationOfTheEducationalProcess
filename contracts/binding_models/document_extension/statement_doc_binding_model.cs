﻿using data_models.Enums;
using data_models.IModels;
using data_models.IModels.document_extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.binding_models.document_extension {
    public class statement_doc_binding_model : Istatement_doc {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string file_path { get; set; } = $@"C:\Users\Ilfe\Documents\AutomationOfTheEducationalProcess\STATEMENT\";
        public DateOnly date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public int UserId { get; set; }
        public enum_file_format_type file_format_type { get; set; }

        public enum_document_type document_type { get; set; } = enum_document_type.statement_document;
        public int TemplateId { get; set; }

        public int student_group_id { get; set; }
        public Dictionary<int, (Istudent, int)> exam_result_list { get; set; } = new();
    }
}
