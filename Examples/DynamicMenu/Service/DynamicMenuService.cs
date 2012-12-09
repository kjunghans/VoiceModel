using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicMenu.Service
{
    public class DynamicMenuService
    {
        Dictionary<string, VoiceMenu> _menus;
        string[] _numbers = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        public DynamicMenuService()
        {
            _menus = new Dictionary<string, VoiceMenu>();

            VoiceMenu myMenu = new VoiceMenu() { Name = "myMenu" };
            myMenu.Options.Add(new MenuOption() { Number = 1, PromptMsg = "To do this", TransitionTarget = "doThis" });
            myMenu.Options.Add(new MenuOption() { Number = 2, PromptMsg = "To do that", TransitionTarget = "doThat" });
            myMenu.Options.Add(new MenuOption() { Number = 3, PromptMsg = "To do whatever", TransitionTarget = "doWhatever" });
            _menus.Add(myMenu.Name, myMenu);

        }

        public VoiceMenu GetMenu(string name)
        {
            VoiceMenu menu = null;
            _menus.TryGetValue(name, out menu);
            return menu;
        }

        public string GetSelectionPrompt(int selectNum)
        {
            return ", press " + _numbers[selectNum] + ".";
        }
    }
}