using contracts.binding_models;
using contracts.interactor_contracts;
using contracts.search_models;
using contracts.storage_contracts;
using contracts.storage_contracts.db_models;
using Microsoft.Extensions.Logging;

namespace interactors {
    public class direction_logic : Idirection_logic {

        private readonly Idirection_storage _storage;
        private readonly ILogger _logger;

        public direction_logic(Idirection_storage storage, ILogger<direction_logic> logger) {
            _storage = storage;
            _logger = logger;
        }

        public List<direction_binding_model> get_direction_list(direction_search_model? search_model) {
            var models = search_model == null ? _storage.get_direction_list() : _storage.get_direction_filltered_list(search_model);
            if (models.Count == 0) {
                _logger.LogWarning("get_direction_list returned an empty list");
                return new();
            }
            _logger.LogInformation($"get_direction_list:{models.Count} element");
            List<direction_binding_model> bindingModels = new();
            foreach (var model in models) {
                bindingModels.Add(getBindingModel(model));
            }
            return bindingModels;
        }

        public direction_binding_model? get_direction_info(direction_search_model search_model) {
            if (search_model == null) {
                throw new ArgumentNullException(nameof(search_model));
            }
            var model = _storage.get_direction_info(search_model);
            if (model == null) {
                _logger.LogWarning("get_direction_info returned null");
                return null;
            }
            _logger.LogInformation($"{model.id}|full_name:{model.full_name}|alt_name:{model.alt_name}|" +
                                    $"departmentId:{model.DepartmentId}");
            return getBindingModel(model);
        }

        public direction_binding_model getBindingModel(Direction model) {
            return new() {
                id = model.id,
                full_name = model.full_name,
                alt_name = model.alt_name,
                DepartmentId = model.DepartmentId,
                department = model.department,
            };
        }
    }
}
