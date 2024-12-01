using data_models.IModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.storage_contracts.db_models {
    public class Department : Idepartment{
        public int id { get; set; }
        public string name { get; set; } = string.Empty;

        public int FacultyId { get; set; }
        public Faculty? faculty { get; set; }

        public List<Direction> directions { get; set; } = new();
        public List<User> users { get; set; } = new();
    }
}
