using System;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace WinFormsPaczkomat
{
    // Handling of dearchiving service
    partial class Form1
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
            if ((pathOfArch != String.Empty & pathOfArch != null) & (unpackTargetLocation != String.Empty & unpackTargetLocation != null))
            {
                string archiveName = GetArchiveName();

                Directory.CreateDirectory(unpackTargetLocation + "\\" + archiveName);

                if (File.Exists(pathOfArch))
                {
                    try
                    {
                        ZipFile.ExtractToDirectory(pathOfArch, unpackTargetLocation + "\\" + archiveName);
                    }
                    catch (Exception) { }
                    progressBarUnpack.Value = 100;
                }

                UnpackDonePrompt();
                UnpackDoneClear();
            }
        }
    }
}
