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

        public CharacterModule()
        {
        }

    }
}
