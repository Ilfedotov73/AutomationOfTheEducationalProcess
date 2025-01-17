using contracts.binding_models;
using data_models.Enums;
using data_models.IModels;
using System.ComponentModel.DataAnnotations;

namespace contracts.storage_contracts.db_models {
    public class Template : Itemplate {
        public int id { get; set; }

        [Required]
        public string name { get; set; } = string.Empty;

        [Required]
        public string file_path { get; set; } = string.Empty;

        public List<Document> documents { get; set; } = new();

        public int UserId { get; set; }
        public User? user { get; set; }

        [Required]
        public enum_document_type document_type { get; set; }

        public static Template? insert(template_binding_model? model) {
            if (model == null) {
                return null;
            }
            return new Template() {
                id = model.id,
                name = model.name,
                file_path = model.file_path,
                UserId = model.UserId,
                document_type = model.document_type
            };
        }

        public void edit(template_binding_model? model) {
            if (model == null) {
                return;
            }
            name = model.name;
            UserId = model.UserId;
        }
    }
}
