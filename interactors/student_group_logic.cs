using contracts.binding_models;
using contracts.interactor_contracts;
using contracts.search_models;
using contracts.storage_contracts;
using contracts.storage_contracts.db_models;
using Microsoft.Extensions.Logging;

namespace interactors {
    public class student_group_logic : Istudent_group_logic {

        private readonly Istudent_group_storage _storage;
        private readonly ILogger _logger;

        public student_group_logic(Istudent_group_storage storage, ILogger<student_group_logic> logger) { 
            _storage = storage;
            _logger = logger;
        }

        public List<student_group_binding_model> get_student_group_list(student_group_search_model? search_model) {
            var models = search_model == null ? _storage.get_student_group_list() : _storage.get_student_group_filltered_list(search_model);
            if (models.Count == 0) {
                _logger.LogWarning("get_student_group_list returned an empty list");
                return new();
            }
            _logger.LogInformation($"get_student_group_list:{models.Count} elements");
            List<student_group_binding_model> bindingModels = new();
            foreach (var model in models) {
                bindingModels.Add(getBindingModel(model));
            }
            return bindingModels;
        }

        public student_group_binding_model? get_student_group_info(student_group_search_model search_model) {
            if (search_model == null) {
                throw new ArgumentNullException(nameof(search_model));
            }
            var model = _storage.get_student_group_info(search_model);
            if (model == null) {
                _logger.LogWarning("get_studnet_group_info returned null");
                return null;
            }
            _logger.LogInformation($"get_student_group_info:{model.id}|direction_id:{model.DirectionId}|" +
                                    $"group_num:{model.group_num}|course_num{model.course_num}|semester_num{model.semester_num}");
            return getBindingModel(model);
        }

        public student_group_binding_model getBindingModel(StudentGroup model) {
            return new() {
                id = model.id,
                DirectionId = model.DirectionId,
                course_num = model.course_num,
                semester_num = model.semester_num,
                group_num = model.group_num,
                direction = model.direction,
                students = model.students
            };
        }
    }
}
