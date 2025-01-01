using contracts.binding_models;
using contracts.interactor_contracts;
using contracts.search_models;
using contracts.storage_contracts;
using contracts.storage_contracts.db_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interactors {
    public class student_group_logic : Istudent_group_logic {

        private readonly Istudent_group_storage _storage;

        public student_group_logic(Istudent_group_storage storage) { 
            _storage = storage; 
        }

        public List<student_group_binding_model> get_student_group_list(student_group_search_model? search_model) {
            var models = search_model == null ? new() : _storage.get_student_group_filltered_list(search_model);
            if (models.Count == 0) {
                return new();
            }
            List<student_group_binding_model> bindingModels = new();
            foreach (var model in models) {
                bindingModels.Add(getBindingModel(model));
            }
            return bindingModels;
        }

        public student_group_binding_model? get_student_info(student_group_search_model search_model) {
            if (search_model == null) {
                throw new ArgumentNullException(nameof(search_model));
            }
            var model = _storage.get_student_group_info(search_model);
            if (model == null) {
                return null;
            }
            return getBindingModel(model);
        }

        public student_group_binding_model getBindingModel(StudentGroup model) {
            return new() {
                id = model.id,
                DirectionId = model.DirectionId,
                course_num = model.course_num,
                semester_num = model.semester_num,
                group_num = model.group_num,
            };
        }
    }
}
