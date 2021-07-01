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
    partial class MainForm
    {
        private void buttonPackDestinationLocation_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.ShowNewFolderButton = true;
                folderBrowserDialog.SelectedPath = startingPath;

                if (lastDestinationLocation != null)
                {
                    folderBrowserDialog.SelectedPath = lastDestinationLocation;
                }

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    newZipFolderLocation = folderBrowserDialog.SelectedPath;
                    textBoxPackDestination.Text = folderBrowserDialog.SelectedPath;
                    lastDestinationLocation = folderBrowserDialog.SelectedPath;
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
                openFileDialog.DereferenceLinks = false;

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

                if (lastFolderLocation != null)
                {
                    folderBrowserDialog.SelectedPath = lastFolderLocation;
                }

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    lastFolderLocation = folderBrowserDialog.SelectedPath;

                    // Add path of selected folder to foldersToArchivePaths and listOfFilesToPack lists
                    foldersToArchiveFullNames.Add(folderBrowserDialog.SelectedPath);
                    listOfFilesToPack.Items.Add(folderBrowserDialog.SelectedPath);
                }
            }
        }

        private void buttonPackDeleteSelectedItem_Click(object sender, EventArgs e)
        {
            int listSelectedIndex = listOfFilesToPack.SelectedIndex;

            // Check if any element of list is selected
            if (listSelectedIndex != -1)
            {
                string selectedItemString = listOfFilesToPack.SelectedItem.ToString();
                // Get attribute of selected entry
                FileAttributes attr = File.GetAttributes(selectedItemString);

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
            if (filesToArchiveFullNames.Count != 0 || foldersToArchiveFullNames.Count != 0)
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
                if (IsNameOfNewArchiveCorrect(newZipName))
                {
                    // Check if paths of folders and files are repeated
                    CheckIfPathIsAddedToListAlready(ref filesToArchiveFullNames);
                    CheckIfPathIsAddedToListAlready(ref foldersToArchiveFullNames);

                    // Initialization of variables - load names of files and folders to archive
                    filesToArchiveNames = GetNamesOfFiles(filesToArchiveFullNames);
                    foldersToArchiveNames = GetNamesOfFolders(foldersToArchiveFullNames);
                    // Full name of new archive
                    newZipFullName = newZipFolderLocation + "\\" + newZipName + ".zip";

                    // If target archive with given name exists
                    if (File.Exists(newZipFullName))
                    {
                        // Ask user if he wants to overwrite archive or add files to it
                        FileExistsPrompt(ref fileExistsOption);
                        if (fileExistsOption == "overwrite")
                        {
                            // Give user an information that program works
                            Cursor.Current = Cursors.WaitCursor;

                            // Delete a file and create new one
                            File.Delete(newZipFullName);
                            FileStream createNewZip = File.Create(newZipFullName);
                            createNewZip.Close();

                            // Pack selected folders and files
                            InitialPacking();
                            // Here's version with other task, which doesn't allow to show packed location at the moment in UI
                            //Task packFilesAndFoldersTask = new Task(new Action(InitialPacking));
                            //packFilesAndFoldersTask.Start();
                            //packFilesAndFoldersTask.Wait();

                            // Restore custor status - program done its work
                            Cursor.Current = Cursors.Default;

                            // Give user an update on changes
                            PackDoneClear();
                            PackDonePrompt_NewZip();
                        }
                        else if (fileExistsOption == "add")
                        {
                            // Give user an information that program works
                            Cursor.Current = Cursors.WaitCursor;

                            // Pack selected folders and files
                            InitialPacking();
                            // Here's version with other task, which doesn't allow to show packed location at the moment in UI
                            //Task packFilesAndFoldersTask = new Task(new Action(InitialPacking));
                            //packFilesAndFoldersTask.Start();
                            //packFilesAndFoldersTask.Wait();

                            // Restore custor status - program done its work
                            Cursor.Current = Cursors.Default;

                            // Give user an update on changes
                            PackDoneClear();
                            PackDonePrompt_UpdatedZip();
                        }
                        else { };
                    }
                    // If target archive doesn't exist yet
                    else
                    {
                        // Give user an information that program works
                        Cursor.Current = Cursors.WaitCursor;

                        // Create a new archive
                        FileStream createNewZip = File.Create(newZipFullName);
                        createNewZip.Close();

                        // Pack selected folders and files
                        InitialPacking();
                        // Here's version with other task, which doesn't allow to show packed location at the moment in UI
                        //Task packFilesAndFoldersTask = new Task(new Action(InitialPacking));
                        //packFilesAndFoldersTask.Start();
                        //packFilesAndFoldersTask.Wait();

                        // Restore custor status - program done its work
                        Cursor.Current = Cursors.Default;

                        // Give user an update on changes
                        PackDoneClear();
                        PackDonePrompt_NewZip();
                    }
                }
            }
        }
    }
}