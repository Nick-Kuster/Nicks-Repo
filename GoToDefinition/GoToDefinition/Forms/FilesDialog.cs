using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using    System.Runtime.InteropServices;
namespace GoToDefinition.Forms
{
    public partial class FilesDialog : Form
    {
        public FilesDialog()
        {
            InitializeComponent();
        }

        public FilesDialog(string[] fileNames)
        {
            InitializeComponent();
            this.listBox1.Items.AddRange(fileNames);
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listBox1.SelectedItem != null)
            {
                var file = this.listBox1.SelectedItem;
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "notepad++.exe";
                var fileString = file.ToString();
                var combinedFileString = "\"" + file + "\"";
                psi.Arguments = combinedFileString;
                Process.Start(psi);
                this.TopMost = true;
                this.Focus();
            }
        }

        private void listBox1_onKeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.listBox1.SelectedItem != null)
            {
                var file = this.listBox1.SelectedItem;
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "notepad++.exe";
                var fileString = file.ToString();
                var combinedFileString = "\"" + file + "\"";
                psi.Arguments = combinedFileString;
                Process.Start(psi);
                this.TopMost = true;
                this.Focus();
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
