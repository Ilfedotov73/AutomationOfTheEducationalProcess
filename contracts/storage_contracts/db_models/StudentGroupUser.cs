using data_models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.storage_contracts.db_models {
    public class StudentGroupUser {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StudentGroupId { get; set; } 
    }
}
