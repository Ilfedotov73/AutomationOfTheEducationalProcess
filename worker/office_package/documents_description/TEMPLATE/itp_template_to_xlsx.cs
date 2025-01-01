using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using worker.office_package.helper_enums;
using worker.office_package.helper_models;

namespace worker.office_package.documents_description.TEMPLATE {
    public class itp_template_to_xlsx {
        private readonly Icreate_xlsx_file _office;
        public itp_template_to_xlsx(Icreate_xlsx_file office) {
            _office = office;
        }

        public byte[]? create_template(itp_Info info) {
            _office.create_xlsx();

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 1,
                text = info.title,
                style_info = xlsxStyleInfoType.Title
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 2,
                text = info.date.ToString(),
                style_info = xlsxStyleInfoType.TextWithBorder
            });


            var document = _office.save_xlsx();
            return document;
        }
    }
}
