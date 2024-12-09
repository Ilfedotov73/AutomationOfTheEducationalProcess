using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using worker.office_package.helper_enums;
using worker.office_package.helper_models;

namespace worker.office_package.implements {
    public class create_to_xlsx : Icreate_xlsx_file {
        private SpreadsheetDocument? _spreadsheet;
        private SharedStringTablePart? _sharedStringTablePart;
        private Worksheet? _worksheet;
        private MemoryStream _stream = new();

        // Настройка стилей для файла
        private static void create_style(WorkbookPart workbookPart) {
            var sp = workbookPart.AddNewPart<WorkbookStylesPart>();
            sp.Stylesheet = new Stylesheet();

            var fonts = new Fonts() { Count = 2U, KnownFonts = true };

            var fontUsual = new Font();
            fontUsual.Append(new FontSize() { Val = 12D });
            fontUsual.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color() { Theme = 1U });
            fontUsual.Append(new FontName() { Val = "Times New Roman" });
            fontUsual.Append(new FontFamilyNumbering() { Val = 2 });
            fontUsual.Append(new FontScheme() { Val = FontSchemeValues.Minor });

            var fontTitle = new Font();
            fontTitle.Append(new Bold());
            fontTitle.Append(new FontSize() { Val = 14D });
            fontTitle.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color() { Theme = 1U });
            fontTitle.Append(new FontName() { Val = "Times New Roman" });
            fontTitle.Append(new FontFamilyNumbering() { Val = 2 });
            fontTitle.Append(new FontScheme() { Val = FontSchemeValues.Minor });

    
            fonts.Append(fontUsual);
            fonts.Append(fontTitle);

            var fills = new Fills() { Count = 2U };

            var fill1 = new Fill();
            fill1.Append(new PatternFill() { PatternType = PatternValues.None });

            var fill2 = new Fill();
            fill2.Append(new PatternFill() { PatternType = PatternValues.Gray125 });


            fills.Append(fill1);
            fills.Append(fill2);


            var borders = new Borders() { Count = 2U };

            var border_no_border = new Border();
            border_no_border.Append(new LeftBorder());
            border_no_border.Append(new RightBorder());
            border_no_border.Append(new TopBorder());
            border_no_border.Append(new BottomBorder());
            border_no_border.Append(new DiagonalBorder());

            var border_thin = new Border();

            var left_border = new LeftBorder() { Style = BorderStyleValues.Thin };
            left_border.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color() { Indexed = 64U });

            var right_border = new RightBorder() { Style = BorderStyleValues.Thin };
            right_border.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color() { Indexed = 64U });

            var top_border = new TopBorder() { Style = BorderStyleValues.Thin };
            top_border.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color() { Indexed = 64U });

            var bottom_border = new BottomBorder() { Style = BorderStyleValues.Thin };
            bottom_border.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color { Indexed = 64U });

            border_thin.Append(left_border);
            border_thin.Append(right_border);
            border_thin.Append(top_border);
            border_thin.Append(bottom_border);
            border_thin.Append(new DiagonalBorder());


            borders.Append(border_no_border);
            borders.Append(border_thin);


            var cell_style_formats = new CellStyleFormats() { Count = 1U };
            var cell_format_style = new CellFormat() { NumberFormatId = 0U, FontId = 0U, FillId = 0U, BorderId = 0U };

            cell_style_formats.Append(cell_format_style);

            var cell_formats = new CellFormats() { Count = 3U };
            var cell_format_font = new CellFormat() { 
                NumberFormatId = 0U,                
                FontId = 0U, FillId = 0U, 
                BorderId = 0U, 
                FormatId = 0U,                                 
                ApplyFont = true 
            };

            var cell_format_font_and_border = new CellFormat() { 
                NumberFormatId = 0U, 
                FontId = 0U, 
                FillId = 0U, 
                BorderId = 1U, 
                FormatId = 0U,
                ApplyFont = true, ApplyBorder = true 
            };

            var cell_format_title = new CellFormat() {
                NumberFormatId = 0U,
                FontId = 1U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                Alignment = new Alignment() {
                    Vertical = VerticalAlignmentValues.Center,
                    WrapText = true,
                    Horizontal = HorizontalAlignmentValues.Center
                },
                ApplyFont = true
            };


            cell_formats.Append(cell_format_font);
            cell_formats.Append(cell_format_font_and_border);
            cell_formats.Append(cell_format_title);

            var cell_styles = new CellStyles() { Count = 1U };
            cell_styles.Append(new CellStyle() { Name = "Normal", FormatId = 0U, BuiltinId = 0U });

            var differential_formats = new DocumentFormat.OpenXml.Office2013.Excel.DifferentialFormats() { Count = 0U };
            var table_styles = new TableStyles() { Count = 0U, DefaultTableStyle = "TableStyleMedium2", DefaultPivotStyle = "PivotStyleLight16" };
            
            var stylesheet_extension_list = new StylesheetExtensionList();

            var stylesheet_extension1 = new StylesheetExtension() { Uri = "{EB79DEF2-80B8-43e5-95BD-54CBDDF9020C}" };
            stylesheet_extension1.AddNamespaceDeclaration("x14", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            stylesheet_extension1.Append(new SlicerStyles() { DefaultSlicerStyle = "SlicerStyleLight1" });

            var stylesheet_extension2 = new StylesheetExtension() { Uri = "{9260A510-F301-46a8-8635-F512D64BE5F5}" };
            stylesheet_extension2.AddNamespaceDeclaration("x15", "http://schemas.microsoft.com/office/spreadsheetml/2010/11/main");
            stylesheet_extension2.Append(new SlicerStyles() { DefaultSlicerStyle = "TimeSlicerStyleLight1" });


            stylesheet_extension_list.Append(stylesheet_extension1);
            stylesheet_extension_list.Append(stylesheet_extension2);


            sp.Stylesheet.Append(fonts);
            sp.Stylesheet.Append(fills);
            sp.Stylesheet.Append(borders);
            sp.Stylesheet.Append(cell_style_formats);
            sp.Stylesheet.Append(cell_formats);
            sp.Stylesheet.Append(cell_styles);
            sp.Stylesheet.Append(differential_formats);
            sp.Stylesheet.Append(table_styles);
            sp.Stylesheet.Append(stylesheet_extension_list);
        }

        // Получение номера стиля из типа
        private static uint get_style_value(xlsxStyleInfoType xlsxStyle) {
            return xlsxStyle switch {
                xlsxStyleInfoType.Title => 2U,
                xlsxStyleInfoType.TextWithBorder => 1U,
                xlsxStyleInfoType.Text => 0U,
                _ => 0U,
            };
        }

        void Icreate_xlsx_file.create_xlsx() {
            _spreadsheet = SpreadsheetDocument.Create(_stream, SpreadsheetDocumentType.Workbook);
            // Создаеам книгу в (в нех хранятся листы)
            var workbookpart = _spreadsheet.AddWorkbookPart();
            create_style(workbookpart);

            // Получем/создаем хранилище текстов для книги
            _sharedStringTablePart = _spreadsheet.WorkbookPart!.GetPartsOfType<SharedStringTablePart>().Any()
                ? _spreadsheet.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First()
                : _spreadsheet.WorkbookPart.AddNewPart<SharedStringTablePart>();

            if (_sharedStringTablePart.SharedStringTable == null) {
                _sharedStringTablePart.SharedStringTable = new SharedStringTable();
            }

            // Создаем лист в книгу
            var worksheet_part = workbookpart.AddNewPart<WorksheetPart>();
            worksheet_part.Worksheet = new Worksheet(new SheetData());

            // Добавляем лист в книгку
            var sheets = _spreadsheet.WorkbookPart.Workbook.AppendChild(new Sheets());
            var sheet = new Sheet() {
                Id = _spreadsheet.WorkbookPart.GetIdOfPart(worksheet_part),
                SheetId = 1,
                Name = "Лист"
            };

            sheets.Append(sheet);
            _worksheet = worksheet_part.Worksheet;
        }

        void Icreate_xlsx_file.insert_cell_in_worksheet(xlsxCellParameters xlsxCellParameters) {
            if (_worksheet == null || _sharedStringTablePart == null) {
                return;
            }
            var sheet_data = _worksheet.GetFirstChild<SheetData>();
            if (sheet_data == null) {
                return;
            }

            // Ищем строку, либо добавляем ее
            Row row;
            if (sheet_data.Elements<Row>().Where(x => x.RowIndex! == xlsxCellParameters.row_index).Any()) {
                row = sheet_data.Elements<Row>().Where(x => x.RowIndex! == xlsxCellParameters.row_index).First();
            }
            else {
                row = new Row() { RowIndex = xlsxCellParameters.row_index };
                sheet_data.Append(row);
            }

            // Ищем нужную ячейку
            Cell cell;
            if (row.Elements<Cell>().Where(x => x.CellReference!.Value == xlsxCellParameters.cell_reference).Any()) {
                cell = row.Elements<Cell>().Where(x => x.CellReference!.Value == xlsxCellParameters.cell_reference).First();
            }
            else {
                // Все ячейки должны быть последовательно друг за другом расположены
                // нужно определить, после какой вставлять
                Cell? ref_cell = null;
                foreach (Cell rowCell in row.Elements<Cell>()) {
                    if (string.Compare(rowCell.CellReference!.Value, xlsxCellParameters.cell_reference, true) > 0) {
                        ref_cell = rowCell;
                        break;
                    }
                }

                var new_cell = new Cell() { CellReference = xlsxCellParameters.cell_reference };
                row.InsertBefore(new_cell, ref_cell);

                cell = new_cell;
            }

            // Вставляем новый текст
            _sharedStringTablePart.SharedStringTable.AppendChild(new SharedStringItem(new Text(xlsxCellParameters.text)));
            _sharedStringTablePart.SharedStringTable.Save();
            
            cell.CellValue = new CellValue((_sharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().Count() - 1).ToString());
            cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
            cell.StyleIndex = get_style_value(xlsxCellParameters.style_info);
        }

        void Icreate_xlsx_file.merge_cells(xlsxMergeParameters xlsxMergeParameters) {
            if (_worksheet == null) {
                return;
            }
            MergeCells mergeCells;
            if (_worksheet.Elements<MergeCells>().Any()) {
                mergeCells = _worksheet.Elements<MergeCells>().First();
            }
            else {
                mergeCells = new MergeCells();
                if (_worksheet.Elements<CustomSheetView>().Any()) {
                    _worksheet.InsertAfter(mergeCells, _worksheet.Elements<CustomSheetView>().First());
                }
                else {
                    _worksheet.InsertAfter(mergeCells, _worksheet.Elements<SheetData>().First());
                }
            }

            var mergeCell = new MergeCell() {
                Reference = new StringValue(xlsxMergeParameters.merge)
            };
            mergeCells.Append(mergeCell);
        }

        byte[]? Icreate_xlsx_file.save_xlsx() {
            if (_spreadsheet == null) {
                return null;
            }
            _spreadsheet.WorkbookPart!.Workbook.Save();
            _spreadsheet.Dispose();
            return _stream.ToArray();
        }
    }
}
