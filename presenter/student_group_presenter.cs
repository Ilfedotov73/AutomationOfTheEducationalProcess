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
            var model = _logic.get_student_info(search_model);
            var newViewModel = new student_group_view_model {
                id = model.id,
                group_num = model.group_num,
                student_count = model.students.Count(),
                direction_name = model.direction.alt_name,
                students_fio = new()
            };

            foreach (var student in model.students) {
                newViewModel.students_fio.Add(student.fio);
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
                    group_num = item.group_num,
                    student_count = item.students.Count(),
                    direction_name = item.direction.alt_name,
                    students_fio = new()
                };

                foreach (var student in item.students) {
                    newViewModel.students_fio.Add(student.fio);
                }
                newViewModels.Add(newViewModel);
            }
            return newViewModels;
        }
    }
}
