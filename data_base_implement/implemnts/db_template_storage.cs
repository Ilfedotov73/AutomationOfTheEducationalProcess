using contracts.binding_models;
using contracts.search_models;
using contracts.storage_contracts;
using contracts.storage_contracts.db_models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_base_implement.implemnts {
    public class db_template_storage : Itemplate_storage {
        public bool insert_template(template_binding_model model) {
            var new_rec = Template.insert(model);
            if (new_rec == null) {
                return false;
            }
            using var context = new data_base();
            context.templates.Add(new_rec);
            context.SaveChanges();
            return true;
        }

        public bool edit_tempalte(template_binding_model model) {
            using var context = new data_base();
            var edit_rec = context.templates.FirstOrDefault(x => x.id == model.id);
            if (edit_rec == null) {
                return false;
            }
            edit_rec.edit(model);
            context.SaveChanges();
            return true;
        }

        public bool delete_template(template_binding_model model) {
            using var context = new data_base();
            var del_rec = context.templates.FirstOrDefault(x => x.id == model.id);
            if (del_rec == null) {
                return false;
            }
            context.templates.Remove(del_rec);
            context.SaveChanges();
            return true;
        }

        public List<Template> get_template_list() {
            using var context = new data_base();
            return context.templates
                .Include(x => x.documents)
                .ToList();
        }

        public Template? get_template_info(template_search_model search_model) {
            using var context = new data_base();
            if (search_model.id.HasValue) {
                return context.templates
                    .Include(x => x.documents)
                    .FirstOrDefault(x => x.id == search_model.id);
            }
            if (!string.IsNullOrEmpty(search_model.name)) {
                return context.templates
                    .Include(x => x.documents)
                    .FirstOrDefault(x => x.name == search_model.name);
            }
            return null;
        }
    }
}