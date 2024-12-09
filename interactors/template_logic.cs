using contracts.binding_models;
using contracts.interactor_contracts;
using contracts.search_models;
using contracts.storage_contracts;
using contracts.storage_contracts.db_models;
using contracts.worker_contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interactors {
    public class template_logic : Itemplate_logic {

        private readonly Itemplate_storage _storage;
        private readonly Itemplate_worker _worker;
        
        public template_logic(Itemplate_storage storage, Itemplate_worker worker) {
            _storage = storage;
            _worker = worker;
        }

        public byte[] insert_template(template_binding_model model) {
            check_model(model);
            model.file_path += $"{model.name}.xlsx";
            byte[] document = _worker.create_template_file(model);

            if (_storage.insert_template(model) == false) {
                throw new Exception("insert operation failed in database");
            }
            return document;
        }

        public void edit_template(template_binding_model model, byte[] rewire_data) {
            check_model(model);

            File.Exists(model.file_path);
            File.WriteAllBytes(model.file_path, rewire_data);

            if (_storage.edit_tempalte(model) == false) {
                throw new Exception("edit operation failed in database");
            };
        }

        public void delete_template(template_binding_model model) {
            check_model(model, true);

            File.Exists(model.file_path);
            File.Delete(model.file_path);

            if (_storage.delete_template(model) == false) {
                throw new Exception("delete operation failed in database");
            }
        }

        public void check_model(template_binding_model model, bool onDelete = false) {
            if (string.IsNullOrEmpty(model.id.ToString())) {
                throw new ArgumentNullException("template id is missing", nameof(model.id));
            }
            if (onDelete) {
                return;
            }
            if (string.IsNullOrEmpty(model.name)) {
                throw new ArgumentNullException("template name is missing", nameof(model.name));
            }
            if (string.IsNullOrEmpty(model.file_path)) {
                throw new ArgumentNullException("template file path is missing", nameof(model.file_path));
            }
        }

        public List<template_binding_model> get_template_list() {
            var models = _storage.get_template_list();
            if (models.Count == 0) {
                return new();
            }
            List<template_binding_model> bindingModels = new();
            foreach (var model in models) {
                bindingModels.Add(getBindingModel(model));
            }
            return bindingModels;
        }

        public template_binding_model? get_template_info(template_search_model search_model) {
            if (search_model == null) {
                throw new ArgumentNullException(nameof(search_model));
            }
            var model = _storage.get_template_info(search_model);
            if (model == null) {
                return null;
            }
            return getBindingModel(model);
        }

        public byte[] on_export_template(template_search_model search_model) {
            var model = get_template_info(search_model);
            if (model == null) {
                throw new Exception("Такого шаблона нет");
            }

            byte[] document = File.ReadAllBytes(model.file_path);
            return document;
        }

        public template_binding_model getBindingModel(Template model) {
            return new() {
                id = model.id,
                name = model.name,
                file_path = model.file_path,
            };
        }
    }
}
