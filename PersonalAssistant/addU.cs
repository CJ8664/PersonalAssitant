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
    public partial class addU : Form
    {
        Main m;
        public String filename = "C:\\PersonalAssistant\\Users.CJ";
        public addU(Main m)
        {
            this.m = m;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            String u = textBox1.Text;
            if (!UserExists(u))
            {
                var user = new FileStream(filename, FileMode.Append);
                byte[] name = Encoding.ASCII.GetBytes(u + "$");
                user.Write(name, 0, name.Length);
                user.Close();
                Directory.CreateDirectory("C:\\PersonalAssistant\\" + u);
                if (!File.Exists("C:\\PersonalAssistant\\"+u+"\\"+u+"projects.CJ"))
                {
                    FileStream users = new FileStream("C:\\PersonalAssistant\\" + u + "\\" + u + "projects.CJ", FileMode.CreateNew);
                    users.Close();
                }

                m.createList();
                MessageBox.Show("User Added Successfully");
                
                
                addU.ActiveForm.Close();
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
        public bool UserExists(String name)
        {
            byte[] text = GetFileBytes(filename);
            String mat = ASCIIEncoding.ASCII.GetString(text);
            bool exixts = mat.Contains(name);
            if (exixts)
            {
                MessageBox.Show("User Already Exists !");
            }
            return exixts;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
