using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using worker.office_package.helper_enums;
using worker.office_package.helper_models;

namespace worker.office_package.documents_description {
    public class itp_document_to_docx {

        private readonly Icreate_docx_file _office;
        
        public itp_document_to_docx(Icreate_docx_file office) {
            _office = office;
        }

        public byte[]? create_document(itp_Info info) {

            // todo
            // Дорабоать шаблон для itp

            _office.create_docx();
            _office.create_paragraph(new docxParagraph {
                texts = new List<(string, docxTextProperties)> { (info.title, new docxTextProperties { bold = true, size = "24" }) },
                text_properties = new docxTextProperties {
                    size = "24",
                    justification_type = wordJustificationType.Center
                }
            });

            var document = _office.save_docx();
            return document;
        }
    }
}
