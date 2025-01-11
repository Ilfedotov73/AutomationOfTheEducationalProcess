using contracts.search_models;
using contracts.view_moedels;

namespace contracts.presenter_contracts {
    public interface Idirection_presenter {
        public direction_view_model make_direction_presenter(direction_search_model search_model);
        public List<direction_view_model> make_direction_list_presenter(direction_search_model? search_model);
    }
}
