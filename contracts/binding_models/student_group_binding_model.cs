﻿using contracts.storage_contracts.db_models;
using data_models.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.binding_models {
    public class student_group_binding_model : Istudent_group {
        public int id { get; set; }
        public int DirectionId { get; set; }
        public int course_num { get; set; }
        public int semester_num { get; set; }
        public int group_num { get; set; }

        public Direction? direction { get; set; }
        public List<Student>? students { get; set; }
    }
}
