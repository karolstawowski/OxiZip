using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace WinFormsPaczkomat
{
    public partial class Form1 : Form
    {
        // Initializing fields
        static readonly string userName = Environment.UserName;
        static readonly string startingPath = @$"C:\Users\{userName}\Desktop\";

        // Fields of arrays of files to compress
        static List<string> filesToArchiveFullNames = new List<string>();
        static List<string> filesToArchiveNames = new List<string>();
        static List<string> foldersToArchivePaths = new List<string>();
        static List<string> foldersToArchiveNames = new List<string>();

        // Compression-related fiels
        static string newZipFullName;
        static string newZipFolderLocation;
        static string newZipName = "Archiwum";

        // Memorizing last created zip folder location
        static string lastZipFolderLocation = null;

        static string fileExistsOption = String.Empty;

        // Decompression-related fields
        static string pathOfArch;
        static string unpackTargetLocation;

        // Compression level field
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
