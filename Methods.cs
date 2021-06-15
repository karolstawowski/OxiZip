using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO.Compression;

namespace WinFormsPaczkomat
{
    public partial class Form1
    {
        public static List<string> GetNamesOfFiles()
        {
            List<string> justNames = new List<string>();
            string[] holder;
            foreach (string s in filePaths)
            {
                holder = s.Split("\\");
                justNames.Add(holder[^1]);
            }
            return justNames;
        }
        public static string GetArchiveName()
        {
            string[] ingredients = pathOfArch.Split('\\');
            string nameWithDot = ingredients[^1];
            string[] ingredientsSeparatedByDot = nameWithDot.Split('.');
            return ingredientsSeparatedByDot[0];
        }
        public void PackDonePrompt_NewZip()
        {
            string message = "Archiwum zostało utworzone z powodzeniem.";
            string caption = "Ukończono pakowanie";
            MessageBoxButtons button = MessageBoxButtons.OK;
            DialogResult result;
            result = MessageBox.Show(message, caption, button, MessageBoxIcon.Information);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                progressBarPack.Value = 0;
            }
        }
        public void PackDonePrompt_UpdatedZip()
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
        public void UnpackDonePrompt()
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
        public void PackDoneClear()
        {
            Array.Clear(fileNames, 0, fileNames.Length);
            Array.Resize(ref fileNames, 0);
            Array.Clear(filePaths, 0, filePaths.Length);
            Array.Resize(ref filePaths, 0);
            newZipEntireLocation = String.Empty;
            newZipName = String.Empty;
            textBoxPackName.Text = "Archiwum";
            newZipName = "Archiwum";
            listOfFilesToPack.Items.Clear();
            progressBarPack.Value = 0;
            comboBoxCompressionLevel.SelectedIndex = 1;
            comboBoxCompressionLevel.Text = "średni";
        }
        public void UnpackDoneClear()
        {
            pathOfArch = String.Empty;
            unpackTargetLocation = String.Empty;
            textBoxUnpackSourceLocation.Text = "";
            textBoxUnpackTargetLocation.Text = "";
            progressBarUnpack.Value = 0;
        }
        public void FileExistsPrompt(ref int wybor)
        {
            string message = "Archwium o tej nazwie istnieje. Czy chcesz go nadpisać?\n\nWybór opcji \"Nie\" spowoduje dopisanie plików do archiwum."; 
            string caption = "Plik już istnieje";
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Warning);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                wybor = 1;
            }
            else if (result == System.Windows.Forms.DialogResult.No)
            {
                wybor = 2;
            }
            else 
            {
                progressBarPack.Value = 0;
            };
        }
        public static CompressionLevel SelectedCompressionLevel(int compressionNumber)
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
        public static bool IsNameOfArchiveCorrect(string ArchiveName)
        {
            char[] check = new char[] { '/', '\\', ':', '*', '?', '"', '<', '>', '|' };
            foreach(char character in check)
            {
                if (ArchiveName.Contains(character) == true)
                {
                    string message = "Wprowadzono niepoprawną nazwę dla archiwum.";
                    string caption = "Niepoprawna nazwa";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
    }
}
