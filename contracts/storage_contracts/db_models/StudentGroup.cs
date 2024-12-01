using data_models.IModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace contracts.storage_contracts.db_models {
    public class StudentGroup : Istudent_group {
        public int id { get; set; }
        public int DirectionId { get; set; }
        public Direction? direction { get; set; }

        public int course_num { get; set; }
        public int semester_num { get; set; }
        public int group_num { get; set; }

        public List<Student> students { get; set; } = new();
        public List<User> users { get; set; } = new();
    }
}
