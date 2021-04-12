using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 点名器
{
    public partial class Setting_Random : Form
    {
        public Setting_Random()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) textBox2.Enabled = false;
            else if (checkBox1.Checked == false) textBox2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Setting_Random_Load(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(Form1.f1.Interval);
            if (Form1.f1.t_seed == "default")
            {
                checkBox1.Checked = true;
                checkBox1_CheckedChanged(sender, e);
            }
            else
            {
                textBox2.Text = Convert.ToString(Form1.f1.seed);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Form1.f1.Interval = Convert.ToInt32(textBox1.Text);
            }
            catch (FormatException e1)
            {
                MessageBox.Show(this, textBox1.ToString() + "中数据不为整数！\r\n" + e1.ToString(), "错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                return;
            }
            
            if (textBox2.Enabled == false)
            {
                Form1.f1.t_seed = "default";
            }
            else if (textBox2.Enabled == true)
            {
                try
                {
                    Form1.f1.seed = Convert.ToInt32(textBox2.Text);
                }
                catch (FormatException e1)
                {
                    MessageBox.Show(this, textBox2.ToString() + "中数据不为整数！\r\n" + e1.ToString(), "错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Form1.f1.t_seed = textBox2.Text;
            }
            MessageBox.Show(this, "设置成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}
