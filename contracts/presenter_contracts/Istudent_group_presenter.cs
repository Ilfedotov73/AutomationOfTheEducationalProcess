using contracts.binding_models;
using contracts.view_moedels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.presenter_contracts {
    public interface Istudent_group_presenter {
        public student_group_view_model make_student_group_presenter(student_group_binding_model model);
        public List<student_group_binding_model> make_student_group_list_presenter(List<student_group_binding_model> models);
    }
}
