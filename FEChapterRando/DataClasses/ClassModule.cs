using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FEChapterRando.DataClasses
{
    class ClassModule : Module
    {
        public string classID = "";
        public ArrayList animationModuleIDs = new ArrayList();
        public ArrayList promotionItems = new ArrayList();

        public ClassModule(string id, CollectionBase animationModules, CollectionBase promoItems)
        {
            classID = id;
            animationModuleIDs.AddRange(animationModules);
            promotionItems.AddRange(promoItems);
        }

    }
}
