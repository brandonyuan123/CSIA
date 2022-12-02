using ConvertApiDotNet;
using ConvertApiDotNet.Exceptions;
using CSIA.Models;
using NuGet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI;
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
            btn.Text = "Downloading...";

            using (var client = new WebClient()) //How to grant the program permission to download files?
            {
                client.DownloadFile("https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/chow-chow-portrait-royalty-free-image-1652926953.jpg?crop=0.44455xw:1xh", "C:\\");
            }
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

        private void uploadClear_Click(object sender, EventArgs e)
        {
            //Clears files queued to be uploaded
            listBox1.Items.Clear();
        }

        private void downloadClear_Click(object sender, EventArgs e)
        {
            //Clears files that are ready to be downloaded
            listBox1.Items.Clear();
        }




        //**************************  CONVERTING PROCESS  ********************************

        private void buttonConvert_ClickAsync(object sender, EventArgs e)
        {
            //HERE: REACH INTO FILES THAT HAVE BEEN CONVERTED 

            var result = this.PerformConvertAPIAsync();

            //nothing to convert?
            if (listBox1.Items.Count == 0)
            {
                //Errormsg.set("01");                   Need to figure out how to transfer error message to 2nd form - make a class maybe?
                this.BtnOpenSecondForm_Click();
            }

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

            //Set the File Filter of OpenFileDialog                         
            openDialog.Filter = "All Files (*.*)|*.*";
            openDialog.Multiselect = true;

            //Get the OK press of the Dialog Box
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                //Get Selected File(s)                                      
                foreach (String file in openDialog.FileNames)
                {
                    listBox1.Items.Add(file.Substring(file.LastIndexOf('\\') + 1));
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


        //****************************** ERROR MESSAGE ***********************************

        //This needs to be able to be called

        private void BtnOpenSecondForm_Click()    
        {

            //Create a thread to RUN a NEW application with the desired form
            Thread t = new Thread(new ThreadStart(ThreadFormTwo));
            t.Start();
        }

        private void ThreadFormTwo()
        {
            //shows error message 
            Application.Run(new frmErrorMessage());

            /*Once the code encounters error,
            create display reasons for each
            error upon creating second form*/
        }
    }
}
