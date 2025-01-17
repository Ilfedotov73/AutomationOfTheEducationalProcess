using data_models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_models.IModels {
    public interface Iuser : Iid {
        string fio { get; }
        int DepartmentId { get; }
        enum_position position { get; }
        DateOnly year_of_birth { get; }
        enum_academic_degree academic_degree { get; }
        DateOnly year_of_award_ad { get; }
        enum_academic_title academic_title { get; }
        DateOnly year_of_award_at { get; }
        string password { get; }
        string email { get; }
    }
}