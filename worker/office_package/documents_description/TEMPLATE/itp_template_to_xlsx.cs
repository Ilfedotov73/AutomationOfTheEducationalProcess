using DocumentFormat.OpenXml.EMMA;
using System.Net.WebSockets;
using worker.office_package.helper_enums;
using worker.office_package.helper_models;
using worker.office_package.helper_models.info_models;

namespace worker.office_package.documents_description.TEMPLATE {
    public class itp_template_to_xlsx {
        private readonly Icreate_xlsx_file _office;
        public itp_template_to_xlsx(Icreate_xlsx_file office) {
            _office = office;
        }

        //ад

        public uint sheet_id = 0;
        string[] sheetNameList = [
            "Титульный лист", 
            "Учебная работа",
            "Учебно-Методическая работа",
            "Научно-исследовательская работа",
            "Орг.-методическая работа",
            "Сводная таблица"
        ];
        string[] disciplinesName = [
            "Дисциплина N группы (потока)",
            "Студентов",
            "Лекции",
            "Практ. занятия и семинары",
            "Лабораторные занятия",
            "Курсовое проектирование",
            "Консультации",
            "Зачеты",
            "Экзамены",
            "Диф. Зачеты",
            "Руководство практиками студентов",
            "Расчетно-графические работы",
            "рук-во дипл. проектом (вып. работой)",
            "рецензирование дипл. проектов (вып. работ)",
            "работа в ГИА",
            "Рефераты, РГР",
            "руководство аспирантами",
            "Другие виды работ",
            "всего часов",
            "всего часов фактически"
        ];
        string[] workTypesDir2 = [
            "2.1. Подготовка к лекционным, практическим, семинарским, лабораторным занятиям и другие виды учебно-методической работы (для программ ВО)",
            "2.2. Подготовка электронных обучающих ресурсов (ЭОР) в ЭИОС",
            "2.3. Использование в учебном процессе электронных обучающих ресурсов (ЭОР)",
            "2.4. Написание и переработка учебных учебников, учебных пособий и других учебно-методических материалов"
        ];
        string[] workTypesDir3 = [
            "3.1. Написание и подготовка к изданию монографий, научных статей, докладов, заявок на избретение",
            "3.2. Другие виды научно-исследовательских работ"
        ];

        string[] workTypesDir4 = [
            "4.1. Участие в проведении работы по профессиональной ориентации молодежи при поступлении в вуз, исполнение обязанностей, проведение олимпиад, конференций и другие организационно-методические работы",
            "4.2. Организационно-методическая работа по физической подготовке"
        ];
        string[] workListDir4 = [
            "Перечень опубликованных в учебном году научных и научно-методических работ",
            "Перечень материалов, сданных в учебном году  в печать"
        ];
        string _str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";


        public byte[]? create_template(itp_temp_info info) {
            _office.create_xlsx(sheetNameList);

            sheet_0(info);
            sheet_1(info);
            sheet_2(info);
            sheet_3(info);
            sheet_4(info);
            sheet_5(info);

            var document = _office.save_xlsx();
            return document;
        }

        //-----ТИТУЛЬНЫЙ ЛИСТ-----
        private void sheet_0(itp_temp_info info) {
            _office.setWorksheet(sheetNameList[sheet_id]);
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 1,
                text = info.title,
                style_info = xlsxStyleInfoType.Title,
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "A1",
                cell_to_name = "H1"
            });
            string[] user_info = [
                info.user_info.faculty_name,
                info.user_info.department_name,
                info.user_info._fio[0],
                info.user_info._fio[1],
                info.user_info._fio[2],
                info.user_info.position,
                info.user_info.year_of_birth,
                info.user_info.academic_degree,
                info.user_info.academic_title
            ];
            string[] str = ["Факультет", "Кафедра", "Фамилия", "Имя", "Отчество", "Должность", "Год рождения", "Ученая степень", 
                            "Ученое звание"];
            for (int i = 0; i < str.Length; i++) {
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "A",
                    row_index = (uint)i + 2,
                    text = str[i],
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.merge_cells(new xlsxMergeParameters {
                    cell_from_name = $"A{(uint)i + 2}",
                    cell_to_name = $"B{(uint)i + 2}"
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "B",
                    row_index = (uint)i + 2,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "C",
                    row_index = (uint)i + 2,
                    text = $"{user_info[i]}",
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
            }

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "D",
                row_index = 9,
                text = "Год присуждения",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "D9",
                cell_to_name = "E9"
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "E",
                row_index = 9,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "D",
                row_index = 10,
                text = "Год присуждения",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "D10",
                cell_to_name = "E10"
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "E",
                row_index = 10,
                style_info = xlsxStyleInfoType.TextWithBorder
            });

            user_info = [info.user_info.year_of_award_ad, info.user_info.year_of_award_at];

            for (int i = 9; i < 11; i++) {
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "F",
                    row_index = (uint)i,
                    text = $"{user_info[i - 9]}",
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
            }
            sheet_id++;
        }

        //-----УЧЕБНАЯ РАБОТА-----
        private void sheet_1(itp_temp_info info) {
            _office.setWorksheet(sheetNameList[sheet_id]);
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 1,
                text = "1. Учебная работа",
                style_info = xlsxStyleInfoType.Title
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "A1",
                cell_to_name = "W1"
            });

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 2,
                text = "Утверждено Зав. кафедрой",
                style_info = xlsxStyleInfoType.Text
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "A2",
                cell_to_name = "C2"
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "D",
                row_index = 2,
                style_info = xlsxStyleInfoType.TextWithBorder
            });

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "G",
                row_index = 2,
                text = "1.1 Нагрузка преподавателя по программам высшего образования (ВО)",
                style_info = xlsxStyleInfoType.Text
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "G2",
                cell_to_name = "N2"
            });

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "P",
                row_index = 2,
                text = $"{info.date_from.Year}/{info.date_to.Year} уч. год",
                style_info = xlsxStyleInfoType.Text
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "P2",
                cell_to_name = "R2"
            });

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "T",
                row_index = 2,
                text = $"{info.date}",
                style_info = xlsxStyleInfoType.Text
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "T2",
                cell_to_name = "U2"
            });

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 3,
                text = "а) Осенний семестр",
                style_info = xlsxStyleInfoType.Text
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "A3",
                cell_to_name = "W3"
            });

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 4,
                text = $"{disciplinesName[0]}",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "B",
                row_index = 4,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "C",
                row_index = 4,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "A4",
                cell_to_name = "C4"
            });

            for (int i = 3, j = 1; j < disciplinesName.Length; i++, j++) {
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = $"{_str[i]}",
                    row_index = 4,
                    text = disciplinesName[j],
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
            }

            uint firstRow = 5;
            uint lastRow = firstRow;

            firstRow = addSheet_1_Table(firstRow, lastRow, info.disciplines_A) + 3;
            lastRow = firstRow;

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = firstRow - 1,
                text = "б) Весенний семестр",
                style_info = xlsxStyleInfoType.Text
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"A{firstRow - 1}",
                cell_to_name = $"W{firstRow - 1}"
            });

            lastRow = addSheet_1_Table(firstRow, lastRow, info.disciplines_B) + 1;
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = lastRow,
                text = "Итого за учебный год по ВО",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "B",
                row_index = lastRow,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "C",
                row_index = lastRow,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"A{lastRow}",
                cell_to_name = $"C{lastRow}"
            });
            for (int i = 3; i <= disciplinesName.Length + 1; i++) {
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = $"{_str[i]}",
                    row_index = lastRow,
                    text = "-",
                    style_info = xlsxStyleInfoType.TextWithBorder,
                });
            }

            lastRow++;

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = lastRow,
                text = "Фактически выполнено за учебный год по ВО",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "B",
                row_index = lastRow,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "C",
                row_index = lastRow,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"A{lastRow}",
                cell_to_name = $"C{lastRow}"
            });
            for (int i = 3; i <= disciplinesName.Length + 1; i++) {
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = $"{_str[i]}",
                    row_index = lastRow,
                    text = "-",
                    style_info = xlsxStyleInfoType.TextWithBorder,
                });
            }

            sheet_id++;
        }

        private uint addSheet_1_Table(uint firstRow, uint lastRow, List<string> disciplines) {
            for (int j = 0; j < disciplines.Count; lastRow++, j++) {
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "A",
                    row_index = lastRow,
                    text = disciplines[j],
                    style_info = xlsxStyleInfoType.TextWithBorder,
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "B",
                    row_index = lastRow,
                    style_info = xlsxStyleInfoType.TextWithBorder,
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "C",
                    row_index = lastRow,
                    style_info = xlsxStyleInfoType.TextWithBorder,
                });
                _office.merge_cells(new xlsxMergeParameters {
                    cell_from_name = $"A{lastRow}",
                    cell_to_name = $"C{lastRow}"
                });
            }
            for (uint i = firstRow; i != lastRow; i++) {
                for (int j = 3; j <= disciplinesName.Length + 1; j++) {
                    _office.insert_cell_in_worksheet(new xlsxCellParameters {
                        column_name = $"{_str[j]}",
                        row_index = i,
                        text = "-",
                        style_info = xlsxStyleInfoType.TextWithBorder
                    });
                }
            }
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = lastRow,
                text = "Итого за семестр",
                style_info = xlsxStyleInfoType.TextWithBorder,
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "B",
                row_index = lastRow,
                style_info = xlsxStyleInfoType.TextWithBorder,
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "C",
                row_index = lastRow,
                style_info = xlsxStyleInfoType.TextWithBorder,
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"A{lastRow}",
                cell_to_name = $"C{lastRow}"
            });
            for (int i = 3; i <= disciplinesName.Length + 1; i++) {
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = $"{_str[i]}",
                    row_index = lastRow,
                    text = "-",
                    style_info = xlsxStyleInfoType.TextWithBorder,
                });
            }

            lastRow++;

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = lastRow,
                text = "Фактически выполнено",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "B",
                row_index = lastRow,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "C",
                row_index = lastRow,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"A{lastRow}",
                cell_to_name = $"C{lastRow}"
            });
            for (int i = 3; i <= disciplinesName.Length + 1; i++) {
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = $"{_str[i]}",
                    row_index = lastRow,
                    text = "-",
                    style_info = xlsxStyleInfoType.TextWithBorder,
                });
            }
            return lastRow;
        }

        //-----УЧЕБНО МЕТОДИЧЕСКАЯ РАБОТА-----
        private void sheet_2(itp_temp_info info) {
            _office.setWorksheet(sheetNameList[sheet_id]);
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 1,
                text = "2.Учебно-методическая работа",
                style_info = xlsxStyleInfoType.Title,
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "A1",
                cell_to_name = "I1"
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 2,
                text = "(Учитывается по второй половине рабочего дня)",
                style_info = xlsxStyleInfoType.Title,
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "A2",
                cell_to_name = "I2"
            });
            
            metodicalWorkHead();

            uint firstRow = 7; 
            int workTypesDirIndex = 0;
            firstRow = addSheetWorkTypesTable(firstRow, info.workTypes_21, workTypesDirIndex++, workTypesDir2);
            firstRow = addSheetWorkTypesTable(firstRow, info.workTypes_22, workTypesDirIndex++, workTypesDir2);
            firstRow = addSheetWorkTypesTable(firstRow, info.workTypes_23, workTypesDirIndex++, workTypesDir2);
            firstRow = addSheetWorkTypesTable(firstRow, info.workTypes_24, workTypesDirIndex, workTypesDir2);

            totalHoursWorkTypes(firstRow);

            sheet_id++;
        }

        //------НАУЧНО ИССЛЕДОВАТЕЛЬСКАЯ РАБОТА-----
        private void sheet_3(itp_temp_info info) {
            _office.setWorksheet(sheetNameList[sheet_id]);
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 1,
                text = "3.Учебно-методическая работа",
                style_info = xlsxStyleInfoType.Title
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "A1",
                cell_to_name = "I1"
            });

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 2,
                text = "(учитывается по второй половине рабочего дня)",
                style_info = xlsxStyleInfoType.Title
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "A2",
                cell_to_name = "I2"
            });

            metodicalWorkHead();

            uint firstRow = 7;
            int workTypesDirIndex = 0;
            firstRow = addSheetWorkTypesTable(firstRow, info.workTypes_31, workTypesDirIndex++, workTypesDir3);
            firstRow = addSheetWorkTypesTable(firstRow, info.workTypes_22, workTypesDirIndex++, workTypesDir3);

            totalHoursWorkTypes(firstRow);

            sheet_id++;
        }

        private uint totalHoursWorkTypes(uint firstRow) {
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = firstRow,
                text = "Итого часов по данному разделу",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "B",
                row_index = firstRow,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "C",
                row_index = firstRow,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "D",
                row_index = firstRow,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"A{firstRow}",
                cell_to_name = $"D{firstRow}"
            });
            for (int i = 4; i < 9; i++) {
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = $"{_str[i]}",
                    row_index = firstRow,
                    text = "-",
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
            }
            firstRow += 2;
            return firstRow;
        }

        private uint addSheetWorkTypesTable(uint firstRow, List<string> worksTypes, int workTypesDirIndex, 
                                            string[] workTypesDir) {
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = firstRow,
                text = $"{workTypesDir[workTypesDirIndex]}",
                style_info = xlsxStyleInfoType.Text
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"A{firstRow}",
                cell_to_name = $"I{firstRow + 1}"
            });
            firstRow += 2;
            for (uint i = 0, j = firstRow; i < worksTypes.Count; i++, j++) {
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "A",
                    row_index = j,
                    text = $"{sheet_id}.{workTypesDirIndex + 1}.{i + 1}",
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
            }
            for (int i = 0; i < worksTypes.Count; firstRow++, i++) {
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "B",
                    row_index = firstRow,
                    text = worksTypes[i],
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "C",
                    row_index = firstRow,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "D",
                    row_index = firstRow,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.merge_cells(new xlsxMergeParameters {
                    cell_from_name = $"B{firstRow}",
                    cell_to_name = $"D{firstRow}"
                });
                for (int j = 4; j < 9; j++) {
                    _office.insert_cell_in_worksheet(new xlsxCellParameters {
                        column_name = $"{_str[j]}",
                        row_index = firstRow,
                        text = "-",
                        style_info = xlsxStyleInfoType.TextWithBorder
                    });
                }
            }
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = firstRow,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "B",
                row_index = firstRow,
                text = "Всего часов",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "C",
                row_index = firstRow,
                text = "Всего часов",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "D",
                row_index = firstRow,
                text = "Всего часов",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"B{firstRow}",
                cell_to_name = $"D{firstRow}"
            });
            for (int j = 4; j < 9; j++) {
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = $"{_str[j]}",
                    row_index = firstRow,
                    text = "-",
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
            }

            return firstRow + 1;
        }

        private void metodicalWorkHead() {
            uint firstRow = 3, lastRow = 5;

            for (uint i = 3; i < 6; i++) {
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = $"A",
                    row_index = i,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
            }
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"A3",
                cell_to_name = $"A5"
            });

            for (int i = 1; i < 4; i++) {
                for (uint j = firstRow; j <= lastRow; j++ ) {
                    _office.insert_cell_in_worksheet(new xlsxCellParameters {
                        column_name = $"{_str[i]}",
                        row_index = j,
                        style_info = xlsxStyleInfoType.TextWithBorder
                    });
                }
            }
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"B{firstRow}",
                cell_to_name = $"D{lastRow}"
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "B",
                row_index = firstRow,
                text = "Вид работы",
                style_info = xlsxStyleInfoType.TextWithBorder
            });


            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "E",
                row_index = firstRow,
                text = "Нормы времени",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "E",
                row_index = firstRow + 1,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "E",
                row_index = firstRow + 2,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"E{firstRow}",
                cell_to_name = $"E{lastRow}"
            });

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "F",
                row_index = firstRow,
                text = "Осенний семестр",
                style_info = xlsxStyleInfoType.TextWithBorder,
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "G",
                row_index = firstRow,
                style_info = xlsxStyleInfoType.TextWithBorder,
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"F{firstRow}",
                cell_to_name = $"G{firstRow}"
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "H",
                row_index = firstRow,
                text = "Всенний семестр",
                style_info = xlsxStyleInfoType.TextWithBorder,
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "I",
                row_index = firstRow,
                style_info = xlsxStyleInfoType.TextWithBorder,
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"H{firstRow}",
                cell_to_name = $"I{firstRow}"
            });

            for (int i = 5; i < 9; i++) {
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = $"{_str[i]}",
                    row_index = firstRow + 1,
                    text = "кол-во часов",
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                i++;
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = $"{_str[i]}",
                    row_index = firstRow + 1,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.merge_cells(new xlsxMergeParameters {
                    cell_from_name = $"{_str[i - 1]}{firstRow + 1}",
                    cell_to_name = $"{_str[i]}{firstRow + 1}"
                });
            }
            for (int i = 5; i < 9; i++) {
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = $"{_str[i]}",
                    row_index = lastRow,
                    text = "по плану",
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                i++;
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = $"{_str[i]}",
                    row_index = lastRow,
                    text = "факт. выполнения",
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
            }
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = lastRow + 1,
                text = "1",
                style_info = xlsxStyleInfoType.TextWithBorder,
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "B",
                row_index = lastRow + 1,
                text = "2",
                style_info = xlsxStyleInfoType.TextWithBorder,
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "C",
                row_index = lastRow + 1,
                style_info = xlsxStyleInfoType.TextWithBorder,
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "D",
                row_index = lastRow + 1,
                style_info = xlsxStyleInfoType.TextWithBorder,
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"B{lastRow + 1}",
                cell_to_name = $"D{lastRow + 1}"
            });
            for (int i = 4; i < 9; i++) {
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = $"{_str[i]}",
                    row_index = firstRow + 3,
                    text = $"{i - 1}",
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
            }
        }

        //-----ОРГАНИЗАЦИОННО МЕТОДИЧЕСКАЯ РАБОТА-----
        private void sheet_4(itp_temp_info info) {
            _office.setWorksheet(sheetNameList[sheet_id]);
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 1,
                text = "4.Организационно-методическая работа",
                style_info = xlsxStyleInfoType.Title
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "A1",
                cell_to_name = "L1"
            });

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 2,
                text = "(учитывается по второй половине рабочего дня)",
                style_info = xlsxStyleInfoType.Title
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "A2",
                cell_to_name = "L2"
            });

            metodicalWorkHead();

            uint firstRow = 7;
            int workTypesDirIndex = 0;
            firstRow = addSheetWorkTypesTable(firstRow, info.workTypes_41, workTypesDirIndex++, workTypesDir4);
            firstRow = addSheetWorkTypesTable(firstRow, info.workTypes_42, workTypesDirIndex++, workTypesDir4);

            firstRow = totalHoursWorkTypes(firstRow);

            firstRow = AddListScientificWorks(workListDir4[0], (uint)info.workListRowCount_43, firstRow);
            firstRow = AddListScientificWorks(workListDir4[1], (uint)info.workListRowCount_44, firstRow);

            //todo nir

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = firstRow,
                text = "Участие в хоздоговорной НИР",
                style_info = xlsxStyleInfoType.Title
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"A{firstRow}",
                cell_to_name = $"H{firstRow}"
            });


            firstRow++;

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = firstRow,
                text = "(сверх 6-часов рабочего для преподавателя - для ВО)",
                style_info = xlsxStyleInfoType.Title
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"A{firstRow}",
                cell_to_name = $"H{firstRow}"
            });

            firstRow++;

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = firstRow,
                text = "Содержание выполняемой работы",
                style_info = xlsxStyleInfoType.TextWithBorder
            });

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "B",
                row_index = firstRow,
                style_info = xlsxStyleInfoType.TextWithBorder
            });

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "C",
                row_index = firstRow,
                style_info = xlsxStyleInfoType.TextWithBorder
            });

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "D",
                row_index = firstRow,
                style_info = xlsxStyleInfoType.TextWithBorder
            });

            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"A{firstRow}",
                cell_to_name = $"D{firstRow}"
            });


            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "E",
                row_index = firstRow,
                text = "В качестве кого участвует НИР",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "F",
                row_index = firstRow,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "G",
                row_index = firstRow,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"E{firstRow}",
                cell_to_name = $"G{firstRow}"
            });

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "H",
                row_index = firstRow,
                text = "Планируемый строк выполнения",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "I",
                row_index = firstRow,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "J",
                row_index = firstRow,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"H{firstRow}",
                cell_to_name = $"J{firstRow}"
            });

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "K",
                row_index = firstRow,
                text = "Отметка о фактическом выполнении",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "L",
                row_index = firstRow,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"K{firstRow}",
                cell_to_name = $"L{firstRow}"
            });

            firstRow++;

            for (int i = 0; i < info.nirListRowCount; i++, firstRow++) {
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "A",
                    row_index = firstRow,
                    text = "-",
                    style_info = xlsxStyleInfoType.TextWithBorder
                });

                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "B",
                    row_index = firstRow,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });

                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "C",
                    row_index = firstRow,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });

                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "D",
                    row_index = firstRow,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });

                _office.merge_cells(new xlsxMergeParameters {
                    cell_from_name = $"A{firstRow}",
                    cell_to_name = $"D{firstRow}"
                });


                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "E",
                    row_index = firstRow,
                    text = "-",
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "F",
                    row_index = firstRow,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "G",
                    row_index = firstRow,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.merge_cells(new xlsxMergeParameters {
                    cell_from_name = $"E{firstRow}",
                    cell_to_name = $"G{firstRow}"
                });

                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "H",
                    row_index = firstRow,
                    text = "-",
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "I",
                    row_index = firstRow,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "J",
                    row_index = firstRow,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.merge_cells(new xlsxMergeParameters {
                    cell_from_name = $"H{firstRow}",
                    cell_to_name = $"J{firstRow}"
                });

                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "K",
                    row_index = firstRow,
                    text = "-",
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "L",
                    row_index = firstRow,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.merge_cells(new xlsxMergeParameters {
                    cell_from_name = $"K{firstRow}",
                    cell_to_name = $"L{firstRow}"
                });
            }

            sheet_id++;
        }

        private uint AddListScientificWorks(string Name, uint rowCount, uint firstRow) {
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = firstRow,
                text = Name,
                style_info = xlsxStyleInfoType.Title
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"A{firstRow}",
                cell_to_name = $"J{firstRow}"
            });

            firstRow++;

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = firstRow,
                text = "№",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "B",
                row_index = firstRow,
                text = "Название",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "C",
                row_index = firstRow,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "D",
                row_index = firstRow,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"B{firstRow}",
                cell_to_name = $"D{firstRow}"
            });

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "E",
                row_index = firstRow,
                text = "Вид публикации (учебник, учебное посо­бие, монография, мето­дические указания, статья, тезисы ит. п.)",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "F",
                row_index = firstRow,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "G",
                row_index = firstRow,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"E{firstRow}",
                cell_to_name = $"G{firstRow}"
            });


            string[] str = [
                "Объем (п.л.)",
                "Издательство",
                "Год"
            ];

            for (int i = 7, j = 0; j < str.Length; i++, j++) { 
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = $"{_str[i]}",
                    row_index = firstRow,
                    text = str[j],
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                
            }

        firstRow++;

            for (uint i = 0; i <= rowCount; firstRow++, i++) {
                for (int j = 0; j <= 9; j++) {

                    _office.insert_cell_in_worksheet(new xlsxCellParameters {
                        column_name = $"{_str[j]}",
                        row_index = firstRow,
                        text = "-",
                        style_info = xlsxStyleInfoType.TextWithBorder
                    });

                    if (j == 1) {
                        j += 2;
                        _office.insert_cell_in_worksheet(new xlsxCellParameters {
                            column_name = "C",
                            row_index = firstRow,
                            style_info = xlsxStyleInfoType.TextWithBorder
                        });
                        _office.insert_cell_in_worksheet(new xlsxCellParameters {
                            column_name = "D",
                            row_index = firstRow,
                            style_info = xlsxStyleInfoType.TextWithBorder
                        });
                        _office.merge_cells(new xlsxMergeParameters {
                            cell_from_name = $"B{firstRow}",
                            cell_to_name = $"D{firstRow}"
                        });
                    }
                    else if (j == 4) {
                        j += 2;
                        _office.insert_cell_in_worksheet(new xlsxCellParameters {
                            column_name = "F",
                            row_index = firstRow,
                            style_info = xlsxStyleInfoType.TextWithBorder
                        });
                        _office.insert_cell_in_worksheet(new xlsxCellParameters {
                            column_name = "G",
                            row_index = firstRow,
                            style_info = xlsxStyleInfoType.TextWithBorder
                        });
                        _office.merge_cells(new xlsxMergeParameters {
                            cell_from_name = $"E{firstRow}",
                            cell_to_name = $"G{firstRow}"
                        });
                    }
                }
            }
            firstRow += 2;
            return firstRow;
        }

        private void sheet_5(itp_temp_info info) {
            _office.setWorksheet(sheetNameList[sheet_id]);
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 1,
                text = "Сводная таблица",
                style_info = xlsxStyleInfoType.Title
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "A1",
                cell_to_name = "M1"
            });

            _office.setWorksheet(sheetNameList[sheet_id]);
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 2,
                text = $"нормируемой работы, выполняемой в течение 6-часового рабочего для на " +
                        $"{info.date_from.Year}/{info.date_to.Year} учебный год",
                style_info = xlsxStyleInfoType.Title
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "A2",
                cell_to_name = "M2"
            });

            _office.setWorksheet(sheetNameList[sheet_id]);
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 3,
                text = "(количество занимаемых ставок  - 1,1)",
                style_info = xlsxStyleInfoType.Title
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "A3",
                cell_to_name = "M3"
            });

            string[] str = [
                "Вид работы",
                "1.Учебна работа",
                "2.Учебно-методическая работа",
                "3.Научно-исследовательская работа (по госбюджету)",
                "4.Организационно-методическая работа",
                "Всего"
            ];

            for (int i = 0, j = 4; i < str.Length; i++, j++) {
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "A",
                    row_index = (uint)j,
                    text = str[i],
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "B",
                    row_index = (uint)j,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "C",
                    row_index = (uint)j,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "D",
                    row_index = (uint)j,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "E",
                    row_index = (uint)j,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });

                _office.merge_cells(new xlsxMergeParameters {
                    cell_from_name = $"A{j}",
                    cell_to_name = $"E{j}"
                });
            }
            str = ["План на год в часах", "факт. Выполн. в часах", "примечание"];

            for (int i = 0, j = 5; j < 11; i++, j++) {
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = $"{_str[j]}",
                    row_index = 4,
                    text = str[i],
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                j++;
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = $"{_str[j]}",
                    row_index = 4,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.merge_cells(new xlsxMergeParameters {
                    cell_from_name = $"{_str[j - 1]}4",
                    cell_to_name = $"{_str[j]}4"
                });
            }

            for (uint i = 5; i < 10; i++) {
                for (int j = 5; j < 11; j++) {
                    _office.insert_cell_in_worksheet(new xlsxCellParameters {
                        column_name = $"{_str[j]}",
                        row_index = i,
                        text = "-",
                        style_info = xlsxStyleInfoType.TextWithBorder
                    });
                    _office.insert_cell_in_worksheet(new xlsxCellParameters {
                        column_name = $"{_str[j + 1]}",
                        row_index = i,
                        style_info = xlsxStyleInfoType.TextWithBorder
                    });
                    j++;
                    _office.merge_cells(new xlsxMergeParameters {
                        cell_from_name = $"{_str[j - 1]}{i}",
                        cell_to_name = $"{_str[j]}{i}"
                    });
                }
            }

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 10,
                text = "Примечание: общий объем работы преподавателя в течение учебного года (строка \"всего\") должен " +
                        "составлять 1540 часов на одну ставку, при этом объем учебной работы (первая половина дня) " +
                        "не должен превышать 900 часов на одну ставку.",
                style_info = xlsxStyleInfoType.Text
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "A10",
                cell_to_name = "K11"
            });

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 12,
                text = "План повышения квалификации за учебный год",
                style_info = xlsxStyleInfoType.Title
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "A12",
                cell_to_name = "K12"
            });

            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = 13,
                text = "Форма повышения квалификации преподавателя",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "B",
                row_index = 13,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "C",
                row_index = 13,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "D",
                row_index = 13,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "E",
                row_index = 13,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "F",
                row_index = 13,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "G",
                row_index = 13,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "A13",
                cell_to_name = "G13"
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "H",
                row_index = 13,
                text = "Срок выполнения (причина невыполнения)",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "I",
                row_index = 13,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "J",
                row_index = 13,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "K",
                row_index = 13,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = "H13",
                cell_to_name = "K13"
            });
            int z = 14;
            for (int i = 0; i < info.pdpPlanRowCount; i++, z++) {
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "A",
                    row_index = (uint)z,
                    text = "-",
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "B",
                    row_index = (uint)z,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "C",
                    row_index = (uint)z,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "D",
                    row_index = (uint)z,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "E",
                    row_index = (uint)z,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "F",
                    row_index = (uint)z,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "G",
                    row_index = (uint)z,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.merge_cells(new xlsxMergeParameters {
                    cell_from_name = $"A{z}",
                    cell_to_name = $"G{z}"
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "H",
                    row_index = (uint)z,
                    text = "-",
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "I",
                    row_index = (uint)z,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "J",
                    row_index = (uint)z,
                    style_info = xlsxStyleInfoType.TextWithBorder
                }); 
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "K",
                    row_index = (uint)z,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.merge_cells(new xlsxMergeParameters {
                    cell_from_name = $"H{z}",
                    cell_to_name = $"K{z}"
                });
            }
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = (uint)z,
                text = "Замечания зав. кафедрой и декана о выполнении преподавателем плана работы за учебный год",
                style_info = xlsxStyleInfoType.Title
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"A{z}",
                cell_to_name = $"K{z}"
            });
            z++;
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "A",
                row_index = (uint)z,
                text = "Дата",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "B",
                row_index = (uint)z,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "C",
                row_index = (uint)z,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "D",
                row_index = (uint)z,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "E",
                row_index = (uint)z,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "F",
                row_index = (uint)z,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "G",
                row_index = (uint)z,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"A{z}",
                cell_to_name = $"G{z}"
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "H",
                row_index = (uint)z,
                text = "Содержание замечаний",
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "I",
                row_index = (uint)z,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "J",
                row_index = (uint)z,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.insert_cell_in_worksheet(new xlsxCellParameters {
                column_name = "K",
                row_index = (uint)z,
                style_info = xlsxStyleInfoType.TextWithBorder
            });
            _office.merge_cells(new xlsxMergeParameters {
                cell_from_name = $"H{z}",
                cell_to_name = $"K{z}"
            });
            z++;
            for (int i = 0; i < info.commentRowCount; i++, z++) {
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "A",
                    row_index = (uint)z,
                    text = "-",
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "B",
                    row_index = (uint)z,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "C",
                    row_index = (uint)z,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "D",
                    row_index = (uint)z,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "E",
                    row_index = (uint)z,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "F",
                    row_index = (uint)z,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "G",
                    row_index = (uint)z,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.merge_cells(new xlsxMergeParameters {
                    cell_from_name = $"A{z}",
                    cell_to_name = $"G{z}"
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "H",
                    row_index = (uint)z,
                    text = "-",
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "I",
                    row_index = (uint)z,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "J",
                    row_index = (uint)z,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.insert_cell_in_worksheet(new xlsxCellParameters {
                    column_name = "K",
                    row_index = (uint)z,
                    style_info = xlsxStyleInfoType.TextWithBorder
                });
                _office.merge_cells(new xlsxMergeParameters {
                    cell_from_name = $"H{z}",
                    cell_to_name = $"K{z}"
                });
            }
        }
    }
}