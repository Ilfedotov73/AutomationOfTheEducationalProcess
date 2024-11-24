using contracts.binding_models;
using contracts.view_moedels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.presenter_contracts {
    public interface Idirection_presenter {
        public direction_view_model make_direction_presenter(direction_binding_model model);
        public List<direction_binding_model> make_direction_list_presenter(List<direction_binding_model> models);
    }
}
