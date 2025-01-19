using contracts.search_models;
using contracts.storage_contracts;
using contracts.storage_contracts.db_models;
using Microsoft.EntityFrameworkCore;

namespace data_base_implement.implemnts {
    public class db_department_storage : Idepartment_storage {
        public List<Department> get_department_list() {
            using var context = new data_base();
            return context.departments
                .Include(x => x.faculty)
                .Include(x => x.directions)
                .Include(x => x.users).ThenInclude(x => x.student_groups).ThenInclude(x => x.students)
                .ToList();
        }

        public List<Department> get_department_filltered_list(department_search_model search_model) {
            using var context = new data_base();
            if (search_model.faculty_id.HasValue) {
                return context.departments
                    .Where(x => x.FacultyId == search_model.faculty_id)
                    .Include(x => x.faculty)
                    .Include(x => x.directions)
                    .Include(x => x.users).ThenInclude(x => x.student_groups).ThenInclude(x => x.students)
                    .ToList();
                    
            }
            return new();
        }

        public Department? get_department_info(department_search_model search_model) {
            using var context = new data_base();
            if (search_model.id.HasValue) {
                return context.departments
                    .Include(x => x.faculty)
                    .Include(x => x.directions)
                    .Include(x => x.users).ThenInclude(x => x.student_groups).ThenInclude(x => x.students)
                    .FirstOrDefault(x => x.id == search_model.id);
            }
            return null;
        }
    }
}
