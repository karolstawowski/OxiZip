using System;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WinFormsPaczkomat
{
    // Handling of archiving service
    partial class Form1
    {
        private void buttonPackDestinationLocation_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.ShowNewFolderButton = true;
                if (lastZipFolderLocation != null)
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
                    // Add locations of selected files to filesToArchiveFullNames and listOfFilesToPack lists
                    foreach (string s in openFileDialog.FileNames)
                    {
                        filesToArchiveFullNames.Add(s);
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
                folderBrowserDialog.SelectedPath = startingPath;
                if (lastZipFolderLocation != null)
                {
                    folderBrowserDialog.SelectedPath = lastZipFolderLocation;
                }

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    lastZipFolderLocation = folderBrowserDialog.SelectedPath;

                    // Add path of selected folder to foldersToArchivePaths and listOfFilesToPack lists
                    foldersToArchiveFullNames.Add(folderBrowserDialog.SelectedPath);
                    listOfFilesToPack.Items.Add(folderBrowserDialog.SelectedPath);
                }
            }
        }

        private void buttonPackDeleteSelectedItem_Click(object sender, EventArgs e)
        {
            int listSelectedIndex = listOfFilesToPack.SelectedIndex;
            string selectedItemString = listOfFilesToPack.SelectedItem.ToString();
            FileAttributes attr = File.GetAttributes(selectedItemString);

            // Check if any element of list is selected
            if (listSelectedIndex != -1)
            {
                listOfFilesToPack.Items.RemoveAt(listSelectedIndex);

                // Check if selected item is a directory
                if (attr.HasFlag(FileAttributes.Directory))
                {
                    foldersToArchiveFullNames.Remove(selectedItemString);
                }
                // Then selected item is a file
                else
                {
                    filesToArchiveFullNames.Remove(selectedItemString);
                }
            }
        }

        private void buttonPackDeleteAllItems_Click(object sender, EventArgs e)
        {
            if (filesToArchiveFullNames.Count != 0 || foldersToArchiveNames.Count != 0)
            {
                listOfFilesToPack.Items.Clear();
                filesToArchiveFullNames.Clear();
                foldersToArchiveFullNames.Clear();
            }
        }

        private void buttonPackPack_Click(object sender, EventArgs e)
        {
            // Check if required data is delivered
            if (newZipName != null & (filesToArchiveFullNames.Count != 0 | foldersToArchiveFullNames.Count != 0) & newZipFolderLocation != null & newZipFolderLocation != String.Empty)
            {
                // Check if name of new archive meets the conditions
                if (IsNameOfNewArchiveCorrect(newZipName) == true)
                {
                    // Check if paths of folders and files are repeated
                    CheckIfPathIsAddedAlready(ref filesToArchiveFullNames);
                    CheckIfPathIsAddedAlready(ref foldersToArchiveFullNames);

                    // Initialization of variables - load names of files and folders to archive
                    filesToArchiveNames = GetNamesOfFiles(filesToArchiveFullNames);
                    foldersToArchiveNames = GetNamesOfFolders(foldersToArchiveFullNames);
                    newZipFullName = newZipFolderLocation + "\\" + newZipName + ".zip";
                    progressBarPack.Value = 20;

                    // If target archive with given name exists
                    if (File.Exists(newZipFullName))
                    {
                        // Ask user if he wants to overwrite archive or add files to it
                        FileExistsPrompt(ref fileExistsOption);
                        if (fileExistsOption == "overwrite")
                        {
                            // Delete a file and create new one
                            File.Delete(newZipFullName);
                            FileStream createNewZip = File.Create(newZipFullName);
                            createNewZip.Close();

                            // Create other task, which packs files and folders
                            Task packFilesAndFoldersTask = new Task(new Action(PackFilesAndFolders));
                            packFilesAndFoldersTask.Start();
                            packFilesAndFoldersTask.Wait();

                            // Give user and update on changes
                            progressBarPack.Value = 100;
                            PackDonePrompt_NewZip();
                            PackDoneClear();
                        }
                        else if (fileExistsOption == "add")
                        {
                            // Create other task, which packs files and folders
                            Task packFilesAndFoldersTask = new Task(new Action(PackFilesAndFolders));
                            packFilesAndFoldersTask.Start();
                            packFilesAndFoldersTask.Wait();

                            // Give user and update on changes
                            progressBarPack.Value = 100;
                            PackDonePrompt_UpdatedZip();
                            PackDoneClear();
                        }
                        else { };
                    }
                    // If target archive doesn't exist
                    else
                    {
                        // Create a new archive
                        FileStream createNewZip = File.Create(newZipFullName);
                        createNewZip.Close();

                        // Create other task, which packs files and folders
                        Task packFilesAndFoldersTask = new Task(new Action(PackFilesAndFolders));
                        packFilesAndFoldersTask.Start();
                        packFilesAndFoldersTask.Wait();

                        // Give user and update on changes
                        progressBarPack.Value = 100;
                        PackDonePrompt_NewZip();
                        PackDoneClear();
                    }
                }
            }
        }
    }
}