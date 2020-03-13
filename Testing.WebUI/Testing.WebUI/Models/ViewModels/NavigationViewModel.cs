using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testing.WebUI.Models.ViewModels
{
    public class NavigationViewModel
    {
        public IEnumerable<string> categories { get; set; }
        public IEnumerable<string> Complexity { get; set; }
    }
}