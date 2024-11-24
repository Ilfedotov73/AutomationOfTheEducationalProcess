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
        public List<template_binding_model> get_template_filltered_lst(template_search_model search_model);
        public template_binding_model get_template_info(template_search_model search_model);
        public void insert_template(template_binding_model model);
        public void delete_template(template_binding_model model);
        public void edit_template(template_binding_model model);
        public byte[] on_export_template(template_search_model search_model);
    }
}
