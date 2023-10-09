using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FEChapterRando
{
    class RandomizerSettings
    {
        public int seed = 0;
        public int chapterVariance = 2;
        public ArrayList chapterSet = new ArrayList();
        public bool allowRepeatChapters = false;


        public RandomizerSettings() { }
    }
}
