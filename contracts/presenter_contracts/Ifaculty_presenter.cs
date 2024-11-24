using contracts.binding_models;
using contracts.view_moedels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.presenter_contracts {
    public interface Ifaculty_presenter {
        public faculty_view_model make_faculty_presenter(faculty_binding_model model);
        public List<faculty_view_model> make_faculty_list_presenter(List<faculty_binding_model> models);
    }
}
