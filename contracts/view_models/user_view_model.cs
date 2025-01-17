using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.view_moedels {
    public class user_view_model {
        public int id { get; set; }
        public string fio { get; set; } = string.Empty;
        public string faculty_name { get; set; } = string.Empty;
        public string department_name { get; set; } = string.Empty;
        public string position { get; set; } = string.Empty;
        public string year_of_birth { get; set; } = string.Empty;
        public string academic_degree { get; set; } = string.Empty;
        public string year_of_award_ad { get; set; } = string.Empty;
        public string academic_title { get; set; } = string.Empty;
        public string year_of_award_at { get; set; } = string.Empty;
        public string password {  get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;

        public List<template_view_model> templates { get; set; } = new();

        public List<string> groups { get; set; } = new();

        public string[] _fio => fio.Split(new char[] { ' ' });
    }
}
