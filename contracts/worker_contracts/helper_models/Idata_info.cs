using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.worker_contracts.helper_models {
    public interface Idata_info {
        string title { get; }
        DateOnly date { get; }
        List<string> templateData { get; }
    }
}
