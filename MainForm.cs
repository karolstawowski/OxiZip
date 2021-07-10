using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace WinFormsPaczkomat
{
    public partial class MainForm : Form
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

        // Memorizing last created zip location and last selected folder location - increase user experience
        static string lastDestinationLocation = null;

        static string lastFolderLocation = null;

        // Field containing selected by user option when archive with selected path exists already
        static string fileExistsOption = String.Empty;

        // Dearchiving-related fields
        static string pathOfArch;

        static string unpackTargetLocation;

        // Compression level field - medium level as default
        static int compressionLevel = 1;

        public MainForm()
        {
            InitializeComponent();
        }
    }
}
