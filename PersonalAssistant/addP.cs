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
    public partial class addP : Form
    {
        public String usernamefile = "C:\\PersonalAssistant\\Users.CJ";
        bool exists = false;
        public Main m;
        String duser;
        public addP(Main m,String duser)
        {
            InitializeComponent();
            this.m = m;
            this.duser = duser;
            byte[] text = GetFileBytes(usernamefile);
            String mat = ASCIIEncoding.ASCII.GetString(text);
            String[] a = mat.Split('$');
            for (int i = 0; i < a.Length - 1; i++)
            {
                comboBox1.Items.Add(a[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String user = comboBox1.Text;
            String p = textBox1.Text;
            Directory.CreateDirectory("C:\\PersonalAssistant\\" + user + "\\" + p);
            if (!File.Exists("C:\\PersonalAssistant\\" + user + "\\" + p +"\\log.CJ"))
            {
                FileStream users = new FileStream("C:\\PersonalAssistant\\" + user + "\\" + p + "\\log.CJ", FileMode.CreateNew);
                users.Close();
                exists = true;
            }
            else
            {
                MessageBox.Show("Project Already Exists !");
                exists = false;
            }
            if(exists)
            {
                var project = new FileStream("C:\\PersonalAssistant\\" + user + "\\"+user+"projects.CJ", FileMode.Append);
                byte[] pname = Encoding.ASCII.GetBytes(p + "$");
                project.Write(pname, 0, pname.Length);
                project.Close();
                if (user != null && p != null)
                {
                    var file = new FileStream("C:\\PersonalAssistant\\" + user + "\\" + p +"\\log.CJ", FileMode.Append);
                    byte[] name = Encoding.ASCII.GetBytes("   DATE          TIME      DURATION\n");
                    file.Write(name, 0, name.Length);
                    file.Close();
                    FileStream users = new FileStream("C:\\PersonalAssistant\\" + comboBox1.Text + "\\" + textBox1.Text +"\\Comments.CJ", FileMode.Append);
                    users.Close();
                    FileStream use = new FileStream("C:\\PersonalAssistant\\" + comboBox1.Text + "\\" + textBox1.Text + "\\Duration.CJ", FileMode.Append);
                    use.Close();
                    var use1 = new FileStream("C:\\PersonalAssistant\\" + comboBox1.Text + "\\" + textBox1.Text + "\\Duration.CJ", FileMode.Append);
                    byte[] name1 = Encoding.ASCII.GetBytes("0:0:0");
                    use1.Write(name1, 0, name1.Length);
                    use1.Close();
                } 
                MessageBox.Show("Project Added Successfully");
                if (user.Equals(duser))
                {
                    m.ListProject(user);
                }
                addP.ActiveForm.Close();
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
    }
}
