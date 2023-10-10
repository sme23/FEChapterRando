using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FEChapterRando.DataClasses
{
    class CharacterModule : Module
    {
        public string characterID = "";
        public string type = "";
        public ArrayList animationModuleIDs = new ArrayList();
        public ArrayList supportPairs = new ArrayList();

        public CharacterModule(string id, string chartype, ArrayList animationModules, ArrayList supports)
        {
            characterID = id;
            type = chartype;
            animationModuleIDs.AddRange(animationModules);
            supportPairs.AddRange(supports);
        }

    }
}
