using contracts.binding_models;
using contracts.search_models;
using contracts.storage_contracts.db_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.storage_contracts {
    public interface Itemplate_storage {
        public List<Template> get_template_list();
        public Template? get_template_info(template_search_model search_model);
        public bool insert_template(template_binding_model model);
        public bool delete_template(template_binding_model model);
        public bool edit_tempalte(template_binding_model model);
    }
}
