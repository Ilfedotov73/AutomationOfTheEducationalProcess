using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using worker.office_package.helper_enums;

namespace worker.office_package.helper_models {
    public class docxTextProperties {
        public string size { get; set; } = string.Empty;
        public bool bold { get; set; }
        public wordJustificationType justification_type { get; set; }
    }
}
