using contracts.binding_models;
using contracts.interactor_contracts;
using contracts.search_models;
using contracts.storage_contracts;
using contracts.storage_contracts.db_models;
using Microsoft.Extensions.Logging;

namespace interactors {
    public class faculty_logic : Ifaculty_logic {

        private readonly Ifaculty_storage _storage;
        private readonly ILogger _logger;

        public faculty_logic(Ifaculty_storage storage, ILogger<faculty_logic> logger) {
            _storage = storage;
            _logger = logger;
        }
        
        public List<faculty_binding_model> get_faculty_list() {
            var models = _storage.get_faculty_list();
            if (models.Count == 0) {
                _logger.LogWarning("get_faculty_list returned an empty list");
                return new();
            }
            _logger.LogInformation($"get_faculty_list:{models.Count} elements");
            List<faculty_binding_model> bindingModels = new();
            foreach (var model in models) {
                bindingModels.Add(getBindingModel(model));
            }
            return bindingModels;
        }

        public faculty_binding_model? get_faculty_info(faculty_search_model search_model) {
            if (search_model == null) {
                throw new ArgumentNullException(nameof(search_model));
            }
            var model = _storage.get_faculty_info(search_model);
            if (model == null) {
                _logger.LogWarning("get_faculty_info returned null");
                return null;
            }
            _logger.LogInformation($"get_faculty_info:{model.id}|{model.name}");
            return getBindingModel(model);
        }

        public faculty_binding_model getBindingModel (Faculty model) {
            return new() {
                id = model.id,
                name = model.name,
            };
        }
    }
}
