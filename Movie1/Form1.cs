using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Movie1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = "영화관 관리";

            label5.Text = DataManager.Books.Count.ToString();
            label6.Text = DataManager.Users.Count.ToString();
            label7.Text = DataManager.Books.Where((x) => x.IsBorrowed).Count().ToString();
            label8.Text = DataManager.Books.Where((x) =>

            {
                return x.IsBorrowed && x.BorrowedAt.AddDays(7) < DateTime.Now;
            }).Count().ToString();

            dataGridView1.DataSource = DataManager.Books;
            dataGridView2.DataSource = DataManager.Users;
        }

        private void button1_Click(object sender, EventArgs e)//예매
        {
            if (textBox1.Text.Trim() == "")
            {

            }
            else if (textBox2.Text.Trim() == "")
            {

            }
            else
            {
                try
                {

                    Book book = DataManager.Books.Single(x => x.Isbn == textBox1.Text);
                    if (book.IsBorrowed)
                    {
                        MessageBox.Show("이미 예매 중인 영화입니다.");
                    }
                    else
                    {
                        User user = DataManager.Users.Single(x => x.Id.ToString() == textBox3.Text);
                        book.UserId = user.Id;
                        book.UserName = user.Name;
                        book.IsBorrowed = true;
                        book.BorrowedAt = DateTime.Now;

                        dataGridView1.DataSource = null;
                        dataGridView2.DataSource = DataManager.Books;
                        DataManager.Save();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("존재하지 않는 영화/사용자입니다.");
                }
            }
        }
        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                Book book = dataGridView1.CurrentRow.DataBoundItem as Book;
                textBox1.Text = book.Isbn;
                textBox2.Text = book.Name;
            } catch (Exception ex)
            {

            }
        }

        private void dataGridView2_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                User user = dataGridView2.CurrentRow.DataBoundItem as User;
                textBox3.Text = user.Id.ToString();
            } catch (Exception ex)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try {
                Book book = DataManager.Books.Single(x => x.Isbn == textBox1.Text);
                if (book.IsBorrowed)
                {
                    User user = DataManager.Users.Single(x => x.Id.ToString() == textBox3.Text);
                    book.UserId = 0;
                    book.UserName = "";
                    book.IsBorrowed = false;
                    book.BorrowedAt = new DateTime();

                    dataGridView1.DataSource = null;
                    dataGridView2.DataSource = DataManager.Books;
                    DataManager.Save();
                }
                else
                {

                }
            } catch (Exception ex)
            {

            }
    }

        private void 영화관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form2().ShowDialog();
        }

        private void 사용자관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form3().ShowDialog();
        }
    }
    }
