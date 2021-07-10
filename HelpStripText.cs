using System;

namespace WinFormsPaczkomat
{
    // Help strip activity

    public partial class MainForm
    {
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
