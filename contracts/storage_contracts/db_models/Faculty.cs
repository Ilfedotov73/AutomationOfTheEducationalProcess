using data_models.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.storage_contracts.db_models {
    public class Faculty : Ifaculty {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;

        public List<Department> departments { get; set; } = new();
    }
}
