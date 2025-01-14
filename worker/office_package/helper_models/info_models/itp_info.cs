using contracts.worker_contracts.helper_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worker.office_package.helper_models.info_models {
    public class itp_info : Idata_info {
        public string title { get; set; } = string.Empty;

        public int[]? data_11 { get; set; } = new int[17];
        public int[]? data_12 { get; set; } = new int[17];

        public List<(int, int)> data_21 { get; set; } = new();
        public List<(int, int)> data_22 { get; set; } = new();
        public List<(int, int)> data_23 { get; set; } = new();
        public List<(int, int)> data_24 { get; set; } = new();
        public List<(int, int)> data_31 { get; set; } = new();
        public List<(int, int)> data_32 { get; set; } = new();
        public List<(int, int)> data_41 { get; set; } = new();
        public List<(int, int)> data_42 { get; set; } = new();

        public int[] data_51 { get; set; } = new int[4];
    }
}
