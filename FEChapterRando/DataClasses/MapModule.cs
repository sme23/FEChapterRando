using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FEChapterRando.DataClasses
{
    class MapModule : Module
    {
        public string mapID = "";
        public int mapChanges = -1;
        public ArrayList mapResourceIDs = new ArrayList();

        public MapModule()
        {
        }
    }
}
