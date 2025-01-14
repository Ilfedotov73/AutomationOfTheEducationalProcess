using contracts.binding_models;
using contracts.worker_contracts;
using worker.office_package;
using worker.office_package.documents_description;
using worker.office_package.helper_models.info_models;

namespace worker.implements {
    public class itp_document_worker : Idocument_worker {

        private readonly itp_document_to_docx _itpDocx;
        private readonly itp_document_to_xlsx _itpXlsx;
        private readonly Itemplate_worker _templateWorker;
        private itp_temp_info _tempInfo = new();

        // Вызов из document_itp_facade
        public itp_document_worker(Icreate_docx_file docxImp, Itemplate_worker templateWorker) {
            _itpDocx = new(docxImp);
            _itpXlsx = new();
            _templateWorker = templateWorker;
        }

        public void create_document_to_docx(document_binding_model model, template_binding_model? template = null) {

            if (template == null) {
                throw new ArgumentNullException("Аргумент template равен null");
            }

            if (model.data_doc.GetType() != typeof(itp_info) || model.data_doc == null) {
                throw new Exception("mismatch of info types");
            }

            check_data((itp_info)model.data_doc, template);
            var document = _itpDocx.create_document((itp_info)model.data_doc, _tempInfo);
            if (document == null) {
                throw new Exception("Ошибка создания документа");
            }

            // Создает какталог (если еще не создан)
            if (!Directory.Exists(model.file_path)) {
                Directory.CreateDirectory(model.file_path);
            }

            // Записывают массив байт в бинарный файл. Если файл не существует, он создается. Если существует, то перезаписывается
            model.file_path += $"{model.name}" + $".{model.file_format_type}";
            File.WriteAllBytes(model.file_path, document);
        }

        public void create_document_to_xlsx(document_binding_model model, template_binding_model? template = null) {

            if (template == null) {
                throw new ArgumentNullException("template not found");
            }

            string temporary = @"C:\Users\Ilfe\Documents\AutomationOfTheEducationalProcess\TEMPORARY\";
            if (!Directory.Exists(temporary)) {
                Directory.CreateDirectory(temporary);
            }

            if (model.data_doc.GetType() != typeof(itp_info) || model.data_doc == null) {
                throw new Exception("mismatch of info types");
            }
            check_data((itp_info)model.data_doc,template);

            var document = _itpXlsx.create_document((itp_info)model.data_doc, template.file_path, temporary + 
                                                     $"tmp.xlsx");
            if (document == Array.Empty<byte>()) {
                throw new Exception("Ошибка создания документа");
            }

            // Создает какталог (если еще не создан)
            if (!Directory.Exists(model.file_path)) {
                Directory.CreateDirectory(model.file_path);
            }

            model.file_path += $"{model.name}" + $".{model.file_format_type}";
            File.WriteAllBytes(model.file_path, document);
        }

        private void check_data(itp_info info, template_binding_model template) {
            itp_temp_info template_info = (itp_temp_info)_templateWorker.read_temp_file(template);
            if (template_info == null) {
                throw new Exception("operatin read template file is failed");
            } 
            
            if (info.data_11.Length > 17) {
                throw new Exception("invalid data set");
            }
            if (info.data_12.Length > 17) {
                throw new Exception("invalid data set");
            }

            if (info.data_21.Count > template_info.workTypes_21.Count) {
                throw new Exception("invalid data set");
            }
            if (info.data_22.Count > template_info.workTypes_22.Count) {
                throw new Exception("invalid data set");
            }
            if (info.data_23.Count > template_info.workTypes_23.Count) {
                throw new Exception("invalid data set");
            }
            if (info.data_24.Count > template_info.workTypes_24.Count) {
                throw new Exception("invalid data set");
            }

            if (info.data_31.Count > template_info.workTypes_31.Count) {
                throw new Exception("invalid data set");
            }
            if (info.data_32.Count > template_info.workTypes_32.Count) {
                throw new Exception("invalid data set");
            }

            if (info.data_41.Count > template_info.workTypes_41.Count) {
                throw new Exception("invalid data set");
            }
            if (info.data_42.Count > template_info.workTypes_42.Count) {
                throw new Exception("invalid data set");
            }

            if (info.data_51.Length > 4) {
                throw new Exception("invalid data set");
            }

            _tempInfo = template_info;
        }
    }
}
