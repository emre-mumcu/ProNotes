﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProNotes.AppLib.MVC.ViewModels
{
    public class WebExceptionViewModel
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
