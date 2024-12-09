using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                _ => JustificationValues.Left,
            };
        }

        // Настройка страницы
        private static SectionProperties CreateSectionProperties() { 
            var properties = new SectionProperties();

            var pageSize = new PageSize {
                Orient = PageOrientationValues.Portrait,
            };
            properties.AppendChild(pageSize);
            return properties;
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

        byte[]? Icreate_docx_file.save_docx() {
            if (_docBody == null || _wordDocument == null) {
                return null;
            }
            _docBody.AppendChild(CreateSectionProperties());
            _wordDocument.MainDocumentPart!.Document.Save();
            _wordDocument.Dispose();
            return _stream.ToArray();
        }
    }
}
