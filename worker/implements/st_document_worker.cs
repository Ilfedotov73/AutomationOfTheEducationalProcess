using contracts.binding_models;
using contracts.worker_contracts;
using contracts.worker_contracts.helper_models;
using data_models.IModels;
using worker.office_package;
using worker.office_package.documents_description;
using worker.office_package.helper_models.info_models;

namespace worker.implements {
    public class st_document_worker : Idocument_worker {

        private readonly st_document_to_docx _stDocx;
        private readonly st_document_to_xlsx _stXlsx;

        // Вызов из document_st_facade
        public st_document_worker(Icreate_docx_file docxImp, Icreate_xlsx_file xlsxImp) {
            _stDocx = new(docxImp);
            _stXlsx = new(xlsxImp);
        } 

        public void create_document_to_docx(document_binding_model model, template_binding_model? template = null) {

            if (model.data_doc.GetType() != typeof(st_info) || model.data_doc == null) {
                throw new Exception("mismatch of info types");
            }

            var document = _stDocx.create_document((st_info)model.data_doc);
            if (document == null) {
                throw new Exception("Ошибка создания документа");
            }

            // Создает какталог (если еще не создан)
            if (!Directory.Exists(model.file_path)) {
                Directory.CreateDirectory(model.file_path);
            }

            // Записывают массив байт в бинарный файл. Если файл не существует, он создается. Если существует, то перезаписывается
            model.file_path += $"{model.name}{model.date.Year}" + $".{model.file_format_type}";
            File.WriteAllBytes(model.file_path, document);
        }

        public void create_document_to_xlsx(document_binding_model model, template_binding_model? template = null) {

            if (model.data_doc.GetType() != typeof(st_info) || model.data_doc == null) {
                throw new Exception("mismatch of info types");
            }

            var document = _stXlsx.create_document((st_info)model.data_doc);
            if (document == null) {
                throw new Exception("Ошибка создания документа");
            }

            // Создает какталог (если еще не создан)
            if (!Directory.Exists(model.file_path)) {
                Directory.CreateDirectory(model.file_path);
            }

            model.file_path += $"{model.name}" + $".{model.file_format_type}";
            File.WriteAllBytes(model.file_path, document);
        }
    }
}
