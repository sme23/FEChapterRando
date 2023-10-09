using System;
using System.Collections.Generic;
using System.Text;

namespace FEChapterRando.DataClasses
{
    class ItemModule : Module
    {
        public string itemID = "";

        public ItemModule(string id)
        {
            itemID = id;
        }
    }
}
