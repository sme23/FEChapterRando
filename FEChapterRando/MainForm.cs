using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FEChapterRando
{
    public partial class MainForm : Form
    {
        RandomizerSettings settings = new RandomizerSettings();
        ArrayList allChapters = new ArrayList();

        public MainForm()
        {
            InitializeComponent();
        }

        private void StartRandomization_Click(object sender, EventArgs e)
        {
            //this is the button that does the randomization
            RandomizerEngine randomizer = new RandomizerEngine(settings);
            randomizer.Randomize();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //init the collection of available chapter modules from CWD\Backend\Modules\Chapter
            //including subdirectories for sorting reasons; we're looking for a folder with a ChapterModule.yaml in it, otherwise we look in its subfolders

            string baseDir = Directory.GetCurrentDirectory() + "\\Backend\\Modules\\Chapters";

            ArrayList modulePaths = new ArrayList();
            ArrayList folderCheckQueue = new ArrayList();
            
            folderCheckQueue.Add(baseDir);

            while (folderCheckQueue.Count > 0)
            {
                if (!File.Exists(folderCheckQueue[0] + "\\ChapterModule.yaml"))
                {
                    //not a chapter, add all subdirectories
                    foreach (string folder in Directory.EnumerateDirectories((string)folderCheckQueue[0])) {
                        folderCheckQueue.Add(folder);
                    }
                }
                else
                {
                    //this is a chapter, add it to all chapters
                    modulePaths.Add(folderCheckQueue[0]);
                }
                //remove head of queue
                folderCheckQueue.RemoveAt(0);
            }

            //deserialize objects from .yaml in each folder
            IDeserializer des = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();

            foreach (string path in modulePaths)
            {
                FileStream file = File.Open(path+"\\ChapterModule.yaml", FileMode.Open);
                Span<byte> buffer = new Span<byte>();
                file.Read(buffer);
                string yaml = Encoding.ASCII.GetString(buffer.ToArray());
                DataClasses.ChapterModule chapter = des.Deserialize<DataClasses.ChapterModule>(yaml);
                chapter.filePath = path;
                allChapters.Add(chapter);
                file.Close();
            }

            //default settings to all chapters selected
            settings.chapterSet.AddRange(allChapters);
        
        }
    }
}
