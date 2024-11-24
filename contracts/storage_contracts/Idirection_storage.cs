using contracts.binding_models;
using contracts.search_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.storage_contracts {
    public interface Idirection_storage {
        public List<direction_binding_model> get_direction_list();
        public List<direction_binding_model> get_direction_filltered_list(direction_search_model search_model);
        public direction_binding_model get_direction_info(direction_search_model search_model);
    }
}
