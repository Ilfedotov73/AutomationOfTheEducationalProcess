using contracts.search_models;
using contracts.view_moedels;

namespace contracts.presenter_contracts {
    public interface Ifaculty_presenter {
        public faculty_view_model make_faculty_presenter(faculty_search_model search_model);
        public List<faculty_view_model> make_faculty_list_presenter();
    }
}
