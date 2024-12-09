using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worker.office_package.helper_models {
    public class docxParagraph {
        public List<(string, docxTextProperties)> texts { get; set; } = new();
        public docxTextProperties? text_properties { get; set; }
    }
}
