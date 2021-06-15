using System;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace WinFormsPaczkomat
{
    // Handling of decompressing service
    partial class Form1
    {
        private void buttonPackDestinationLocation_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.ShowNewFolderButton = true;
                if(lastZipFolderLocation != null)
                {
                    folderBrowserDialog.SelectedPath = lastZipFolderLocation;
                }
                
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    newZipFolderLocation = folderBrowserDialog.SelectedPath;
                    textBoxPackDestination.Text = folderBrowserDialog.SelectedPath;
                    lastZipFolderLocation = folderBrowserDialog.SelectedPath;
                }
            }
        }
        private void textBoxPackName_TextChanged(object sender, EventArgs e)
        {
            newZipName = textBoxPackName.Text;
        }

        private void comboBoxCompressionLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            compressionLevel = comboBoxCompressionLevel.SelectedIndex;
        }

        private void buttonPackSelectFiles_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = startingPath;
                openFileDialog.Filter = "All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;
                openFileDialog.DereferenceLinks = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    int counter = 0;
                    int previousSize = filePaths.Length;
                    Array.Resize(ref filePaths, previousSize + openFileDialog.FileNames.Length);
                    for (int i = previousSize; i < previousSize + openFileDialog.FileNames.Length; i++)
                    {
                        filePaths[i] = openFileDialog.FileNames[counter];
                        counter++;
                    }
                    foreach (string s in openFileDialog.FileNames)
                    {
                        listOfFilesToPack.Items.Add(s);
                    }
                }
            }
        }

        private void buttonPackSelectFolders_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.ShowNewFolderButton = false;
                if (lastZipFolderLocation != null)
                {
                    folderBrowserDialog.SelectedPath = lastZipFolderLocation;
                }

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    Array.Resize(ref folderPaths, folderPaths.Length + 1);
                    filePaths[folderPaths.Length] = folderBrowserDialog.SelectedPath;
                }// Delete button, delete all button, clear after pack, pack button
            }
        }

        private void buttonPackDeleteSelectedItem_Click(object sender, EventArgs e)
        {
            int index = listOfFilesToPack.SelectedIndex;
            if (index != -1)
            {
                listOfFilesToPack.Items.RemoveAt(index);
                Array.Clear(filePaths, index, 1);
                for (int i = index; i < filePaths.Length - 1; i++)
                {
                    filePaths[i] = filePaths[i + 1];
                }
                Array.Resize(ref filePaths, filePaths.Length - 1);
            }
        }

        private void buttonPackDeleteAllItems_Click(object sender, EventArgs e)
        {
            if (filePaths.Length != 0)
            {
                listOfFilesToPack.Items.Clear();
                Array.Clear(fileNames, 0, fileNames.Length);
                Array.Clear(filePaths, 0, filePaths.Length);
                Array.Resize(ref fileNames, 0);
                Array.Resize(ref filePaths, 0);
            }
        }

        private void buttonPackPack_Click(object sender, EventArgs e)
        {
            if (newZipName != null & filePaths.Length != 0 & newZipFolderLocation != null & newZipFolderLocation != String.Empty)
            {
                fileNames = GetNamesOfFiles().ToArray();
                if (IsNameOfArchiveCorrect(newZipName) == true)
                {
                    newZipEntireLocation = newZipFolderLocation + "\\" + newZipName + ".zip";
                    progressBarPack.Value = 50;
                    if (File.Exists(newZipEntireLocation))
                    {
                        FileExistsPrompt(ref fileExistsOption);
                        if (fileExistsOption == 1)
                        {
                            using (FileStream newZip = File.Create(newZipEntireLocation))
                            {
                                using (ZipArchive archive = new ZipArchive(newZip, ZipArchiveMode.Update))
                                {
                                    for (int i = 0; i < filePaths.Length; i++)
                                    {
                                        ZipArchiveEntry entry = archive.CreateEntryFromFile(filePaths[i], fileNames[i], SelectedCompressionLevel(compressionLevel));
                                    }
                                    archive.Dispose();
                                }
                            }
                            progressBarPack.Value = 100;
                            PackDonePrompt_NewZip();
                            PackDoneClear();
                        }
                        else if (fileExistsOption == 2)
                        {
                            using (FileStream newZip = new FileStream(newZipEntireLocation, FileMode.Open))
                            {
                                using (ZipArchive archive = new ZipArchive(newZip, ZipArchiveMode.Update))
                                {
                                    for (int i = 0; i < filePaths.Length; i++)
                                    {
                                        ZipArchiveEntry entry = archive.CreateEntryFromFile(filePaths[i], fileNames[i]);
                                    }
                                    archive.Dispose();
                                }
                            }
                            progressBarPack.Value = 100;
                            PackDonePrompt_UpdatedZip();
                            PackDoneClear();
                        }
                        else { };
                    }
                    else
                    {
                        using (FileStream newZip = File.Create(newZipEntireLocation))
                        {
                            using (ZipArchive archive = new ZipArchive(newZip, ZipArchiveMode.Update))
                            {
                                for (int i = 0; i < filePaths.Length; i++)
                                {
                                    ZipArchiveEntry entry = archive.CreateEntryFromFile(filePaths[i], fileNames[i], SelectedCompressionLevel(compressionLevel));
                                }
                                archive.Dispose();
                            }
                        }
                        progressBarPack.Value = 100;
                        PackDonePrompt_NewZip();
                        PackDoneClear();
                    }
                }
            }
        }
    }
}
