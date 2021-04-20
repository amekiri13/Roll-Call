using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace 点名器
{
    public partial class EditnameForm : Form
    {
        private int index = 0;
        public ArrayList stuList = new ArrayList();
        public static ArrayList sections_list = new ArrayList();
        private string defaultPath;
        
        public EditnameForm()
        {
            InitializeComponent();
        }

        private void EditnameForm_Load(object sender, EventArgs e)
        {
            button4.Enabled = false;
            for(int i = 0; i < Form1.f1.students.Count; i++)
            {
                Person t_p = (Person)Form1.f1.students[i];
                Person t_p1 = new Person(t_p.GetName(), t_p.GetSex(), t_p.GetPersonID(), t_p.GetClass(), t_p.GetPersonnalImagePath(), t_p.GetLocalSection());
                stuList.Add(t_p1);
            }
            show();
            label6.Text = "第 " + Convert.ToString(index + 1) + " 个 / 共 " + Convert.ToString(stuList.Count) + " 个";
            openFileDialog1.Filter = "图像文件|*.jpg;*.png;*.bmp|所有文件|*.*";
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            defaultPath = IniOperate.Read("General", "default_path", Environment.CurrentDirectory, Environment.CurrentDirectory + "\\config.ini");
            openFileDialog1.InitialDirectory = defaultPath;
        }

        public void EnabledButton()
        {
            button4.Enabled = true;
        }
        public void show()
        {
            Person p = (Person)stuList[index];
            string t_sex = p.GetSex();
            textBox1.Text = p.GetName();
            if ("M".Equals(t_sex.ToUpper())) comboBox1.SelectedIndex = 0;
            else if ("F".Equals(t_sex.ToUpper())) comboBox1.SelectedIndex = 1;
            else comboBox1.SelectedIndex = 2;
            textBox2.Text = p.GetClass();
            textBox3.Text = p.GetPersonID();
            textBox4.Text = p.GetPersonnalImagePath();
            label6.Text = "第 " + Convert.ToString(index + 1) + " 个 / 共 " + Convert.ToString(stuList.Count) + " 个";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (index <= 0)
            {
                MessageBox.Show(this, "已经是第一个！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            index -= 1;
            show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (index >= stuList.Count - 1)
            {
                MessageBox.Show(this, "已经是最后一个！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            index += 1;
            show();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnabledButton();
            Person p = (Person)stuList[index];
            if (comboBox1.SelectedIndex == 0) p.SetSex("M");
            else if (comboBox1.SelectedIndex == 1) p.SetSex("F");
            else p.SetSex("other");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            EnabledButton();
            Person p = (Person)stuList[index];
            p.SetClass(textBox2.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            EnabledButton();
            Person p = (Person)stuList[index];
            p.SetPersonID(textBox3.Text);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            EnabledButton();
            Person p = (Person)stuList[index];
            p.SetPersonnalImagePath(textBox4.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog(this);
            textBox4.Text = openFileDialog1.FileName;
            textBox4_TextChanged(sender, e);
            string t_str = openFileDialog1.FileName;
            string[] t_str_group = t_str.Split('\\');
            string t_path = "";
            for(int i = 0; i < t_str_group.Length - 1; i++)
            {
                t_path += t_str_group[i] + "\\";
            }
            defaultPath = t_path;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Person p;
            File.Delete(Environment.CurrentDirectory + "\\names.xml");
           /* for(int i = 0; i < stuList.Count; i++)
            {
                p = (Person)stuList[i];
                if (!p.WriteFile(Environment.CurrentDirectory + "\\names.txt")) 
                {
                    MessageBox.Show(this, "写入文件失败！", "错误！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }*/
            Form1.f1.students.Clear();
            for(int i = 0; i < stuList.Count; i++)
            {
                Person t_p = (Person)stuList[i];
                Person t_p1 = new Person(t_p.GetName(), t_p.GetSex(), t_p.GetPersonID(), t_p.GetClass(), t_p.GetPersonnalImagePath(), t_p.GetLocalSection());
                Form1.f1.students.Add(t_p1);
            }
            Form1.f1.WriteXmlFile(stuList);
            
            MessageBox.Show(this, "修改成功！", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            EnabledButton();
            Person p = (Person)stuList[index];
            p.SetName(textBox1.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Random rd = new Random();

            /*string t_secname = (string)sections_list[0];
            for (int i = 0; isSectionExist(sections_list, t_secname); i++) 
            {
                t_secname = string.Format("Person{0}", i);
            }*/
            
            Person p = new Person("姓名", "M", "ID", "班级", "图像路径");
            stuList.Add(p);
            //sections_list.Add(t_secname);
            index = stuList.Count - 1;
            show();
        }

        public bool isSectionExist(ArrayList allSectionsList, string compareText)
        {
            for (int i = 0; i < allSectionsList.Count; i++)
            {
                string t = (string)allSectionsList[i];
                if (t.Equals(compareText)) return true;
            }
            return false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            stuList.RemoveAt(index);
            if (index < 0) index = 0;
            else if (index >= stuList.Count - 1) index = stuList.Count - 1;
            if (stuList.Count == 0)
            {
                MessageBox.Show(this, "列表已空！", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Information);
                index = 0;
                return;
            }
            show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EditnameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IniOperate.Write("General", "default_path", defaultPath, Environment.CurrentDirectory + "\\config.ini");
        }
    }
}
