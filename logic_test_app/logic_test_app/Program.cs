using contracts.binding_models;
using contracts.binding_models.document_extension;
using contracts.interactor_contracts;
using contracts.search_models;
using contracts.storage_contracts;
using contracts.worker_contracts;
using data_base_implement.implemnts;
using interactors;
using Microsoft.Extensions.DependencyInjection;
using worker.implements;
using worker.office_package;
using worker.office_package.office_implements;


namespace logic_test_app {
    internal class Program {

        private static ServiceProvider? _serviceProvider;
        public static ServiceProvider? ServiceProvider => _serviceProvider;

        static void Main(string[] args) {
            var services = new ServiceCollection();
            ConfiguratuinServices(services);
            _serviceProvider = services.BuildServiceProvider();

            var service = new documentFunc(_serviceProvider.GetService<Idocument_logic>());
            service.insert();
        }
        
        private static void ConfiguratuinServices(ServiceCollection service) {
            // --------WORKER--------
            service.AddTransient<Itemplate_worker, itp_template_worker>();
            service.AddTransient<Icreate_docx_file, create_to_docx>();
            service.AddSingleton<Icreate_xlsx_file, create_to_xlsx>();

            // --------STORAGE--------
            service.AddTransient<Idepartment_storage, db_department_storage>();
            service.AddTransient<Idirection_storage, db_direction_storage>();
            service.AddTransient<Idocument_storage, db_document_storage>();
            service.AddTransient<Ifaculty_storage, db_faculty_storage>();
            service.AddTransient<Istudent_group_storage, db_student_group_storage>();
            service.AddTransient<Istudent_storage, db_student_storage>();
            service.AddTransient<Itemplate_storage, db_template_storage>();
            service.AddTransient<Iuser_storage, db_user_storage>();

            // --------INTERACTORS--------
            service.AddTransient<Idepartment_logic, department_logic>();
            service.AddTransient<Idirection_logic, direction_logic>();
            service.AddTransient<Idocument_logic, document_logic>();
            service.AddTransient<Ifaculty_logic, faculty_logic>();
            service.AddTransient<Istudent_group_logic, student_group_logic>();
            service.AddTransient<Istudent_logic, student_logic>();
            service.AddTransient<Itemplate_logic, template_logic>();
            service.AddTransient<Iuser_logic, user_logic>();            
        }
    }

    public class userFunc {

        private readonly Iuser_logic _logic;

        public userFunc(Iuser_logic logic) {
            _logic = logic;
        }

        public void insert() {
            var model = new user_binding_model {
                fio = "Иванов Иван Иванович",
                DepartmentId = 1,
                position = data_models.Enums.enum_position.teacher,
                year_of_birth = new DateOnly(1981, 01, 01),
                academic_degree = data_models.Enums.enum_academic_degree.candidate_of_sciences,
                year_of_award_ad = new DateOnly(2023, 06, 21),
                academic_title = data_models.Enums.enum_academic_title.docent,
                year_of_award_at = new DateOnly(2025, 07, 23),
                password = "123"
            };
            _logic.insert_user(model);
        }

        public void update() {
            var model = new user_binding_model {
                id = 2,
                fio = "Скалкин Антон Михайлович",
                DepartmentId = 1,
                position = data_models.Enums.enum_position.teacher,
                year_of_birth = new DateOnly(1981, 01, 01),
                academic_degree = data_models.Enums.enum_academic_degree.candidate_of_sciences,
                year_of_award_ad = new DateOnly(2023, 06, 21),
                academic_title = data_models.Enums.enum_academic_title.docent,
                year_of_award_at = new DateOnly(2025, 07, 23),
                password = "123"
            };
            _logic.edit_user(model);
        }

        public void info() {
            var model = _logic.get_user_info(new user_search_model { id = 2 });
            string result = model.fio + " " + model.position;
            Console.WriteLine(result);
        }

        public void select() {
            var models = _logic.get_user_list(new user_search_model { department_id = 1 });
            foreach (var model in models) {
                string result = model.fio + " " + model.position;
                foreach (var studentGroup in model.studentGroups) {
                    Console.WriteLine(studentGroup.group_num);
                    foreach (var student in studentGroup.students) {
                        Console.WriteLine(student.fio);
                    }
                }
            }
        }

        public void delete() {
            _logic.delete_user(new user_binding_model { id = 3 });
        }
    }

    public class templateFunc {
        private readonly Itemplate_logic _logic;

        public templateFunc(Itemplate_logic logic) {
            _logic = logic;
        }

        public void insert() {
            var document = _logic.insert_template(new template_binding_model { name = "itp2025" });
            File.WriteAllBytes(@"C:\Users\Ilfe\Downloads\" + "itp2024.xlsx", document);
        }

        public void export() {
            string name = _logic.get_template_info(new template_search_model { id = 5 }).name;
            var document = _logic.on_export_template(new template_search_model { id = 5 });
            File.WriteAllBytes($@"C:\Users\Ilfe\Downloads\{name}" + ".xlsx", document);
        }

        public void edit() {
            var temp = _logic.get_template_info(new template_search_model { id = 6 });
            string new_name = "itp2027";

            var new_doc = File.ReadAllBytes($@"C:\Users\Ilfe\Downloads\itp2024.xlsx");
            _logic.edit_template(temp, new_doc, new_name);
        }

        public void select() {
            var list = _logic.get_template_list();
            foreach (var item in list) {
                Console.WriteLine(item.name + " " + item.file_path);
            }
        }

        public void delete() {
            var temp_del = _logic.get_template_info(new template_search_model { id = 1 });
            _logic.delete_template(temp_del);
        }
    }

    public class documentFunc {
        private readonly Idocument_logic _logic;

        public documentFunc(Idocument_logic logic) {
            _logic = logic;
        }

        public void insert() {
            var st = new statement_doc_binding_model {
                name = "st2025",
                UserId = 2,
                file_format_type = data_models.Enums.enum_file_format_type.xlsx,
                document_type = data_models.Enums.enum_document_type.statement_document,
                TemplateId = 1,
                student_group_id = 1
            };

            var itp = new itp_doc_binding_model {
                name = "itp2024",
                UserId = 2,
                file_format_type = data_models.Enums.enum_file_format_type.xlsx,
                TemplateId = 6,
            };

            _logic.insert_document(itp);
        }
    }
}
