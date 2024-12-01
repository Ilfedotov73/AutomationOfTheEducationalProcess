using data_models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_models.IModels {
    public interface Idocument : Iid {
        string name { get; }
        string file_path { get; set; }
        int UserId { get; }
        enum_file_format_type file_format_type { get; }
        enum_document_type document_type { get; }
        int TemplateId { get; }
    }
}
