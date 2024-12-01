using contracts.binding_models;
using contracts.search_models;
using contracts.storage_contracts.db_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.storage_contracts {
    public interface Iuser_storage {
        public List<User> get_user_list();
        public List<User> get_user_filltered_list(user_search_model search_model);
        public User? get_user_info(user_search_model search_model);
        public bool insert_user(user_binding_model model);
        public bool delete_user(user_binding_model model);
        public bool edit_user(user_binding_model model);
    }
}
