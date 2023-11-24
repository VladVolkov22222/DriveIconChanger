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
using Microsoft.Win32;

namespace DriveIcons
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string selectedDiscLetter;

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (DriveInfo info in DriveInfo.GetDrives())
            {
                comboBox1.Items.Add(info.Name);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedDiscLetter = comboBox1.SelectedItem.ToString().Substring(0, 1);
            if (pictureBox1.Image != DriveIcons.Properties.Resources.no)
            {
                button2.Enabled = true;
                button3.Enabled = true;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox1.Image = Icon.ExtractAssociatedIcon(openFileDialog1.FileName).ToBitmap();
            if (comboBox1.SelectedItem != null)
            {
                button2.Enabled = true;
                button3.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\DriveIcons
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\DriveIcons\" + selectedDiscLetter + @"\DefaultIcon", "", openFileDialog1.FileName);
            MessageBox.Show("Иконка была установлена.\nЕсли она не установилась, попробуйте обновить Проводник или перезапустите компьютер.", "Готово!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\DriveIcons\" + selectedDiscLetter + @"\DefaultIcon", "", "");
            MessageBox.Show("Иконка была изменена на обычную.\nЕсли она не изменилась, попробуйте обновить Проводник или перезапустите компьютер.", "Готово!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
