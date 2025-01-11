using contracts.storage_contracts.db_models;
using data_models.IModels;

namespace contracts.binding_models {
    public class student_binding_model : Istudent {
        public int id { get; set; }
        public string fio { get; set; } = string.Empty;
        public int StudentGroupId { get; set; }
        public int grade_book_num { get; set; }

        public StudentGroup? student_group { get; set; }
    }
}
