using ConvertApiDotNet;
using ConvertApiDotNet.Exceptions;
using CSIA.Models;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSIA
{
    public partial class Form1 : Form
    {
        /*************************************** FORM CREATION ***************************************/

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            this.SetControls();
        }

        private void textBoxDownloadDirectory_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelDownloadingTo_Click_2(object sender, EventArgs e)
        {

        }
        private void SetControls()
        {
            //Form
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
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
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
        }

        private void uploadClear_Click(object sender, EventArgs e)
        {
            //Clears files queued to be uploaded
            listBoxInput.Items.Clear();
        }

        /**********************************************************************************************/



        /**********************************  CONVERTING PROCESS  ***************************************/

        private void buttonConvert_ClickAsync(object sender, EventArgs e)
        {
            //HERE: REACH INTO FILES THAT HAVE BEEN CONVERTED 
            //var result = this.PerformConvertAPIAsync();

            

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
            if (f.Substring(f.LastIndexOf('.')).Equals(".heic"))
            {
                Console.WriteLine("File is HEIC type");

                try
                {
                    var convertApi = new ConvertApi("DkZglWGd1z8IKnJb");
                    var convert = await convertApi.ConvertAsync("heic", "jpg", new ConvertApiFileParam("File", @f));
                    await convert.SaveFilesAsync(textBox1.Text);

                    //TESTING IN CONSOLE
                    Console.WriteLine("File converted");
                    Console.WriteLine(textBox1.Text);
                }
                catch (ConvertApiException e)
                {
                    Console.WriteLine("Status Code: " + e.StatusCode);
                    Console.WriteLine("Response: " + e.Response);
                }
            }
            else
            {
                Console.WriteLine("File is not HEIC type");

                try
                {
                    File.Move(@f, textBox1.Text + "\\NEW" + f.Substring(f.LastIndexOf('\\') + 1));
                }
                catch (IOException iox)
                {
                    Console.WriteLine(iox.Message);
                }

                Console.WriteLine("Non-HEIC file moved");
            }
        }

        /**********************************  CONVERTING PROCESS  ***************************************/



        /*************************************** ERROR MESSAGE *****************************************/

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

        /**********************************************************************************************/
    }
}
