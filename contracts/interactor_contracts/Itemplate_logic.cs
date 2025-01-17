using contracts.binding_models;
using contracts.search_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.interactor_contracts {
    public interface Itemplate_logic {
        public List<template_binding_model> get_template_list();
        public template_binding_model? get_template_info(template_search_model search_model);
        public byte[] insert_template(template_binding_model model);
        public void delete_template(template_binding_model model);
        public void edit_template(template_binding_model model, byte[] rewire_data, string? new_name = null);
        public void check_model(template_binding_model model, bool onDelete, bool onEdit);
        public byte[] on_import_template(template_search_model search_model);
    }
}
