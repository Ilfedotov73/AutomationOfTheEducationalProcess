using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using worker.office_package.helper_models;

namespace worker.office_package {
    public interface Icreate_docx_file {
        void create_docx();
        void create_paragraph(docxParagraph docxParagraph);
        byte[]? save_docx(); 
    }
}
