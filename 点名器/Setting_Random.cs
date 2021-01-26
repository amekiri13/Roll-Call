using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
                textBox2.Text = Form1.f1.t_seed;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.f1.Interval = Convert.ToInt32(textBox1.Text);
            if (textBox2.Enabled == false)
            {
                Form1.f1.t_seed = "default";
            }
            else if (textBox2.Enabled == true)
            {
                Form1.f1.t_seed = textBox2.Text;
            }
            MessageBox.Show(this, "设置成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}
