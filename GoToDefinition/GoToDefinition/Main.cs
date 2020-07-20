using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using GoToDefinition.Forms;
using Kbg.NppPluginNET.PluginInfrastructure;

namespace Kbg.NppPluginNET
{
    class Main
    {
        internal const string PluginName = "GoToDefinition";
        static string iniFilePath = null;
        static bool someSetting = false;
        static frmMyDlg frmMyDlg = null;
        static int idMyDlg = -1;
        static Bitmap tbBmp = Properties.Resources.star;
        static Bitmap tbBmp_tbTab = Properties.Resources.star_bmp;
        static Icon tbIcon = null;

        public static void OnNotification(ScNotification notification)
        {  
            // This method is invoked whenever something is happening in notepad++
            // use eg. as
            // if (notification.Header.Code == (uint)NppMsg.NPPN_xxx)
            // { ... }
            // or
            //
            // if (notification.Header.Code == (uint)SciMsg.SCNxxx)
            // { ... }
        }

        internal static void CommandMenuInit()
        {
            StringBuilder sbIniFilePath = new StringBuilder(Win32.MAX_PATH);
            Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_GETPLUGINSCONFIGDIR, Win32.MAX_PATH, sbIniFilePath);
            iniFilePath = sbIniFilePath.ToString();
            if (!Directory.Exists(iniFilePath)) Directory.CreateDirectory(iniFilePath);
            iniFilePath = Path.Combine(iniFilePath, PluginName + ".ini");
            someSetting = (Win32.GetPrivateProfileInt("SomeSection", "SomeKey", 0, iniFilePath) != 0);

            PluginBase.SetCommand(0, "MyMenuCommand", myMenuFunction, new ShortcutKey(false, false, false, Keys.None));
            PluginBase.SetCommand(1, "MyDockableDialog", myDockableDialog); idMyDlg = 1;
            PluginBase.SetCommand(2, "GoToDefinition", goToDefinition, new ShortcutKey(true, false, false, Keys.F12));
            PluginBase.SetCommand(3, "PrintDebugLine", printDebugLine, new ShortcutKey(false, true, false, Keys.F12));
            PluginBase.SetCommand(4, "Idk", idk, new ShortcutKey(true, true, true, Keys.F12));
            PluginBase.SetCommand(4, "CloseOpenForms", closeOpenForms, new ShortcutKey(false, false, false, Keys.Escape));
        }

        internal static void SetToolBarIcon()
        {
            toolbarIcons tbIcons = new toolbarIcons();
            tbIcons.hToolbarBmp = tbBmp.GetHbitmap();
            IntPtr pTbIcons = Marshal.AllocHGlobal(Marshal.SizeOf(tbIcons));
            Marshal.StructureToPtr(tbIcons, pTbIcons, false);
            Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_ADDTOOLBARICON, PluginBase._funcItems.Items[idMyDlg]._cmdID, pTbIcons);
            Marshal.FreeHGlobal(pTbIcons);
        }

        internal static void closeOpenForms()
        {
            var openForms = new List<Form>();
            foreach(Form form in Application.OpenForms)
                openForms.Add(form);

            foreach (Form form in openForms)
                form.Close();
        }

        internal static void PluginCleanUp()
        {
            Win32.WritePrivateProfileString("SomeSection", "SomeKey", someSetting ? "1" : "0", iniFilePath);
        }


        internal static void myMenuFunction()
        {
            MessageBox.Show("Hello N++!");
        }

        

        internal static void goToDefinition()
        {
            IntPtr hCurrScintilla = PluginBase.GetCurrentScintilla();
            NotepadPPGateway npp = new NotepadPPGateway();
            ScintillaGateway sci = new ScintillaGateway(hCurrScintilla);
            var selectedText = sci.GetSelText();
            if (string.IsNullOrEmpty(selectedText)) return;
            Dictionary<string, string> filePaths = new Dictionary<string, string>();
            var stream = new FileStream(@"plugins/Config/GoToDefinition/sql_repos.txt", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(stream, Encoding.UTF8))
            {
                var text = streamReader.ReadToEnd();
                var allFilePaths = text.Split(';');
                foreach (var filePath in allFilePaths)
                {
                    var kvp = filePath.Split('=');
                    if(kvp.Length > 1)
                        filePaths.Add(kvp[0], kvp[1]);
                }
            }
            StringBuilder sb = new StringBuilder();

            var matchingFileNames = new List<string>();
            foreach (var kvp in filePaths)
            {
                var matchingFiles = Directory.GetFiles(kvp.Value, $"{selectedText}*", SearchOption.TopDirectoryOnly);
                matchingFileNames.AddRange(matchingFiles);
            }

            FilesDialog fd = new FilesDialog(matchingFileNames.ToArray());
            fd.Show();

        }

        internal static void printDebugLine()
        {
            IntPtr hCurrScintilla = PluginBase.GetCurrentScintilla();
            ScintillaGateway sci = new ScintillaGateway(hCurrScintilla);
            var lineNumber = sci.GetCurrentLineNumber() + 1;
            NotepadPPGateway npp = new NotepadPPGateway();
            var fileName = Path.GetFileNameWithoutExtension(npp.GetCurrentFilePath());
            var stream = new FileStream(@"plugins/Config/GoToDefinition/debug_table.txt", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(stream, Encoding.UTF8))
            {
                var text = streamReader.ReadToEnd();
                text = text + $"'{fileName}', {lineNumber}, )";
                sci.AddText(text.Length, text);
            }
        }

        internal static void myDockableDialog()
        {
            if (frmMyDlg == null)
            {
                frmMyDlg = new frmMyDlg();

                using (Bitmap newBmp = new Bitmap(16, 16))
                {
                    Graphics g = Graphics.FromImage(newBmp);
                    ColorMap[] colorMap = new ColorMap[1];
                    colorMap[0] = new ColorMap();
                    colorMap[0].OldColor = Color.Fuchsia;
                    colorMap[0].NewColor = Color.FromKnownColor(KnownColor.ButtonFace);
                    ImageAttributes attr = new ImageAttributes();
                    attr.SetRemapTable(colorMap);
                    g.DrawImage(tbBmp_tbTab, new Rectangle(0, 0, 16, 16), 0, 0, 16, 16, GraphicsUnit.Pixel, attr);
                    tbIcon = Icon.FromHandle(newBmp.GetHicon());
                }

                NppTbData _nppTbData = new NppTbData();
                _nppTbData.hClient = frmMyDlg.Handle;
                _nppTbData.pszName = "My dockable dialog";
                _nppTbData.dlgID = idMyDlg;
                _nppTbData.uMask = NppTbMsg.DWS_DF_CONT_RIGHT | NppTbMsg.DWS_ICONTAB | NppTbMsg.DWS_ICONBAR;
                _nppTbData.hIconTab = (uint)tbIcon.Handle;
                _nppTbData.pszModuleName = PluginName;
                IntPtr _ptrNppTbData = Marshal.AllocHGlobal(Marshal.SizeOf(_nppTbData));
                Marshal.StructureToPtr(_nppTbData, _ptrNppTbData, false);
                Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_DMMREGASDCKDLG, 0, _ptrNppTbData);
            }
            else
            {
                Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_DMMSHOW, 0, frmMyDlg.Handle);
            }
        }

        internal static void idk()
        {
            Process.Start("Chrome.exe", "https://www.youtube.com/watch?v=dQw4w9WgXcQ");
        }
    }
}