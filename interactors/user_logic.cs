﻿using contracts.binding_models;
using contracts.interactor_contracts;
using contracts.search_models;
using contracts.storage_contracts;
using contracts.storage_contracts.db_models;
using Microsoft.Extensions.Logging;

namespace interactors {
    public class user_logic : Iuser_logic {

        private readonly Iuser_storage _storage;
        private readonly ILogger _logger;

        public user_logic(Iuser_storage storage, ILogger<user_logic> logger) {
            _storage = storage;
            _logger = logger;
        }

        public void insert_user(user_binding_model model) {
            check_model(model);
            if (_storage.insert_user(model) == false) {
                throw new Exception("insert operation failed");
            }
        }

        public void edit_user(user_binding_model model) {
            check_model(model, onEdit: true);
            if (_storage.edit_user(model) == false) {
                throw new Exception("edit operation failed");
            }
            _logger.LogInformation($"user:{model.id} is edited");
        }

        public void delete_user(user_binding_model model) {
            check_model(model, true);
            if (_storage.delete_user(model) == false) {
                throw new Exception("delete operation failed");
            }
            _logger.LogInformation($"user:{model.id} is deleted");
        }

        public void check_model(user_binding_model model, bool obDelete = false, bool onEdit = false) {
            if (string.IsNullOrEmpty(model.id.ToString())) {
                throw new ArgumentNullException("user id is missing", nameof(model.id));
            }
            if (obDelete) {
                return;
            }
            if (string.IsNullOrEmpty(model.DepartmentId.ToString())) {
                throw new ArgumentNullException("user departmentId is missing", nameof(model.DepartmentId));
            }
            if (string.IsNullOrEmpty(model.position.ToString())) {
                throw new ArgumentNullException("user position is missing", nameof(model.position));
            }
            if (string.IsNullOrEmpty(model.year_of_birth.ToString())) {
                throw new ArgumentNullException("user year of birth is missing", nameof(model.year_of_birth));
            }
            if (string.IsNullOrEmpty(model.academic_degree.ToString())) {
                throw new ArgumentNullException("user academic degree is missing", nameof(model.academic_degree));
            }
            if (string.IsNullOrEmpty(model.year_of_award_ad.ToString())) {
                throw new ArgumentNullException("user year of award ad is missing", nameof(model.year_of_award_ad));
            }
            if (string.IsNullOrEmpty(model.academic_title.ToString())) {
                throw new ArgumentNullException("user academic title is missing", nameof(model.academic_title));
            }
            if (string.IsNullOrEmpty(model.year_of_award_at.ToString())) {
                throw new ArgumentNullException("user year of award at is missing", nameof(model.year_of_award_at));
            }
            if (string.IsNullOrEmpty(model.password)) {
                throw new ArgumentNullException("user password is missing", nameof(model.password));
            }
            if (onEdit) {
                return;
            }

            string[] _fio = model.fio.Split(new char[] { ' ' });
            if (string.IsNullOrEmpty(model.fio) || _fio.Length < 3) {
                throw new ArgumentNullException("user fio is entered incorrectly", nameof(model.fio));
            }

            var user = get_user_info(new user_search_model { fio = model.fio });
            if (user != null) {
                throw new Exception("the user is already registered");
            }
        }

        public List<user_binding_model> get_user_list(user_search_model? search_model) {
            var models = search_model == null ? _storage.get_user_list() : _storage.get_user_filltered_list(search_model);
            if (models.Count == 0) {
                _logger.LogWarning("get_user_list returned am empty list");
                return new();
            }
            _logger.LogInformation($"get_user_list:{models.Count} elements");
            List<user_binding_model> bindingModels = new();
            foreach (var model in models) {
                bindingModels.Add(getBindingModel(model));
            }
            return bindingModels;
        }

        public user_binding_model? get_user_info(user_search_model search_model) {
            if (search_model == null) {
                throw new ArgumentNullException(nameof(search_model));
            }
            var model = _storage.get_user_info(search_model);
            if (model == null) {
                _logger.LogWarning("get_user_info returned null");
                return null;
            }
            _logger.LogInformation($"get_user_info:{model.id}|fio:{model.fio}|departmentId{model.DepartmentId}|" +
                                    $"position:{model.position}|year_of_bith:{model.year_of_birth}" +
                                    $"|academic_degree:{model.academic_degree}|year_of_award_ad:{model.year_of_award_ad}" +
                                    $"|academic_title:{model.academic_title}|year_of_award_at:{model.year_of_award_at}" +
                                    $"|password:{model.password}|email:{model.email}");
            return getBindingModel(model);
        }

        public user_binding_model getBindingModel(User model) {
            return new() {
                id = model.id,
                fio = model.fio,
                DepartmentId = model.DepartmentId,
                position = model.position,
                year_of_birth = model.year_of_birth,
                academic_degree = model.academic_degree,
                year_of_award_ad = model.year_of_award_ad,
                academic_title = model.academic_title,
                year_of_award_at = model.year_of_award_at,
                password = model.password,
                faculty = model.department.faculty,
                department = model.department,
                email = model.email,
                studentGroups = model.student_groups,
                templates = model.templates,
            };
        }
    }
}
