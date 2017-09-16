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
    public partial class Main : Form
    {
        public DateTime start;
        public DateTime stop;
        public String usernamefile = "C:\\PersonalAssistant\\Users.CJ";
        public Main()
        {
            InitializeComponent();
            button2.Visible = false;
            textBox1.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            Directory.CreateDirectory("C:\\PersonalAssistant");
            if (!File.Exists("C:\\PersonalAssistant\\Users.CJ"))
            {
                FileStream users = new FileStream("C:\\PersonalAssistant\\Users.CJ", FileMode.CreateNew);
                users.Close();
            }
            createList();
        }

        public  void createList()
        {
            byte[] text = GetFileBytes(usernamefile);
            String mat = ASCIIEncoding.ASCII.GetString(text);
            String[] a = mat.Split('$');
            comboBox1.Items.Clear();
            for (int i = 0; i < a.Length - 1; i++)
            {
                
                comboBox1.Items.Add(a[i]);
                
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void useToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            addU f2 = new addU(this);
            f2.Show();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            String user = comboBox1.Text;
            String project = comboBox2.Text;
            if (user=="")
            {
                MessageBox.Show("Enter a valid user");
            }
            else if (project == "")
            {
                MessageBox.Show("Enter a valid project");
            }
            else
            {
                start = DateTime.Now;
                button2.Visible = true;
                button1.Visible = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                Main.ActiveForm.ControlBox = false;
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.ShowDialog();
            
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addP ap = new addP(this,comboBox1.Text);
            ap.ShowDialog();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            comboBox2.Items.Clear();
            deleteP dp = new deleteP(this);
            dp.ShowDialog();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showLog sl = new showLog();
            sl.ShowDialog();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            removeU ru = new removeU(this);
            ru.ShowDialog();
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
            String uname = comboBox1.Text;
            ListProject(uname);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button1.Visible = true;
            stop = DateTime.Now;
            TimeSpan temp = stop - start;
            String user = comboBox1.Text;
            String project = comboBox2.Text;
            if (user != null && project != null && temp.ToString()!=null)
            {
                var file = new FileStream("C:\\PersonalAssistant\\" + user + "\\" + project +"\\" +"log.CJ", FileMode.Append);
                String date = start.Day.ToString() + "-" + start.Month.ToString() + "-" + start.Year.ToString();
                String time = start.Hour.ToString() + ":" + start.Minute.ToString() + ":" + start.Second.ToString();
                String duration = temp.Hours.ToString() + ":" + temp.Minutes.ToString() + ":" + temp.Seconds.ToString();

                byte[] name = Encoding.ASCII.GetBytes(date + "    " + time + "      " + duration + "\n");
                file.Write(name, 0, name.Length);
                file.Close();
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                MessageBox.Show("Log Record Added Successfully");
                byte []text= GetFileBytes("C:\\PersonalAssistant\\" + comboBox1.Text + "\\" + comboBox2.Text + "\\Duration.CJ");
                String dur = ASCIIEncoding.ASCII.GetString(text);
                String[] dura = dur.Split(':');
                String[] dura1 = dura[2].Split('.');
                TimeSpan d = new TimeSpan(int.Parse(dura[0]+""), int.Parse(dura[1]+""), int.Parse(dura1[0]+""));
                var file1 = new FileStream("C:\\PersonalAssistant\\" + comboBox1.Text + "\\" + comboBox2.Text + "\\Duration.CJ",FileMode.Truncate);
                byte[] text2=Encoding.ASCII.GetBytes((temp+d)+" ");
                file1.Write(text2, 0, text2.Length);
                file1.Close();
                Main.ActiveForm.ControlBox = true;
            }
            
        }
        public void ListProject(String uname)
        {
            comboBox2.Items.Clear();
            byte[] text = GetFileBytes("C:\\PersonalAssistant\\" + uname + "\\" + uname + "projects.CJ");
            String mat = ASCIIEncoding.ASCII.GetString(text);
            String[] a = mat.Split('$');
            for (int i = 0; i < a.Length - 1; i++)
            {
                comboBox2.Items.Add(a[i]);
            }
        }

        private void addCommentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Enter a valid user");
            }
            else if (comboBox2.Text == "")
            {
                MessageBox.Show("Enter a valid project");
            }
            else
            {
                textBox1.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var file = new FileStream("C:\\PersonalAssistant\\" + comboBox1.Text + "\\" + comboBox2.Text + "\\Comments.CJ", FileMode.Append);
            byte[] name = Encoding.ASCII.GetBytes(textBox1.Text+"  "+System.DateTime.Now.ToString()+"\n");
            file.Write(name, 0, name.Length);
            file.Close();
            textBox1.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            byte[] text = GetFileBytes("C:\\PersonalAssistant\\" + comboBox1.Text + "\\" + comboBox2.Text + "\\Comments.CJ");
            String mat = ASCIIEncoding.ASCII.GetString(text);
            MessageBox.Show(mat,"Comments");
        }

        private void finishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Enter a valid user");
            }
            else if (comboBox2.Text == "")
            {
                MessageBox.Show("Enter a valid project");
            }
            else
            {
                byte[] text = GetFileBytes("C:\\PersonalAssistant\\" + comboBox1.Text + "\\" + comboBox2.Text + "\\Duration.CJ");
                String dur = ASCIIEncoding.ASCII.GetString(text);
                MessageBox.Show(dur, "Total time spent by " + comboBox1.Text + " on " + comboBox2.Text);

            }

        }
        
    }
}
