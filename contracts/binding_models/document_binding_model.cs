using contracts.storage_contracts.db_models;
using contracts.worker_contracts.helper_models;
using data_models.Enums;
using data_models.IModels;

namespace contracts.binding_models {
    public class document_binding_model : Idocument {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string file_path { get; set; } = string.Empty;
        public DateOnly date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public int UserId { get; set; }
        public enum_file_format_type file_format_type { get; set; }
        public enum_document_type document_type { get; set; }
        public int TemplateId { get; set; }

        public User? user { get; set; }
        public Template? template { get; set; }
        public Idata_info? data_doc { get; set; }

        public void setFilePath() {
            switch(document_type) {
                case enum_document_type.individual_teacher_plan_document:
                    file_path = $@"C:\Users\Ilfe\Documents\AutomationOfTheEducationalProcess\INDIVIDUAL TEACHER PLAN\";
                    return;
                case enum_document_type.statement_document:
                    file_path = $@"C:\Users\Ilfe\Documents\AutomationOfTheEducationalProcess\STATEMENT\";
                    return;
                default:
                    file_path = string.Empty;
                    return;
            }
        }
    }
}
