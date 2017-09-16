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
    public partial class removeU : Form
    {
        public Main m;
        public String filename = "C:\\PersonalAssistant\\Users.CJ";
        public removeU(Main m)
        {
            this.m = m;
            InitializeComponent();
            byte[] text = GetFileBytes(filename);
            String mat = ASCIIEncoding.ASCII.GetString(text);
            String [] a=mat.Split('$');
            for (int i = 0; i < a.Length - 1; i++)
            {
                comboBox1.Items.Add(a[i]);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            String name = comboBox1.Text;
            
            byte[] text = GetFileBytes(filename);
            String mat = ASCIIEncoding.ASCII.GetString(text);
            mat=mat.Replace(name+"$", "");
            FileStream fs = new FileStream(filename, FileMode.Create);
            byte [] users=Encoding.ASCII.GetBytes(mat);
            fs.Write(users, 0, mat.Length);
            fs.Close();
            Directory.Delete("C:\\PersonalAssistant\\" + name,true);
            MessageBox.Show("User Removed Successfully");
            m.createList();
            removeU.ActiveForm.Close();
        }
        public bool UserExists(String name)
        {
            byte[] text = GetFileBytes(filename);
            String mat = ASCIIEncoding.ASCII.GetString(text);
            bool exixts = mat.Contains(name);
            if (!exixts)
            {
                MessageBox.Show("User doesn't Exist !");
            }
            return exixts;
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
    }
}
