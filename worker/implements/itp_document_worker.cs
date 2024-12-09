using contracts.binding_models;
using contracts.worker_contracts;
using contracts.worker_contracts.helper_models;
using data_models.IModels;
using worker.office_package;
using worker.office_package.documents_description;
using worker.office_package.helper_models;

namespace worker.implements {
    public class itp_document_worker : Idocument_worker {

        private readonly itp_document_to_docx _itpDocx;
        private readonly itp_document_to_xlsx _itpXlsx;
        private readonly itp_template_worker _templateWorker;

        // Вызов из document_itp_facade
        public itp_document_worker(Icreate_docx_file docxImp, itp_template_worker templateWorker) {
            _itpDocx = new(docxImp);
            _itpXlsx = new();
            _templateWorker = templateWorker;
        }

        public void create_document_to_docx(Idocument model, template_binding_model? template = null) {

            if (template == null) {
                throw new ArgumentNullException("Аргумент template равен null");
            }

            itp_Info info = (itp_Info)prepare_data(model, template);

            var document = _itpDocx.create_document(info);
            if (document == null) {
                throw new Exception("Ошибка создания документа");
            }

            // Записывают массив байт в бинарный файл. Если файл не существует, он создается. Если существует, то перезаписывается
            File.WriteAllBytes(model.file_path, document);
        }

        public void create_document_to_xlsx(Idocument model, template_binding_model? template = null) {

            if (template == null) {
                throw new ArgumentNullException("Аргумент template равен null");
            }

            itp_Info info = (itp_Info)prepare_data(model);

            var docuement = _itpXlsx.create_document(info, template.file_path);
            if (docuement == Array.Empty<byte>()) {
                throw new Exception("Ошибка создания документа");
            }
            File.WriteAllBytes(model.file_path, docuement);
        }

        public Idata_info prepare_data(Idocument model, template_binding_model? template = null) {
            // todo
            // Заполнений полей факт.
            var info = new itp_Info {
                title = model.name,
                date = DateOnly.FromDateTime(DateTime.Now),
            };
            if (template != null) {
                info.templateData = _templateWorker.read_temp_file(template);
            }
            return info;
        }
    }
}
