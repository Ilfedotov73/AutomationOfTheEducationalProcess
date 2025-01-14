using contracts.search_models;
using contracts.storage_contracts;
using contracts.storage_contracts.db_models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_base_implement.implemnts {
    public class db_student_group_storage : Istudent_group_storage {
        public List<StudentGroup> get_student_group_filltered_list(student_group_search_model search_model) {
            using var context = new data_base();
            if (search_model.direction_id.HasValue) {
                return context.student_groups
                    .Where(x => x.DirectionId == search_model.direction_id)
                    .Include(x => x.direction).ThenInclude(x => x.department).ThenInclude(x => x.faculty)
                    .Include(x => x.students)
                    .Include(x => x.users)
                    .ToList();
            }
            return new();
        }

        public StudentGroup? get_student_group_info(student_group_search_model search_model) {
            using var context = new data_base();
            if (search_model.id.HasValue) {
                return context.student_groups
                    .Include(x => x.direction).ThenInclude(x => x.department).ThenInclude(x => x.faculty)
                    .Include(x => x.students)
                    .Include(x => x.users)
                    .FirstOrDefault(x => x.id == search_model.id);
            }
            return null;   
        }
    }
}
