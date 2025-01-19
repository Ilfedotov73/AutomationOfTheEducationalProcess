using AutomationOfTheEducationalProcessApp.Models;
using contracts.binding_models;
using contracts.view_moedels;
using data_models.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using worker.office_package.helper_models.info_models;

namespace AutomationOfTheEducationalProcessApp.Controllers {
    public class HomeController : Controller {

        public IActionResult Index() {
            if (APIclient.user == null) {
                return Redirect("~/Home/Enter");
            }

            List<document_view_model> docs = new();
            if (APIclient.user.position == "teacher") {
                docs = APIclient.GetRequest<List<document_view_model>>($"api/main/get_document_list?userId={APIclient.user.id}") 
                        ?? throw new Exception("data retrieval error");
            }
            else {
                docs = APIclient.GetRequest<List<document_view_model>>("api/main/get_document_list") 
                        ?? throw new Exception("data retrieval error"); ;
            }
            return View(docs);
        }

        [HttpGet]
        public IActionResult Template() {
            if (APIclient.user == null) {
                return Redirect("~/Home/Enter");
            }

            List<template_view_model> templates = new();
            if (APIclient.user.position == "teacher") {
                templates = APIclient.GetRequest<List<template_view_model>>($"api/user/get_user_template_list?userId=" +
                              $"{APIclient.user.id}") ?? throw new Exception("data retrieval error");
            }
            else {
                templates = APIclient.GetRequest<List<template_view_model>>("api/main/get_templates_list") 
                    ?? throw new Exception("data retrieval error");
            }

            return View(templates);
        }

        [HttpGet]
        public IActionResult TemplateCreate() {
            if (APIclient.user == null) {
                return Redirect("~/Home/Enter");
            }
            ViewBag.users = APIclient.GetRequest<List<user_view_model>>("api/main/get_user_list");
            return View(APIclient.user);
        }

        [HttpPost]
        public void TemplateCreate(int user, string name, string[] dispNameA, string[] dispNameB, string[] work21, string[] work22, 
                                    string[] work23, string[] work24, string[] work31, string[] work32, string[] work41, string[] work42) {
            APIclient.ListPostRequest("api/main/insert_template", new template_binding_model {
                name = name,
                UserId = user,
                document_type = enum_document_type.individual_teacher_plan_document
            }, new itp_temp_info {
                user_info = APIclient.user,
                disciplines_A = dispNameA.ToList(),
                disciplines_B = dispNameB.ToList(),
                workTypes_21 = work21.ToList(),
                workTypes_22 = work22.ToList(),
                workTypes_23 = work23.ToList(),
                workTypes_24 = work24.ToList(),
                workTypes_31 = work31.ToList(),
                workTypes_32 = work32.ToList(),
                workTypes_41 = work41.ToList(),
                workTypes_42 = work42.ToList()
            }, null);
            Response.Redirect("Template");
        }

        [HttpGet]
        public IActionResult ImportTemplate(int id, string name) {
            byte[] data = APIclient.GetRequest<byte[]>($"api/main/ImportTemplate?templateId={id}") 
                        ?? throw new Exception("data retrieval error");
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{name}.xlsx");
        }

        [HttpGet]
        public IActionResult TemplateFile(int id) {

            var templateInfo = APIclient.GetRequest<template_view_model>($"api/main/get_template_info?templateId={id}");
            return View(templateInfo);
        }

        [HttpPost]
        public IActionResult TemplateFile(IFormFile uploads, int id) {
            if (uploads == null) {
                throw new Exception("Загружаемый файл отсутствует");
            }
            var memStream = new MemoryStream();
            uploads.OpenReadStream().CopyTo(memStream);
            APIclient.ListPostRequest("api/main/ExportTemplate", memStream.ToArray(), id, null);
            return RedirectToAction("Template");
        }

        [HttpGet]
        public IActionResult DeleteTemplate(int id) {
            APIclient.PostRequest("api/main/DeleteTemplate", new template_binding_model { id = id });
            return RedirectToAction("Template");
        }

        [HttpGet]
        public IActionResult CreateStDocument() {
            if (APIclient.user == null) {
                return Redirect("~/Home/Enter");
            }

            ViewBag.groups = APIclient.GetRequest<List<student_group_view_model>>($"api/main/get_student_group_list");
            return View();
        }

        [HttpPost]
        public IActionResult CreateStDocument(int group, string subject, string examiner, int test, int hours, string docName) {

            var documentDocx = new document_binding_model() {
                name = docName,
                UserId = APIclient.user.id,
                document_type = enum_document_type.statement_document,
                file_format_type = enum_file_format_type.docx,
                TemplateId = 32
            };
            documentDocx.setFilePath();
            var documentXlsx = new document_binding_model() {
                name = docName,
                UserId = APIclient.user.id,
                document_type = enum_document_type.statement_document,
                file_format_type = enum_file_format_type.xlsx,
                TemplateId = 32
            };
            documentXlsx.setFilePath();

            var info = new st_info {
                group_info = new(),
                subject = subject,
                examiner = examiner,
                test = (test_type)test,
                totalHoursNum = hours,
            };
            APIclient.ListPostRequest("api/main/CreateStDocument", documentDocx, info, group);
            APIclient.ListPostRequest("api/main/CreateStDocument", documentXlsx, info, group);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateItpDocument(int id) {
            var info = APIclient.GetRequest<itp_temp_info>($"api/main/read_temp_file?templateId={id}");
            ViewBag.id = id;
            return View(info);
        }

        [HttpPost]
        public IActionResult CreateItpDocument(int[] dispNameA, int[] dispNameB, int[] work21_1, int[] work21_2, int[] work22_1, 
                                    int[] work22_2, int[] work23_1, int[] work23_2, int[] work24_1, int[] work24_2,
                                    int[] work31_1, int[] work31_2, int[] work32_1, int[] work32_2,
                                    int[] work41_1, int[] work41_2, int[] work42_1, int[] work42_2,
                                    int id, string docName) {
             var dataInfo = new itp_info() {
                data_11 = dispNameA,
                data_12 = dispNameB,
                data_21 = addData(work21_1, work21_2),
                data_22 = addData(work22_1, work22_2),
                data_23 = addData(work23_1, work23_2),
                data_24 = addData(work24_1, work24_2),
                data_31 = addData(work31_1, work31_2),
                data_32 = addData(work32_1, work32_2), 
                data_41 = addData(work41_1, work41_2),
                data_42 = addData(work42_1, work42_2),
            };

            var documentDocx = new document_binding_model {
                name = docName,
                UserId = APIclient.user.id,
                document_type = enum_document_type.individual_teacher_plan_document,
                file_format_type = enum_file_format_type.docx,
                TemplateId = id
            };
            documentDocx.setFilePath();
            var documentXlsx = new document_binding_model {
                name = docName,
                UserId = APIclient.user.id,
                document_type = enum_document_type.individual_teacher_plan_document,
                file_format_type = enum_file_format_type.xlsx,
                TemplateId = id
            };
            documentXlsx.setFilePath();

            APIclient.ListPostRequest("api/main/CreateItpDocument", documentDocx, dataInfo, null);
            APIclient.ListPostRequest("api/main/CreateItpDocument", documentXlsx, dataInfo, null);
            return RedirectToAction("Index");
        }

        public List<(int, int)> addData(int[] data1, int[] data2) {
            var list = new List<(int, int)>();
            for (int i = 0, j = 0; i < data1.Length; i++, j++) {
                list.Add((data1[i], data2[j]));
            }
            return list;
        }

        [HttpGet]
        public IActionResult ImportDocument(int id, string name, string format) {
            byte[] data = APIclient.GetRequest<byte[]>($"api/main/ImportDocument?documentId={id}") 
                        ?? throw new Exception("data retrieval error");

            if (format == "xlsx") {
                return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{name}.xlsx");
            }
            return File(data, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"{name}.docx");            
        }

        [HttpGet]
        public IActionResult DocumentFile(int id) {
            var documentInfo = APIclient.GetRequest<document_view_model>($"api/main/get_document_info?documentId={id}");
            return View(documentInfo);
        }

        [HttpPost]
        public IActionResult DocumentFile(IFormFile uploads, int id) {
            if (uploads == null) {
                throw new Exception("Загружаемый файл отсутствует");
            }
            var memStream = new MemoryStream();
            uploads.OpenReadStream().CopyTo(memStream);
            APIclient.ListPostRequest("api/main/ExportDocument", memStream.ToArray(), id, null);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteDocument(int id) {
            APIclient.PostRequest("api/main/DeleteDocument", new document_binding_model {
                id = id
            });
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Enter() {
            return View();
        }

        [HttpPost]
        public void Enter(string email, string password) {
            APIclient.user = APIclient.GetRequest<user_view_model>($"api/user/login?email={email}&password={password}");
            if (APIclient.user == null) {
                throw new Exception("Неверный логин/пароль");
            }
            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult Register() {
            ViewBag.departments = APIclient.GetRequest<List<department_view_model>>("api/main/get_department_list");
            return View();
        }

        [HttpPost]
        public void Register(string email, string password, string fio, int department, DateOnly yearOfBirth,
                                enum_position position, enum_academic_degree ad, DateOnly adDate, enum_academic_title at,
                                DateOnly atDate) {
            APIclient.PostRequest("api/user/register", new user_binding_model {
                email = email,
                password = password, 
                fio = fio,
                DepartmentId = department,
                year_of_birth = yearOfBirth,
                position = position,
                academic_degree = ad,
                academic_title = at,
                year_of_award_ad = adDate,
                year_of_award_at = atDate
            });
            Response.Redirect("Enter");
        }

        [HttpGet]
        public IActionResult Privacy() {
            if (APIclient.user == null) {
                return Redirect("~/Home/Enter");
            }
            return View(APIclient.user);
        }

        [HttpPost]
        public void Privacy(string password, string email) {
            APIclient.PostRequest("api/user/edit", new user_binding_model {
                id = APIclient.user.id,
                password = password,
                email = email
            });
            APIclient.user.password = password;
            APIclient.user.email = email;
            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult ImportBackup() {
            var data = APIclient.GetRequest<List<string>>("api/backup/get_backup");

            var bytes = JsonConvert.DeserializeObject<byte[]>(data[0]);
            var name = JsonConvert.DeserializeObject<string>(data[1]);

            return File(bytes, "application/octet-stream", name);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
