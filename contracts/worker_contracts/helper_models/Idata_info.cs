﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.worker_contracts.helper_models {
    public interface Idata_info {
        string titel { get; }
        DateOnly date { get; }
    }
}