using contracts.binding_models;
using contracts.view_moedels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.presenter_contracts {
    public interface Idepartment_presenter {
        public department_view_model make_department_presenter(department_binding_model model);
        public List<department_view_model> male_department_list_presenter(List<department_binding_model> models);
    }
}
