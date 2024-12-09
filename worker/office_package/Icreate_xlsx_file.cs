using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using worker.office_package.helper_models;

namespace worker.office_package {
    public interface Icreate_xlsx_file {
        void create_xlsx();
        void insert_cell_in_worksheet(xlsxCellParameters xlsxCellParameters);
        void merge_cells(xlsxMergeParameters xlsxMergeParameters);
        byte[]? save_xlsx();
    }
}
