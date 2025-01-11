using contracts.interactor_contracts;
using contracts.presenter_contracts;
using contracts.search_models;
using contracts.view_moedels;

namespace presenter {
    public class department_presenter : Idepartment_presenter {

        private readonly Idepartment_logic _logic;
        public department_presenter(Idepartment_logic logic) {
            _logic = logic;
        }

        public department_view_model make_department_presenter(department_search_model search_model) {
            var model = _logic.get_department_info(search_model);
            var newViewModel = new department_view_model {
                id = model.id,
                name = model.name,
                faculty_name = model.faculty.name
            };
            return newViewModel;
        }

        public List<department_view_model> make_department_list_presenter(department_search_model? search_model) {
            var models = _logic.get_department_list(search_model);
            List<department_view_model> newViewModels = new();

            foreach (var item in models) {
                newViewModels.Add(new department_view_model {
                    id = item.id,
                    name = item.name,
                    faculty_name = item.faculty.name
                });
            }
            return newViewModels;
        }
    }
}
