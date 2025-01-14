using contracts.view_moedels;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using worker.office_package.helper_enums;
using worker.office_package.helper_models;
using worker.office_package.helper_models.info_models;

namespace worker.office_package.documents_description {
    public class itp_document_to_docx {

        private readonly Icreate_docx_file _office;
        
        public itp_document_to_docx(Icreate_docx_file office) {
            _office = office;
        }

        public byte[]? create_document(itp_info info, itp_temp_info temp_info) {

            _office.create_docx();
            fontList(temp_info.user_info);

            _office.page_breaks();

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("1.УЧЕБНАЯ РАБОТА",
                new docxTextProperties {bold = true, size = "24"}) },
                text_properties = new docxTextProperties { bold = true, justification_type = wordJustificationType.Center },
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("Утверждено Зав. кафедрой__________ \t\t\t\t 2020/2021 уч. год",
                new docxTextProperties {bold = true, size = "24"}) },
                text_properties = new docxTextProperties { bold = true, justification_type = wordJustificationType.Left },
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("\"___\"________________20__г",
                new docxTextProperties {bold = true, size = "24"}) },
                text_properties = new docxTextProperties { bold = true, justification_type = wordJustificationType.Right },
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("1.1 Нагрузка преподавателя по программам высшего образования (ВО)",
                new docxTextProperties {bold = true, size = "24"}) },
                text_properties = new docxTextProperties { bold = true, justification_type = wordJustificationType.Center },
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("a) Осенний семестр",
                new docxTextProperties {bold = true, size = "24"}) },
                text_properties = new docxTextProperties { bold = false, justification_type = wordJustificationType.Center },
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("Количество часов по видам учебной работы",
                new docxTextProperties {bold = true, size = "24"}) },
                text_properties = new docxTextProperties { bold = true, justification_type = wordJustificationType.Center },
            });

            List<string[]> data = new();
            data.Add(["Дисциплина\nN группы\n(поток)", "Студентов", "Лекции", "практ. занятия и семинары", "лабораторные занятия", 
                    "консультации","зачеты", "экзамены", "Диф. Зачеты", "руководство практиками студентов", 
                     "расчетно-графические работы","рук-во дипл. проектом (вып. работой)", "работа в ГИА", "Рефераты, РГР",
                    "руководство аспирантами","Другие виды работ", "всего часов", "всего часов фактически"]);
            for (int i = 0; i < temp_info.disciplines_A.Count; i++) {
                data.Add([$"{temp_info.disciplines_A[i]}", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", 
                            " ", " ", " ", " "]);
            }
            data.Add(["Итого за семестр", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "]);
            string[] tmpStr = new string[18]; 
            tmpStr[0] = "Фактически выполнено";
            for (int i = 1; i < 18; i++) {
                tmpStr[i] = info.data_11[i - 1].ToString();
            }
            data.Add(tmpStr);
            _office.create_table(data, null);

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("",
                new docxTextProperties {bold = true, size = "24"}) },
                text_properties = new docxTextProperties { bold = false, justification_type = wordJustificationType.Center },
            });

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("б) весенний семестр",
                new docxTextProperties {bold = true, size = "24"}) },
                text_properties = new docxTextProperties { bold = false, justification_type = wordJustificationType.Center },
            });
            data = new();
            data.Add(["Дисциплина\nN группы\n(поток)", "Студентов", "Лекции", "практ. занятия и семинары", "лабораторные занятия",
                    "консультации","зачеты", "экзамены", "Диф. Зачеты", "руководство практиками студентов",
                     "расчетно-графические работы","рук-во дипл. проектом (вып. работой)", "работа в ГИА", "Рефераты, РГР",
                    "руководство аспирантами","Другие виды работ", "всего часов", "всего часов фактически"]);
            for (int i = 0; i < temp_info.disciplines_B.Count; i++) {
                data.Add([$"{temp_info.disciplines_B[i]}", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ",
                            " ", " ", " ", " "]);
            }
            data.Add(["Итого за семестр", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "]);
            tmpStr = new string[18];
            tmpStr[0] = "Фактически выполнено";
            for (int i = 1; i < 18; i++) {
                tmpStr[i] = info.data_12[i - 1].ToString();
            }
            data.Add(tmpStr);
            data.Add(["Итого за учебный год по ВО", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", 
                     " ", " "]);
            data.Add(["Фактически выполнено за", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", 
                    " ", " "]);
            _office.create_table(data, null);

            _office.page_breaks();

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("2.УЧЕБНО-МЕТОДИЧЕСКАЯ РАБОТА", new docxTextProperties {
                    bold = true, size = "28"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("(учитывается по второй половине рабочего дня)", 
                    new docxTextProperties {
                    bold = true, size = "28"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("2.1. Подготовка к лекционным, практическим, семинарским, " +
                                                                    "лабораторным занятиям и другие виды учебно-методической " +
                                                                    "работы (для программ ВО)", 
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });

            data = new();
            data.Add(["", "Вид работы", "Нормы времени", "Осенний семестр", "M", "Весенний семестр", "M"]);
            data.Add(["", "", "", "кол-во часов", "M", "кол-во часов", "M"]);
            data.Add(["3HM", "3HM", "3HM", "по плану", "факт. выполнения", "по плану", "факт. выполнения"]);
            data.Add(["1", "2", "3", "4", "5", "6", "7"]);
            for (int i = 0; i < temp_info.workTypes_21.Count; i++) {
                tmpStr = new string[7];
                tmpStr = ["", temp_info.workTypes_21[i].ToString(), "", "", info.data_21[i].Item1.ToString(), "", 
                                                                            info.data_21[i].Item2.ToString()];
                data.Add(tmpStr);
            }
            data.Add(["", "Всего часов", "", "", "", "", ""]);
            _office.create_table(data, 2);

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("",
                    new docxTextProperties {
                    bold = true, size = "28"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("2.2. Подготовка электронных обучающих ресурсов (ЭОР) в ЭИОС", 
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });
            data = new();
            data.Add(["", "Вид работы", "Нормы времени", "Осенний семестр", "M", "Весенний семестр", "M"]);
            data.Add(["", "", "", "кол-во часов", "M", "кол-во часов", "M"]);
            data.Add(["3HM", "3HM", "3HM", "по плану", "факт. выполнения", "по плану", "факт. выполнения"]);
            data.Add(["1", "2", "3", "4", "5", "6", "7"]);
            for (int i = 0; i < temp_info.workTypes_22.Count; i++) {
                tmpStr = new string[7];
                tmpStr = ["", temp_info.workTypes_22[i].ToString(), "", "", info.data_22[i].Item1.ToString(), "",
                                                                            info.data_22[i].Item2.ToString()];
                data.Add(tmpStr);
            }
            data.Add(["", "Всего часов", "", "", "", "", ""]);
            _office.create_table(data, 2);

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {
                    ("",
                    new docxTextProperties {
                    bold = true, size = "28"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("2.3. Использование в учебном процессе электронных " +
                                                                    "обучающих ресурсов (ЭОР) ", 
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });
            data = new();
            data.Add(["", "Вид работы", "Нормы времени", "Осенний семестр", "M", "Весенний семестр", "M"]);
            data.Add(["", "", "", "кол-во часов", "M", "кол-во часов", "M"]);
            data.Add(["3HM", "3HM", "3HM", "по плану", "факт. выполнения", "по плану", "факт. выполнения"]);
            data.Add(["1", "2", "3", "4", "5", "6", "7"]);
            for (int i = 0; i < temp_info.workTypes_23.Count; i++) {
                tmpStr = new string[7];
                tmpStr = ["", temp_info.workTypes_23[i].ToString(), "", "", info.data_23[i].Item1.ToString(), "",
                                                                            info.data_23[i].Item2.ToString()];
                data.Add(tmpStr);
            }
            data.Add(["", "Всего часов", "", "", "", "", ""]);
            _office.create_table(data, 2);

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {
                    ("",
                    new docxTextProperties {
                    bold = true, size = "28"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("2.4. Написание и переработка учебников, учебных пособий и " +
                                                                    "других учебно-методических материалов ", 
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });

            data = new();
            data.Add(["", "Вид работы", "Нормы времени", "Осенний семестр", "M", "Весенний семестр", "M"]);
            data.Add(["", "", "", "кол-во часов", "M", "кол-во часов", "M"]);
            data.Add(["3HM", "3HM", "3HM", "по плану", "факт. выполнения", "по плану", "факт. выполнения"]);
            data.Add(["1", "2", "3", "4", "5", "6", "7"]);
            for (int i = 0; i < temp_info.workTypes_24.Count; i++) {
                tmpStr = new string[7];
                tmpStr = ["", temp_info.workTypes_24[i].ToString(), "", "", info.data_24[i].Item1.ToString(), "",
                                                                            info.data_24[i].Item2.ToString()]; ;
                data.Add(tmpStr);
            }
            data.Add(["", "Всего часов", "", "", "", "", ""]);
            data.Add(["Итого часов по данному разделу", "M", "", "", "", "", ""]);
            _office.create_table(data, 2);


            _office.page_breaks();

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("3.НАУЧНО-ИССЛЕДОВАТЕЛЬСКАЯ РАБОТА",
                    new docxTextProperties {
                    bold = true, size = "28"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("(учитывается по второй половине рабочего дня для ВО)",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("3.1. Написание и подготовка к изданию монографий, научных " +
                                                                "статей, докладов, заявок на изобретение",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });
            data = new();
            data.Add(["", "Вид работы", "Нормы времени", "Осенний семестр", "M", "Весенний семестр", "M"]);
            data.Add(["", "", "", "кол-во часов", "M", "кол-во часов", "M"]);
            data.Add(["3HM", "3HM", "3HM", "по плану", "факт. выполнения", "по плану", "факт. выполнения"]);
            data.Add(["1", "2", "3", "4", "5", "6", "7"]);
            for (int i = 0; i < temp_info.workTypes_31.Count; i++) {
                tmpStr = new string[7];
                tmpStr = ["", temp_info.workTypes_31[i].ToString(), "", "", info.data_31[i].Item1.ToString(), "",
                                                                            info.data_31[i].Item2.ToString()];
                data.Add(tmpStr);
            }
            data.Add(["", "Всего часов", "", "", "", "", ""]);
            _office.create_table(data, 2);

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {
                    ("",
                    new docxTextProperties {
                    bold = true, size = "28"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("3.2. Другие виды научно-исследовательских работ",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });
            data = new();
            data.Add(["", "Вид работы", "Нормы времени", "Осенний семестр", "M", "Весенний семестр", "M"]);
            data.Add(["", "", "", "кол-во часов", "M", "кол-во часов", "M"]);
            data.Add(["3HM", "3HM", "3HM", "по плану", "факт. выполнения", "по плану", "факт. выполнения"]);
            data.Add(["1", "2", "3", "4", "5", "6", "7"]);
            for (int i = 0; i < temp_info.workTypes_32.Count; i++) {
                tmpStr = new string[7];
                tmpStr = ["", temp_info.workTypes_32[i].ToString(), "", "", info.data_32[i].Item1.ToString(), "",
                                                                            info.data_32[i].Item2.ToString()];
                data.Add(tmpStr);
            }
            data.Add(["", "Всего часов", "", "", "", "", ""]);
            data.Add(["Итого часов по данному разделу", "M", "", "", "", "", ""]);
            _office.create_table(data, 2);

            _office.page_breaks();

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("4.ОРГАНИЗАЦИОННО-МЕТОДИЧЕСКАЯ РАБОТА",
                    new docxTextProperties {
                    bold = true, size = "28"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("(учитывается по второй половине рабочего дня для ВО)",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("4.1. Участие в проведении работы по профессиональной " +
                                                                    "ориентации молодежи при поступлении в вуз, исполнение " +
                                                                    "обязанностей, проведение олимпиад, конференций и другие " +
                                                                    "организационно-методические работы",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });
            data = new();
            data.Add(["", "Вид работы", "Нормы времени", "Осенний семестр", "M", "Весенний семестр", "M"]);
            data.Add(["", "", "", "кол-во часов", "M", "кол-во часов", "M"]);
            data.Add(["3HM", "3HM", "3HM", "по плану", "факт. выполнения", "по плану", "факт. выполнения"]);
            data.Add(["1", "2", "3", "4", "5", "6", "7"]);
            for (int i = 0; i < temp_info.workTypes_41.Count; i++) {
                tmpStr = new string[7];
                tmpStr = ["", temp_info.workTypes_41[i].ToString(), "", "", info.data_41[i].Item1.ToString(), "",
                                                                            info.data_41[i].Item2.ToString()];
                data.Add(tmpStr);
            }
            data.Add(["", "Всего часов", "", "", "", "", ""]);
            _office.create_table(data, 2);

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {
                    ("",
                    new docxTextProperties {
                    bold = true, size = "28"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("4.2. Организационно-методическая работа по физической " +
                                                                    "подготовке",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });
            data = new();
            data.Add(["", "Вид работы", "Нормы времени", "Осенний семестр", "M", "Весенний семестр", "M"]);
            data.Add(["", "", "", "кол-во часов", "M", "кол-во часов", "M"]);
            data.Add(["3HM", "3HM", "3HM", "по плану", "факт. выполнения", "по плану", "факт. выполнения"]);
            data.Add(["1", "2", "3", "4", "5", "6", "7"]);
            for (int i = 0; i < temp_info.workTypes_42.Count; i++) {
                tmpStr = new string[7];
                tmpStr = ["", temp_info.workTypes_42[i].ToString(), "", "", info.data_42[i].Item1.ToString(), "",
                                                                            info.data_42[i].Item2.ToString()];
                data.Add(tmpStr);
            }
            data.Add(["", "Всего часов", "", "", "", "", ""]);
            data.Add(["Итого часов по данному разделу", "M", "", "", "", "", ""]);
            _office.create_table(data, 2);

            _office.page_breaks();

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("Перечень опубликованных в учебном году научных и " +
                                                                    "научно-методических работ",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });
            data = new();
            data.Add(["№", "Название", "Вид публикации (учебник, учебное посо¬бие, монография, мето¬дические указания, " +
                        "статья, тезисы ит. п.)", "Объем (п.л.)", "Издательство", "Год"]);
            data.Add(["", "", "", "", "", ""]);
            data.Add(["", "", "", "", "", ""]);
            data.Add(["", "", "", "", "", ""]);
            data.Add(["", "", "", "", "", ""]);
            _office.create_table(data, null);

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {
                    ("",
                    new docxTextProperties {
                    bold = true, size = "28"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("Перечень материалов, сданных в учебном году в печать",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });
            data = new();
            data.Add(["№", "Название", "Вид публикации (учебник, учебное посо¬бие, монография, мето¬дические указания, " +
                        "статья, тезисы ит. п.)", "Объем (п.л.)", "Издательство", "Год"]);
            data.Add(["", "", "", "", "", ""]);
            data.Add(["", "", "", "", "", ""]);
            data.Add(["", "", "", "", "", ""]);
            data.Add(["", "", "", "", "", ""]);
            _office.create_table(data, null);

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("Участие в хоздоговорной НИР",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("(сверх 6-часового рабочего дня преподавателя – для ВО)",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });
            data = new();
            data.Add(["Содержание выполняемое работы", "В качестве кого участвует в НИР", "Планируемый срок выполнения", 
                    "Отметка о фактическом выполнении"]);
            data.Add(["", "", "", ""]);
            data.Add(["", "", "", ""]);
            data.Add(["", "", "", ""]);
            data.Add(["", "", "", ""]);
            _office.create_table(data, null);

            _office.page_breaks();

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("СВОДНАЯ ТАБЛИЦА",
                    new docxTextProperties {
                    bold = true, size = "28"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("нормируемой работы, выполняемой в течение 6-часового " +
                                                                    "рабочего дня ",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("на 2020/2021 учебный год ",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("(количество занимаемых ставок – 1,1)",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });

            data = new();
            data.Add(["Вид работы", "План на год в часах", "факт. выполн. в часах", "примечание"]);
            data.Add(["1 Учебная работа", "", "", ""]);
            data.Add(["2 Учебно-методическая работа", "", "", ""]);
            data.Add(["3 Научно-исследовательская работа (по госбюджету)", "", "", ""]);
            data.Add(["4 Организационно-методическая работа)", "", "", ""]);
            data.Add(["Всего", "", "", ""]);

            _office.create_table(data, null);

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("Примечание:  общий объем работы преподавателя в течение " +
                                                                    "учебного года (строка \"всего\") должен составлять 1540 " +
                                                                    "часов на одну ставку, при этом объем учебной работы (первая " +
                                                                    "половина дня) не должен превышать 900 часов на одну ставку",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("План повышения квалификации за учебный год",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });

            data = new();
            data.Add(["Форма повышения кваоификации преподавателя", "Срок выполнения\r\n( причина невыполнения)\r\n"]);
            data.Add(["", ""]);
            data.Add(["", ""]);
            data.Add(["", ""]);
            data.Add(["", ""]);
            data.Add(["", ""]);
            _office.create_table(data, null);

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("Замечания зав. кафедрой и декана о выполнении " +
                                                                    "преподавателем плана работы за учебный год",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Center
                }
            });

            data = new();
            data.Add(["Дата", "Содержание замечаний"]);
            data.Add(["", ""]);
            data.Add(["", ""]);
            data.Add(["", ""]);
            data.Add(["", ""]);
            data.Add(["", ""]);
            _office.create_table(data, null);

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Left
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Left
                }
            });

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("Преподаватель_____________________________________________(________________________ )",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Left
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("Зав кафедрой_____________________________________________(________________________ )",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Left
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> {  ("Декан факультета_____________________________________________(________________________ )",
                    new docxTextProperties {
                    bold = true, size = "24"
                }) },
                text_properties = new docxTextProperties {
                    bold = true,
                    justification_type = wordJustificationType.Left
                }
            });

            var document = _office.save_docx();
            return document;
        }

        private void fontList(user_view_model user) {
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("МИНИСТЕРСТВО НАУКИ И ВЫСШЕГО ОБРАЗОВАНИЯ РОССИЙСКОЙ ФЕДЕРАЦИИ",
                new docxTextProperties { bold = true, size = "24"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Center
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("федеральное государственное бюджетное образовательное",
                new docxTextProperties { bold = true, size = "24"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Center
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("учреждение высшего образования",
                new docxTextProperties { bold = true, size = "24"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Center
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("«УЛЬЯНОВСКИЙ ГОСУДАРСТВЕННЫЙ ТЕХНИЧЕСКИЙ УНИВЕРСИТЕТ»",
                new docxTextProperties { bold = true, size = "24"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Center
                }
            });

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("",
                new docxTextProperties { bold = true, size = "24"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Center
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("",
                new docxTextProperties { bold = true, size = "24"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Center
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("",
                new docxTextProperties { bold = true, size = "24"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Center
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("",
                new docxTextProperties { bold = true, size = "24"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Center
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("",
                new docxTextProperties { bold = true, size = "24"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Center
                }
            });

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("ИНДИВИДУАЛЬНЫЙ ПЛАН",
                new docxTextProperties { bold = true, size = "40"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Center
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("ПРЕПОДАВАТЕЛЯ",
                new docxTextProperties { bold = true, size = "40"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Center
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("(на 1 год)",
                new docxTextProperties { bold = true, size = "24"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Center
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("",
                new docxTextProperties { bold = true, size = "40"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Center
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ("",
                new docxTextProperties { bold = true, size = "40"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Center
                }
            });

            string[] str = ["Факультет", "Кафедра", "Фамилия", "Имя", "Отчество", "Должность", "Год рождения",
                            "Ученая степень и год присуждения", "Ученое звание и год присуждения"];

            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ($"{str[0]} \t{user.faculty_name}",
                new docxTextProperties { bold = true, size = "24"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Left
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ($"Кафедра \t{user.department_name}",
                new docxTextProperties { bold = true, size = "24"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Left
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ($"Фамилия \t{user._fio[0]}",
                new docxTextProperties { bold = true, size = "24"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Left
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ($"Имя \t{user._fio[1]}",
                new docxTextProperties { bold = true, size = "24"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Left
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ($"Отчество \t{user._fio[2]}",
                new docxTextProperties { bold = true, size = "24"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Left
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ($"Должность \t{user.position}",
                new docxTextProperties { bold = true, size = "24"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Left
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ($"Год рождения \t{user.year_of_birth}",
                new docxTextProperties { bold = true, size = "24"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Left
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ($"Ученая степень и год присуждения \t{user.academic_title} " +
                                                                    $"({user.year_of_award_at})",
                new docxTextProperties { bold = true, size = "24"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Left
                }
            });
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { ($"Ученое звание и год присуждения \t{user.academic_degree} " +
                                                                    $"({user.year_of_award_ad})",
                new docxTextProperties { bold = true, size = "24"}) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Left
                }
            });
        }
    }
}
