using contracts.binding_models;
using contracts.search_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.interactor_contracts {
    public interface Iuser_logic {
        public List<user_binding_model> get_user_list(user_search_model search_model);
        public user_binding_model? get_user_info(user_search_model search_model);
        public void insert_user(user_binding_model model);
        public void delete_user(user_binding_model model);
        public void edit_user(user_binding_model model);
        public void check_model(user_binding_model model, bool obDelete);
    }
}
