using contracts.search_models;
using contracts.view_moedels;

namespace contracts.presenter_contracts {
    public interface Idocument_presenter {
        public document_view_model make_document_presenter(document_search_model search_model);
        public List<document_view_model> make_document_list_presenter(document_search_model? search_model);
    }
}
