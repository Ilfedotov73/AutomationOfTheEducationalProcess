using contracts.search_models;
using contracts.view_moedels;

namespace contracts.presenter_contracts {
    public interface Idepartment_presenter {
        public department_view_model make_department_presenter(department_search_model search_model);
        public List<department_view_model> make_department_list_presenter(department_search_model? search_model);
    }
}
