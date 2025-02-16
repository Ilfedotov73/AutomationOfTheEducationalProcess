﻿using contracts.interactor_contracts;
using contracts.presenter_contracts;
using contracts.search_models;
using contracts.view_moedels;

namespace presenter {
    public class template_presenter : Itemplate_presenter {
        
        private readonly Itemplate_logic _logic;

        public template_presenter(Itemplate_logic logic) {
            _logic = logic;
        }
        
        public template_view_model make_template_presenter(template_search_model search_model) {
            var model = _logic.get_template_info(search_model);
            var newViewModel = new template_view_model {
                id = model.id,
                name = model.name,
                author_name = model.user.fio,
                document_type = model.document_type.ToString()
            };
            return newViewModel;
        }

        public List<template_view_model> make_template_list_presenter() {
            var models = _logic.get_template_list();
            List<template_view_model> newViewModels = new();

            foreach (var item in models) {
                newViewModels.Add(new template_view_model {
                    id = item.id,
                    name = item.name,
                });
            }
            return newViewModels;
        }
    }
}
