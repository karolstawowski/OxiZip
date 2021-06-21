using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;

namespace WinFormsPaczkomat
{
    public partial class Form1
    {
        private static List<string> GetNamesOfFiles()
        {
            List<string> justNames = new List<string>();
            string[] holder;
            foreach (string s in filesToArchiveFullNames)
            {
                holder = s.Split("\\");
                justNames.Add(holder[^1]);
            }
            return justNames;
        }
        private static List<string> GetNamesOfFolders()
        {
            List<string> justNames = new List<string>();
            string[] holder;
            foreach (string s in foldersToArchivePaths)
            {
                holder = s.Split("\\");
                justNames.Add(holder[^1]);
            }
            return justNames;
        }
        private static string GetArchiveName()
        {
            string[] ingredients = pathOfArch.Split('\\');
            string nameWithDot = ingredients[^1];
            string[] ingredientsSeparatedByDot = nameWithDot.Split('.');
            return ingredientsSeparatedByDot[0];
        }
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
        private void PackDoneClear()
        {
            Array.Clear(filesToArchiveNames, 0, filesToArchiveNames.Length);
            Array.Resize(ref filesToArchiveNames, 0);
            Array.Clear(filesToArchiveFullNames, 0, filesToArchiveFullNames.Length);
            Array.Resize(ref filesToArchiveFullNames, 0);
            Array.Clear(foldersToArchivePaths, 0, filesToArchiveNames.Length);
            Array.Resize(ref foldersToArchivePaths, 0);
            Array.Clear(foldersToArchiveNames, 0, filesToArchiveNames.Length);
            Array.Resize(ref foldersToArchiveNames, 0);
            newZipFullName = String.Empty;
            newZipName = String.Empty;
            textBoxPackName.Text = "Archiwum";
            newZipName = "Archiwum";
            listOfFilesToPack.Items.Clear();
            progressBarPack.Value = 0;
            comboBoxCompressionLevel.SelectedIndex = 1;
            comboBoxCompressionLevel.Text = "średni";
        }
        private void UnpackDoneClear()
        {
            pathOfArch = String.Empty;
            unpackTargetLocation = String.Empty;
            textBoxUnpackSourceLocation.Text = "";
            textBoxUnpackTargetLocation.Text = "";
            progressBarUnpack.Value = 0;
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
            char[] check = new char[] { '/', '\\', ':', '*', '?', '"', '<', '>', '|' };
            foreach(char character in check)
            {
                if (ArchiveName.Contains(character) == true)
                {
                    IncorrectNameOfArchivePrompt();
                    return false;
                }
            }
            return true;
        }
        public static void IncorrectNameOfArchivePrompt()
        {
            string message = "Wprowadzono niepoprawną nazwę dla nowego archiwum.";
            string caption = "Niepoprawna nazwa archiwum";
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, button, MessageBoxIcon.Error);
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
                    catch (UnauthorizedAccessException e)
                    {
                        Console.WriteLine(e.Message);
                    }
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
            }
        }
        private static void CropParentFolderPath(ref string parentFolder)
        {
            parentFolder = parentFolder.Remove(parentFolder.Length - 1);
            parentFolder = parentFolder.Remove(parentFolder.LastIndexOf('\\') + 1);
        }
        private void PackFilesAndFolders()
        {
            using (FileStream newZip = new FileStream(newZipFullName, FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(newZip, ZipArchiveMode.Update))
                {
                    for (int i = 0; i < filesToArchiveFullNames.Length; i++)
                    {
                        ZipArchiveEntry entry = archive.CreateEntryFromFile(filesToArchiveFullNames[i], filesToArchiveNames[i], SelectedCompressionLevel(compressionLevel));
                    }
                    archive.Dispose();
                }
            }
            for (int i = 0; i < foldersToArchivePaths.Length; i++)
            {
                InsertElementsToArchive(foldersToArchivePaths[i]);
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
    }
}
