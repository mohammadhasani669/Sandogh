﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Domain.Common
{
    public class Entity
    {
        public int Id { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime InsertTime { get; set; } = DateTime.Now;

    }
}
