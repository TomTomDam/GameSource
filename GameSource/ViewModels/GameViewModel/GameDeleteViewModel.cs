﻿using GameSource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.ViewModels.GameViewModel
{
    public class GameDeleteViewModel
    {
        public Game Game { get; set; }
        public Genre Genre { get; set; }
        public Developer Developer { get; set; }
        public Publisher Publisher { get; set; }
        public Platform Platform { get; set; }
    }
}
