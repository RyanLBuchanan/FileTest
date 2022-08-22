using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FileTest
{
    public partial class FileTestForm : Form
    {
        public FileTestForm()
        {
            InitializeComponent();
        }


        // Invoked when user presses key
        private void inputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Determine whether user pressed Enter key
            if (e.KeyCode == Keys.Enter)
            {
                // Get user-specified file dr directory
                string fileName = inputTextBox.Text;

                // Determine whether the fileName is a file
                if (File.Exists(fileName))
                {
                    // Get file's creation date, modification date, etc.
                    GetInformation(fileName);

                    // Display file contents through StreamReader 
                    try
                    {
                        // Obtain reader and file contents
                        using (var stream = new StreamReader(fileName))
                        {
                            outputTextBox.AppendText(stream.ReadToEnd());
                        }
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("Error reading from file", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // Determine whether fileName is a directory 
                else if (Directory.Exists(fileName))
                {
                    // Get directory's cretion date, modification date, etc.
                    GetInformation(fileName);

                    // Obtain directory list of specified directory 
                    string[] directoryList = Directory.GetDirectories(fileName);

                    outputTextBox.AppendText("Directory contents:\n");

                    // Output directoryList contents
                    foreach (var directory in directoryList)
                    {
                        outputTextBox.AppendText($"{directory}\n");
                    }
                }
                else
                {
                    // Notify user that neither file nor directory exists
                    MessageBox.Show($"{inputTextBox.Text} does not exist", "File error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Get information on file or directory and output it to outputTextBox
        private void GetInformation(string fileName)
        {
            outputTextBox.Clear();

            // Output that file or directory exists
            outputTextBox.AppendText($"{fileName} exists\n");

            // Output when file or directory was crated 
            outputTextBox.AppendText($"Created: {File.GetCreationTime(fileName)}\n" +
                Environment.NewLine);

            // Output when file or directory was last modified 
            outputTextBox.AppendText($"Last modified: {File.GetLastWriteTime(fileName)}\n" +
                Environment.NewLine);

            // Output when file or directory was last accessed
            outputTextBox.AppendText($"Last accessed: {File.GetLastAccessTime(fileName)}\n" +
                Environment.NewLine);
        }
    }
}
