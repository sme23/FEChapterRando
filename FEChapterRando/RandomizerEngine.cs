using FEChapterRando.DataClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FEChapterRando
{
    class RandomizerEngine
    {
        RandomizerSettings settings;
        Random rng;

        //After randomization, the sets of each type of module we're going to use are placed in these collections
        ArrayList chapterModuleSet = new ArrayList();
        ArrayList characterModuleSet = new ArrayList();
        ArrayList classModuleSet = new ArrayList();
        ArrayList itemModuleSet = new ArrayList();
        ArrayList mapModuleSet = new ArrayList();
        ArrayList animationModuleSet = new ArrayList();
        ArrayList resourceModuleSet = new ArrayList();

        Dictionary<string, int> vanillaClassLookup = new Dictionary<string, int>();


        public RandomizerEngine(RandomizerSettings settings)
        {
            this.settings = settings;
            rng = new Random(settings.seed);

            //init vanillaClassLookup

            vanillaClassLookup.Add("FE8_EphraimLord", 1);
            vanillaClassLookup.Add("FE8_EirikaLord", 2);
            vanillaClassLookup.Add("FE8_EphraimGreatLord", 3);
            vanillaClassLookup.Add("FE8_EirikaGreatLord", 4);
            vanillaClassLookup.Add("FE8_Cavalier", 5);
            vanillaClassLookup.Add("FE8_CavalierF", 6);
            vanillaClassLookup.Add("FE8_Paladin", 7);
            vanillaClassLookup.Add("FE8_PaladinF", 8);
            vanillaClassLookup.Add("FE8_Knight", 9);
            vanillaClassLookup.Add("FE8_KnightF", 10);
            vanillaClassLookup.Add("FE8_General", 11);
            vanillaClassLookup.Add("FE8_GeneralF", 12);
            vanillaClassLookup.Add("FE8_Thief", 13);
            vanillaClassLookup.Add("FE8_ManaketeUnused", 14);
            vanillaClassLookup.Add("FE8_Mercenary", 15);
            vanillaClassLookup.Add("FE8_MercenaryF", 16);
            vanillaClassLookup.Add("FE8_Hero", 17);
            vanillaClassLookup.Add("FE8_HeroF", 18);
            vanillaClassLookup.Add("FE8_Myrmidon", 19);
            vanillaClassLookup.Add("FE8_MyrmidonF", 20);
            vanillaClassLookup.Add("FE8_Swordmaster", 21);
            vanillaClassLookup.Add("FE8_SwordmasterF", 22);
            vanillaClassLookup.Add("FE8_Assassin", 23);
            vanillaClassLookup.Add("FE8_AssassinF", 24);
            vanillaClassLookup.Add("FE8_Archer", 25);
            vanillaClassLookup.Add("FE8_ArcherF", 26);
            vanillaClassLookup.Add("FE8_Sniper", 27);
            vanillaClassLookup.Add("FE8_SniperF", 28);
            vanillaClassLookup.Add("FE8_Ranger", 29);
            vanillaClassLookup.Add("FE8_RangerF", 30);
            vanillaClassLookup.Add("FE8_WyvernRider", 31);
            vanillaClassLookup.Add("FE8_WyvernRiderF", 32);
            vanillaClassLookup.Add("FE8_WyvernLord", 33);
            vanillaClassLookup.Add("FE8_WyvernLordF", 34);
            vanillaClassLookup.Add("FE8_WyvernKnight", 35);
            vanillaClassLookup.Add("FE8_WyvernKnightF", 36);
            vanillaClassLookup.Add("FE8_Mage", 37);
            vanillaClassLookup.Add("FE8_MageF", 38);
            vanillaClassLookup.Add("FE8_Sage", 39);
            vanillaClassLookup.Add("FE8_SageF", 40);
            vanillaClassLookup.Add("FE8_MageKnight", 41);
            vanillaClassLookup.Add("FE8_MageKnightF", 42);
            vanillaClassLookup.Add("FE8_Bishop", 43);
            vanillaClassLookup.Add("FE8_BishopF", 44);
            vanillaClassLookup.Add("FE8_Shaman", 45);
            vanillaClassLookup.Add("FE8_ShamanF", 46);
            vanillaClassLookup.Add("FE8_Druid", 47);
            vanillaClassLookup.Add("FE8_DruidF", 48);
            vanillaClassLookup.Add("FE8_Summoner", 49);
            vanillaClassLookup.Add("FE8_SummonerF", 50);
            vanillaClassLookup.Add("FE8_Rogue", 51);
            vanillaClassLookup.Add("FE8_GorgonEgg", 52);
            vanillaClassLookup.Add("FE8_GreatKnight", 53);
            vanillaClassLookup.Add("FE8_GreatKnightF", 54);
            vanillaClassLookup.Add("FE8_Recruit2", 55);
            vanillaClassLookup.Add("FE8_Journeyman3", 56);
            vanillaClassLookup.Add("FE8_Pupil3", 57);
            vanillaClassLookup.Add("FE8_Recruit3", 58);
            vanillaClassLookup.Add("FE8_ManaketeMorva", 59);
            vanillaClassLookup.Add("FE8_ManaketeMyrrh", 60);
            vanillaClassLookup.Add("FE8_Journeyman1", 61);
            vanillaClassLookup.Add("FE8_Pupil1", 62);
            vanillaClassLookup.Add("FE8_Fighter", 63);
            vanillaClassLookup.Add("FE8_Warrior", 64);
            vanillaClassLookup.Add("FE8_Brigand", 65);
            vanillaClassLookup.Add("FE8_Pirate", 66);
            vanillaClassLookup.Add("FE8_Berserker", 67);
            vanillaClassLookup.Add("FE8_Monk", 68);
            vanillaClassLookup.Add("FE8_Priest", 69);
            vanillaClassLookup.Add("FE8_Bard", 70);
            vanillaClassLookup.Add("FE8_Recruit1", 71);
            vanillaClassLookup.Add("FE8_PegasusKnight", 72);
            vanillaClassLookup.Add("FE8_Falcoknight", 73);
            vanillaClassLookup.Add("FE8_Cleric", 74);
            vanillaClassLookup.Add("FE8_Troubadour", 75);
            vanillaClassLookup.Add("FE8_Valkyrie", 76);
            vanillaClassLookup.Add("FE8_Dancer", 77);
            vanillaClassLookup.Add("FE8_Soldier", 78);
            vanillaClassLookup.Add("FE8_Necromancer", 79);
            vanillaClassLookup.Add("FE8_Fleet", 80);
            vanillaClassLookup.Add("FE8_Phantom", 81);
            vanillaClassLookup.Add("FE8_Revenant", 82);
            vanillaClassLookup.Add("FE8_Entombed", 83);
            vanillaClassLookup.Add("FE8_Bonewalker", 84);
            vanillaClassLookup.Add("FE8_BonewalkerBow", 85);
            vanillaClassLookup.Add("FE8_Wight", 86);
            vanillaClassLookup.Add("FE8_WightBow", 87);
            vanillaClassLookup.Add("FE8_Bael", 88);
            vanillaClassLookup.Add("FE8_ElderBael", 89);
            vanillaClassLookup.Add("FE8_Cyclops", 90);
            vanillaClassLookup.Add("FE8_MautheDoog", 91);
            vanillaClassLookup.Add("FE8_Gwyllgi", 92);
            vanillaClassLookup.Add("FE8_Tarvos", 93);
            vanillaClassLookup.Add("FE8_Maelduin", 94);
            vanillaClassLookup.Add("FE8_Mogall", 95);
            vanillaClassLookup.Add("FE8_ArchMogall", 96);
            vanillaClassLookup.Add("FE8_Gorgon", 97);
            vanillaClassLookup.Add("FE8_GorgonEgg2", 98);
            vanillaClassLookup.Add("FE8_Gargoyle", 99);
            vanillaClassLookup.Add("FE8_Deathgoyle", 100);
            vanillaClassLookup.Add("FE8_DracoZombie", 101);
            vanillaClassLookup.Add("FE8_DemonKing", 102);
            vanillaClassLookup.Add("FE8_BallistaMounted1", 103);
            vanillaClassLookup.Add("FE8_BallistaMounted2", 104);
            vanillaClassLookup.Add("FE8_BallistaMounted3", 105);
            vanillaClassLookup.Add("FE8_Ballista1", 106);
            vanillaClassLookup.Add("FE8_Ballista2", 107);
            vanillaClassLookup.Add("FE8_Ballista3", 108);
            vanillaClassLookup.Add("FE8_CivilianChild", 109);
            vanillaClassLookup.Add("FE8_CivilianChildF", 110);
            vanillaClassLookup.Add("FE8_Civilian", 111);
            vanillaClassLookup.Add("FE8_CivilianF", 112);
            vanillaClassLookup.Add("FE8_Civilian2", 113);
            vanillaClassLookup.Add("FE8_Civilian2F", 114);
            vanillaClassLookup.Add("FE8_Peer", 115);
            vanillaClassLookup.Add("FE8_Queen", 116);
            vanillaClassLookup.Add("FE8_Prince", 117);
            vanillaClassLookup.Add("FE8_Queen2", 118);
            vanillaClassLookup.Add("FE8_LightRune", 119);
            vanillaClassLookup.Add("FE8_FallenPrince", 120);
            vanillaClassLookup.Add("FE8_Tent", 121);
            vanillaClassLookup.Add("FE8_Pontifex", 122);
            vanillaClassLookup.Add("FE8_FallenPeer", 123);
            vanillaClassLookup.Add("FE8_Cyclops2", 124);
            vanillaClassLookup.Add("FE8_ElderBael2", 125);
            vanillaClassLookup.Add("FE8_Journeyman2", 126);
            vanillaClassLookup.Add("FE8_Pupil2", 127);

        }

        public string[] GenerateOutputFile()
        {
            //this function is run after Randomize() and generates an output file as a string array based on what each assigned ID is
            ArrayList outputFile = new ArrayList();

            outputFile.Add("//FILE GENERATED BY CHAPTER RANDOMIZER\n");
            outputFile.Add("//SEED USED: " + settings.seed + "\n\n");

            outputFile.Add("//Chapter Definitions\n");
            foreach (ChapterModule chapter in chapterModuleSet)
            {
                outputFile.Add("#define " + chapter.chapterID + "_Index " + chapter.moduleID + "\n");
                outputFile.Add("#define " + chapter.chapterID + "_Events " + chapter.chapterEvents + "\n");
            }
            outputFile.Add("\n//Character Definitions\n");
            foreach (CharacterModule character in characterModuleSet)
            {
                outputFile.Add("#define " + character.characterID + " " + character.moduleID + "\n");
            }
            outputFile.Add("\n//Class Definitions\n");
            foreach (ClassModule classmod in classModuleSet)
            {
                outputFile.Add("#define " + classmod.classID + " " + classmod.moduleID + "\n");
            }
            outputFile.Add("\n//Item Definitions\n");
            foreach (ItemModule item in itemModuleSet)
            {
                outputFile.Add("#define " + item.itemID + " " + item.moduleID + "\n");
            }
            outputFile.Add("\n//Map Definitions\n");
            foreach (MapModule map in mapModuleSet)
            {
                outputFile.Add("#define " + map.mapID + " " + map.moduleID + "\n");
                outputFile.Add("#define " + map.mapID + "_MapChanges " + map.mapChanges + "\n");
            }
            outputFile.Add("\n//Animation Definitions\n");
            foreach (AnimationModule anim in animationModuleSet)
            {
                outputFile.Add("#define " + anim.animationID + " " + anim.moduleID + "\n");
            }
            outputFile.Add("\n//Resource Definitions\n");
            foreach (ResourceModule resource in resourceModuleSet)
            {
                outputFile.Add("#define " + resource.resourceID + " " + resource.moduleID + "\n");
            }

            // now we need to get the include paths
            // this is going to be a substring of the stored path, since the stored path is the complete path and not just the relative path to this file
            // the file goes in CWD/Backend so we should first get that as a string
            string cullPath = Directory.GetCurrentDirectory() + "\\Backend\\";
            int cullLength = cullPath.Length;

            //now we write out our includes
            outputFile.Add("\n\n//Module Includes\n");
            foreach (ChapterModule chapter in chapterModuleSet)
            {
                outputFile.Add("#include \"" + chapter.filePath.Substring(cullLength) + "\\ChapterInstaller.event\"\n");
            }
            foreach (CharacterModule character in characterModuleSet)
            {
                outputFile.Add("#include \"" + character.filePath.Substring(cullLength) + "\\CharacterInstaller.event\"\n");
            }
            foreach (ClassModule classmod in classModuleSet)
            {
                outputFile.Add("#include \"" + classmod.filePath.Substring(cullLength) + "\\ClassInstaller.event\"\n");
            }
            foreach (ItemModule item in itemModuleSet)
            {
                outputFile.Add("#include \"" + item.filePath.Substring(cullLength) + "\\ItemInstaller.event\"\n");
            }
            foreach (MapModule map in mapModuleSet)
            {
                outputFile.Add("#include \"" + map.filePath.Substring(cullLength) + "\\MapInstaller.event\"\n");
            }
            foreach (AnimationModule anim in animationModuleSet)
            {
                outputFile.Add("#include \"" + anim.filePath.Substring(cullLength) + "\\AnimationInstaller.event\"\n");
            }
            foreach (ResourceModule resource in resourceModuleSet)
            {
                outputFile.Add("#include \"" + resource.filePath.Substring(cullLength) + "\\ResourceInstaller.event\"\n");
            }
            outputFile.Add("\n//END OF FILE\n");

            //file is generated, return as string array
            return (string[])outputFile.ToArray(typeof(string));
        }



        // Returns 0 if randomization completes, -1 if there's an issue
        public int Randomize()
        {
            //each ChapterModule object in settings.chapterSet has a chapterNumber value
            //what we select for each chapter comes from the set of chapters whose chapterNumber +/- settings.chapterVariance is that chapter's index
            //so we should go through that ArrayList and for each ChapterModule if it falls within the range 
            //then do that until we find a set with 0 chapters in it, and we're done
            // ^^ this is a bad idea actually, if there is a set of size 1 then we always want that one, but with variance it could fall into another set and create a set of size 0
            // with variance it can also fall back from higher points and fill in those gaps from indices that it couldn't have gotten to before
            // even for being the last ones in the list they fall back to earlier indices and you just get a slightly shorter but still complete list. you would need actual gaps to be problematic

            ArrayList chapters = new ArrayList();

            for (int i = 0; ; i++)
            {
                ArrayList currentSet = new ArrayList();
                foreach (ChapterModule chapter in settings.chapterSet)
                {
                    //if i is within the range of chapter.chapterNumber-settings.chapterVariance to chapter.chapterNumber+settings.chapterVariance add chapter to collection
                    if (chapter.chapterNumber-settings.chapterVariance <= i && i <= chapter.chapterNumber+settings.chapterVariance ) currentSet.Add(chapter);
                }
                //if there's nothing in the currentSet, exit the loop
                if (currentSet.Count == 0) break;

                //pick a random object from this set and add it to chapters, then remove it from settings.chapterSet
                int rval = rng.Next(0,currentSet.Count-1);
                chapters.Add(currentSet[rval]);
                if (!settings.allowRepeatChapters) settings.chapterSet.Remove(currentSet[rval]);
            }

            //gather up a list of all of the resources we'll need and their locations, for use when constructing the installer; all used resources are downstream from chapter modules
            ArrayList localModules = new ArrayList();
            localModules.AddRange(chapters);

            foreach (ChapterModule chapter in chapters)
            {
                //get the submodules of the chapter module
                localModules.AddRange(gatherSubmodules(chapter.filePath));
            }

            //check each item in localModules for referenced modules not already in localModules

            for (int i = 0; i < localModules.Count; i++) 
            {
                Module module = (Module)localModules[i];
                //no, C# does not support switch statements on type
                if (module is ChapterModule)
                {
                    //get classModuleIDs, itemModuleIDs in a collection, get objects from yamls for those modules
                    ArrayList chapterReferences = new ArrayList();
                    ChapterModule chmod = (ChapterModule)module;
                    chapterReferences.AddRange(chmod.classModuleIDs);
                    chapterReferences.AddRange(chmod.itemModuleIDs);
                    localModules.AddRange(gatherResourceModules(chapterReferences));
                    chapterModuleSet.Add(chmod);
                }
                if (module is CharacterModule)
                {
                    CharacterModule chmod = (CharacterModule)module;
                    ArrayList charRefs = new ArrayList();
                    charRefs.AddRange(chmod.animationModuleIDs);
                    localModules.AddRange(gatherResourceModules(charRefs));
                    characterModuleSet.Add(chmod);
                }
                if (module is ClassModule)
                {
                    ClassModule clmod = (ClassModule)module;
                    ArrayList classRefs = new ArrayList();
                    classRefs.AddRange(clmod.animationModuleIDs);
                    classRefs.AddRange(clmod.promotionClasses);
                    localModules.AddRange(gatherResourceModules(classRefs));
                    classModuleSet.Add(clmod);
                }
                if (module is ItemModule)
                {
                    //no refs
                    ItemModule itmod = (ItemModule)module;
                    itemModuleSet.Add(itmod);
                }
                if (module is MapModule)
                {
                    MapModule mapmod = (MapModule)module;
                    ArrayList mapRefs = new ArrayList();
                    mapRefs.AddRange(mapmod.mapResourceIDs);
                    localModules.AddRange(gatherResourceModules(mapRefs));
                    mapModuleSet.Add(mapmod);

                }
                if (module is AnimationModule)
                {
                    //no refs
                    AnimationModule anmod = (AnimationModule)module;
                    animationModuleSet.Add(anmod);

                }
                if (module is ResourceModule)
                {
                    //no refs
                    ResourceModule remod = (ResourceModule)module;
                    resourceModuleSet.Add(remod);
                }
            }

            //all local modules have been checked, we now have sorted lists of all modules we are using
            //now we need to enumerate each entry in each list, but first let's check bounds on each list's size

            if (characterModuleSet.Count > 252)
            {
                //error: too many characters, not enough slots
                Console.Error.WriteLine("ERROR: Too many characters, " + characterModuleSet.Count + " needed but only 252 slots");
                return -1;
            }
            if (classModuleSet.Count > 128)
            {
                //error: too many classes, not enough slots
                //the actual error condition on this is going to be different because we aren't going to touch the 127 vanilla FE8 classes,
                //but we only want the count of ones that aren't in that group
                Console.Error.WriteLine("ERROR: Too many classes, " + classModuleSet.Count + " needed but only 128 slots");
                Console.Error.WriteLine("(You can actually ignore the class error and probably be fine, it isn't set up properly yet)");
                return -1;
            }
            if (itemModuleSet.Count > 255)
            {
                //error: too many items, not enough slots
                Console.Error.WriteLine("ERROR: Too many items, " + itemModuleSet.Count + " needed but only 255 slots");
                return -1;
            }
            if (chapterModuleSet.Count > 127)
            {
                //error: too many chapters, not enough slots
                //hoping and praying that this error never happens, and would require active work on the user's part to cause it
                Console.Error.WriteLine("ERROR: Too many chapters, " + chapterModuleSet.Count + " needed but only 126 slots, How did you accomplish this");
                return -1;
            }
            if (mapModuleSet.Count*2 + chapterModuleSet.Count + resourceModuleSet.Count > 256)
            {
                //error: too many plist entries
                Console.Error.WriteLine("ERROR: Too many unique chapter elements, " + (mapModuleSet.Count * 2 + chapterModuleSet.Count + resourceModuleSet.Count) + " needed but only 256 slots");
                return -1;
            }
            if (animationModuleSet.Count > 65536)
            {
                //error: too many animations (actually how)
                //like. this isnt even one you can do by accident
                Console.Error.WriteLine("Conglaturation! You managed to have enough unique animations to end time itself! This universe will now collapse.");
                return -1;
            }

            //ok now we can enumerate objects
            //start with the ones that just enumerate 0-max
            int j = 0;
            foreach (ChapterModule chapter in chapterModuleSet)
            {
                chapter.moduleID = j;
                j++;
            }
            j = 0;
            foreach (ItemModule item in itemModuleSet)
            {
                item.moduleID = j;
                j++;
            }

            //class modules get sorted into vanilla FE8 and new, only new ones get enumerated old ones pull from the dict initialized with this object
            ArrayList classWithModules = new ArrayList();
            foreach (ClassModule clmod in classModuleSet)
            {
                if (vanillaClassLookup.ContainsKey(clmod.classID))
                {
                    clmod.moduleID = vanillaClassLookup[clmod.classID];
                    continue;
                }
                classWithModules.Add(clmod);
            }

            j = 128;
            foreach (ClassModule clmod in classWithModules)
            {
                clmod.moduleID = j;
                j++;
            }

            //character modules have a type to them that determine the order that we put them in
            //PCs go at the start, bosses start at 0x48, generics follow bosses
            //the type field is one of PLAYER, BOSS, GENERIC
            //if it's anything else then we throw an error, simply don't put an invalid type 4head
            //sort them by type

            ArrayList playerChars = new ArrayList();
            ArrayList bossChars = new ArrayList();
            ArrayList genericChars = new ArrayList();

            foreach (CharacterModule character in characterModuleSet)
            {
                switch (character.type)
                {
                    case "PLAYER":
                        playerChars.Add(character);
                        break;
                    case "BOSS":
                        bossChars.Add(character);
                        break;
                    case "GENERIC":
                        genericChars.Add(character);
                        break;
                    default:
                        Console.Error.WriteLine("ERROR: Invalid type " + character.type + " on character module " + character.characterID);
                        return -1;
                }
            }

            //now we do them sequentially in order
            j = 0;
            foreach (CharacterModule character in playerChars)
            {
                character.moduleID = j;
                j++;
            }
            foreach (CharacterModule character in bossChars)
            {
                character.moduleID = j;
                j++;
            }
            foreach (CharacterModule character in genericChars)
            {
                character.moduleID = j;
                j++;
            }

            //remaining are animation - map - resource
            //start with animation since that's simple enough

            j = 0;
            foreach (AnimationModule anim in animationModuleSet)
            {
                anim.moduleID = j;
                j++;
            }

            //maps, resources, and chapters are all tied together: maps get 2, resources and chapters get 1. this is for the event pointer table
            j = 0;
            foreach (MapModule map in mapModuleSet)
            {
                map.moduleID = j;
                j++;
                map.mapChanges = j;
                j++;
            }
            foreach (ChapterModule chapter in chapterModuleSet)
            {
                chapter.chapterEvents = j;
                j++;
            }
            foreach (ResourceModule resource in resourceModuleSet)
            {
                resource.moduleID = j;
                j++;
            }
            
            //everything now has IDs assigned to them, we're done in this function

            return 0;
        }


        private ArrayList gatherResourceModules(ArrayList moduleIDs)
        {
            //get the set of all resource modules
            ArrayList resourceModules = gatherAllResourceModules();
            ArrayList pickedModules = new ArrayList();

            foreach (Object module in resourceModules)
            {
                //once again, no, switch statement wouldn't work
                if (module is ClassModule)
                {
                    ClassModule clmod = (ClassModule)module;
                    //if its id exists in moduleIDs, add to pickedModules
                    if (moduleIDs.Contains(clmod.classID)) pickedModules.Add(clmod);
                }
                if (module is ItemModule)
                {
                    ItemModule itmod = (ItemModule)module;
                    if (moduleIDs.Contains(itmod.itemID)) pickedModules.Add(itmod);
                }
                if (module is ResourceModule)
                {
                    ResourceModule remod = (ResourceModule)module;
                    if (moduleIDs.Contains(remod.resourceID)) pickedModules.Add(remod);
                }
            }

            return pickedModules;

        }

        private ArrayList gatherAllResourceModules()
        {
            string baseDirA = Directory.GetCurrentDirectory() + "\\Backend\\Modules\\Class";
            string baseDirB = Directory.GetCurrentDirectory() + "\\Backend\\Modules\\Item";
            string baseDirC = Directory.GetCurrentDirectory() + "\\Backend\\Modules\\Resource";

            ArrayList resourceModules = new ArrayList();

            resourceModules.AddRange(gatherSubmodules(baseDirA));
            resourceModules.AddRange(gatherSubmodules(baseDirB));
            resourceModules.AddRange(gatherSubmodules(baseDirC));

            return resourceModules;
        }


        private ArrayList gatherSubmodules(string modulePath)
        {
            ArrayList paths = gatherSubmodulePaths(modulePath);
            ArrayList modules = new ArrayList();
            IDeserializer des = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
            foreach (string path in paths)
            {
                //get the yaml in this filepath
                string[] yamls = Directory.GetFiles(path, "*.yaml");
                //this should always be size 1 so we can just reference index 0 of the array
                FileStream file = File.OpenRead(yamls[0]);
                Span<byte> buffer = new Span<byte>();
                file.Read(buffer);
                string yaml = Encoding.ASCII.GetString(buffer.ToArray());
                modules.Add(des.Deserialize(yaml));
                file.Close();
            }
            return modules;
        }

        private ArrayList gatherSubmodulePaths(string modulePath)
        {
            //get all the submodules located within subdirectories of the folder given as a parameter into an ArrayList and return it
            ArrayList submodules = new ArrayList();

            ArrayList folderQueue = new ArrayList();
            folderQueue.Add(modulePath); //already done in the loop

            while (folderQueue.Count != 0)
            {
                string[] files = Directory.GetFiles((string)folderQueue[0]);
                bool isModule = false;
                foreach (string file in files)
                {
                    if (file.Substring(file.LastIndexOf('.')).Equals(".yaml")) isModule = true;
                }
                if (isModule)
                {
                    //add path to collection
                    submodules.Add(folderQueue[0]);
                }
                
                //add subfolders to queue
                foreach (string folder in Directory.EnumerateDirectories((string)folderQueue[0]))
                {
                    folderQueue.Add(folder);
                }
                
                //dequeue the first element
                folderQueue.RemoveAt(0);
            }
            return submodules;
        }

        


    }
}
