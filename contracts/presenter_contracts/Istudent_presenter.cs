using contracts.search_models;
using contracts.view_moedels;

namespace contracts.presenter_contracts {
    public interface Istudent_presenter {
        public student_view_model make_student_presenter(student_search_model search_model);
        public List<student_view_model> make_student_list_presenter(student_search_model? search_model);
    }
}
