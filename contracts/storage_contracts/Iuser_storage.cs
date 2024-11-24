using contracts.binding_models;
using contracts.search_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.storage_contracts {
    public interface Iuser_storage {
        public List<user_binding_model> get_user_list();
        public List<user_binding_model> get_user_filltered_list(user_search_model search_model);
        public user_binding_model get_user_info(user_search_model search_model);
        public bool insert_user(user_binding_model model);
        public bool delete_user(user_binding_model model);
        public bool edit_user(user_binding_model model);
    }
}
