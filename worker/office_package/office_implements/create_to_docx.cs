using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Linq;
using worker.office_package.helper_enums;
using worker.office_package.helper_models;

namespace worker.office_package.office_implements {
    public class create_to_docx : Icreate_docx_file {

        private WordprocessingDocument? _wordDocument;
        private Body? _docBody;
        private MemoryStream _stream = new();

        // Получение типа выравнивания
        private static JustificationValues GetJustificationValues(wordJustificationType type) {
            return type switch {
                wordJustificationType.Both => JustificationValues.Both,
                wordJustificationType.Center => JustificationValues.Center,
                wordJustificationType.Right => JustificationValues.Right,
                wordJustificationType.Left => JustificationValues.Left,
                _ => JustificationValues.Left,
            };
        }

        // Задание форматирования для абзаца
        private static ParagraphProperties? CreateParagraphProperties(docxTextProperties? paragraphProperties) {
            if (paragraphProperties == null) {
                return null;
            }
            var properties = new ParagraphProperties();
            properties.AppendChild(new Justification() {
                Val = GetJustificationValues(paragraphProperties.justification_type)
            });

            properties.AppendChild(new SpacingBetweenLines {
                LineRule = LineSpacingRuleValues.Auto
            });

            properties.AppendChild(new SpacingBetweenLines {
                LineRule = LineSpacingRuleValues.Auto
            });

            properties.AppendChild(new Indentation());
            var paragraphMakeRunProperties = new ParagraphMarkRunProperties();
            if (!string.IsNullOrEmpty(paragraphProperties.size)) {
                paragraphMakeRunProperties.AppendChild(new FontSize { Val = paragraphProperties.size });
            }
            properties.AppendChild(paragraphMakeRunProperties);
            return properties;
        }

        private static TableProperties createTableProp() {
            TableProperties tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 5 },
                    new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 5 },
                    new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 5 },
                    new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 5 },
                    new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 5 },
                    new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 5 }
                )
            );
            return tblProp;
        }

        void Icreate_docx_file.create_docx() {
            _wordDocument = WordprocessingDocument.Create(_stream, WordprocessingDocumentType.Document);
            MainDocumentPart mainPart = _wordDocument.AddMainDocumentPart();
            mainPart.Document = new Document();
            _docBody = mainPart.Document.AppendChild(new Body());
        }

        void Icreate_docx_file.create_paragraph(docxParagraph docxParagraph) {
            if (_docBody == null || docxParagraph == null) {
                return;
            }
            var docParagraph = new Paragraph();

            docParagraph.AppendChild(CreateParagraphProperties(docxParagraph.text_properties));

            foreach (var run in docxParagraph.texts) {
                var docRun = new Run();

                var properties = new RunProperties();
                properties.AppendChild(new FontSize { Val = run.Item2.size });
                if (run.Item2.bold) {
                    properties.AppendChild(new Bold());
                }
                docRun.AppendChild(properties);

                docRun.AppendChild(new Text { Text = run.Item1, Space = SpaceProcessingModeValues.Preserve });

                docParagraph.AppendChild(docRun);
            }

            _docBody.AppendChild(docParagraph);
        }

        void Icreate_docx_file.create_table(List<string[]> texts, int? indexRow) {
            Table table = new Table();
            table.AppendChild(createTableProp());

            for (int i = 0; i < texts.Count; i++) {
                TableRow tableRow = new TableRow();

                for (int j = 0; j < texts[i].Length; j++) {
                    // настройка стиля ячейки таблицы
                    var tcProp = new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" });
                    var tcVa = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };
                    tcProp.Append(tcVa);


                    TableCell cell = new TableCell();
                    cell.Append(tcProp);

                    // настройка стиля параграфа
                    var justification = new Justification() { Val = JustificationValues.Center, };
                    var prProp = new ParagraphProperties();
                    prProp.SpacingBetweenLines = new SpacingBetweenLines() { After = "0" };
                    prProp.Append(justification);

                    // создаем параграф, добавляем текст
                    var paragraph = new Paragraph(prProp);
                    paragraph.Append(new Run(new Text(texts[i][j])));

                    cell.Append(paragraph);
                    tableRow.AppendChild(cell);

                    if (texts[i][j] == "M") {
                        var tableCellOneProp = new TableCellProperties(new HorizontalMerge {
                            Val = MergedCellValues.Restart
                        });
                        var tableCellTwoProp = new TableCellProperties(new HorizontalMerge {
                            Val = MergedCellValues.Continue
                        });

                        var cell1 = tableRow.Elements<TableCell>().ElementAt(j - 1);
                        var cell2 = tableRow.Elements<TableCell>().ElementAt(j);

                        cell1.Append(tableCellOneProp);
                        cell2.Append(tableCellTwoProp);
                    }
                }
                table.AppendChild(tableRow);

            }
            if (indexRow != null) {
                int lastRow = (int)indexRow;
                for (int c = 0; c < texts[lastRow].Length; c++) {
                    if (texts[lastRow][c] == "3HM") {
                        var tableCellOneProp = new TableCellProperties(new VerticalMerge {
                            Val = MergedCellValues.Continue
                        });
                        var tableCellTwoProp = new TableCellProperties(new VerticalMerge {
                            Val = MergedCellValues.Continue
                        });
                        var tableCellThreeProp = new TableCellProperties(new VerticalMerge {
                            Val = MergedCellValues.Restart
                        });


                        var cell1 = table.Elements<TableRow>().ElementAt(lastRow).Elements<TableCell>().ElementAt(c);
                        var cell2 = table.Elements<TableRow>().ElementAt(lastRow - 1).Elements<TableCell>().ElementAt(c);
                        var cell3 = table.Elements<TableRow>().ElementAt(lastRow - 2).Elements<TableCell>().ElementAt(c);

                        cell1.Append(tableCellOneProp);
                        cell2.Append(tableCellTwoProp);
                        cell3.Append(tableCellThreeProp);
                    }
                }
            }
            _wordDocument.MainDocumentPart.Document.Body.Append(table);
        }

        void Icreate_docx_file.page_breaks() {
            var pr = new Paragraph(new Run(new Break() {
                Type = BreakValues.Page
            }));
            _wordDocument.MainDocumentPart.Document.Body.Append(pr);
        }

        byte[]? Icreate_docx_file.save_docx() {
            if (_docBody == null || _wordDocument == null) {
                return null;
            }
            _wordDocument.MainDocumentPart!.Document.Save();
            _wordDocument.Dispose();
            return _stream.ToArray();
        }
    }
}
