using contracts.binding_models;
using contracts.view_moedels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.presenter_contracts {
    public interface Istudent_presenter {
        public student_view_model make_student_presenter(student_binding_model model);
        public List<student_view_model> make_student_list_presenter(List<student_binding_model> models);
    }
}
