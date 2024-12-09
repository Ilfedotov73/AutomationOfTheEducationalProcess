using contracts.binding_models;
using contracts.interactor_contracts;
using contracts.search_models;
using contracts.storage_contracts;
using contracts.storage_contracts.db_models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interactors {
    internal class department_logic : Idepartment_logic {

        private readonly Idepartment_storage _storage;

        public department_logic (Idepartment_storage storage) { 
            _storage = storage;
        }

        public List<department_binding_model> get_department_list(department_search_model search_model) {
            var models = search_model == null ? _storage.get_department_list() : _storage.get_department_filltered_list(search_model);
            if (models.Count == 0) {
                return new();
            }
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
                return null;
            }
            return getBindingModel(model);
        }

        public department_binding_model getBindingModel (Department model) {
            return new() {
                id = model.id,
                name = model.name,
                FacultyId = model.FacultyId
            };
        }
    }
}