using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using worker.office_package.helper_models;

namespace worker.office_package.documents_description {
    public class itp_document_to_xlsx {
        public byte[] create_document(itp_Info info, string template_file_path, string temporary_path) {

            byte[] templateData = File.ReadAllBytes(template_file_path);

            temporary_path += ".xlsx";
            File.WriteAllBytes(temporary_path, templateData);

            SpreadsheetDocument newSpreadSheetDocument = SpreadsheetDocument.Open(temporary_path, true);

            // находим нужный лист
            WorksheetPart? _worksheetPart = getWorkSheetPartByName(newSpreadSheetDocument, "Лист");
            if (_worksheetPart == null) {
                return Array.Empty<byte>();
            }

            // получаем ячейку
            Cell? cell = GetCell(_worksheetPart.Worksheet, "C", 2);
            if (cell == null) {
                return Array.Empty<byte>();
            }

            // добавляем значения
            cell.CellValue = new CellValue($"{info.date}");
            cell.DataType = new EnumValue<CellValues>(CellValues.String);
            Row row = GetRow(_worksheetPart.Worksheet, 2);
            row.Append(cell);
                
            newSpreadSheetDocument.WorkbookPart!.Workbook.Save();
            newSpreadSheetDocument.Dispose();
            
            // получение значений из временного файла с последующим его удалением
            byte[] file_data = File.ReadAllBytes(temporary_path);
            File.Exists(temporary_path);
            File.Delete(temporary_path);

            return file_data;
        }

        private WorksheetPart? getWorkSheetPartByName(SpreadsheetDocument document, string sheet_name) {
            var sheets = document.WorkbookPart?.Workbook?.GetFirstChild<Sheets>()?.Elements<Sheet>().Where(s => s.Name == sheet_name);
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
        private Cell? GetCell(Worksheet worksheet, string column_name, uint row_index) {
            Row row = GetRow(worksheet, row_index);
            if (row == null) {
                return null;
            }
            if (row.Elements<Cell>().Where(x => x.CellReference!.Value == column_name + row_index).Any()) {
                return row.Elements<Cell>().First(c => string.Compare(c.CellReference.Value, column_name + row_index, true) == 0);
            }
            else {
                return new Cell() { CellReference = column_name + row_index };
            }
        }

        // Получаем строку в листе по индексу строки
        private Row GetRow(Worksheet worksheet, uint row_index) {
            return worksheet.GetFirstChild<SheetData>().Elements<Row>().First(r => r.RowIndex == row_index);
        }
    }
}
