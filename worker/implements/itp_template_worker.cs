using contracts.binding_models;
using contracts.worker_contracts;
using worker.office_package;
using Microsoft.Office.Interop.Excel;
using worker.office_package.documents_description.TEMPLATE;
using worker.office_package.helper_models.info_models;
using contracts.worker_contracts.helper_models;
using System.Runtime.InteropServices;


namespace worker.implements {
    public class itp_template_worker : Itemplate_worker{

        private readonly itp_template_to_xlsx _template;
        public itp_template_worker(Icreate_xlsx_file xlsxImp) {
            _template = new(xlsxImp);
        }   

        public byte[] create_template_file(template_binding_model model) {
            if (model.temp_info.GetType() != typeof(itp_temp_info) || model.temp_info == null) {
                throw new Exception("mismatch of info types");
            }

            var document = _template.create_template((itp_temp_info)model.temp_info);
            if (document == null) {
                throw new Exception("template creatin operation failed");
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

        public Idata_info read_temp_file(template_binding_model model) {

            Application excel = new();
            var workbooks = excel.Workbooks;
            Workbook wb = workbooks.Open(model.file_path);
            Worksheet ws;

            var info = new itp_temp_info();
            int sheet_id = 1;
            ws = wb.Worksheets[sheet_id];

            // титульный лист
            Microsoft.Office.Interop.Excel.Range cells = ws.Range["C2:C10"];
            string[] values = new string[cells.Count + 1];
            for (int i = 0; i < cells.Count + 1; i++) {
                values[i] = cells[i].Value;
            }
            info.user_info = new();
            info.user_info.faculty_name = values[1];
            info.user_info.department_name = values[2];
            info.user_info.fio = $"{values[3]} {values[4]} {values[5]}";
            info.user_info.position = values[6];
            info.user_info.year_of_birth = values[7];
            info.user_info.academic_degree = values[8];
            info.user_info.academic_title = values[9];
            info.user_info.year_of_award_at = ws.Range["F9"].Value;
            info.user_info.year_of_award_ad = ws.Range["F10"].Value;
            sheet_id++;

            // учебная работа
            ws = wb.Worksheets[sheet_id];
            int indexRow = 5;
            string row = ws.Range[$"A5"].Value;
            while (row != "Итого за семестр") {
                info.disciplines_A.Add(row);
                indexRow++; row = ws.Range[$"A{indexRow}"].Value;
            }
            indexRow += 4;
            row = ws.Range[$"A{indexRow}"].Value;
            while (row != "Итого за семестр") {
                info.disciplines_B.Add(row);
                indexRow++; row = ws.Range[$"A{indexRow}"].Value;
            }
            sheet_id++;

            // 3 - 5 листы
            List<List<string>> str = new();
            for (; sheet_id < 6; sheet_id++) {
                ws = wb.Worksheets[sheet_id];

                List<string> tmpStr = new();
                indexRow = 9; row = ws.Range[$"B{indexRow}"].Value;
                while (row != "Итого часов по данному разделу") {
                    row = ws.Range[$"B{indexRow}"].Value;
                    if (row == "Всего часов") {
                        str.Add(tmpStr); tmpStr = new();
                        row = ws.Range[$"A{indexRow + 1}"].Value;
                        indexRow += 3;
                        continue;
                    }
                    tmpStr.Add(row);
                    indexRow++; row = ws.Range[$"B{indexRow}"].Value;
                }
            }

            info.workTypes_21 = str[0];
            info.workTypes_22 = str[1];
            info.workTypes_23 = str[2];
            info.workTypes_24 = str[3];

            info.workTypes_31 = str[4];
            info.workTypes_32 = str[5];

            info.workTypes_41 = str[6];
            info.workTypes_42 = str[7];

            wb.Close();
            workbooks.Close();
            excel.Quit();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            return info;
        }
    }
}
