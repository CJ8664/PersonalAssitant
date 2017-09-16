using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace PersonalAssistant
{
    public partial class showLog : Form
    {
        public String userfilename = "C:\\PersonalAssistant\\Users.CJ";
        public String projectsfilename = "C:\\PersonalAssistant\\Projects.CJ";
        public showLog()
        {
            InitializeComponent();
            byte[] text1 = GetFileBytes(userfilename);
            String mat1 = ASCIIEncoding.ASCII.GetString(text1);
            String[] a1 = mat1.Split('$');
            for (int i = 0; i < a1.Length - 1; i++)
            {
                comboBox1.Items.Add(a1[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String user = comboBox1.Text;
            String project = comboBox2.Text;
            if (user == "")
            {
                MessageBox.Show("Enter a valid user");
            }
            else if (project == "")
            {
                MessageBox.Show("Enter a valid project");
            }
            else
            {
                String usernamefile = "C:\\PersonalAssistant\\" + user + "\\" + project+"\\" + "log.CJ";
                byte[] text = GetFileBytes(usernamefile);
                String mat = ASCIIEncoding.ASCII.GetString(text);
                MessageBox.Show(mat,"Log Record for "+user+"'s project : "+project);
            }
        }
        public static byte[] GetFileBytes(string filename)
        {
            if (File.Exists(filename))
            {
                FileStream fs = File.Open(filename, FileMode.Open, FileAccess.Read);
                byte[] fileBytes = new byte[fs.Length];
                fs.Read(fileBytes, 0, (int)fs.Length);
                fs.Close();
                return fileBytes;
            }
            return null;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            String uname = comboBox1.Text;
            byte[] text = GetFileBytes("C:\\PersonalAssistant\\" + uname + "\\" + uname + "projects.CJ");
            String mat = ASCIIEncoding.ASCII.GetString(text);
            String[] a = mat.Split('$');
            for (int i = 0; i < a.Length - 1; i++)
            {
                comboBox2.Items.Add(a[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String user = comboBox1.Text;
            String project = comboBox2.Text;
            if (user == "")
            {
                MessageBox.Show("Enter a valid user");
            }
            else if (project == "")
            {
                MessageBox.Show("Enter a valid project");
            }
            else
            {
                var file = new FileStream("C:\\PersonalAssistant\\" + user + "\\" + project +"\\"+ "log.CJ",FileMode.Truncate);
                byte[] name = Encoding.ASCII.GetBytes("   DATE          TIME      DURATION\n");
                file.Write(name, 0, name.Length);
                file.Close();
            }
        }
    }
}
