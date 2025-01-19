using contracts.binding_models;
using contracts.interactor_contracts;
using contracts.search_models;
using contracts.storage_contracts;
using contracts.storage_contracts.db_models;
using Microsoft.Extensions.Logging;

namespace interactors {
    public class department_logic : Idepartment_logic {

        private readonly Idepartment_storage _storage;
        private readonly ILogger _logger;

        public department_logic (Idepartment_storage storage, ILogger<department_logic> logger) { 
            _storage = storage;
            _logger = logger;
        }

        public List<department_binding_model> get_department_list(department_search_model? search_model) {
            var models = search_model == null ? _storage.get_department_list() : _storage.get_department_filltered_list(search_model);
            if (models.Count == 0) {
                _logger.LogWarning("get_department_list returned an empty list");
                return new();
            }
            _logger.LogInformation($"get_department_list:{models.Count} elements");
            List<department_binding_model> bindingModels = new();
            foreach (var model in models) {
                bindingModels.Add(getBindingModel(model));
            }
            return bindingModels;
        }

        public department_binding_model? get_department_info(department_search_model search_model) {
            if (search_model == null) {
                throw new ArgumentNullException(nameof(search_model));
            }
            var model = _storage.get_department_info(search_model);
            if (model == null) {
                _logger.LogWarning("get_department_info returned null");
                return null;
            }
            _logger.LogInformation($"get_department_info:{model.id}|name:{model.name}|facultyId:{model.FacultyId}");
            return getBindingModel(model);
        }

        public department_binding_model getBindingModel (Department model) {
            return new() {
                id = model.id,
                name = model.name,
                FacultyId = model.FacultyId,
                faculty = model.faculty
            };
        }
    }
}