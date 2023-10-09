using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FEChapterRando.DataClasses
{
    class CharacterModule : Module
    {
        public string characterID = "";
        public ArrayList animationModuleIDs = new ArrayList();
        public ArrayList supportPairs = new ArrayList();

        public CharacterModule(string id, CollectionBase animationModules, CollectionBase supports)
        {
            characterID = id;
            animationModuleIDs.AddRange(animationModules);
            supportPairs.AddRange(supports);
        }

    }
}
