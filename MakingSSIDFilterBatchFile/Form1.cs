using System;
using System.IO;
using System.Windows.Forms;

namespace MakingSSIDFilterBatchFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string ssid = textBox1.Text;
            if (ssid.Length <= 0)
            {
                MessageBox.Show("No input SSID");
            }

            string[] lines = {
                "netsh wlan add filter permission = denyall networktype = infrastructure",
                $"netsh wlan add filter permission = allow ssid = \"{ssid}\" networktype = infrastructure",
                $"netsh wlan connect name = \"{ssid}\" ssid = \"{ssid}\"",
                $"netsh wlan set profileparameter name = \"{ssid}\" connectionmode = auto",
            };

            StreamWriter outputAddFile = new($"{Directory.GetCurrentDirectory()}\\MakingFilter_{ssid}.bat");
            if (outputAddFile != null)
            {
                foreach (string line in lines)
                {
                    outputAddFile.WriteLine(line);
                }
            }
            outputAddFile.Close();

            string[] lines2 = {
                "netsh wlan delete filter permission = denyall networktype = infrastructure",
                $"netsh wlan delete filter permission = allow ssid = \"{ssid}\" networktype = infrastructure",
            };

            StreamWriter outputDeleteFile = new($"{Directory.GetCurrentDirectory()}\\RemoveFilter_{ssid}.bat");
            if (outputDeleteFile != null)
            {
                foreach (string line in lines2)
                {
                    outputDeleteFile.WriteLine(line);
                }
            }
            outputDeleteFile.Close();

            MessageBox.Show("Success.");
        }
    }
}
