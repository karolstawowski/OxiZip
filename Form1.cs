using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace WinFormsPaczkomat
{
    public partial class Form1 : Form
    {
        // Initializing fields
        static readonly string userName = Environment.UserName;
        static readonly string startingPath = @$"C:\Users\{userName}\Desktop\";

        // Fields of arrays of files to compress
        static string[] filePaths = Array.Empty<string>();
        static string[] fileNames = Array.Empty<string>();
        static string[] folderPaths = Array.Empty<string>();

        // Compression-related fiels
        static string newZipEntireLocation;
        static string newZipFolderLocation;
        static string newZipName = "Archiwum";
        static string lastZipFolderLocation;

        static int fileExistsOption = 0;

        // Decompression-related fields
        static string pathOfArch;
        static string unpackTargetLocation;

        // Compression level field
        static int compressionLevel = 1;

        public Form1()
        {
            InitializeComponent();
        }
    }
}
