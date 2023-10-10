using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FEChapterRando.DataClasses
{
    class ClassModule : Module
    {
        public string classID = "";
        public ArrayList animationModuleIDs = new ArrayList();
        public ArrayList promotionItems = new ArrayList();
        public ArrayList promotionClasses = new ArrayList();

        public ClassModule()
        {
        }

    }
}
