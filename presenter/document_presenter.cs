using contracts.interactor_contracts;
using contracts.presenter_contracts;
using contracts.search_models;
using contracts.view_moedels;

namespace presenter {
    public class document_presenter : Idocument_presenter {

        private readonly Idocument_logic _logic;

        public document_presenter(Idocument_logic logic) {
            _logic = logic;
        }

        public document_view_model make_document_presenter(document_search_model search_model) {
            var model = _logic.get_document_info(search_model);
            var newViewModel = new document_view_model {
                id = model.id,
                name = model.name,
                date = model.date.ToString(),
                author_name = model.user.fio,
                file_format_type = model.file_format_type.ToString(),
                document_type = model.document_type.ToString(),
                template_name = model.template.name
            };
            return newViewModel;
        }

        public List<document_view_model> make_document_list_presenter(document_search_model? search_model) {
            var models = _logic.get_document_list(search_model);
            List<document_view_model> newViewModels = new();

            foreach ( var item in models ) {
                newViewModels.Add(new document_view_model {
                    id = item.id,
                    name = item.name,
                    date = item.date.ToString(),
                    author_name = item.user.fio,
                    file_format_type = item.file_format_type.ToString(),
                    document_type = item.document_type.ToString(),
                    template_name = item.template.name
                });
            }
            return newViewModels;
        }
    }
}
