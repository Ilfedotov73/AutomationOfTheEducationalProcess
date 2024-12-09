using data_models.IModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.storage_contracts.db_models {
    public class Student : Istudent {
        public int id { get; set; }
        public string fio { get; set; } = string.Empty;

        public int StudentGroupId { get; set; }
        public StudentGroup? student_group { get; set; }

        public int grade_book_num { get; set; }
    }
}
