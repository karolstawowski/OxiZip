using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;

namespace WinFormsPaczkomat
{
    // Methods for packing and unpacking

    public partial class MainForm
    {
        //
        //  Archiving methods
        //

        private static List<string> GetNamesOfFiles()
        {
            List<string> pathsNames = new List<string>();

            string[] holder;

            foreach (string entry in filesToArchiveFullNames)
            {
                holder = entry.Split("\\");
                pathsNames.Add(holder[^1]);
            }
            return pathsNames;
        }

        private static List<string> GetNamesOfFolders()
        {
            List<string> pathsNames = new List<string>();

            string[] holder;

            foreach (string entry in foldersToArchiveFullNames)
            {
                holder = entry.Split("\\");
                pathsNames.Add(holder[^1]);
            }
            return pathsNames;
        }

        // Get name of archive without its extension
        private static string GetArchiveName()
        {
            string[] ingredients = pathOfArch.Split('\\');
            string nameWithExtension = ingredients[^1];
            string[] ingredientsSeparatedByDot = nameWithExtension.Split('.');
            return ingredientsSeparatedByDot[0];
        }

        private void PackDoneClear()
        {
            // Clear all lists
            filesToArchiveNames.Clear();
            filesToArchiveFullNames.Clear();
            foldersToArchiveFullNames.Clear();
            foldersToArchiveNames.Clear();
            listOfItemsToPack.Items.Clear();

            // Set starting values to variables
            newZipFullName = String.Empty;
            newZipName = String.Empty;
            textBoxPackName.Text = "Archiwum";
            newZipName = "Archiwum";
            comboBoxCompressionLevel.SelectedIndex = 1;
            comboBoxCompressionLevel.Text = "średni";
            labelPackingFileName.Text = String.Empty;
        }

        private static CompressionLevel SelectedCompressionLevel(int compressionNumber)
        {
            CompressionLevel compressionLevel;

            if (compressionNumber == 0)
            {
                compressionLevel = CompressionLevel.NoCompression;
            }
            else if (compressionNumber == 1)
            {
                compressionLevel = CompressionLevel.Fastest;
            }
            else compressionLevel = CompressionLevel.Optimal;
            return compressionLevel;
        }

        private static bool IsNameOfNewArchiveCorrect(string ArchiveName)
        {
            // Forbidden characters have to be excluded from archive name
            char[] check = new char[] { '/', '\\', ':', '*', '?', '"', '<', '>', '|' };

            foreach (char character in check)
            {
                if (ArchiveName.Contains(character) == true)
                {
                    IncorrectNameOfArchivePrompt();
                    return false;
                }
            }
            return true;
        }

        private static void CropParentFolderPath(ref string parentFolder)
        {
            parentFolder = parentFolder.Remove(parentFolder.Length - 1);
            parentFolder = parentFolder.Remove(parentFolder.LastIndexOf('\\') + 1);
        }

        private void PackFolders(string folderPath, string parentFolder, string newZipFullName)
        {
            string folderPathWithInnerPath;

            DirectoryInfo di = new DirectoryInfo(folderPath);

            if (di.Exists)
            {
                try
                {
                    try
                    {
                        using (FileStream newZip = new FileStream(newZipFullName, FileMode.Open))
                        {
                            using (ZipArchive archive = new ZipArchive(newZip, ZipArchiveMode.Update))
                            {
                                foreach (FileInfo fileInfo in di.GetFiles())
                                {
                                    labelPackingFileName.Text = parentFolder + fileInfo.Name;
                                    labelPackingFileName.Update();
                                    ZipArchiveEntry entry = archive.CreateEntryFromFile(folderPath + "\\" + fileInfo.Name, parentFolder + fileInfo.Name, CompressionLevel.Optimal);
                                }
                                archive.Dispose();
                            }
                        }
                    }
                    catch (Exception) { }

                    DirectoryInfo[] subdirectories = di.GetDirectories();

                    if (subdirectories[0].Exists)
                    {
                        foreach (DirectoryInfo directoryInfo in subdirectories)
                        {
                            using (FileStream newZip = new FileStream(newZipFullName, FileMode.Open))
                            {
                                using (ZipArchive archive = new ZipArchive(newZip, ZipArchiveMode.Update))
                                {
                                    labelPackingFileName.Text = parentFolder + directoryInfo.Name + "\\";
                                    labelPackingFileName.Update();
                                    ZipArchiveEntry entry = archive.CreateEntry(parentFolder + directoryInfo.Name + "\\", CompressionLevel.Optimal);
                                    archive.Dispose();
                                }
                            }
                            parentFolder += directoryInfo.Name + "\\";
                            folderPathWithInnerPath = directoryInfo.FullName;

                            PackFolders(folderPathWithInnerPath, parentFolder, newZipFullName);
                            CropParentFolderPath(ref parentFolder);
                            CropParentFolderPath(ref folderPathWithInnerPath);
                        }
                    }
                }
                catch (UnauthorizedAccessException) { }
                catch (IndexOutOfRangeException)
                {
                    CropParentFolderPath(ref parentFolder);
                }
                catch (Exception) { }
            }
        }

        private void PackFilesAndFolders(string folderPath)
        {
            // rootFolderName = name of parent directory of folder to pack
            string rootFolderName = folderPath.Remove(0, (folderPath.LastIndexOf('\\') + 1));

            using (FileStream newZip = new FileStream(newZipFullName, FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(newZip, ZipArchiveMode.Update))
                {
                    ZipArchiveEntry entry = archive.CreateEntry(rootFolderName + "\\", CompressionLevel.Optimal);
                    archive.Dispose();
                }
            }
            PackFolders(folderPath, rootFolderName + "\\", newZipFullName);
        }

        private void InitialPacking()
        {
            // Pack selected in UI files
            using (FileStream newZip = new FileStream(newZipFullName, FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(newZip, ZipArchiveMode.Update))
                {
                    for (int i = 0; i < filesToArchiveFullNames.Count; i++)
                    {
                        // Update UI element labelPackingFileName
                        labelPackingFileName.Text = filesToArchiveNames[i];
                        labelPackingFileName.Refresh();

                        ZipArchiveEntry entry = archive.CreateEntryFromFile(filesToArchiveFullNames[i], filesToArchiveNames[i], SelectedCompressionLevel(compressionLevel));
                    }
                    archive.Dispose();
                }
            }
            // Pack selected in UI folders
            for (int i = 0; i < foldersToArchiveFullNames.Count; i++)
            {
                PackFilesAndFolders(foldersToArchiveFullNames[i]);
            }
        }

        private static void CheckIfPathIsAddedToListAlready(ref List<string> listOfPaths)
        {
            for (int i = 0; i < listOfPaths.Count; i++)
            {
                while (listOfPaths.LastIndexOf(listOfPaths[i]) != i)
                {
                    listOfPaths.RemoveAt(listOfPaths.LastIndexOf(listOfPaths[i]));
                }
            }
        }

        //
        //  Archiving prompts
        //

        private static void PackDonePrompt_NewZip()
        {
            string message = "Archiwum zostało utworzone z powodzeniem.";
            string caption = "Ukończono pakowanie";
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, button, MessageBoxIcon.Information);
        }

        private static void PackDonePrompt_UpdatedZip()
        {
            string message = "Archiwum zostało zaktualizowane z powodzeniem.";
            string caption = "Ukończono pakowanie";
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, button, MessageBoxIcon.Information);
        }

        private static void FileExistsPrompt(ref string selectedOption)
        {
            string message = "Archwium o tej nazwie istnieje. Czy chcesz go nadpisać?\n\nWybór opcji \"Nie\" spowoduje dopisanie plików do archiwum.";
            string caption = "Plik już istnieje";
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Warning);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                selectedOption = "overwrite";
            }
            else if (result == System.Windows.Forms.DialogResult.No)
            {
                selectedOption = "add";
            }
            else { };
        }

        private static void IncorrectNameOfArchivePrompt()
        {
            string message = "Wprowadzono niepoprawną nazwę dla nowego archiwum.";
            string caption = "Niepoprawna nazwa archiwum";
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, button, MessageBoxIcon.Error);
        }

        //
        //  Dearchiving methods
        //

        private static void UnpackDonePrompt()
        {
            string message = "Archiwum zostało rozpakowane z powodzeniem.";
            string caption = "Ukończono rozpakowywanie";
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, button, MessageBoxIcon.Information);
        }

        private void UnpackDoneClear()
        {
            unpackTargetLocation = String.Empty;
            textBoxUnpackTargetLocation.Text = "";
            textBoxUnpackFolderName.Text = "";
        }
    }
}
