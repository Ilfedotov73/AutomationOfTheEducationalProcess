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
    public class db_faculty_storage : Ifaculty_storage {
        public List<Faculty> get_faculty_list() {
            using var context = new data_base();
            return context.faculties
                .Include(x => x.departments).ThenInclude(x => x.directions).ThenInclude(x => x.student_groups)
                .ToList();
        }

        public Faculty? get_faculty_info(faculty_search_model search_model) {
            using var context = new data_base();
            if (search_model.id.HasValue) {
                return context.faculties
                    .Include(x => x.departments).ThenInclude(x => x.directions).ThenInclude(x => x.student_groups)
                    .FirstOrDefault(x => x.id == search_model.id);
            }
            return null;
        }
    }
}
