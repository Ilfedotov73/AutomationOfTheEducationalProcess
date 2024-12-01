using data_models.IModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace contracts.storage_contracts.db_models {
    public class Direction : Idirection {
        public int id { get; set; }
        public string full_name { get; set; } = string.Empty;
        public string alt_name { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public Department? department { get; set; }

        public List<StudentGroup> student_groups { get; set; } = new();
    }
}
