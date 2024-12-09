using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using worker.office_package.helper_enums;

namespace worker.office_package.helper_models {
    public class xlsxCellParameters {
        public string column_name { get; set; } = string.Empty;
        public uint row_index { get; set; }
        public string text { get; set; } = string.Empty;
        public string cell_reference => $"{column_name}{row_index}";
        public xlsxStyleInfoType style_info { get; set; }
    }
}
