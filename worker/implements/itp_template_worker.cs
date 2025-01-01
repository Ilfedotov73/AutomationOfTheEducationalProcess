using contracts.binding_models;
using contracts.worker_contracts;
using worker.office_package;
using Microsoft.Office.Interop.Excel;
using worker.office_package.documents_description.TEMPLATE;


namespace worker.implements {
    public class itp_template_worker : Itemplate_worker{

        private readonly itp_template_to_xlsx _template;
        public itp_template_worker(Icreate_xlsx_file xlsxImp) {
            _template = new(xlsxImp);
        }   

        public byte[] create_template_file(template_binding_model model) {
            var document = _template.create_template(new office_package.helper_models.itp_Info {
                title = model.name
            });
            if (document == null) {
                throw new Exception("Ошибка создания докумена шаблон");
            }
            // Создает какталог (если еще не создан)
            if (!Directory.Exists(model.file_path)) {
                Directory.CreateDirectory(model.file_path);
            }

            // Записывают массив байт в бинарный файл. Если файл не существует, он создается. Если существует, то перезаписывается
            model.file_path += $"{model.name}.xlsx";
            File.WriteAllBytes(model.file_path, document);

            return document;
        }

        public List<string> read_temp_file(template_binding_model model) {

            List<string> results = new();

            Application excel = new();
            Workbook wb = excel.Workbooks.Open(model.file_path);
            Worksheet ws = wb.Worksheets[1];

            Microsoft.Office.Interop.Excel.Range cell = ws.Range["A2:C2"];
            foreach (string result in cell.Value) {
                results.Add(result);
            }

            excel.Quit();
            return results;
        }
    }
}
