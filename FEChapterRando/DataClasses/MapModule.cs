using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FEChapterRando.DataClasses
{
    class MapModule : Module
    {
        public string mapID = "";
        public ArrayList mapResourceIDs = new ArrayList();

        public MapModule(string id, CollectionBase mapResources)
        {
            mapID = id;
            mapResourceIDs.AddRange(mapResources);
        }
    }
}
