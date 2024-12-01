using contracts.search_models;
using contracts.storage_contracts.db_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.storage_contracts {
    public interface Istudent_group_storage {
        public List<StudentGroup> get_student_group_filltered_list(student_group_search_model search_model);
        public StudentGroup? get_student_group_info(student_group_search_model search_model);
    }
}
