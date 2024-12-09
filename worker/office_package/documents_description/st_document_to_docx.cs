using worker.office_package.helper_enums;
using worker.office_package.helper_models;

namespace worker.office_package.documents_description {
    public class st_document_to_docx {

        private readonly Icreate_docx_file _office;

        public st_document_to_docx(Icreate_docx_file office) { 
            _office = office; 
        }

        public byte[]? create_document(st_info info) {

            // todo 
            // Доработать шаблон st

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
