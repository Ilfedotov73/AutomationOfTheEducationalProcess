﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_models.IModels {
    public interface Istudent_group  : Iid {
        int DirectionId { get; }
        int course_num { get; }
        int semester_num { get; }
        int group_num { get; }
    }
}
