using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace WinFormsPaczkomat
{
    public partial class Form1 : Form
    {
        // Initializing general usage fields
        static readonly string userName = Environment.UserName;
        static readonly string startingPath = @$"C:\Users\{userName}\Desktop\";

        // Arrays of files to compress
        static List<string> filesToArchiveFullNames = new List<string>();
        static List<string> filesToArchiveNames = new List<string>();
        static List<string> foldersToArchiveFullNames = new List<string>();
        static List<string> foldersToArchiveNames = new List<string>();

        // Archiving-related fiels
        static string newZipFullName;
        static string newZipFolderLocation;
        static string newZipName = "Archiwum";

        // Memorizing last created zip folder location - increase user experience
        static string lastZipFolderLocation = null;

        // Field containing selected by user option when archive with selected path exists already
        static string fileExistsOption = String.Empty;

        // Dearchiving-related fields
        static string pathOfArch;
        static string unpackTargetLocation;

        // Compression level field - medium level as default
        static int compressionLevel = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
