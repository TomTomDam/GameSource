﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameSource.Models.GameSource
{
    public class Publisher
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}