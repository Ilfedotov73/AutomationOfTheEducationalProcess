using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using worker.office_package.helper_models.info_models;

namespace worker.office_package.documents_description {
    public class itp_document_to_xlsx {

        string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        uint row_index = 5;
        public byte[] create_document(itp_info info, string template_file_path, string temporary_path) {

            byte[] templateData = File.ReadAllBytes(template_file_path);
            File.WriteAllBytes(temporary_path, templateData);

            SpreadsheetDocument newSpreadSheetDocument = SpreadsheetDocument.Open(temporary_path, true);
            var values = newSpreadSheetDocument.WorkbookPart.SharedStringTablePart.SharedStringTable
                        .Elements<SharedStringItem>().ToArray();


            WorksheetPart? _worksheetPart = setSheet(newSpreadSheetDocument, "Учебная работа");
            // 1.1
            SearchActualCell_1(values, _worksheetPart,info.data_11);
            // 1.2
            SearchActualCell_1(values, _worksheetPart,info.data_12);

            
            _worksheetPart = setSheet(newSpreadSheetDocument, "Учебно-Методическая работа");
            row_index = 9;
            // 2.1
            SearchActuallCell_2(values, _worksheetPart, info.data_21);
            // 2.2
            SearchActuallCell_2(values, _worksheetPart, info.data_22);
            // 2.3
            SearchActuallCell_2(values, _worksheetPart, info.data_23);
            // 2.4
            SearchActuallCell_2(values, _worksheetPart, info.data_24);

            _worksheetPart = setSheet(newSpreadSheetDocument, "Научно-исследовательская работа");
            row_index = 9;

            // 3.1
            SearchActuallCell_2(values, _worksheetPart, info.data_31);
            // 3.2
            SearchActuallCell_2(values, _worksheetPart, info.data_32);

            _worksheetPart = setSheet(newSpreadSheetDocument, "Орг.-методическая работа");
            row_index = 9;

            // 4.1
            SearchActuallCell_2(values, _worksheetPart, info.data_41);
            // 4.2
            SearchActuallCell_2(values, _worksheetPart, info.data_42);

            _worksheetPart = setSheet(newSpreadSheetDocument, "Сводная таблица");

            row_index = 5;
            Cell cell = GetCell(_worksheetPart.Worksheet, "A");
            int index = 0;
            while (values[int.Parse(cell.CellValue.Text)].InnerText != "Всего" && index < info.data_51.Length) {
                Cell _cell = GetCell(_worksheetPart.Worksheet, "H");

                _cell.CellValue = new CellValue($"{info.data_51[index++]}");
                _cell.DataType = new EnumValue<CellValues>(CellValues.Number);
                row_index++; cell = GetCell(_worksheetPart.Worksheet, "A");
            }

            newSpreadSheetDocument.WorkbookPart!.Workbook.Save();
            newSpreadSheetDocument.Dispose();
            
            // получение значений из временного файла с последующим его удалением
            byte[] file_data = File.ReadAllBytes(temporary_path);
            File.Exists(temporary_path);
            File.Delete(temporary_path);

            return file_data;
        }

        private WorksheetPart? getWorkSheetPartByName(SpreadsheetDocument document, string sheet_name) {
            var sheets = document.WorkbookPart?.Workbook?.GetFirstChild<Sheets>()?.Elements<Sheet>()
                        .Where(s => s.Name == sheet_name);
            if (sheets == null || sheets.Count() == 0) {
                // Листы или указанные рабочий лист не существует
                return null;
            }
            string relationshipId = sheets.First()?.Id?.Value ?? string.Empty;
            if (string.IsNullOrEmpty(relationshipId)) {
                return null;
            }
            WorksheetPart part = (WorksheetPart)document.WorkbookPart.GetPartById(relationshipId);
            return part;
        }

        // Получаем ячейку учитывая лист, столбец и индекс строки
        private Cell GetCell(Worksheet worksheet, string column_name) {
            Row row = GetRow(worksheet);
            return row.Elements<Cell>().First(c => string.Compare(c.CellReference.Value, column_name + row_index, true) == 0);
        }

        // Получаем строку в листе по индексу строки
        private Row GetRow(Worksheet worksheet) {
            while(worksheet.GetFirstChild<SheetData>().Elements<Row>().FirstOrDefault(r => r.RowIndex == row_index) == null) {
                row_index++;
            }
            return worksheet.GetFirstChild<SheetData>().Elements<Row>().First(r => r.RowIndex == row_index);
            
        }

        private WorksheetPart setSheet(SpreadsheetDocument spreadsheetDocument,string sheetName) {
            WorksheetPart? _worksheetPart = getWorkSheetPartByName(spreadsheetDocument, sheetName);
            if (_worksheetPart == null) {
                throw new Exception("Страница не найдена");
            }
            return _worksheetPart;
        }

        private void SearchActualCell_1(SharedStringItem[] values, WorksheetPart _worksheetPart, int[] data) {
            Cell cell = GetCell(_worksheetPart.Worksheet, "A");
            while (values[int.Parse(cell.CellValue.Text)].InnerText != "Фактически выполнено") {
                row_index++; cell = GetCell(_worksheetPart.Worksheet, "A");
            }

            for (int j = 0, c = 3; j < data.Length; j++, c++) {
                cell = GetCell(_worksheetPart.Worksheet, $"{str[c]}");
                cell.CellValue = new CellValue($"{data[j]}");
                cell.DataType = new EnumValue<CellValues>(CellValues.Number);
            }
            row_index++;
        }

        private void SearchActuallCell_2(SharedStringItem[] values, WorksheetPart _worksheetPart, List<(int, int)> data) {
            Cell cell = GetCell(_worksheetPart.Worksheet, "B");
            int i = 0;
            while (values[int.Parse(cell.CellValue.Text)].InnerText != "Всего часов" && i < data.Count) {
                Cell? cell1 = GetCell(_worksheetPart.Worksheet, "G");
                Cell? cell2 = GetCell(_worksheetPart.Worksheet, "I");

                cell1.CellValue = new CellValue($"{data[i].Item1}");
                cell1.DataType = new EnumValue<CellValues>(CellValues.Number);
                cell2.CellValue = new CellValue($"{data[i].Item2}");
                cell2.DataType = new EnumValue<CellValues>(CellValues.Number);

                row_index++; i++; 
                cell = GetCell(_worksheetPart.Worksheet, "B");
            }
            row_index += 2;
        }
    }
}
