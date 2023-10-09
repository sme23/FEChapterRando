using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FEChapterRando.DataClasses
{
    class ChapterModule : Module
    {

        public string chapterID = "";
        public int chapterNumber = -1;
        public ArrayList characterModuleIDs = new ArrayList();
        public ArrayList classModuleIDs = new ArrayList();
        public ArrayList itemModuleIDs = new ArrayList();

        public ChapterModule(string id, int number, CollectionBase characterModules, CollectionBase classModules, CollectionBase itemModules)
        {
            chapterID = id;
            chapterNumber = number;
            characterModuleIDs.AddRange(characterModules);
            classModuleIDs.AddRange(classModules);
            itemModuleIDs.AddRange(itemModules);
        }

    }
}
