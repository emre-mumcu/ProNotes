using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProNotes.AppLib.MVC.ViewModels
{
    public class TFAViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string RandomKey { get; set; } = string.Empty;
        public string RandomKeyFormatted { get; set; } = string.Empty;
        public string QRCodeData { get; set; } = string.Empty;
    }
}
