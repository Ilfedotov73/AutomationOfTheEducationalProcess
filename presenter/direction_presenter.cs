using contracts.binding_models;
using contracts.interactor_contracts;
using contracts.presenter_contracts;
using contracts.search_models;
using contracts.view_moedels;
using System.Runtime.InteropServices;

namespace presenter {
    public class direction_presenter : Idirection_presenter {

        private readonly Idirection_logic _logic;

        public direction_presenter(Idirection_logic logic) {
            _logic = logic;
        }

        public direction_view_model make_direction_presenter(direction_search_model search_model) {
            var model = _logic.get_direction_info(search_model);
            var newViewModel = new direction_view_model {
                id = model.id,
                full_name = model.full_name,
                alt_name = model.alt_name,
                department_name = model.department.name
            };
            return newViewModel;
        }

        public List<direction_view_model> make_direction_list_presenter(direction_search_model? search_model) {
            var models = _logic.get_direction_list(search_model);
            List<direction_view_model> newViewModels = new();

            foreach (var item in models) {
                newViewModels.Add(new direction_view_model {
                    id = item.id,
                    full_name = item.full_name,
                    alt_name = item.alt_name,
                    department_name = item.department.name
                });
            }
            return newViewModels;
        }
    }
}
