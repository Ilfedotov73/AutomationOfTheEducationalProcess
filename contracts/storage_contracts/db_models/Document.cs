using data_models.Enums;
using data_models.IModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.storage_contracts.db_models {
    public class Document : Idocument{
        public int id { get; set; }

        [Required]
        public string name { get; set; } = string.Empty;

        [Required]
        public string file_path { get; set; } = string.Empty;

        public int UserId { get; set; }
        public User? user { get; set; }

        [Required]
        public enum_file_format_type file_format_type { get; set; }

        [Required]
        public enum_document_type document_type { get; set; }

        public int TemplateId { get; set; }
        public Template? template { get; set; }

        public static Document? insert(Idocument? model) {
            if (model == null) {
                return null;
            }
            return new Document() {
                id = model.id,
                name = model.name,
                file_path = model.file_path,
                UserId = model.UserId,
                file_format_type = model.file_format_type,
                document_type = model.document_type,
                TemplateId = model.TemplateId,
            };
        }

        public void edit(Idocument? model) {
            if (model == null) {
                return;
            }
            name = model.name;
            file_path = model.file_path;
            TemplateId = model.TemplateId;
            document_type = model.document_type;
            file_format_type = model .file_format_type;
        }
    }
}
