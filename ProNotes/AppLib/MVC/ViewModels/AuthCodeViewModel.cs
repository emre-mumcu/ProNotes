using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProNotes.AppLib.MVC.ViewModels
{
    public class AuthCodeViewModel
    {
        public string AuthCode { get; set; } = string.Empty;
        public string RemainingTime { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
