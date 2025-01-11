using contracts.storage_contracts.db_models;
using data_models.Enums;
using data_models.IModels;

namespace contracts.binding_models {
    public class document_binding_model : Idocument {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string file_path { get; set; } = string.Empty;
        public DateOnly date { get; set; } 
        public int UserId { get; set; }
        public enum_file_format_type file_format_type { get; set; }
        public enum_document_type document_type { get; set; }
        public int TemplateId { get; set; }

        public User? user { get; set; }
        public Template? template { get; set; }
    }
}
