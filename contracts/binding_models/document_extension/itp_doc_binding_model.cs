using data_models.Enums;
using data_models.IModels.document_extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.binding_models.document_extension {
    public class itp_doc_binding_model : Iitp_doc {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string file_path { get; set; } = $@"C:\Users\Ilfe\Documents\AutomationOfTheEducationalProcess\INDIVIDUAL TEACHER PLAN\";
        public int UserId { get; set; }
        public enum_file_format_type file_format_type { get; set; }
        public enum_document_type document_type { get; private set; } = enum_document_type.individual_teacher_plan_document;
        public int TemplateId { get; set; }
    }
}