using System.Windows.Forms;

namespace OxiZip
{
    public class Prompts
    {
        //
        //  Archiving prompts
        //

        public static void PackDonePrompt_NewZip()
        {
            string message = "Archiwum zostało utworzone z powodzeniem.";
            string caption = "Ukończono pakowanie";
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, button, MessageBoxIcon.Information);
        }

        public static void PackDonePrompt_UpdatedZip()
        {
            string message = "Archiwum zostało zaktualizowane z powodzeniem.";
            string caption = "Ukończono pakowanie";
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, button, MessageBoxIcon.Information);
        }

        public static void FileExistsPrompt(ref ArchiveExistsOptions.ArchiveExistsOptionsEnum selectedOption)
        {
            string message = "Archwium o tej nazwie istnieje. Czy chcesz go nadpisać?\n\nWybór opcji \"Nie\" spowoduje dopisanie plików do archiwum.";
            string caption = "Plik już istnieje";
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Warning);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                selectedOption = ArchiveExistsOptions.ArchiveExistsOptionsEnum.Overwrite;
            }
            else if (result == System.Windows.Forms.DialogResult.No)
            {
                selectedOption = ArchiveExistsOptions.ArchiveExistsOptionsEnum.Append;
            }
        }

        public static void IncorrectNameOfArchivePrompt()
        {
            string message = "Wprowadzono niepoprawną nazwę dla nowego archiwum.";
            string caption = "Niepoprawna nazwa archiwum";
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, button, MessageBoxIcon.Error);
        }

        public static void UnauthorizedAccessPrompt()
        {
            string message = "Program ma niewystarczające uprawnienia, aby spakować plik.";
            string caption = "Przerwano pakowanie";
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, button, MessageBoxIcon.Error);
        }

        //
        // Dearchiving prompts
        //

        public static void UnpackDonePrompt()
        {
            string message = "Archiwum zostało rozpakowane z powodzeniem.";
            string caption = "Ukończono rozpakowywanie";
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBox.Show(message, caption, button, MessageBoxIcon.Information);
        }
    }
}
