﻿using GameSource.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.ViewModels.PlatformViewModel
{
    public class PlatformEditViewModel
    {
        public Platform Platform { get; set; }
        public int PlatformTypeID { get; set; }
        public List<SelectListItem> PlatformTypes { get; set; }
    }
}
