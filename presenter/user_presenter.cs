using contracts.interactor_contracts;
using contracts.presenter_contracts;
using contracts.search_models;
using contracts.view_moedels;

namespace presenter {
    public class user_presenter : Iuser_presenter {

        private readonly Iuser_logic _logic;

        public user_presenter(Iuser_logic logic) {
            _logic = logic;
        }

        public user_view_model make_user_presenter(user_search_model search_model) {
            var model = _logic.get_user_info(search_model);
            var newViewModel = new user_view_model {
                id = model.id,
                fio = model.fio,
                department_name = model.department.name,
                position = model.position.ToString(),
                year_of_birth = model.year_of_birth.ToString(),
                academic_degree = model.academic_degree.ToString(),
                year_of_award_ad = model.year_of_award_ad.ToString(),
                academic_title = model.academic_title.ToString(),
                year_of_award_at = model.year_of_award_at.ToString(),
                password = model.password.ToString(),
                groups = new()
            };

            foreach (var item in model.studentGroups) {
                newViewModel.groups.Add(item.direction.alt_name + "-" + item.group_num.ToString());
            }
            return newViewModel;
        }

        public List<user_view_model> make_user_list_presenter(user_search_model? search_model) {
            var models = _logic.get_user_list(search_model);
            List<user_view_model> newViewModels = new();

            foreach (var item in models) {
                var newViewModel = new user_view_model {
                    id = item.id,
                    fio = item.fio,
                    department_name = item.department.name,
                    position = item.position.ToString(),
                    year_of_birth = item.year_of_birth.ToString(),
                    academic_degree = item.academic_degree.ToString(),
                    year_of_award_ad = item.year_of_award_ad.ToString(),
                    academic_title = item.academic_title.ToString(),
                    year_of_award_at = item.year_of_award_at.ToString(),
                    password = item.password.ToString(),
                    groups = new()
                };

                foreach (var sg in item.studentGroups) {
                    newViewModel.groups.Add(sg.direction.alt_name + "-" + sg.group_num.ToString());
                }
                newViewModels.Add(newViewModel);
            }
            return newViewModels;
        }
    }
}
