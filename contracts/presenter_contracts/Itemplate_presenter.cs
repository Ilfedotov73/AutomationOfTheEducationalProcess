using contracts.search_models;
using contracts.view_moedels;

namespace contracts.presenter_contracts {
    public interface Itemplate_presenter {
        public template_view_model make_template_presenter(template_search_model search_model);
        public List<template_view_model> make_template_list_presenter();
    }
}
