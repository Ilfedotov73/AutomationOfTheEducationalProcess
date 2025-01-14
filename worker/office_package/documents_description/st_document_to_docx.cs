using worker.office_package.helper_enums;
using worker.office_package.helper_models;
using worker.office_package.helper_models.info_models;

namespace worker.office_package.documents_description {
    public class st_document_to_docx {

        private readonly Icreate_docx_file _office;

        public st_document_to_docx(Icreate_docx_file office) { 
            _office = office; 
        }

        public byte[]? create_document(st_info info) {

            _office.create_docx();
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { (info.title, new docxTextProperties { bold = true, size = "28" }) },
                text_properties = new docxTextProperties {
                    size = "28",
                    justification_type = wordJustificationType.Center
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("УлГТУ", new docxTextProperties { bold = false, size = "20" }) },
                text_properties = new docxTextProperties {
                    size = "20",
                    justification_type = wordJustificationType.Left
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { 
                    ($"Факультет: {info.group_info.faculty_name}\t\tКурс: {info.group_info.course_num}\tСеместр: " +
                    $"{info.group_info.semester_num}\tГруппа: {info.group_info.group}", 
                new docxTextProperties { bold = false, size = "20" }) },
                text_properties = new docxTextProperties {
                    size = "20",
                    justification_type = wordJustificationType.Left
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {
                    ($"{info.group_info.direction_name}",
                new docxTextProperties { bold = false, size = "20" }) },
                text_properties = new docxTextProperties {
                    size = "20",
                    justification_type = wordJustificationType.Left
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {
                    ($"Предмет: \t\tТип испытания: {info.test}",
                new docxTextProperties { bold = false, size = "20" }) },
                text_properties = new docxTextProperties {
                    size = "20",
                    justification_type = wordJustificationType.Left
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {
                    ($"Экзаменатор: {info.examiner}\tДата: {info.date}",
                new docxTextProperties { bold = false, size = "20" }) },
                text_properties = new docxTextProperties {
                    size = "20",
                    justification_type = wordJustificationType.Left
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {
                    ($"Общее количество часов: {info.totalHoursNum}",
                new docxTextProperties { bold = false, size = "20" }) },
                text_properties = new docxTextProperties {
                    size = "20",
                    justification_type = wordJustificationType.Left
                }
            });
            List<string[]> data = new();
            data.Add(["№", "№ зач.кн", "ФИО", "Оценка"]);
            for (int i = 0; i < info.group_info.students.Count; i++) {
                data.Add([$"{i + 1}", $"{info.group_info.students[i].grade_book_num}",
                            $"{info.group_info.students[i].fio}", "\t"]);
            }
            _office.create_table(data, null);
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {
                    ("",
                new docxTextProperties { bold = false, size = "20" }) },
                text_properties = new docxTextProperties {
                    size = "20",
                    justification_type = wordJustificationType.Left
                }
            });
            _office.create_table(data, null);
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {
                    ("",
                new docxTextProperties { bold = false, size = "20" }) },
                text_properties = new docxTextProperties {
                    size = "20",
                    justification_type = wordJustificationType.Left
                }
            });
            _office.create_table(data, null);
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {
                    ($"Отл _________    Хор ________ \tУдовл ________   Не аттест ________   Не явка __________",
                new docxTextProperties { bold = false, size = "20" }) },
                text_properties = new docxTextProperties {
                    size = "20",
                    justification_type = wordJustificationType.Left
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {
                    ($"Отл _________    Хор ________ \tУдовл ________   Не аттест ________   Не явка __________",
                new docxTextProperties { bold = false, size = "20" }) },
                text_properties = new docxTextProperties {
                    size = "20",
                    justification_type = wordJustificationType.Left
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {
                    ($"Подпись декана___________ / ___________ /   \r\n",
                new docxTextProperties { bold = false, size = "20" }) },
                text_properties = new docxTextProperties {
                    size = "20",
                    justification_type = wordJustificationType.Left
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {
                    ($"Подпись Экзаменатора__________ / ___________ /   \r\n",
                new docxTextProperties { bold = false, size = "20" }) },
                text_properties = new docxTextProperties {
                    size = "20",
                    justification_type = wordJustificationType.Left
                }
            });

            var document = _office.save_docx();
            return document;
        }
    }
}
