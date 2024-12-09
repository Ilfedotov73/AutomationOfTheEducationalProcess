using contracts.binding_models;
using contracts.worker_contracts;
using data_models.Enums;
using data_models.IModels;
using worker.implements;
using worker.office_package;

namespace worker {
    public class document_itp_facade {

        private readonly Idocument_worker _documentWorker;

        public delegate void is_function(Idocument model, template_binding_model template);
        static is_function[]? funcs;

        public document_itp_facade(Icreate_docx_file _docxImp, itp_template_worker _templateWorker) {
            _documentWorker = new itp_document_worker(_docxImp, _templateWorker);
            funcs = [_documentWorker.create_document_to_docx, _documentWorker.create_document_to_xlsx];
        }

        public static is_function get_function(enum_file_format_type format) {
            if ((int)format > funcs.Length - 1) {
                throw new Exception("Отсутствует адрес метода");
            }
            return funcs[(int)format];
        }
    }
}
