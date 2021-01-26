using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace 点名器
{
    public partial class Form1 : Form
    {
        string path;
        string[] sections_list;
        public ArrayList students = new ArrayList();
        static int i;
        public int Interval, seed, type, rate, volume;
        public string t_seed;
        public bool voiceEnable;
        public static Form1 f1;

        IniOperate iniop = new IniOperate();
        public Form1()
        {
            InitializeComponent();
            f1 = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "点名器 Alpha V1.0";
            pictureBox1.BackColor = Color.Gray;
            path = Environment.CurrentDirectory;
            checkBox1.Visible = false;
            语音相关设置ToolStripMenuItem.Visible = false;

            if (File.Exists(path + "\\names.txt") == false)
            {
                //File.Create(path + @"\names.txt");
                iniop.Write("Person0", "name", "这里输入姓名", path + "\\names.txt");
                iniop.Write("Person0", "sex", "这里输入性别（M:男，F:女）", path + "\\names.txt");
                iniop.Write("Person0", "class", "这里输入班级", path + "\\names.txt");
                iniop.Write("Person0", "ID", "这里输入学号", path + "\\names.txt");
                //iniop.Write("Person0", "IsDefault", "false", path + "\\names.txt");
                iniop.Write("Person0", "ImagePath", "这里输入学生相关图片路径", path + "\\names.txt");
                Initial();
            }
            else
            {
                //names_init = File.ReadAllText(path + @"\names.txt");
                Initial();
            }
            //names_list = names_init.Split('\n');
        }
        public void Initial()
        {
            Interval = Convert.ToInt32(iniop.Read("Random", "interval", "100", path + "\\config.ini"));
            //seed = Convert.ToInt32(iniop.Read("Random", "seed", "-1", path + "\\config.ini"));
            t_seed = iniop.Read("Random", "seed", "default", path + "\\config.ini");
            type = Convert.ToInt32(iniop.Read("General", "type", "0", path + "\\config.ini"));
            string t_ve = iniop.Read("Voice", "voiceEnable", "false", path + "\\config.ini");
            if (t_ve == "true") voiceEnable = true;
            else if (t_ve == "false") voiceEnable = false;
            if (type == 0) radioButton1.Checked = true;
            else if (type == 1) radioButton2.Checked = true;
            checkBox1.Checked = voiceEnable;
            sections_list = iniop.GetAllSectionNames(path + "\\names.txt");
            foreach(string section in sections_list)
            {
                string t_name = iniop.Read(section, "name", "null", path + "\\names.txt");
                string t_sex = iniop.Read(section, "sex", "null", path + "\\names.txt");
                string t_class = iniop.Read(section, "class", "null", path + "\\names.txt");
                string t_ID = iniop.Read(section, "ID", "null", path + "\\names.txt");
                string t_IP = iniop.Read(section, "ImagePath", "null", path + "\\names.txt");
                Person t_student = new Person(t_name, t_sex, t_ID, t_class, t_IP);
                students.Add(t_student);
            }
        }
        private void Show(Person stu)
        {
            textBox1.Text = stu.GetName();
            string t_str = stu.GetSex();
            if (t_str == "M") textBox2.Text = "男";
            else if (t_str == "F") textBox2.Text = "女";
            else textBox2.Text = "其他";
            textBox3.Text = stu.GetClass();
            textBox4.Text = stu.GetPersonID();
            if (File.Exists(stu.GetPersonnalImagePath()))
            {
                pictureBox1.Image = Image.FromFile(stu.GetPersonnalImagePath());
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (type == 0)
            {
                Random rd;
                if (t_seed == "default") rd = new Random();
                else
                {
                    seed = Convert.ToInt32(t_seed);
                    rd = new Random(seed);
                }
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
            iniop.Write("General", "type", Convert.ToString(type), path + "\\config.ini");
            //iniop.Write("Random", "seed", Convert.ToString(seed), path + "\\config.ini");
            if (t_seed == "default") iniop.Write("Random", "seed", "default", path + "\\config.ini");
            else iniop.Write("Random", "seed", Convert.ToString(seed), path + "\\config.ini");
            iniop.Write("Random", "interval", Convert.ToString(Interval), path + "\\config.ini");
            if (voiceEnable == true) iniop.Write("Voice", "voiceEnable", "true", path + "\\config.ini");
            else if (voiceEnable == false) iniop.Write("Voice", "voiceEnable", "false", path + "\\config.ini");
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
            }
        }
    }
}
