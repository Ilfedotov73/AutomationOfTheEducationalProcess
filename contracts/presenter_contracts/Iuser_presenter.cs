using contracts.binding_models;
using contracts.view_moedels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.presenter_contracts {
    public interface Iuser_presenter {
        public user_view_model make_user_presenter(user_binding_model model);
        public List<user_view_model> make_user_list_presenter(List<user_binding_model> models);
    }
}
