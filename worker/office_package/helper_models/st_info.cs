using contracts.worker_contracts.helper_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worker.office_package.helper_models {
    public class st_info : Idata_info {
        public string title { get; set; } = string.Empty;
        public DateOnly date { get; set; }
        public List<string> templateData { get; set; } = new();
    }
}
