using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using worker.office_package.helper_enums;
using worker.office_package.helper_models;

namespace worker.office_package.documents_description {
    public class st_document_to_xlsx {

        private readonly Icreate_xlsx_file _office;

        public st_document_to_xlsx(Icreate_xlsx_file offuce) {
            _office = offuce;
        }

        public byte[]? create_document(st_info info) {
            _office.create_xlsx();

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 1,
                text = info.title,
                style_info = xlsxStyleInfoType.Title
            });

            var document = _office.save_xlsx();
            return document;
        }
    }
}
