using contracts.binding_models;
using contracts.view_moedels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.presenter_contracts {
    public interface Itemplate_presenter {
        public template_view_model make_template_presenter(template_binding_model model);
        public List<template_view_model> make_template_list_presenter(List<template_binding_model> models);
    }
}
