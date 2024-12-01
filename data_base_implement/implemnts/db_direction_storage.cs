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
    public class db_direction_storage : Idirection_storage {
        public List<Direction> get_direction_list() {
            using var context = new data_base();
            return context.directions
                .Include(x => x.department)
                .Include(x => x.student_groups).ThenInclude(x => x.students)
                .ToList();
        }

        public List<Direction> get_direction_filltered_list(direction_search_model search_model) {
            using var context = new data_base();
            if (search_model.department_id.HasValue) {
                return context.directions
                    .Where(x => x.DepartmentId == search_model.department_id)
                    .Include(x => x.department)
                    .Include(x => x.student_groups).ThenInclude(x => x.students)
                    .ToList();
            }
            return new();
        }

        public Direction? get_direction_info(direction_search_model search_model) {
            using var context = new data_base();
            if (search_model.id.HasValue) {
                return context.directions
                    .Include(x => x.department)
                    .Include(x => x.student_groups).ThenInclude(x => x.students)
                    .FirstOrDefault(x => x.id == search_model.id);
            }
            return null;
        }
    }
}
