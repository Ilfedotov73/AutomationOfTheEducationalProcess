using contracts.search_models;
using contracts.storage_contracts.db_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.storage_contracts {
    public interface Idepartment_storage {
        public List<Department> get_department_list();
        public List<Department> get_department_filltered_list(department_search_model search_model);
        public Department? get_department_info(department_search_model search_model);        
    }
}
