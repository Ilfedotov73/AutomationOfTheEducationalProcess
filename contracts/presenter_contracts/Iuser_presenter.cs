using contracts.search_models;
using contracts.view_moedels;

namespace contracts.presenter_contracts {
    public interface Iuser_presenter {
        public user_view_model make_user_presenter(user_search_model search_model);
        public List<user_view_model> make_user_list_presenter(user_search_model? search_model);
    }
}
