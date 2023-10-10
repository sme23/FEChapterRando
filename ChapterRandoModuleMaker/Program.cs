using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FEChapterRando.DataClasses;

namespace ChapterRandoModuleMaker
{
    class Program
    {
        static void Main()
        {
            //make a dummy of each data class, serialize, print to console
            AnimationModule anim = new AnimationModule("animation_id");
            ArrayList collectionOfAnims = new ArrayList();
            collectionOfAnims.Add(anim.animationID);

            ResourceModule resource = new ResourceModule("resource_id");
            ArrayList collectionOfResources = new ArrayList();
            collectionOfResources.Add(resource.resourceID);

            ItemModule item = new ItemModule("item_id");
            ArrayList collectionOfItems = new ArrayList();
            collectionOfItems.Add(item.itemID);

            MapModule map = new MapModule("map_id", collectionOfResources);
            ArrayList collectionOfMaps = new ArrayList();
            collectionOfMaps.Add(map.mapID);

            ArrayList emptyArrayList = new ArrayList();
            ClassModule classmod = new ClassModule("class_id", collectionOfAnims, collectionOfItems, emptyArrayList);
            ClassModule classmod2 = new ClassModule("class_id_2", collectionOfAnims, collectionOfItems, emptyArrayList);
            classmod.promotionClasses.Add(classmod2.classID);
            ArrayList collectionOfClasses = new ArrayList();
            collectionOfClasses.Add(classmod.classID);
            collectionOfClasses.Add(classmod2.classID);

            ArrayList exampleSupports = new ArrayList{"char1 + char2 {0, 15}", "char1 + char3 {15, 5}"};

            CharacterModule charmod = new CharacterModule("char_id", "PLAYER", collectionOfAnims, exampleSupports);
            ArrayList collectionOfChars = new ArrayList();
            collectionOfChars.Add(charmod.characterID);

            ChapterModule chapter = new ChapterModule("chapter_id", 69, collectionOfChars, collectionOfClasses, collectionOfItems);

            Console.WriteLine("Begin chapter yaml");
            Console.WriteLine(chapter.ToYAML());
            Console.WriteLine("End chapter yaml\n");

            Console.WriteLine("Begin character yaml");
            Console.WriteLine(charmod.ToYAML());
            Console.WriteLine("End character yaml\n");

            Console.WriteLine("Begin class yaml");
            Console.WriteLine(classmod.ToYAML());
            Console.WriteLine("End class yaml\n");

            Console.WriteLine("Begin item yaml");
            Console.WriteLine(item.ToYAML());
            Console.WriteLine("End item yaml\n");

            Console.WriteLine("Begin map yaml");
            Console.WriteLine(map.ToYAML());
            Console.WriteLine("End map yaml\n");

            Console.WriteLine("Begin animation yaml");
            Console.WriteLine(anim.ToYAML());
            Console.WriteLine("End animation yaml\n");

            Console.WriteLine("Begin resource yaml");
            Console.WriteLine(resource.ToYAML());
            Console.WriteLine("End resource yaml\n");

            Console.ReadLine();

        }
    }
}
