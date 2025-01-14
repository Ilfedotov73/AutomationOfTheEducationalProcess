using contracts.binding_models;
using contracts.worker_contracts.helper_models;
using data_models.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace contracts.worker_contracts {
    public interface Idocument_worker {
        public void create_document_to_docx(document_binding_model model, template_binding_model? template = null);
        public void create_document_to_xlsx(document_binding_model model, template_binding_model? template = null);
    }
}
