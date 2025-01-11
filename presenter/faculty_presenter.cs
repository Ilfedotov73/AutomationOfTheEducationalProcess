using contracts.interactor_contracts;
using contracts.presenter_contracts;
using contracts.search_models;
using contracts.view_moedels;

namespace presenter {
    public class faculty_presenter : Ifaculty_presenter {

        private readonly Ifaculty_logic _logic;

        public faculty_presenter(Ifaculty_logic logic) {
            _logic = logic;
        }
        
        public faculty_view_model make_faculty_presenter(faculty_search_model search_model) {
            var model = _logic.get_faculty_info(search_model);
            var newViewModel = new faculty_view_model {
                id = model.id,
                name = model.name,
            };
            return newViewModel;
        }

        public List<faculty_view_model> make_faculty_list_presenter() {
            var models = _logic.get_faculty_list();
            List<faculty_view_model> newViewModels = new();

            foreach (var item in models) {
                newViewModels.Add(new faculty_view_model {
                    id = item.id,
                    name = item.name,
                });
            }
            return newViewModels;
        }
    }
}
