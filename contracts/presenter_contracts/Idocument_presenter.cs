using contracts.view_moedels;
using data_models.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.presenter_contracts {
    public interface Idocument_presenter {
        public document_view_model make_document_presenter(Idocumnet model);
        public List<document_view_model> make_document_list_presenter(List<Idocumnet> models);
    }
}
