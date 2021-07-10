using System;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace WinFormsPaczkomat
{
    // Handling of dearchiving service

    partial class MainForm
    {
        private void buttonUnpackSelectToExtract_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = startingPath;
                openFileDialog.Filter = "Zip files (*.zip)|*.zip";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = false;
                openFileDialog.DereferenceLinks = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pathOfArch = openFileDialog.FileName;
                    textBoxUnpackSourceLocation.Text = openFileDialog.FileName;
                }
            }
        }

        private void buttonUnpackTargetLocation_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    unpackTargetLocation = folderBrowserDialog.SelectedPath;
                    textBoxUnpackTargetLocation.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void buttonUnpackUnpack_Click(object sender, EventArgs e)
        {
            if ((pathOfArch != String.Empty && pathOfArch != null) && (unpackTargetLocation != String.Empty && unpackTargetLocation != null))
            {
                string archiveName = GetArchiveName();
                string locationOfUnpackedFilesAndFolders = String.Empty;

                // Give user an information that program works
                Cursor.Current = Cursors.WaitCursor;

                // Check if user gave a specific name for a new folder
                if (textBoxUnpackFolderName.Text == "")
                {
                    locationOfUnpackedFilesAndFolders = unpackTargetLocation + "\\" + archiveName;
                    Directory.CreateDirectory(locationOfUnpackedFilesAndFolders);

                }
                // Else create a folder with name of archive
                else
                {
                    if (IsNameOfNewArchiveCorrect(textBoxUnpackFolderName.Text))
                    {
                        locationOfUnpackedFilesAndFolders = unpackTargetLocation + "\\" + textBoxUnpackFolderName.Text;
                        Directory.CreateDirectory(locationOfUnpackedFilesAndFolders);
                    }
                }

                // Unpack file
                if (File.Exists(pathOfArch))
                {
                    try
                    {
                        ZipFile.ExtractToDirectory(pathOfArch, locationOfUnpackedFilesAndFolders);
                    }
                    catch (Exception) { }
                }

                // Restore custor status - program done its work
                Cursor.Current = Cursors.Default;

                // Give user an update on changes
                UnpackDoneClear();
                UnpackDonePrompt();
            }
        }
    }
}
