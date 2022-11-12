using ConvertApiDotNet;
using ConvertApiDotNet.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSIA
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //test
            Button btn = sender as Button;
            btn.Text = "clicked!";
        }

        private async Task convert_ClickAsync(object sender, EventArgs ee) //need to open file directory to store files to convert
        {
            //var convertApi = new ConvertApi("DkZglWGd1z8IKnJb");
            //var convert = await convertApi.ConvertAsync("heic", "jpg", new ConvertApiFileParam("File", @"C:\path\to\my_file.heic"));
            //await convert.SaveFilesAsync(@"C:\converted-files\");

            try
            {
                var convertApi = new ConvertApi("DkZglWGd1z8IKnJb");
                var conversionTask = await convertApi.ConvertAsync("heic", "jpg",
                    new ConvertApiFileParam(@"c:\source\test.docx")
                    );
                var fileSaved = await conversionTask.Files.SaveFilesAsync(@"c:\");
            }
            //Catch exceptions from asynchronous methods
            catch (ConvertApiException e)
            {
                //?
            }

            //test
            Button btn = sender as Button;
            btn.Text = "clicked!";
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //listBox1.Items.Add("Item");
            // add items????
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
        }
    }
}
