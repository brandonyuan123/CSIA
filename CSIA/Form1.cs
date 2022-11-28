using ConvertApiDotNet;
using ConvertApiDotNet.Exceptions;
using CSIA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSIA
{
    public partial class Form1 : Form
    {
        string selectedfile = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            this.SetControls();
        }

        private void SetControls()
        {
            //Form
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            // displays something when the button is clicked
            Button btn = sender as Button;
            btn.Text = "clicked!";
        }

        private void listBoxUpload_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
        }

        private void listBoxDownload_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void labelTitle_Click(object sender, EventArgs e)
        {

        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            this.OpenFileDialogWindow();

            //DONT NEED THIS FOR NOW
            //this.ImportData();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
        }




//*****************  CONVERTING PROCESS  **********************

        private void buttonConvert_ClickAsync(object sender, EventArgs e)
        {
            //HERE: REACH INTO FILES THAT HAVE BEEN CONVERTED 

            var result = this.PerformConvertAPIAsync();

            //converting text
            Button btn = sender as Button;
            btn.Text = "Converting...";
        }

        private void OpenFileDialogWindow()
        {
            string dbasepath = CurrentPath.GetDbasePath();

            OpenFileDialog openDialog = new OpenFileDialog();

            //Set Title of OpenFileDialog
            openDialog.Title = "Select A Text File";
            //Set directory path
            openDialog.InitialDirectory = dbasepath;

            //Set the File Filter of OpenFileDialog                         THIS NEEDS TO FILTER FOR FOLDERS/IMAGES 
            openDialog.Filter = "All Files (*.*)|*.*";
            openDialog.Multiselect = true;

            //Get the OK press of the Dialog Box
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                //Get Selected File(s)                                      this works for one: selectedfile = openDialog.FileName;
                foreach (String file in openDialog.FileNames)
                {

                }
            }
        }
        private void ImportData()
        {
            
        }

        private async Task PerformConvertAPIAsync()
        {
            var convertApi = new ConvertApi("DkZglWGd1z8IKnJb");
            var convert = await convertApi.ConvertAsync("heic", "jpg", new ConvertApiFileParam("File", @"INSERT FILES THAT NEED TO BE CONVERTED HERE"));
            await convert.SaveFilesAsync(@"C:\Users\Default\Downloads");
        }

        private void uploadClear_Click(object sender, EventArgs e)
        {

        }

        private void downloadClear_Click(object sender, EventArgs e)
        {

        }
    }
}
