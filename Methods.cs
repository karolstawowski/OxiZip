using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;

namespace WinFormsPaczkomat
{
    public partial class Form1
    {
        private static List<string> GetNamesOfFiles(List<string> filesToArchiveFullNames)
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

        private static List<string> GetNamesOfFolders(List<string> foldersToArchiveNames)
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

        private static string GetArchiveName()
        {
            string[] ingredients = pathOfArch.Split('\\');
            string nameWithExtension = ingredients[^1];
            string[] ingredientsSeparatedByDot = nameWithExtension.Split('.');
            // Get name of archive without its extension
            return ingredientsSeparatedByDot[0];
        }

        private void PackDoneClear()
        {
            // Clear all lists
            filesToArchiveNames.Clear();
            filesToArchiveFullNames.Clear();
            foldersToArchiveFullNames.Clear();
            foldersToArchiveNames.Clear();
            listOfFilesToPack.Items.Clear();

            newZipFullName = String.Empty;
            newZipName = String.Empty;
            textBoxPackName.Text = "Archiwum";
            newZipName = "Archiwum";
            comboBoxCompressionLevel.SelectedIndex = 1;
            comboBoxCompressionLevel.Text = "średni";
            progressBarPack.Value = 0;
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
        
        // Packing methods in reversed hierarchical order

        private static void CropParentFolderPath(ref string parentFolder)
        {
            parentFolder = parentFolder.Remove(parentFolder.Length - 1);
            parentFolder = parentFolder.Remove(parentFolder.LastIndexOf('\\') + 1);
        }

        private static void PackFolders(string folderPath, string parentFolder, string newZipFullName)
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
                                foreach (FileInfo fi in di.GetFiles())
                                {
                                    ZipArchiveEntry entry = archive.CreateEntryFromFile(folderPath + "\\" + fi.Name, parentFolder + fi.Name, CompressionLevel.Optimal);
                                }
                                archive.Dispose();
                            }
                        }
                    }
                    catch (Exception) { }
                    DirectoryInfo[] podkatalogi = di.GetDirectories();
                    if (podkatalogi[0].Exists)
                    {
                        foreach (DirectoryInfo x in podkatalogi)
                        {
                            using (FileStream newZip = new FileStream(newZipFullName, FileMode.Open))
                            {
                                using (ZipArchive archive = new ZipArchive(newZip, ZipArchiveMode.Update))
                                {
                                    ZipArchiveEntry entry = archive.CreateEntry(parentFolder + x.Name + "\\", CompressionLevel.Optimal);
                                    archive.Dispose();
                                }
                            }
                            parentFolder += x.Name + "\\";
                            folderPathWithInnerPath = x.FullName;

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
        
        static void InsertElementsToArchive(string folderPath)
        {
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
        private void PackFilesAndFolders()
        {
            using (FileStream newZip = new FileStream(newZipFullName, FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(newZip, ZipArchiveMode.Update))
                {
                    for (int i = 0; i < filesToArchiveFullNames.Count; i++)
                    {
                        ZipArchiveEntry entry = archive.CreateEntryFromFile(filesToArchiveFullNames[i], filesToArchiveNames[i], SelectedCompressionLevel(compressionLevel));
                    }
                    archive.Dispose();
                }
            }
            for (int i = 0; i < foldersToArchiveFullNames.Count; i++)
            {
                InsertElementsToArchive(foldersToArchiveFullNames[i]);
            }
        }

        static void CheckIfPathIsAddedAlready(ref List<string> listOfPaths)
        {
            for (int i = 0; i < listOfPaths.Count; i++)
            {
                while (listOfPaths.LastIndexOf(listOfPaths[i]) != i)
                {
                    listOfPaths.RemoveAt(listOfPaths.LastIndexOf(listOfPaths[i]));
                }
            }
        }

        /*
         *  Archiving prompts
         */
        private void PackDonePrompt_NewZip()
        {
            string message = "Archiwum zostało utworzone z powodzeniem.";
            string caption = "Ukończono pakowanie";
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, button, MessageBoxIcon.Information);
        }

        private void PackDonePrompt_UpdatedZip()
        {
            string message = "Archiwum zostało zaktualizowane z powodzeniem.";
            string caption = "Ukończono pakowanie";
            MessageBoxButtons button = MessageBoxButtons.OK;
            DialogResult result;
            result = MessageBox.Show(message, caption, button, MessageBoxIcon.Information);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                progressBarPack.Value = 0;
            }
        }

        private void FileExistsPrompt(ref string selectedOption)
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
            else
            {
                progressBarPack.Value = 0;
            };
        }

        public static void IncorrectNameOfArchivePrompt()
        {
            string message = "Wprowadzono niepoprawną nazwę dla nowego archiwum.";
            string caption = "Niepoprawna nazwa archiwum";
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, button, MessageBoxIcon.Error);
        }

        /*
         *  Dearchiving methods
         */

        private void UnpackDonePrompt()
        {
            string message = "Archiwum zostało rozpakowane z powodzeniem.";
            string caption = "Ukończono rozpakowywanie";
            MessageBoxButtons button = MessageBoxButtons.OK;
            DialogResult result;
            result = MessageBox.Show(message, caption, button, MessageBoxIcon.Information);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                progressBarPack.Value = 0;
            }
        }

        private void UnpackDoneClear()
        {
            pathOfArch = String.Empty;
            unpackTargetLocation = String.Empty;
            textBoxUnpackSourceLocation.Text = "";
            textBoxUnpackTargetLocation.Text = "";
            progressBarUnpack.Value = 0;
        }
    }
}
