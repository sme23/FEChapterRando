using FEChapterRando.DataClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FEChapterRando
{
    class RandomizerEngine
    {
        RandomizerSettings settings;
        Random rng;

        public RandomizerEngine(RandomizerSettings settings)
        {
            this.settings = settings;
            rng = new Random(settings.seed);
        }

        public void Randomize()
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
                    if (chapter.chapterNumber-settings.chapterVariance <= i || i <= chapter.chapterNumber+settings.chapterVariance ) currentSet.Add(chapter);
                }
                //if there's nothing in the currentSet, exit the loop
                if (currentSet.Count == 0) break;

                //pick a random object from this set and add it to chapters, then remove it from settings.chapterSet
                int rval = rng.Next(0,currentSet.Count-1);
                chapters.Add(currentSet[rval]);
                if (!settings.allowRepeatChapters) settings.chapterSet.Remove(currentSet[rval]);
            }
         
            //gather up a list of all of the resources we'll need and their locations, for use when constructing the installer; all used resources are downstream from chapter modules

        }

        


    }
}
