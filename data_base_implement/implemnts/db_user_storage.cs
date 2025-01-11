using contracts.binding_models;
using contracts.search_models;
using contracts.storage_contracts;
using contracts.storage_contracts.db_models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace data_base_implement.implemnts {
    public class db_user_storage : Iuser_storage {
        public bool insert_user(user_binding_model model) {
            var new_rec = User.insert(model);
            if (new_rec == null) {
                return false;
            }
            using var context = new data_base();
            context.users.Add(new_rec);
            context.SaveChanges();
            return true;
        }

        public bool edit_user(user_binding_model model) {
            using var context = new data_base();
            var edit_rec = context.users.FirstOrDefault(x => x.id == model.id);
            if (edit_rec == null) {
                return false;
            }
            edit_rec.edit(model);
            context.SaveChanges();
            return true;
        }

        public bool delete_user(user_binding_model model) {
            using var context = new data_base();
            var del_rec = context.users.FirstOrDefault(y => y.id == model.id);
            if (del_rec == null) {
                return false;
            }

            var userStudentGroupRec = context.studentGroupUsers.Where(x => x.UserId == del_rec.id);
            context.studentGroupUsers.RemoveRange(userStudentGroupRec);

            context.users.Remove(del_rec);
            context.SaveChanges();
            return true;
        }

        public List<User> get_user_list() {
            using var context = new data_base();
            return context.users
                .Include(x => x.documents).ThenInclude(x => x.template)
                .Include(x => x.student_groups)
                .ToList();
        }

        public List<User> get_user_filltered_list(user_search_model search_model) {
            using var context = new data_base();
            if (search_model.department_id.HasValue) {
                return context.users
                    .Where(x => x.DepartmentId == search_model.department_id)
                    .Include(x => x.department)
                    .Include(x => x.documents).ThenInclude(x => x.template)
                    .Include(x => x.student_groups).ThenInclude(x => x.students)
                    .Include(x => x.student_groups).ThenInclude(x => x.direction)
                    .ToList();
            }
            return new();
        }

        public User? get_user_info(user_search_model search_model) {
            using var context = new data_base();
            if (search_model.id.HasValue) {
                return context.users
                    .Include(x => x.department)
                    .Include(x => x.documents).ThenInclude(x => x.template)
                    .Include(x => x.student_groups).ThenInclude(x => x.students)
                    .Include(x => x.student_groups).ThenInclude(x => x.direction)
                    .FirstOrDefault(x => x.id == search_model.id);
            }
            if (!string.IsNullOrEmpty(search_model.fio)) {
                return context.users
                    .Include(x => x.department)
                    .Include(x => x.documents).ThenInclude(x => x.template)
                    .Include(x => x.student_groups).ThenInclude(x => x.students)
                    .Include(x => x.student_groups).ThenInclude(x => x.direction)
                    .FirstOrDefault(x => x.fio == search_model.fio);
            }
            return null;
        }
    }
}