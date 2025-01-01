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
    public class direction_logic : Idirection_logic {

        private readonly Idirection_storage _storage;

        public direction_logic(Idirection_storage storage) {
            _storage = storage;
        }

        public List<direction_binding_model> get_direction_list(direction_search_model? search_model) {
            var models = search_model == null ? _storage.get_direction_list() : _storage.get_direction_filltered_list(search_model);
            if (models.Count == 0) {
                return new();
            }
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
                return null;
            }
            return getBindingModel(model);
        }

        public direction_binding_model getBindingModel(Direction model) {
            return new() {
                id = model.id,
                full_name = model.full_name,
                alt_name = model.alt_name,
                DepartmentId = model.DepartmentId,
            };
        }
    }
}
