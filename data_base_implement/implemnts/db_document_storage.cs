using contracts.search_models;
using contracts.storage_contracts;
using contracts.storage_contracts.db_models;
using data_models.IModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_base_implement.implemnts {
    public class db_document_storage : Idocument_storage {
        public bool insert_document(Idocument model) {
            var new_rec = Document.insert(model);
            if (new_rec ==  null) {
                return false;
            }
            using var context = new data_base();
            context.documets.Add(new_rec);
            context.SaveChanges();
            return true;
        }

        public bool edit_docuemnt(Idocument model) {
            using var context = new data_base();
            var edit_rec = context.documets.FirstOrDefault(x => x.id == model.id);
            if (edit_rec == null ) {
                return false;
            }
            edit_rec.edit(model);
            context.SaveChanges();
            return true;
        }

        public bool delete_docuemnt(Idocument model) {
            using var context = new data_base();
            var del_rec = context.documets.FirstOrDefault(x => x.id == model.id);
            if (del_rec == null) {
                return false;
            }
            context.documets.Remove(del_rec);
            context.SaveChanges();
            return true;
        }

        public List<Document> get_document_list() {
            using var context = new data_base();
            return context.documets.Include(x => x.user).Include(x => x.template).ToList();
            
        }

        public List<Document> get_document_filltered_list(document_search_model search_model) {
            using var context = new data_base();
            if (search_model.document_type.HasValue) {
                return context.documets
                    .Where(x => x.document_type == search_model.document_type)
                    .Include(x => x.user)
                    .Include(x => x.template).ToList();
            }
            else if (search_model.template_id.HasValue) {
                return context.documets
                    .Where(x => x.TemplateId == search_model.template_id)
                    .Include(x => x.user)
                    .Include(x => x.template).ToList();
            }
            return new();
        }

        public Document? get_document_info(document_search_model search_model) {
            using var context = new data_base();
            if (!string.IsNullOrEmpty(search_model.name)) {
                return context.documets
                    .Include(x => x.user)
                    .Include(x => x.template)
                    .FirstOrDefault(x => x.name == search_model.name);
            }
            if (search_model.id.HasValue) {
                return context.documets
                    .Include(x => x.user)
                    .Include(x => x.template)
                    .FirstOrDefault(x => x.id == search_model.id);
            }
            return null;
        }
    }
}
