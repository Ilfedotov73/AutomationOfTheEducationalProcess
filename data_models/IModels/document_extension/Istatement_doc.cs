using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_models.IModels.document_extension {
    public interface Istatement_doc : Idocument {
        int student_group_id { get; }
        Dictionary<int, (Istudent, int)> exam_result_list { get; }
    }
}
