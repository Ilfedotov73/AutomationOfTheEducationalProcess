using contracts.interactor_contracts;
using contracts.presenter_contracts;
using contracts.search_models;
using contracts.view_moedels;

namespace presenter {
    public class student_presenter : Istudent_presenter {

        private readonly Istudent_logic _logic;

        public student_presenter(Istudent_logic logic) {
            _logic = logic;
        }

        public student_view_model make_student_presenter(student_search_model search_model) {
            var model = _logic.get_student_info(search_model);
            var newViewModel = new student_view_model {
                id = model.id,
                fio = model.fio,
                group = $"{model.student_group.direction.alt_name}-{model.student_group.group_num}",
                grade_book_num = model.grade_book_num,
            };
            return newViewModel;
        }

        public List<student_view_model> make_student_list_presenter(student_search_model? search_model) {
            var models = _logic.get_student_list(search_model);
            List<student_view_model> newViewModels = new();

            foreach (var item in models) {
                newViewModels.Add(new student_view_model {
                    id = item.id,
                    fio = item.fio,
                    group = $"{item.student_group.direction.alt_name}-{item.student_group.group_num}",
                    grade_book_num = item.grade_book_num,
                });
            }
            return newViewModels;
        }
    }
}
