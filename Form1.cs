using System;
using System.Windows.Forms;

namespace WinFormsPaczkomat
{
    public partial class Form1 : Form
    {
        // Initializing fields
        static readonly string userName = Environment.UserName;
        static readonly string startingPath = @$"C:\Users\{userName}\Desktop\";

        // Fields of arrays of files to compress
        static string[] filesToArchiveFullNames = Array.Empty<string>();
        static string[] filesToArchiveNames = Array.Empty<string>();
        static string[] foldersToArchivePaths = Array.Empty<string>();
        static string[] foldersToArchiveNames = Array.Empty<string>();

        // Compression-related fiels
        static string newZipFullName;
        static string newZipFolderLocation;
        static string newZipName = "Archiwum";

        // Memorizing last created zip folder location
        static string lastZipFolderLocation;

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
