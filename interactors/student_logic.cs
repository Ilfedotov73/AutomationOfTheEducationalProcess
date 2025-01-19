using contracts.binding_models;
using contracts.interactor_contracts;
using contracts.search_models;
using contracts.storage_contracts;
using contracts.storage_contracts.db_models;
using Microsoft.Extensions.Logging;

namespace interactors {
    public class student_logic : Istudent_logic {

        private readonly Istudent_storage _storage;
        private readonly ILogger _logger;

        public student_logic(Istudent_storage storage, ILogger<student_logic> logger) {
            _storage = storage;
            _logger = logger;
        }

        public List<student_binding_model> get_student_list(student_search_model? search_model) {
            var models = search_model == null ? new() : _storage.get_student_filltered_list(search_model);
            if (models.Count == 0) {
                _logger.LogWarning("get_student_list returned null");
                return new();
            }
            _logger.LogInformation($"get_student_list:{models.Count} elements");
            List<student_binding_model> bindingModels = new();
            foreach (var model in models) {
                bindingModels.Add(getBindingModel(model));
            }
            return bindingModels;
        }

        public student_binding_model? get_student_info(student_search_model search_model) {
            if (search_model == null) {
                throw new ArgumentNullException(nameof(search_model));
            }
            var model = _storage.get_student_info(search_model);
            if (model == null) {
                return null;
            }
            _logger.LogInformation($"get_student_info:{model.id}|fio:{model.fio}|student_group_id:{model.StudentGroupId}|" +
                                    $"grade_book_num:{model.grade_book_num}");
            return getBindingModel(model);
        }

        public student_binding_model getBindingModel(Student model) {
            return new() {
                id = model.id,
                fio = model.fio,
                StudentGroupId = model.StudentGroupId,
                grade_book_num = model.grade_book_num,
                student_group = model.student_group
            };
        }
    }
}
