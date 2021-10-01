using System;
using System.Windows.Forms;
using System.IO;

namespace OxiZip
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
                        listOfItemsToPack.Items.Add(s);
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
                    listOfItemsToPack.Items.Add(folderBrowserDialog.SelectedPath);
                }
            }
        }

        private void buttonPackDeleteSelectedItem_Click(object sender, EventArgs e)
        {
            int listSelectedIndex = listOfItemsToPack.SelectedIndex;

            // Check if any element of list is selected
            if (listSelectedIndex != -1)
            {
                string selectedItemString = listOfItemsToPack.SelectedItem.ToString();
                // Get attribute of selected entry
                FileAttributes attr = File.GetAttributes(selectedItemString);

                listOfItemsToPack.Items.RemoveAt(listSelectedIndex);

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
                listOfItemsToPack.Items.Clear();
                filesToArchiveFullNames.Clear();
                foldersToArchiveFullNames.Clear();
            }
        }

        // Drag and drop functionality for list of items to pack - requires DragEnter, DragDrop and AllowDrop
        private void listOfItemsToPack_DragEnter(object sender, DragEventArgs e)
        {
            // Copy file/folder information
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void listOfItemsToPack_DragDrop(object sender, DragEventArgs e)
        {
            // Get names of items to pack
            string[] itemsToPack = (string[])e.Data.GetData(DataFormats.FileDrop);

            for (int i = 0; i < itemsToPack.Length; i++)
            {
                // Get attribute of item, then check if item is a directory or file
                FileAttributes attr = File.GetAttributes(itemsToPack[i]);

                // Add items to listOfItemsToPack 
                listOfItemsToPack.Items.Add(itemsToPack[i]);

                if (attr.HasFlag(FileAttributes.Directory))
                {
                    foldersToArchiveFullNames.Add(itemsToPack[i]);
                }
                else
                {
                    filesToArchiveFullNames.Add(itemsToPack[i]);
                }
            }
        }

        private void buttonPackPack_Click(object sender, EventArgs e)
        {
            // Check if required data is delivered
            if (newZipName != null && (filesToArchiveFullNames.Count != 0 || foldersToArchiveFullNames.Count != 0) && newZipFolderLocation != null && newZipFolderLocation != String.Empty)
            {
                // Check if name of new archive meets the conditions
                if (IsNameOfNewArchiveCorrect(newZipName))
                {
                    // Check if paths of folders and files are repeated
                    CheckIfPathIsAddedToListAlready(ref filesToArchiveFullNames);
                    CheckIfPathIsAddedToListAlready(ref foldersToArchiveFullNames);

                    // Initialization of variables - load names of files and folders to archive
                    filesToArchiveNames = GetNamesFromList(filesToArchiveFullNames);
                    foldersToArchiveNames = GetNamesFromList(foldersToArchiveFullNames);
                    // Full name of new archive
                    newZipFullName = newZipFolderLocation + "\\" + newZipName + ".zip";

                    // If target archive with given name exists
                    if (File.Exists(newZipFullName))
                    {
                        // Ask user if he wants to overwrite archive or add files to it
                        Prompts.FileExistsPrompt(ref fileExistsOption);
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
                            Prompts.PackDonePrompt_NewZip();
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
                            Prompts.PackDonePrompt_UpdatedZip();
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
                        Prompts.PackDonePrompt_NewZip();
                    }
                }
            }
        }
    }
}