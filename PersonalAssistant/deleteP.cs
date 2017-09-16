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
    public partial class deleteP : Form
    {
        public String usernamefile = "C:\\PersonalAssistant\\Users.CJ";
        public Main m;
        public deleteP(Main m)
        {
            this.m = m;
            InitializeComponent();
            byte[] text = GetFileBytes(usernamefile);
            String mat = ASCIIEncoding.ASCII.GetString(text);
            String[] a = mat.Split('$');
            for (int i = 0; i < a.Length-1; i++)
            {
                comboBox2.Items.Add(a[i]);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            String pname = comboBox1.Text;
            String uname = comboBox2.Text;
            byte[] text = GetFileBytes("C:\\PersonalAssistant\\" + uname + "\\" + uname + "projects.CJ");
            String mat = ASCIIEncoding.ASCII.GetString(text);
            mat = mat.Replace(pname + "$", "");
            FileStream fs = new FileStream("C:\\PersonalAssistant\\" + uname + "\\" + uname + "projects.CJ", FileMode.Create);
            byte[] projects = Encoding.ASCII.GetBytes(mat);
            fs.Write(projects, 0, mat.Length);
            fs.Close();
            Directory.Delete("C:\\PersonalAssistant\\" + uname + "\\" + pname,true);
            MessageBox.Show("Project Closed Successfully");
            m.ListProject(uname);
            removeU.ActiveForm.Close();
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            String uname=comboBox2.Text;
            byte[] text = GetFileBytes("C:\\PersonalAssistant\\"+uname+"\\"+uname+"projects.CJ");
            String mat = ASCIIEncoding.ASCII.GetString(text);
            String[] a = mat.Split('$');
            for (int i = 0; i < a.Length - 1; i++)
            {
                comboBox1.Items.Add(a[i]);
            }
        }
    }
}
