using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicMenu.Service
{
    public class MenuOption
    {
        public int Number { get; set; }
        public string PromptMsg { get; set; }
        public string TransitionTarget { get; set; }
    }
}