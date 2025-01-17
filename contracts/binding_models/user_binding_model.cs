using contracts.storage_contracts.db_models;
using data_models.Enums;
using data_models.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.binding_models {
    public class user_binding_model : Iuser {
        public int id { get; set; }
        public string fio { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public enum_position position { get; set; }
        public DateOnly year_of_birth { get; set; }
        public enum_academic_degree academic_degree { get; set; }
        public DateOnly year_of_award_ad { get; set; }
        public enum_academic_title academic_title { get; set; }
        public DateOnly year_of_award_at { get; set; }
        public string password { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;

        public Faculty? faculty { get; set; }
        public Department? department { get; set; }
        public List<StudentGroup>? studentGroups { get; set; }
        public List<Template>? templates { get; set; }
    }
}
