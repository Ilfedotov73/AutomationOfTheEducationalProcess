using contracts.search_models;
using contracts.storage_contracts.db_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.storage_contracts {
    public interface Ifaculty_storage {
        public List<Faculty> get_faculty_list();
        public Faculty? get_faculty_info(faculty_search_model search_model);
    }
}
