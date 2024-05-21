using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace HealthPassport
{
    public partial class Form1 : Form
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BD2.mdb";
        private OleDbConnection myConnection;
        OleDbCommand command;

        public Form1()
        {
            InitializeComponent();
            rbn_man.Checked = true;
        }

        //
        //*** Добавить
        //
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Не все поля заполнены.");
                return;
            }

            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Необходимо добавть фото.");
                return;
            }

            string gender = "";
            if (rbn_man.Checked)        gender = "Man";
            else if (rbn_woman.Checked) gender = "Woman";


            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            command = new OleDbCommand();
            command.Connection = myConnection;
            command.CommandText = "INSERT INTO [Pass]([FIO], [Gender], [DateOfBirth], [SNILS], [Addres], [Complaints], [Image]) VALUES (\"" + textBox1.Text + "\", \"" + gender + "\", \"" + textBox2.Text + "\", \"" + textBox3.Text + "\", \"" + textBox4.Text + "\", \"" + textBox5.Text + "\", \"" + Image.FromFile(openFileDialog1.FileName) + "\")";
            command.ExecuteNonQuery();
            myConnection.Close();
        }

        //
        //*** Сброс
        //
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            rbn_man.Checked = true;
            pictureBox1.Image = null;
        }

        //
        //*** Добавить фото
        //
        private void btn_open_image_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image files|*.bmp; *.jpg";
            openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Bitmap bm = new Bitmap(openFileDialog1.FileName);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image = (Image)bm;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось открыть файл");
                }
            }
        }
    }
}
