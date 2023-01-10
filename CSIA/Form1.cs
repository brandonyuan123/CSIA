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
                client.DownloadFile("https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/chow-chow-portrait-royalty-free-image-1652926953.jpg?crop=0.44455xw:1xh", "C:\\Users\\Default\\Downloads");
            }
        }

        private void listBoxUpload_SelectedIndexChanged_1(object sender, EventArgs e)
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
            listBoxInput.Items.Clear();
        }

        //**************************  CONVERTING PROCESS  ********************************

        private void buttonConvert_ClickAsync(object sender, EventArgs e)
        {
            //HERE: REACH INTO FILES THAT HAVE BEEN CONVERTED 
            //var result = this.PerformConvertAPIAsync();

            //Test Case
            //_ = this.PerformConvertAPIAsync(@"C:\Users\s312467\Downloads\s.heic");

            //nothing to convert?
            if (listBoxInput.Items.Count == 0)
            {
                this.BtnOpenSecondForm_Click();
            }

            foreach (string x in listBoxInput.Items)
            {
                _ = this.PerformConvertAPIAsync(x.Substring(x.IndexOf("        ---->        ") + 21));
            }
        }

        private void OpenFileDialogWindow()
        {
            string dbasepath = CurrentPath.GetDbasePath();

            OpenFileDialog openDialog = new OpenFileDialog();

            //Set Title of OpenFileDialog
            openDialog.Title = "Select File(s)";
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
                    listBoxInput.Items.Add(file.Substring(file.LastIndexOf('\\') + 1) + "        ---->        " + file);
                }
            }
        }
        
        private async Task PerformConvertAPIAsync(String f)
        {
            try
            {
                var convertApi = new ConvertApi("DkZglWGd1z8IKnJb");
                var convert = await convertApi.ConvertAsync("heic", "jpg", new ConvertApiFileParam("File", @f));
                await convert.SaveFilesAsync(textBox1.Text);                                 

                //TESTING IN CONSOLE
                Console.WriteLine("working");
                Console.WriteLine(textBox1.Text);
            }
            catch (ConvertApiException e)
            {
                Console.WriteLine("Status Code: " + e.StatusCode);
                Console.WriteLine("Response: " + e.Response);
            }
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

        

        private void textBoxDownloadDirectory_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelDownloadingTo_Click_2(object sender, EventArgs e)
        {

        }

        private void labelUploadingFrom_Click(object sender, EventArgs e)
        {

        }

        private void textBoxUploadDirectory_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
