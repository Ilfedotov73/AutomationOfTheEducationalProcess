using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_models.IModels {
    public interface Idepartment : Iid {
        string name { get; set; }
        int FacultyId { get; set; }
    }
}
