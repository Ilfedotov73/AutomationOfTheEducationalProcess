using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using worker.office_package.helper_enums;
using worker.office_package.helper_models;
using worker.office_package.helper_models.info_models;

namespace worker.office_package.documents_description {
    public class st_document_to_xlsx {

        private readonly Icreate_xlsx_file _office;

        public st_document_to_xlsx(Icreate_xlsx_file offuce) {
            _office = offuce;
        }

        public byte[]? create_document(st_info info) {
            _office.create_xlsx(["Ведомость"]);


            _office.setWorksheet("Ведомость");
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 1,
                text = info.title,
                style_info = xlsxStyleInfoType.Title
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "A1",
                cell_to_name = "H1"
            });

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 2,
                text = "УлГТУ",
                style_info = xlsxStyleInfoType.Text,
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 3,
                text = "Факультет:",
                style_info = xlsxStyleInfoType.Text
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "B",
                row_index = 3,
                text = info.group_info.faculty_name,
                style_info = xlsxStyleInfoType.Text
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "C",
                row_index = 3,
                text = "Курс:",
                style_info = xlsxStyleInfoType.Text
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "D",
                row_index = 3,
                text = info.group_info.course_num.ToString(),
                style_info = xlsxStyleInfoType.Text
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "E",
                row_index = 3,
                text = "Семестр:",
                style_info = xlsxStyleInfoType.Text
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "F",
                row_index = 3,
                text = info.group_info.semester_num.ToString(),
                style_info = xlsxStyleInfoType.Text
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "G",
                row_index = 3,
                text = "Группа:",
                style_info = xlsxStyleInfoType.Text
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "H",
                row_index = 3,
                text = info.group_info.group,
                style_info = xlsxStyleInfoType.Text
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 5,
                text = "Предмет:",
                style_info = xlsxStyleInfoType.Text
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "B",
                row_index = 5,
                text = info.subject,
                style_info = xlsxStyleInfoType.Text
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "C",
                row_index = 5,
                text = "Направление",
                style_info = xlsxStyleInfoType.Text
            });

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "D",
                row_index = 5,
                text = info.group_info.direction_name,
                style_info = xlsxStyleInfoType.Text
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "E",
                row_index = 5,
                text = "Тип испытания:",
                style_info = xlsxStyleInfoType.Text
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "F",
                row_index = 5,
                text = info.test.ToString(),
                style_info = xlsxStyleInfoType.Text,
            });

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 6,
                text = "Экзаменатор",
                style_info = xlsxStyleInfoType.Text
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "B",
                row_index = 6,
                text = info.examiner,
                style_info = xlsxStyleInfoType.Text
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "C",
                row_index = 6,
                text = "Дата:",
                style_info = xlsxStyleInfoType.Text
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "D",
                row_index = 6,
                text = info.date.ToString(),
                style_info = xlsxStyleInfoType.Text
            });

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "E",
                row_index = 6,
                text = "Общее количество часов:",
                style_info = xlsxStyleInfoType.Text
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "F",
                row_index = 6,
                text = info.totalHoursNum.ToString(),
                style_info = xlsxStyleInfoType.Text
            });
            
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 8,
                text = "№",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "B",
                row_index = 8,
                text = "№ зач. кн.",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "C",
                row_index = 8,
                text = "ФИО",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "D",
                row_index = 8,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "E",
                row_index = 8,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "C8",
                cell_to_name = "E8"
            });

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "F",
                row_index = 8,
                text = "Оценка",
                style_info = xlsxStyleInfoType.TextWithBorder
            });

            int r = 9;
            for (int i = 0; i < info.group_info.students.Count; i++, r++) {
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "A",
                    row_index = (uint)r,
                    text = $"{i + 1}",
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "B",
                    row_index = (uint)r,
                    text = info.group_info.students[i].grade_book_num.ToString(),
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "C",
                    row_index = (uint)r,
                    text = info.group_info.students[i].fio,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "D",
                    row_index = (uint)r,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "E",
                    row_index = (uint)r,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.merge_cells(new xlsxMergeParameters {
                    cell_from_name = $"C{r}",
                    cell_to_name = $"E{r}"
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "F",
                    row_index = (uint)r,
                    text = " ",
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
            }
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = (uint)r,
                text = "Отлично",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "B",
                row_index = (uint)r,
                text = "Хорошо",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "C",
                row_index = (uint)r,
                text = "Удовл.",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "D",
                row_index = (uint)r,
                text = "Не аттест",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "E",
                row_index = (uint)r,
                text = "Не явка",
                style_info = xlsxStyleInfoType.TextWithBorder
            });

            var document = _office.save_xlsx();
            return document;
        }
    }
}
