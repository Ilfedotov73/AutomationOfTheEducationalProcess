
using data_models.Enums;

namespace data_models.IModels {
    public interface Itemplate : Iid {
        string name { get; }
        string file_path { get; }
        int UserId { get; }
        enum_document_type document_type { get; }
    }
}
