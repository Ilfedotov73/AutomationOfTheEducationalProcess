using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worker.office_package.helper_models {
    public class xlsxMergeParameters {
        public string cell_from_name { get; set; } = string.Empty;
        public string cell_to_name { get; set; } = string.Empty;
        public string merge => $"{cell_from_name}:{cell_to_name}";
    }
}
