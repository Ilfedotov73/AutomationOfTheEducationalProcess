using contracts.storage_contracts.db_models;
using contracts.worker_contracts.helper_models;
using data_models.Enums;
using data_models.IModels;

namespace contracts.binding_models {
    public class template_binding_model : Itemplate {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string file_path { get; set; } = @"C:\Users\Ilfe\Documents\AutomationOfTheEducationalProcess\TEMPLATE\";

        public Idata_info? temp_info { get; set; }

        public int UserId { get; set; }
        public User? user { get; set; }
        public enum_document_type document_type { get; set; }
    }
}
