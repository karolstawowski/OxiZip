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

        private void MainForm_Load(object sender, EventArgs e)
        {
            comboBoxCompressionLevel.SelectedIndex = 1;
        }

        private void textBoxPackDestination_MouseEnter(object sender, EventArgs e)
        {
            helpStripLabel.Text = "Tutaj znajduje się pełna nazwa folderu, w którym zostanie utworzone nowe archiwum.";
        }

        private void textBoxPackDestination_MouseLeave(object sender, EventArgs e)
        {
            helpStripLabel.Text = String.Empty;
        }

        private void buttonPackDestinationLocation_MouseEnter(object sender, EventArgs e)
        {
            helpStripLabel.Text = "Wybierz lokalizację, w której ma powstać nowe archiwum.";
        }

        private void buttonPackDestinationLocation_MouseLeave(object sender, EventArgs e)
        {
            helpStripLabel.Text = String.Empty;
        }

        private void textBoxPackName_MouseEnter(object sender, EventArgs e)
        {
            helpStripLabel.Text = "Wybierz nazwę dla nowego archiwum.";
        }

        private void textBoxPackName_MouseLeave(object sender, EventArgs e)
        {
            helpStripLabel.Text = String.Empty;
        }

        private void comboBoxCompressionLevel_MouseEnter(object sender, EventArgs e)
        {
            helpStripLabel.Text = "Wybierz poziom kompresji dla archwium. Najwyższy poziom kompresji tworzy możliwie najmniejszy plik, lecz prowadzi do większego obciążenia komputera.";
        }

        private void comboBoxCompressionLevel_MouseLeave(object sender, EventArgs e)
        {
            helpStripLabel.Text = String.Empty;
        }

        private void listOfItemsToPack_MouseEnter(object sender, EventArgs e)
        {
            helpStripLabel.Text = "Tutaj znajdują się lokalizacje plików i folderów, które chcesz dodać do archiwum. Możesz przeciągnąć tutaj pliki.";
        }

        private void listOfItemsToPack_MouseLeave(object sender, EventArgs e)
        {
            helpStripLabel.Text = String.Empty;
        }

        private void buttonPackSelectFiles_MouseEnter(object sender, EventArgs e)
        {
            helpStripLabel.Text = "Wybierz pliki, które chcesz dodać do archiwum.";
        }

        private void buttonPackSelectFiles_MouseLeave(object sender, EventArgs e)
        {
            helpStripLabel.Text = String.Empty;
        }

        private void buttonPackSelectFolders_MouseEnter(object sender, EventArgs e)
        {
            helpStripLabel.Text = "Wybierz folder, który chcesz dodać do archiwum.";
        }

        private void buttonPackSelectFolders_MouseLeave(object sender, EventArgs e)
        {
            helpStripLabel.Text = String.Empty;
        }

        private void buttonPackDeleteSelectedItem_MouseEnter(object sender, EventArgs e)
        {
            helpStripLabel.Text = "Usuń wybraną pozycję z listy.";
        }

        private void buttonPackDeleteSelectedItem_MouseLeave(object sender, EventArgs e)
        {
            helpStripLabel.Text = String.Empty;
        }

        private void buttonPackDeleteAllItems_MouseEnter(object sender, EventArgs e)
        {
            helpStripLabel.Text = "Usuń wszystkie pozycje z listy.";
        }

        private void buttonPackDeleteAllItems_MouseLeave(object sender, EventArgs e)
        {
            helpStripLabel.Text = String.Empty;
        }

        private void buttonPackPack_MouseEnter(object sender, EventArgs e)
        {
            helpStripLabel.Text = "Spakuj pliki i foldery do archiwum o wybranej lokalizacji.";
        }

        private void buttonPackPack_MouseLeave(object sender, EventArgs e)
        {
            helpStripLabel.Text = String.Empty;
        }

        private void textBoxUnpackSourceLocation_MouseEnter(object sender, EventArgs e)
        {
            helpStripLabel.Text = "Tutaj znajduje się lokalizacja archiwum, które chcesz rozpakować.";
        }

        private void textBoxUnpackSourceLocation_MouseLeave(object sender, EventArgs e)
        {
            helpStripLabel.Text = String.Empty;
        }

        private void buttonUnpackSelectToExtract_MouseEnter(object sender, EventArgs e)
        {
            helpStripLabel.Text = "Wybierz archiwum, które chcesz rozpakować.";
        }

        private void buttonUnpackSelectToExtract_MouseLeave(object sender, EventArgs e)
        {
            helpStripLabel.Text = String.Empty;
        }

        private void textBoxUnpackFolderName_MouseEnter(object sender, EventArgs e)
        {
            helpStripLabel.Text = "Tutaj możesz wybrać nazwę folderu, do którego zostanie wypakowana zawartość archiwum (opcjonalnie).";
        }

        private void textBoxUnpackFolderName_MouseLeave(object sender, EventArgs e)
        {
            helpStripLabel.Text = String.Empty;
        }

        private void textBoxUnpackTargetLocation_MouseEnter(object sender, EventArgs e)
        {
            helpStripLabel.Text = "Tutaj znajduje się nazwa folderu, do którego zostanie rozpakowana zawartość archiwum.";
        }

        private void textBoxUnpackTargetLocation_MouseLeave(object sender, EventArgs e)
        {
            helpStripLabel.Text = String.Empty;
        }

        private void buttonUnpackDestinationLocation_MouseEnter(object sender, EventArgs e)
        {
            helpStripLabel.Text = "Wybierz folder, do którego zostanie wypakowana zawartość archiwum.";
        }

        private void buttonUnpackDestinationLocation_MouseLeave(object sender, EventArgs e)
        {
            helpStripLabel.Text = String.Empty;
        }

        private void buttonUnpackUnpack_MouseEnter(object sender, EventArgs e)
        {
            helpStripLabel.Text = "Wypakuj zawartość archiwum do wybranej lokalizacji.";
        }

        private void buttonUnpackUnpack_MouseLeave(object sender, EventArgs e)
        {
            helpStripLabel.Text = String.Empty;
        }
    }
}
