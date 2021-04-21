using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace 点名器
{
    public partial class Form1 : Form
    {
        string path;
        [Obsolete]
        string[] sections_list;
        public ArrayList students = new ArrayList();
        static int i;
        public int Interval, seed, type, rate, volume;
        public string t_seed;
        public bool voiceEnable;
        Random rd;
        public static Form1 f1;
        XMLOperator ope;

        public Form1()
        {
            InitializeComponent();
            f1 = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "点名器 V1.1.1.0(Beta)";
            pictureBox1.BackColor = Color.Gray;
            path = Environment.CurrentDirectory;
            checkBox1.Visible = false;
            语音相关设置ToolStripMenuItem.Visible = false;
            ope = new XMLOperator(path + "\\names.xml");

            /*if (File.Exists(path + "\\names.txt") == false)
            {
                //File.Create(path + @"\names.txt");
                IniOperate.Write("Person0", "name", "这里输入姓名", path + "\\names.txt");
                IniOperate.Write("Person0", "sex", "这里输入性别（M:男，F:女）", path + "\\names.txt");
                IniOperate.Write("Person0", "class", "这里输入班级", path + "\\names.txt");
                IniOperate.Write("Person0", "ID", "这里输入学号", path + "\\names.txt");
                //IniOperate.Write("Person0", "IsDefault", "false", path + "\\names.txt");
                IniOperate.Write("Person0", "ImagePath", "这里输入学生相关图片路径", path + "\\names.txt");
                Initial_1();
            }*/
            if (File.Exists(path + "\\names.xml") == false)
            {
                Person tp = new Person("姓名", "性别", "学号", "班级", "学生相关图片路径");
                students.Add(tp);
                ope.CreateFile(students);
                Initial();
            }
            else
            {
                //names_init = File.ReadAllText(path + @"\names.txt");
                //Initial_1();
                Initial();
            }
            /*for (int i = 0; i < sections_list.Length; i++)
            {
                EditnameForm.sections_list.Add(sections_list[i]);
            }*/
            //names_list = names_init.Split('\n');
        }
        public void Initial()
        {
            Interval = Convert.ToInt32(IniOperate.Read("Random", "interval", "100", path + "\\config.ini"));
            //seed = Convert.ToInt32(IniOperate.Read("Random", "seed", "-1", path + "\\config.ini"));
            t_seed = IniOperate.Read("Random", "seed", "default", path + "\\config.ini");
            try
            {
                type = Convert.ToInt32(IniOperate.Read("General", "type", "0", path + "\\config.ini"));
            }
            catch (FormatException e1)
            {
                MessageBox.Show(this, "config.ini文件中type的值\"" + IniOperate.Read("General", "type", "0", path + "\\config.ini").ToString() + "\"类型不正确，请将config.ini中的type值改为0或1!\r\n" + e1.ToString(), "错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string t_log = "";
                try
                {
                    t_log = File.ReadAllText(Environment.CurrentDirectory + "\\debug.log", Encoding.UTF8);
                }
                catch (FileNotFoundException e2)
                {
                    File.WriteAllText(Environment.CurrentDirectory + "\\debug.log", t_log);
                }
                finally
                {
                    t_log = t_log + "\n" + DateTime.Now.ToString() + ": " + e1.ToString();
                    File.WriteAllText(Environment.CurrentDirectory + "\\debug.log", t_log);
                }
                Close();
            }

            string t_ve = IniOperate.Read("Voice", "voiceEnable", "false", path + "\\config.ini");
            if (t_ve == "true") voiceEnable = true;
            else if (t_ve == "false") voiceEnable = false;
            if (type == 0) radioButton1.Checked = true;
            else if (type == 1) radioButton2.Checked = true;
            checkBox1.Checked = voiceEnable;
            students = ope.GetAllInfomationInPerson();
            if (t_seed == "default") rd = new Random();
            else
            {
                try
                {
                    seed = Convert.ToInt32(t_seed);
                }
                catch (FormatException e1)
                {
                    MessageBox.Show(this, "config.ini文件中seed的值\"" + t_seed.ToString() + "\"类型不正确，请将config.ini中的seed值改为\"default\"或整数!\r\n" + e1.ToString(), "错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    t_seed = "default";
                    string t_log = "";
                    try
                    {
                        t_log = File.ReadAllText(Environment.CurrentDirectory + "\\debug.log", Encoding.UTF8);
                    }
                    catch (FileNotFoundException e2)
                    {
                        File.WriteAllText(Environment.CurrentDirectory + "\\debug.log", t_log);
                    }
                    finally
                    {
                        t_log = t_log + "\n" + DateTime.Now.ToString() + ": " + e1.ToString();
                        File.WriteAllText(Environment.CurrentDirectory + "\\debug.log", t_log);
                    }
                    Close();
                }
                rd = new Random(seed);
            }
        }
        [Obsolete]
        public void Initial_1()
        {
            Interval = Convert.ToInt32(IniOperate.Read("Random", "interval", "100", path + "\\config.ini"));
            //seed = Convert.ToInt32(IniOperate.Read("Random", "seed", "-1", path + "\\config.ini"));
            t_seed = IniOperate.Read("Random", "seed", "default", path + "\\config.ini");
            try
            {
                type = Convert.ToInt32(IniOperate.Read("General", "type", "0", path + "\\config.ini"));
            }
            catch (FormatException e1)
            {
                MessageBox.Show(this, "config.ini文件中type的值\"" + IniOperate.Read("General", "type", "0", path + "\\config.ini").ToString() + "\"类型不正确，请将config.ini中的type值改为0或1!\r\n" + e1.ToString(), "错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string t_log = "";
                try
                {
                    t_log = File.ReadAllText(Environment.CurrentDirectory + "\\debug.log", Encoding.UTF8);
                }
                catch (FileNotFoundException e2)
                {
                    File.WriteAllText(Environment.CurrentDirectory + "\\debug.log", t_log);
                }
                finally
                {
                    t_log = t_log + "\n" + DateTime.Now.ToString() + ": " + e1.ToString();
                    File.WriteAllText(Environment.CurrentDirectory + "\\debug.log", t_log);
                }
                Close();
            }

            string t_ve = IniOperate.Read("Voice", "voiceEnable", "false", path + "\\config.ini");
            if (t_ve == "true") voiceEnable = true;
            else if (t_ve == "false") voiceEnable = false;
            if (type == 0) radioButton1.Checked = true;
            else if (type == 1) radioButton2.Checked = true;
            checkBox1.Checked = voiceEnable;
            sections_list = IniOperate.GetAllSectionNames(path + "\\names.txt");
            foreach(string section in sections_list)
            {
                string t_name = IniOperate.Read(section, "name", "null", path + "\\names.txt");
                string t_sex = IniOperate.Read(section, "sex", "null", path + "\\names.txt");
                string t_class = IniOperate.Read(section, "class", "null", path + "\\names.txt");
                string t_ID = IniOperate.Read(section, "ID", "null", path + "\\names.txt");
                string t_IP = IniOperate.Read(section, "ImagePath", "null", path + "\\names.txt");
                Person t_student = new Person(t_name, t_sex, t_ID, t_class, t_IP, section);
                students.Add(t_student);
            }
            
            if (t_seed == "default") rd = new Random();
            else
            {
                try
                {
                    seed = Convert.ToInt32(t_seed);
                }
                catch (FormatException e1)
                {
                    MessageBox.Show(this, "config.ini文件中seed的值\"" + t_seed.ToString() + "\"类型不正确，请将config.ini中的seed值改为\"default\"或整数!\r\n" + e1.ToString(), "错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    t_seed = "default";
                    string t_log = "";
                    try
                    {
                        t_log = File.ReadAllText(Environment.CurrentDirectory + "\\debug.log", Encoding.UTF8);
                    }
                    catch (FileNotFoundException e2)
                    {
                        File.WriteAllText(Environment.CurrentDirectory + "\\debug.log", t_log);
                    }
                    finally
                    {
                        t_log = t_log + "\n" + DateTime.Now.ToString() + ": " + e1.ToString();
                        File.WriteAllText(Environment.CurrentDirectory + "\\debug.log", t_log);
                    }
                    Close();
                }
                rd = new Random(seed);
            }
        }
        private void Show(Person stu)
        {
            textBox1.Text = stu.GetName();
            string t_str = stu.GetSex();
            if ("M".Equals(t_str.ToUpper())) textBox2.Text = "男";
            else if ("F".Equals(t_str.ToUpper())) textBox2.Text = "女";
            else textBox2.Text = "其他";
            textBox3.Text = stu.GetClass();
            textBox4.Text = stu.GetPersonID();
            if (File.Exists(stu.GetPersonnalImagePath()))
            {
                try
                {
                    pictureBox1.Image = Image.FromFile(stu.GetPersonnalImagePath());
                }
                catch (OutOfMemoryException e)
                {

                    pictureBox1.Image = null; 
                    string t_log = "";
                    try
                    {
                        t_log = File.ReadAllText(path + "\\debug.log", Encoding.UTF8);
                    }
                    catch (FileNotFoundException e1)
                    {
                        File.WriteAllText(path + "\\debug.log", t_log);
                    }
                    finally
                    {
                        t_log = t_log + "\n" + DateTime.Now.ToString() + ": " + e.ToString();
                        File.WriteAllText(path + "\\debug.log", t_log);
                    }
                }
            }
            else
            {
                pictureBox1.Image = null;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (type == 0)
            {
                Person element = (Person)students[rd.Next(0, students.Count)];
                Show(element);

            }
            else if (type == 1)
            {
                Person element = (Person)students[i];
                Show(element);
                if (i > students.Count - 2) i = 0;
                else i++;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            type = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            type = 1;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            voiceEnable = checkBox1.Checked;
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm f = new AboutForm();
            f.Show();
        }
        private bool IsProcessExist()
        {
            Process[] processList = Process.GetProcesses();
            foreach (Process process in processList)
            {
                //MessageBox.Show(process.ProcessName);
                if (process.ProcessName == "notepad")
                {
                    //MessageBox.Show("Y");
                    //process.Kill(); //结束进程
                    return true;
                }

            }
            return false;
        }
        private void 编辑名单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*MessageBox.Show(this, "完成编辑后，请重启程序！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Process.Start("notepad", "names.txt");
            Close();*/
            EditnameForm f = new EditnameForm();
            f.Show();
        }

        private void 抽取设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setting_Random f = new Setting_Random();
            f.Show();
        }

        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
            contextMenuStrip1.Show((Button)sender, new Point(e.X, e.Y));
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            IniOperate.Write("General", "type", Convert.ToString(type), path + "\\config.ini");
            //IniOperate.Write("Random", "seed", Convert.ToString(seed), path + "\\config.ini");
            if (t_seed == "default") IniOperate.Write("Random", "seed", "default", path + "\\config.ini");
            else IniOperate.Write("Random", "seed", Convert.ToString(seed), path + "\\config.ini");
            IniOperate.Write("Random", "interval", Convert.ToString(Interval), path + "\\config.ini");
            if (voiceEnable == true) IniOperate.Write("Voice", "voiceEnable", "true", path + "\\config.ini");
            else if (voiceEnable == false) IniOperate.Write("Voice", "voiceEnable", "false", path + "\\config.ini");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(students.Count <= 0)
            {
                MessageBox.Show(this, "名单为空，请添加名单！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (button1.Text == "开始")
            {
                button1.Text = "停止";
                timer1.Interval = Interval;
                timer1.Enabled = true;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                timer1.Start();
            }
            else if (button1.Text == "停止")
            {
                button1.Text = "开始";
                timer1.Enabled = false;
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                timer1.Stop();
                textBox5.Text += "姓名：" + textBox1.Text + " 学号：" + textBox4.Text + "\r\n";
                textBox5.Focus();
                textBox5.Select(textBox5.TextLength, 0);//光标定位到文本最后
                textBox5.ScrollToCaret();
            }
        }
        public void WriteXmlFile(ArrayList per)
        {
            ope.CreateFile(per);
        }
        
    }
}
