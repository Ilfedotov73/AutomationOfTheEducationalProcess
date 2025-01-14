using contracts.binding_models;
using contracts.interactor_contracts;
using contracts.presenter_contracts;
using contracts.search_models;
using contracts.storage_contracts;
using contracts.view_moedels;
using contracts.worker_contracts;
using data_base_implement.implemnts;
using interactors;
using Microsoft.Extensions.DependencyInjection;
using presenter;
using worker.implements;
using worker.office_package;
using worker.office_package.helper_models.info_models;
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

            // --------PRESENTERS--------
            service.AddTransient<Idepartment_presenter, department_presenter>();
            service.AddTransient<Idirection_presenter, direction_presenter>();
            service.AddTransient<Idocument_presenter, document_presenter>();
            service.AddTransient<Ifaculty_presenter, faculty_presenter>();
            service.AddTransient<Istudent_group_presenter, student_group_presenter>();
            service.AddTransient<Istudent_presenter, student_presenter>();
            service.AddTransient<Itemplate_presenter, template_presenter>();
            service.AddTransient<Iuser_presenter, user_presenter>();
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

            var user = new user_view_model {
                fio = "Скалкин Антон Михайлович",
                department_name = "ИС",
                position = "Препод",
                year_of_birth = "01.01.2002",
                academic_degree = "test",
                year_of_award_ad = "01.01.2002",
                academic_title = "test",
                year_of_award_at = "01.01.2002",
                password = "123",
                faculty_name = "ФИСТ"
            };

            var tempInfo = new itp_temp_info {
                disciplines_A = ["dis1", "dis2", "dis3"],
                disciplines_B = ["dis2", "dis2", "dis3", "dis4"],

                workTypes_21 = ["work1", "work2", "work3"],
                workTypes_22 = ["work1", "work2", "work3", "work4"],
                workTypes_23 = ["work1", "work2", "work3"],
                workTypes_24 = ["work1", "work2", "work3"],

                workTypes_31 = ["work1", "work2", "work3", "work4"],
                workTypes_32 = ["work1", "work2", "work3"],

                workTypes_41 = ["work1", "work2", "work3"],
                workTypes_42 = ["work1", "work2", "work3"],
                user_info = user
            };

            var document = _logic.insert_template(new template_binding_model { 
                name = "test", 
                temp_info = tempInfo
            });
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
        private readonly Idocument_presenter _presenter;

        public documentFunc(Idocument_logic logic) {
            _logic = logic;
        }

        public void insert() {
            var data = new itp_info {
                title = "Индивидуальный план Скалкин",
                data_11 = [2, 4, 5, 2, 3, 6, 2, 8, 1, 10, 12, 23, 43, 15, 15, 65, 1],
                data_12 = [2, 4, 5, 2, 3, 6, 2, 8, 1, 10, 12, 23, 43, 15, 43, 13, 5],

                data_21 = new List<(int, int)> { (2, 1), (2, 2), (3, 1) },
                data_22 = new List<(int, int)> { (2, 1), (2, 2), (3, 1), (3, 1) },
                data_23 = new List<(int, int)> { (2, 1), (2, 2), (3, 1) },
                data_24 = new List<(int, int)> { (2, 1), (2, 2), (3, 1) },

                data_31 = new List<(int, int)> { (2, 1), (2, 2), (3, 1), (3, 1) },
                data_32 = new List<(int, int)> { (2, 1), (2, 2), (3, 1), (3, 1) },

                data_41 = new List<(int, int)> { (2, 1), (2, 2), (3, 1) },
                data_42 = new List<(int, int)> { (2, 1), (2, 2), (3, 1) },

                data_51 = [3, 4, 1]
            };

            var data1 = new st_info {
                date = DateOnly.FromDateTime(DateTime.Now),
                subject = "РПП",
                examiner = "Эгов",
                test = test_type.Экзамен,
                totalHoursNum = 75,
                group_info = new student_group_view_model {
                    faculty_name = "ФИСТ",
                    group = "ИСЭ-21",
                    direction_name = "Прикладная информатика",
                    course_num = 2,
                    semester_num = 4,
                    students = new List<(int grade_book_num, string fio)> {
                        (1, "Гисматуллин Равиль Марсович"),
                        (2, "Гордеев Игоря Вячеславович"),
                        (3, "Иванов Иван Иваныч")
                    }
                }
            };
            
            var st = new document_binding_model() {
                name = "Ведеомст2025",
                UserId = 2,
                date = DateOnly.FromDateTime(DateTime.Now),
                file_format_type = data_models.Enums.enum_file_format_type.xlsx,
                document_type = data_models.Enums.enum_document_type.statement_document,
                TemplateId = 110,
                data_doc = data1
            };
            st.setFilePath();

            _logic.insert_document(st);
        }

        public void info() {
            var model = _presenter.make_document_presenter(new document_search_model { id = 19 });
            Console.WriteLine($"{model.id} {model.template_name} {model.author_name} {model.date} {model.name}");
        }

        public void select() {
            var models = _presenter.make_document_list_presenter(null);
            foreach (var model in models) {
                Console.WriteLine($"{model.id} {model.template_name} {model.author_name} {model.date} {model.name}");
            }
        }
    }

    public class departmentFunc { 

        private readonly Idepartment_presenter _presenter;

        public departmentFunc(Idepartment_presenter presenter) {
            _presenter = presenter;
        }

        public void info() {
            var model = _presenter.make_department_presenter(new department_search_model {
                id = 1,
            });
            Console.WriteLine($"{model.id} {model.name} {model.faculty_name}");
        }

        public void select() {
            var models = _presenter.make_department_list_presenter(new department_search_model { faculty_id = 1 });
            foreach (var model in models) {
                Console.WriteLine($"{model.id} {model.name} {model.faculty_name}");
            }
        }
    }

    public class directionFunc {
        private readonly Idirection_presenter _logic;

        public directionFunc(Idirection_presenter logic) {
            _logic = logic;
        }

        public void info() {
            var model = _logic.make_direction_presenter(new direction_search_model {
                id = 1,
            });
            Console.WriteLine($"{model.id} {model.full_name} {model.alt_name} {model.department_name}");
        }

        public void select() {
            var models = _logic.make_direction_list_presenter(new direction_search_model {
                department_id = 1,
            });
            foreach (var model in models) {
                Console.WriteLine($"{model.id} {model.full_name} {model.alt_name} {model.department_name}");
            }
        }
    }

    public class facultyFunc {
        private readonly Ifaculty_presenter _logic;

        public facultyFunc(Ifaculty_presenter logic) {
            _logic = logic;
        }

        public void info() {
            var model = _logic.make_faculty_presenter(new faculty_search_model { id = 1 });
            Console.WriteLine($"{model.id} {model.name}");
        }

        public void select() {
            var models = _logic.make_faculty_list_presenter();
            foreach (var model in models) {
                Console.WriteLine($"{model.id} {model.name}");
            }
        }
    }

    public class studentFunc {
        private readonly Istudent_presenter _logic;

        public studentFunc(Istudent_presenter logic) {
            _logic = logic;
        }

        public void info() {
            var model = _logic.make_student_presenter(new student_search_model { id = 1 });
            Console.WriteLine($"{model.id} {model.fio} {model.group}");
        }

        public void select() {
            var models = _logic.make_student_list_presenter(new student_search_model {
                group_id = 1
            });

            foreach (var model in models) {
                Console.WriteLine($"{model.id} {model.fio} {model.group}");
            }
        }
    }

    public class studentGroupFunc {
        private readonly Istudent_group_presenter _logic;

        public studentGroupFunc(Istudent_group_presenter logic) {
            _logic = logic;
        }

        public void info() {
            var model = _logic.make_student_group_presenter(new student_group_search_model { id = 1 });
            Console.WriteLine($"{model.id} {model.direction_name}-{model.group}");
            foreach (var item in model.students) {
                Console.WriteLine($"->{item}");
            }
        }

        public void select() {
            var models = _logic.make_student_group_list_presenter(new student_group_search_model { direction_id = 1 });
            foreach (var model in models) {
                Console.WriteLine($"{model.id} {model.direction_name}-{model.group}");
                foreach (var item in model.students) {
                    Console.WriteLine($"->{item}");
                }
            }
        }
    }

    public class templateViewFunc {
        private readonly Itemplate_presenter _logic;

        public templateViewFunc(Itemplate_presenter logic) {
            _logic = logic;
        }

        public void info() {
            var model = _logic.make_template_presenter(new template_search_model { id = 6 });
            Console.WriteLine($"{model.id} {model.name}");
        }

        public void select() {
            var models = _logic.make_template_list_presenter();
            foreach (var item in models) {
                Console.WriteLine($"{item.id} {item.name}");
            }
        }
    }

    public class userViewFunc {
        private readonly Iuser_presenter _logic;

        public userViewFunc(Iuser_presenter logic) {
            _logic = logic;
        }

        public void info() {
            var model = _logic.make_user_presenter(new user_search_model { id = 2 });
            Console.WriteLine($"{model.fio}");
            foreach (var item in model.groups) {
                Console.WriteLine(item);
            }
        }

        public void select() {
            var models = _logic.make_user_list_presenter(new user_search_model { department_id = 1 });
            foreach (var model in models) {
                Console.WriteLine(model.fio);
                foreach (var item in model.groups) {
                    Console.WriteLine(item);
                }
            }
        }
    }
}
