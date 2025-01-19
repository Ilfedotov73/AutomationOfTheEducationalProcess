using contracts.binding_models;
using contracts.interactor_contracts;
using contracts.presenter_contracts;
using contracts.search_models;
using contracts.view_moedels;
using contracts.worker_contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using worker.office_package.helper_models.info_models;

namespace WebApplicationRestAPI.Controllers {

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController {
        private readonly Idepartment_presenter _departmentPresenter;

        private readonly Idocument_presenter _documentPresenter;
        private readonly Idocument_logic _documentLogic;

        private readonly Itemplate_presenter _templatePresenter;
        private readonly Itemplate_logic _templateLogic;
        private readonly Itemplate_worker _templateWorker;

        private readonly Istudent_group_presenter _studentGroupPresenter;

        private readonly Iuser_presenter _userPresenter;

        private readonly ILogger _logger;

        public MainController(Idepartment_presenter departmentPresenter, Idocument_presenter documentPresenter, 
                                Idocument_logic documentLogic, Itemplate_presenter templaste_presenter, 
                                Itemplate_logic template_logic, Itemplate_worker templateWorker, 
                                Iuser_presenter userPresenter, Istudent_group_presenter studentGroupPresenter, 
                                ILogger<MainController> logger) {
            _departmentPresenter = departmentPresenter;
            
            _documentPresenter = documentPresenter;
            _documentLogic = documentLogic;

            _templatePresenter = templaste_presenter;
            _templateLogic = template_logic;
            _templateWorker = templateWorker;

            _userPresenter = userPresenter;

            _studentGroupPresenter = studentGroupPresenter;
            _logger = logger;
        }

        [HttpGet]
        public List<department_view_model> get_department_list(int? facultyId) {
            try {
                if (facultyId == null) {
                    return _departmentPresenter.make_department_list_presenter(null);
                }
                return _departmentPresenter.make_department_list_presenter(new department_search_model { 
                    faculty_id = facultyId 
                });
                
            }
            catch (Exception ex) {
                _logger.LogError(ex, "controller error");
                throw;
            }
        }

        [HttpPost] 
        public void CreateItpDocument(List<string> data) {
            var model = JsonConvert.DeserializeObject<document_binding_model>(data[0]);
            var info = JsonConvert.DeserializeObject<itp_info>(data[1]);
            model.data_doc = info;

            try {
                _documentLogic.insert_document(model);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "controller error");
                throw;
            }
        }
        [HttpPost]
        public void CreateStDocument(List<string> data) {
            var model = JsonConvert.DeserializeObject<document_binding_model>(data[0]);
            var info = JsonConvert.DeserializeObject<st_info>(data[1]);
            int group = JsonConvert.DeserializeObject<int>(data[2]);

            info.group_info = _studentGroupPresenter.make_student_group_presenter(new student_group_search_model { id = group });
            model.data_doc = info;

            try {
                _documentLogic.insert_document(model);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "controller error");
                throw;
            }
        }

        [HttpGet]
        public List<document_view_model> get_document_list(int? userId) {
            try {
                if (userId == null) {
                    return _documentPresenter.make_document_list_presenter(null);
                }
                return _documentPresenter.make_document_list_presenter(new document_search_model {
                    author_id = userId
                });
            }
            catch (Exception ex) {
                _logger.LogError(ex, "controller error");
                throw;
            }
        }

        [HttpGet]
        public document_view_model? get_document_info(int documentId) {
            try {
                return _documentPresenter.make_document_presenter(new document_search_model { id = documentId });
            }
            catch (Exception ex) {
                _logger.LogError(ex, "controller error");
                throw;
            }
        }

        [HttpGet]
        public byte[]? ImportDocument(int documentId) {
            try {
                return _documentLogic.on_import_docfile(new document_search_model {
                    id = documentId
                });
            }
            catch (Exception ex) {
                _logger.LogError(ex, "controller error");
                throw;
            }
        }

        [HttpPost]
        public void ExportDocument(List<string> data) {
            var bytes = JsonConvert.DeserializeObject<byte[]>(data[0]);
            var documentId = JsonConvert.DeserializeObject<int>(data[1]);

            try {
                _documentLogic.edit_document(new document_binding_model { id = documentId }, bytes);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "controller error");
                throw;
            }
        }

        [HttpPost]
        public void DeleteDocument(document_binding_model model) {
            try {
                _documentLogic.delete_document(model);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "controller error");
                throw;
            }
        }

        [HttpPost]
        public void insert_template(List<string> data) {

            var model = JsonConvert.DeserializeObject<template_binding_model>(data[0]);
            model.temp_info = JsonConvert.DeserializeObject<itp_temp_info>(data[1]);

            try {
                _templateLogic.insert_template(model);
            }
            catch(Exception ex) {
                _logger.LogError(ex, "controller error");
                throw;
            }
        }

        [HttpGet]
        public byte[]? ImportTemplate(int templateId) {
            try {
                return _templateLogic.on_import_template(new template_search_model { id = templateId });
            }
            catch (Exception ex) {
                _logger.LogError(ex, "controler error");
                throw;
            }
        }

        [HttpPost]
        public void ExportTemplate(List<string> data) {
            var bytes = JsonConvert.DeserializeObject<byte[]>(data[0]);
            var templateId = JsonConvert.DeserializeObject<int>(data[1]);


            try {
                _templateLogic.edit_template(new template_binding_model { id = templateId }, bytes);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "controller error");
                throw;
            }
        }

        [HttpPost]
        public void DeleteTemplate(template_binding_model model) {
            try {
                _templateLogic.delete_template(model);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "controller error");
                throw;
            }
        }

        [HttpGet]
        public List<template_view_model>? get_templates_list() {
            try {
                return _templatePresenter.make_template_list_presenter();
            }
            catch (Exception ex) {
                _logger.LogError(ex, "controller error");
                throw;
            }
        }

        [HttpGet]
        public template_view_model? get_template_info(int templateId) {
            try {
                return _templatePresenter.make_template_presenter(new template_search_model { id = templateId });
            }
            catch (Exception ex) {
                _logger.LogError(ex, "controller error");
                throw;
            }
        }

        [HttpGet]
        public itp_temp_info? read_temp_file(int templateId) {
            try {
                var model = _templateLogic.get_template_info(new template_search_model { id = templateId });
                return (itp_temp_info)_templateWorker.read_temp_file(model);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "controller error");
                throw;
            }
        }

        [HttpGet]
        public List<user_view_model>? get_user_list() {
            try {
                return _userPresenter.make_user_list_presenter(null);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "controller error");
                throw;
            }
        }

        [HttpGet]
        public List<student_group_view_model> get_student_group_list() {
            try {
                return _studentGroupPresenter.make_student_group_list_presenter(null);
            }
            catch(Exception ex) {
                _logger.LogError(ex, "controller error");
                throw;
            }
        }
    }
}
