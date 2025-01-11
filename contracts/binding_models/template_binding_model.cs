using data_models.IModels;

namespace contracts.binding_models {
    public class template_binding_model : Itemplate {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string file_path { get; set; } = @"C:\Users\Ilfe\Documents\AutomationOfTheEducationalProcess\TEMPLATE\";
    }
}
