using contracts.binding_models;
using contracts.search_models;
using contracts.view_moedels;

namespace contracts.presenter_contracts {
    public interface Istudent_group_presenter {
        public student_group_view_model make_student_group_presenter(student_group_search_model search_model);
        public List<student_group_view_model> make_student_group_list_presenter(student_group_search_model? 
                                                                                search_model);
    }
}
