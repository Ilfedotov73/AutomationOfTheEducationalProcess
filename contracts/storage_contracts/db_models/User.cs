using contracts.binding_models;
using data_models.Enums;
using data_models.IModels;
using System.ComponentModel.DataAnnotations;

namespace contracts.storage_contracts.db_models {
    public class User : Iuser {
        public int id { get; set; }

        [RegularExpression(@"\D")]
        public string fio { get; set; } = string.Empty;

        public int DepartmentId { get; set; }
        public Department? department { get; set; }

        [Required]
        public enum_position position { get; set; }

        [Required]
        public DateOnly year_of_birth { get; set; }

        [Required]
        public enum_academic_degree academic_degree { get; set; }

        [Required]
        public DateOnly year_of_award_ad { get; set; }

        [Required]
        public enum_academic_title academic_title { get; set; }

        [Required]
        public DateOnly year_of_award_at { get; set; }

        [Required]
        public string password { get; set; } = string.Empty;

        public List<Document> documents { get; set; } = new();
        public List<StudentGroup> student_groups { get; set; } = new();
        public List<Template> templates { get; set; } = new();


        [EmailAddress, Required]
        public string email { get; set; } = string.Empty;

        public static User? insert(user_binding_model? model) {
            if (model == null) {
                return null;
            }
            return new User() {
                id = model.id,
                fio = model.fio,
                DepartmentId = model.DepartmentId,
                position = model.position,
                year_of_birth = model.year_of_birth,
                academic_degree = model.academic_degree,
                year_of_award_ad = model.year_of_award_at,
                academic_title = model.academic_title,
                year_of_award_at = model.year_of_award_at,
                password = model.password,
                email = model.email
            };
        }

        public void edit(user_binding_model? model) {
            if (model == null) {
                return;
            }
            email = model.email;
            password = model.password;
        }
    }
}
