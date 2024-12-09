using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using worker.office_package.helper_models;

namespace worker.office_package.documents_description {
    public class itp_document_to_xlsx {
        public byte[] create_document(itp_Info info, string template_file_path) {
            MemoryStream _stream = new();

            SpreadsheetDocument template = SpreadsheetDocument.Open(template_file_path, true);
            
            // находим нужный лист
            WorksheetPart? worksheetPart = getWorkSheetPartByName(template, "Лист1");
            if (worksheetPart == null) {
                return Array.Empty<byte>();
            }

            // получаем ячейку
            Cell? cell = GetCell(worksheetPart.Worksheet, "A", 2);
            if (cell == null) {
                return Array.Empty<byte>();
            }

            // добавляем значения
            cell.CellValue = new CellValue($"{info.date}");
            cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);

            // Создаем новый документ
            SpreadsheetDocument newSpreadSheetDocument = SpreadsheetDocument.Create(_stream, SpreadsheetDocumentType.Workbook);
            WorkbookPart workbookPart = newSpreadSheetDocument.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();
            WorksheetPart newWorkSheetPart = workbookPart.AddNewPart<WorksheetPart>();

            // Копируем изменные листы
            newWorkSheetPart = worksheetPart;

            newSpreadSheetDocument.WorkbookPart!.Workbook.Save();
            newSpreadSheetDocument.Dispose();
            return _stream.ToArray();
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
            return row.Elements<Cell>().First(c => string.Compare(c.CellReference.Value, column_name + row_index, true) == 0);
        }

        // Получаем строку в листе по индексу строки
        private Row GetRow(Worksheet worksheet, uint row_index) {
            return worksheet.GetFirstChild<SheetData>().Elements<Row>().First(r => r.RowIndex == row_index);
        }
    }
}
