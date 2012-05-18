using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel
{
    public class OneOf
    {
        public List<Item> Items { get; set; }

        public OneOf()
        {
            Items = new List<Item>();
        }

        public OneOf(List<string> uterances)
        {
            foreach (string uterance in uterances)
                Items.Add(new Item(uterance));
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }
    }
}
