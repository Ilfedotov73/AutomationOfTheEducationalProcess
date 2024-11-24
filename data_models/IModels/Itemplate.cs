using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_models.IModels {
    public interface Itemplate : Iid {
        string name { get; }
        string file_path { get; }
        int author_id { get; }
    }
}
