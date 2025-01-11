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
    public class db_student_storage : Istudent_storage {
        public List<Student> get_student_filltered_list(student_search_model search_model) {
            using var context = new data_base();
            if (search_model.group_id.HasValue) {
                return context.students
                    .Where(x => x.StudentGroupId == search_model.group_id)
                    .Include(x => x.student_group).ThenInclude(x => x.direction)
                    .Include(x => x.student_group).ThenInclude(x => x.users)
                    .ToList();
            }
            return new();
        }

        public Student? get_student_info(student_search_model search_model) {
            using var context = new data_base();
            if (search_model.id.HasValue) {
                return context.students
                    .Include(x => x.student_group).ThenInclude(x => x.direction)
                    .Include(x => x.student_group).ThenInclude(x => x.users)
                    .FirstOrDefault(x => x.id == search_model.id);
            }
            return null;
        }
    }
}
