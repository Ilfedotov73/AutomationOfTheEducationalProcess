using contracts.binding_models;
using contracts.worker_contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using worker.office_package;
using Microsoft.Office.Interop.Excel;
using worker.office_package.documents_description.TEMPLATE;


namespace worker.implements {
    public class itp_template_worker : Itemplate_worker {

        private readonly itp_template_to_xlsx _template;
        private readonly Application excel;
        public itp_template_worker(Icreate_xlsx_file xlsxImp) {
            _template = new(xlsxImp);
            excel = new();
        }

        public byte[] create_template_file(template_binding_model model) {
            var docuement = _template.create_template(new office_package.helper_models.itp_Info {
                title = model.name
            });
            if (docuement == null) {
                throw new Exception("Ошибка создания докумена шаблон");
            }

            // Записывают массив байт в бинарный файл. Если файл не существует, он создается. Если существует, то перезаписывается
            File.WriteAllBytes(model.file_path, docuement);
            return docuement;
        }

        public List<string> read_temp_file(template_binding_model model) {

            //todo
            // Доработать для сложного файла
            List<string> results = new();

            Workbook wb = excel.Workbooks.Open(model.file_path);
            Worksheet ws = wb.Worksheets[1];

            Microsoft.Office.Interop.Excel.Range cell = ws.Range["A2:C2"];
            foreach (string result in cell.Value) {
                results.Add(result);
            }
            return results;
        }
    }
}
