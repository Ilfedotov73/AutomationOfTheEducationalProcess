using contracts.interactor_contracts;
using contracts.presenter_contracts;
using contracts.search_models;
using contracts.view_moedels;

namespace presenter {
    public class student_group_presenter : Istudent_group_presenter {

        private readonly Istudent_group_logic _logic;

        public student_group_presenter(Istudent_group_logic logic) {
            _logic = logic;
        }

        public student_group_view_model make_student_group_presenter(student_group_search_model search_model) {
            var model = _logic.get_student_group_info(search_model);
            var newViewModel = new student_group_view_model {
                id = model.id,
                faculty_name = model.direction.department.faculty.name,
                group = model.direction.alt_name + model.group_num.ToString(),
                direction_name = model.direction.full_name,
                course_num = model.course_num,
                semester_num = model.semester_num,
                students = new()
            };

            foreach (var student in model.students) {
                newViewModel.students.Add((student.grade_book_num,student.fio));
            }
            return newViewModel;
        }

        public List<student_group_view_model> make_student_group_list_presenter(student_group_search_model? 
                                                                                search_model) {
            var models = _logic.get_student_group_list(search_model);
            List<student_group_view_model> newViewModels = new();

            foreach (var item in models) {

                var newViewModel = new student_group_view_model {
                    id = item.id,
                    faculty_name = item.direction.department.faculty.name,
                    group = item.direction.alt_name + item.group_num.ToString(),
                    direction_name = item.direction.full_name,
                    course_num = item.course_num,
                    semester_num = item.semester_num,
                    students = new()
                };

                foreach (var student in item.students) {
                    newViewModel.students.Add((student.grade_book_num,student.fio));
                }
                newViewModels.Add(newViewModel);
            }
            return newViewModels;
        }
    }
}
