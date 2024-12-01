using data_base_implement.implemnts;
using data_base_implement;
using contracts.binding_models;
using contracts.storage_contracts.db_models;
using System.Runtime.InteropServices;
using Microsoft.Identity.Client;
using System.Diagnostics.Contracts;
using data_models.IModels;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace db_storage_test_app {
    internal class Program {
        static void Main(string[] args) {
            faculty_func faculty_Func = new();
            department_func department_Func = new();
            direction_func direction_Func = new();
            student_group_Func student_group_Func = new();
            student_Func student_Func = new();
            user_func user_Func = new();
            user_student_group_func user_Student_Group_Func = new();
            template_func template_Func = new();
            document_func document_Func = new();

            //faculty_Func.insert();
            //department_Func.insert();
            //direction_Func.insert();
            //student_group_Func.insert();
            //student_Func.insert();
            //user_Func.insert();
            //user_Student_Group_Func.insert();
            template_Func.delete();
            //document_Func.insert();
        }
    }

    class faculty_func {
        public void insert() {
            using var context = new data_base();

            var model = new Faculty{
                name = "Фист"
            };
            context.Add(model);
            context.SaveChanges();
        }
        public void select() {
            db_faculty_storage storage = new();
            
            var models = storage.get_faculty_list();
            foreach (var model in models) {
                Console.WriteLine(model.id + " " + model.name);
                foreach (var department in model.departments) {
                    Console.WriteLine("->" + department.id + " " + department.name);
                }
            }
        }
        public void get() {
            db_faculty_storage storage = new();

            var model = storage.get_faculty_info(new contracts.search_models.faculty_search_model { id = 1 });
            Console.WriteLine(model?.name);
            
            foreach (var department in model.departments) {
                Console.WriteLine("->" + department.id + " " + department.name);
            }
        }
    }
    class department_func {
        public void insert() {
            using var context = new data_base();

            var model = new Department {
                name = "ИС",
                FacultyId = 1
            };

            context.Add(model);
            context.SaveChanges();
        }
        public void select() {
            db_department_storage storage = new();
            var models = storage.get_department_list();
            foreach (var model in models) {
                Console.WriteLine(model.id + " " + model.name);
                foreach (var user in model.users) {
                    Console.WriteLine("->" + user.id + " " + user.fio);
                }
                foreach (var direction in model.directions) {
                    Console.WriteLine("->" + direction.id + " " + direction.full_name);
                }
            }
        }
        public void selectFill() {
            db_department_storage storage = new();
            var models = storage.get_department_filltered_list(new contracts.search_models.department_search_model { faculty_id = 1 });
            foreach (var model in models) {
                Console.WriteLine(model.id + " " + model.name);
                foreach (var user in model.users) {
                    Console.WriteLine("->" + user.id + " " + user.fio);
                }
                foreach (var direction in model.directions) {
                    Console.WriteLine("->" + direction.id + " " + direction.full_name);
                }
            }
        }
        public void get() {
            db_department_storage storage = new();
            var model = storage.get_department_info(new contracts.search_models.department_search_model { id = 2 });
            Console.WriteLine(model?.name);
            foreach (var user in model.users) {
                Console.WriteLine("->" + user.id + " " + user.fio);
            }
            foreach (var direction in model.directions) {
                Console.WriteLine("->" + direction.id + " " + direction.full_name);
            }
        }
    }
    class direction_func() {
        public void insert() {
            using var context = new data_base();

            var model = new Direction {
                full_name = "Програмная инженерия",
                alt_name = "ПИ",
                DepartmentId = 1
            };

            context.Add(model);
            context.SaveChanges();
        }
        public void select() {
            db_direction_storage storage = new();
            var models = storage.get_direction_list();
            foreach (var model in models) {
                Console.WriteLine(model.id + " " + model.full_name + " " + model.alt_name);
                foreach (var studentGroup in model.student_groups) {
                    Console.WriteLine("->" + studentGroup.id + " " + studentGroup.group_num);
                }
            }
        }
        public void selectFill() {
            db_direction_storage storage = new();
            var models = storage.get_direction_filltered_list(new contracts.search_models.direction_search_model { department_id = 2 });
            foreach (var model in models) {
                Console.WriteLine(model.id + " " + model.full_name + " " + model.alt_name);
                foreach (var studentGroup in model.student_groups) {
                    Console.WriteLine("->" + studentGroup.id + " " + studentGroup.group_num);
                }
            }
        }
        public void get() {
            db_direction_storage storage = new();
            var model = storage.get_direction_info(new contracts.search_models.direction_search_model { id = 1 });
            Console.WriteLine(model?.id + " " + model?.full_name + " " + model?.alt_name);
            foreach (var studentGroup in model.student_groups) {
                Console.WriteLine("->" + studentGroup.id + " " + studentGroup.group_num);
            }
        }
    }
    class student_group_Func() {
        public void insert() {
            using var context = new data_base();

            var model = new StudentGroup {
                course_num = 1,
                semester_num = 2,
                group_num = 11,
                DirectionId = 1
            };

            context.Add(model);
            context.SaveChanges();
        }
        public void select() {
            db_student_group_storage storage = new();
            var models = storage.get_student_group_filltered_list(new contracts.search_models.student_group_search_model { direction_id = 1 });
            foreach (var model in models) {
                Console.WriteLine(model.id + " " + model.group_num + " " + model?.direction?.full_name);
                foreach (var student in model.students) {
                    Console.WriteLine("->" + student.id + " " + student.fio + " " + student.grade_book_num);
                }
            }
        }
        public void get() {
            db_student_group_storage storage = new();
            var model = storage.get_student_group_info(new contracts.search_models.student_group_search_model { id = 1 });
            Console.WriteLine(model.id + " " + model.group_num + " " + model?.direction?.full_name);
            foreach (var student in model.students) {
                Console.WriteLine("->" + student.id + " " + student.fio + " " + student.grade_book_num);
            }
        }
    }
    class student_Func() {
        public void insert() {
            using var context = new data_base();

            for (int i = 0; i < 5; i++) {
                var model = new Student {
                    fio = $"Студент_{i}",
                    grade_book_num = i,
                    StudentGroupId = 1,
                };
                context.Add(model);
            }
            context.SaveChanges();
        }
        public void select() {
            db_student_storage storage = new();
            var models = storage.get_student_filltered_list(new contracts.search_models.student_search_model { group_id = 1 });
            foreach (var student in models) {
                Console.WriteLine(student.id + " " + student.fio + " " + student.grade_book_num);
            }
        }
        public void get() {
            db_student_storage storage = new();
            var model = storage.get_student_info(new contracts.search_models.student_search_model { id = 2 });
            Console.WriteLine(model?.id + " " + model?.fio + " " + model?.grade_book_num);
        }
    }
    class user_func() {
        public void insert() {
            db_user_storage storage = new();
            storage.insert_user(new user_binding_model {
                fio = "Препод1",
                position = data_models.Enums.enum_position.teacher,
                year_of_birth = new DateOnly(0001, 01, 01),
                academic_degree = data_models.Enums.enum_academic_degree.doctor_of_sciences,
                year_of_award_ad = new DateOnly(0001, 01, 01),
                academic_title = data_models.Enums.enum_academic_title.docent,
                year_of_award_at = new DateOnly(0001, 01, 01),
                DepartmentId = 1,
                password = "1234"
            });
        }
        public void selectFill() {
            db_user_storage storage = new();
            var models = storage.get_user_filltered_list(new contracts.search_models.user_search_model { department_id = 1 });
            foreach (var model in models) {
                Console.WriteLine(model.id + " " + model.fio);
                foreach(var doc in model.documents) {
                    Console.WriteLine("->" + doc.id + " " + doc.name + " " + doc.file_path + " " + doc.file_format_type + " " + doc.document_type);
                }
                foreach (var studentGroup in model.student_groups) {
                    Console.WriteLine("->" + studentGroup.id + " " + studentGroup.group_num);
                    foreach (var student in studentGroup.students) {
                        Console.WriteLine("-> ->" + student.id + " " + student.fio + " " + student.grade_book_num);
                    } 
                }
            }
        }
        public void get() {
            db_user_storage storage = new();
            var model = storage.get_user_info(new contracts.search_models.user_search_model { id = 2 });
            Console.WriteLine(model.id + " " + model.fio);
            foreach (var doc in model.documents) {
                Console.WriteLine("->" + doc.id + " " + doc.name + " " + doc.file_path + " " + doc.file_format_type + " " + doc.document_type);
            }
            foreach (var studentGroup in model.student_groups) {
                Console.WriteLine("->" + studentGroup.id + " " + studentGroup.group_num);
                foreach (var student in studentGroup.students) {
                    Console.WriteLine("-> ->" + student.id + " " + student.fio + " " + student.grade_book_num);
                }
            }
        }
        public void edit() {
            db_user_storage storage = new();
            storage.edit_user(new user_binding_model {
                id = 3,
                fio = "Препод3",
                DepartmentId = 2,
                position = data_models.Enums.enum_position.dean,
                academic_degree = data_models.Enums.enum_academic_degree.doctor_of_sciences,
                year_of_award_ad = new DateOnly(0001, 01, 01),
                academic_title = data_models.Enums.enum_academic_title.professor,
                year_of_award_at = new DateOnly(0001, 01, 01)
            });
        }
        public void delete() {
            db_user_storage storage = new();
            storage.delete_user(new user_binding_model { id = 1 });
        }
    }
    class user_student_group_func() {
        public void insert() {
            using var context = new data_base();
            var rec = new StudentGroupUser {
                StudentGroupId = 1,
                UserId = 2,
            };

            context.Add(rec);
            context.SaveChanges();
        }
    }
    class template_func {
        public void insert() {
            db_template_storage storage = new();
            storage.insert_template(new template_binding_model {
                name = "individual teacher plan",
                file_path = @"C:\template"
            });
        }
        public void select() {
            db_template_storage storage = new();
            var models = storage.get_template_list();
            foreach (var model in models) {
                Console.WriteLine(model.id + " " + model.name + " " + model.file_path);
                foreach (var doc in model.documents) {
                    Console.WriteLine("->" + doc.id + " " + doc.name + " " + doc.file_path + " " + doc.file_format_type + " " + doc.document_type);
                }
            }
        }
        public void get() {
            db_template_storage storage = new();
            var model = storage.get_template_info(new contracts.search_models.template_search_model { id = 1 });
            Console.WriteLine(model?.id + " " + model?.name + " " + model?.file_path);
            foreach (var doc in model.documents) {
                Console.WriteLine("->" + doc.id + " " + doc.name + " " + doc.file_path + " " + doc.file_format_type + " " + doc.document_type);
            }
        }
        public void edit() {
            db_template_storage storage = new();
            storage.edit_tempalte(new template_binding_model {
                id = 1,
                name = "statementTEMP",
                file_path = @"D:\STATEMENT"
            });
        }
        public void delete() {
            db_template_storage storage = new();
            storage.delete_template(new template_binding_model { id = 1 });
        }
    }
    public class document_func() {
        public void insert() {
            db_document_storage storage = new();
            storage.insert_document(new itp_doc_binding_model {
                name = "individual_teacher_plan2024",
                file_path = @"C:\ITP",
                UserId = 1,
                file_format_type = data_models.Enums.enum_file_format_type.docx,
                TemplateId = 1
            });
        }
        public void select() {
            db_document_storage storage = new();
            var models = storage.get_document_list();
            foreach (var model in models) {
                Console.WriteLine("->" + model.id + " " + model.name + " " + model.file_path + " " + model.file_format_type + " " + model.document_type);
            }
        }
        public void selectFill() {
            db_document_storage storage = new();
            var models = storage.get_document_filltered_list(new contracts.search_models.document_search_model { template_id = 1 });
            foreach (var model in models) {
                Console.WriteLine("->" + model.id + " " + model.name + " " + model.file_path + " " + model.file_format_type + " " + model.document_type);
            }
        }
        public void get() {
            db_document_storage storage = new();
            var model = storage.get_document_info(new contracts.search_models.document_search_model { id = 5 });
            Console.WriteLine("->" + model?.id + " " + model?.name + " " + model?.file_path + " " + model?.file_format_type + " " + model?.document_type);
        }
        public void edit() {
            db_document_storage storage = new();
            storage.edit_docuemnt(new statement_doc_binding_model {
                id = 5,
                name = "statement2025",
                file_path = @"C:\STATEMENT",
                document_type = data_models.Enums.enum_document_type.statement_document,
                TemplateId = 1
            });
        }
        public void delete() {
            db_document_storage storage = new();
            storage.delete_docuemnt(new statement_doc_binding_model { id = 1 });
        }
    }
}