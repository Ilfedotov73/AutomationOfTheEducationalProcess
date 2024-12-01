using contracts.binding_models;
using data_models.IModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.storage_contracts.db_models {
    public class Template : Itemplate {
        public int id { get; set; }

        [Required]
        public string name { get; set; } = string.Empty;

        [Required]
        public string file_path { get; set; } = string.Empty;

        public List<Document> documents { get; set; } = new();

        public static Template? insert(template_binding_model? model) {
            if (model == null) {
                return null;
            }
            return new Template() {
                id = model.id,
                name = model.name,
                file_path = model.file_path,
            };
        }

        public void edit(template_binding_model? model) {
            if (model == null) {
                return;
            }
            name = model.name;
            file_path = model.file_path;
        }
    }
}
