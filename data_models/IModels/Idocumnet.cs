using data_models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_models.IModels {
    public interface Idocumnet : Iid {
        string name { get; }
        string file_path { get; set; }
        int author_id { get; }
        enum_file_format_type file_format_type { get; }
        int template_id { get; }
    }
}
