using contracts.binding_models;
using contracts.worker_contracts;
using contracts.worker_contracts.helper_models;
using data_models.IModels;
using worker.office_package;
using worker.office_package.documents_description;
using worker.office_package.helper_models;

namespace worker.implements {
    public class st_document_worker : Idocument_worker {

        private readonly st_document_to_docx _stDocx;
        private readonly st_document_to_xlsx _stXlsx;

        // Вызов из document_st_facade
        public st_document_worker(Icreate_docx_file docxImp, Icreate_xlsx_file xlsxImp) {
            _stDocx = new(docxImp);
            _stXlsx = new(xlsxImp);
        } 

        public void create_document_to_docx(Idocument model, template_binding_model? template = null) {
            st_info info = (st_info)prepare_data(model);

            var document = _stDocx.create_document(info);
            if (document == null) {
                throw new Exception("Ошибка создания документа");
            }

            // Записывают массив байт в бинарный файл. Если файл не существует, он создается. Если существует, то перезаписывается
            File.WriteAllBytes(model.file_path, document);
        }

        public void create_document_to_xlsx(Idocument model, template_binding_model? template = null) {
            st_info info = (st_info)prepare_data(model);

            var document = _stXlsx.create_document(info);
            if (document == null) {
                throw new Exception("Ошибка создания документа");
            }
            File.WriteAllBytes(model.file_path, document);
        }

        public Idata_info prepare_data(Idocument model, template_binding_model? template = null) {

            // todo 
            // Дополнить для ведомости

            return new st_info {
                title = model.name,
                date = DateOnly.FromDateTime(DateTime.Now)
            };
        }
    }
}
