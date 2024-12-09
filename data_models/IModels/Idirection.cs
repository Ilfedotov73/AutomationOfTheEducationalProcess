using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_models.IModels {
    public interface Idirection : Iid {
        string full_name { get; }
        string alt_name { get; }
        int DepartmentId { get; }
    }
}
