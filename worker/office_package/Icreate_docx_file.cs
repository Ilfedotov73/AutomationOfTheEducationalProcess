using DocumentFormat.OpenXml.Wordprocessing;
using worker.office_package.helper_models;

namespace worker.office_package {
    public interface Icreate_docx_file {
        void create_docx();
        void create_paragraph(docxParagraph docxParagraph);
        void create_table(List<string[]> tables, int? indexRow);
        void page_breaks();
        byte[]? save_docx(); 
    }
}
