using contracts.binding_models;
using contracts.interactor_contracts;
using contracts.presenter_contracts;
using contracts.search_models;
using contracts.view_moedels;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationRestAPI.Controllers {

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller {

        private readonly Iuser_logic _userLogic;
        private readonly Iuser_presenter _userPresenter; 

        public UserController(Iuser_logic userLogic, Iuser_presenter userPresenter) {
            _userLogic = userLogic;
            _userPresenter = userPresenter;
        }

        [HttpPost]
        public void register(user_binding_model model) {
            try {
                _userLogic.insert_user(model);
            }
            catch (Exception ex) {
                throw;
            }
        }

        [HttpGet]
        public user_view_model? login(string email, string password) {
            try {
                return _userPresenter.make_user_presenter(new user_search_model {
                    email = email,
                    password = password
                });
            }
            catch (Exception ex) {
                throw;
            }
        }

        [HttpPost]
        public void edit(user_binding_model model) {
            try {
                _userLogic.edit_user(model);
            }
            catch (Exception ex) {
                throw;
            }
        }

        [HttpGet]
        public List<template_view_model>? get_user_template_list(int userId) {
            try {
                return _userPresenter.make_user_presenter(new user_search_model { id = userId }).templates;
            }
            catch (Exception ex) {
                throw;
            }
        }
    }
}
