using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_models.IModels {
    public interface Istudent : Iid {
        string fio { get; }
        int StudentGroupId { get; }
        int grade_book_num { get; }
    }
}
