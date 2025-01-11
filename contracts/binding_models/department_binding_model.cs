using contracts.storage_contracts.db_models;
using data_models.IModels;

namespace contracts.binding_models {
    public class department_binding_model : Idepartment {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public int FacultyId { get; set; }

        public Faculty? faculty { get; set; }
    }
}
