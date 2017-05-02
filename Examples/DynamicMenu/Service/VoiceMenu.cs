using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicMenu.Service
{
    public class VoiceMenu
    {
        public string Name { get; set; }
        public List<MenuOption> Options { get; set; }

        public VoiceMenu()
        {
            Options = new List<MenuOption>();
        }
    }
}