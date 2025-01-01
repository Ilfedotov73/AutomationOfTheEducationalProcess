using contracts.binding_models;
using contracts.worker_contracts;
using data_models.Enums;
using data_models.IModels;
using worker.implements;
using worker.office_package;

namespace worker {
    public class document_st_facade {
        private readonly Idocument_worker _documentWorker;

        public delegate void is_function(Idocument model, template_binding_model? template = null);
        static is_function[] funcs = {};

        public document_st_facade(Icreate_docx_file _docxImp, Icreate_xlsx_file _xlsxImp) {
            _documentWorker = new st_document_worker(_docxImp, _xlsxImp);
            funcs = [_documentWorker.create_document_to_docx, _documentWorker.create_document_to_xlsx];
        }

        public static is_function get_function(enum_file_format_type format) {
            if ((int)format > funcs.Length - 1) {
                throw new Exception("missing method address");
            }
            return funcs[(int)format];
        }
    }
}
