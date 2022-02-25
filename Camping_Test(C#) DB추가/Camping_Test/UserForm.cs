using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Camping_Test
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
            Text = "회원 관리";

            if(DataManager.users.Count > 0)
                dataGridView1.DataSource = DataManager.users;

            button_Add.Click += (sender, e) =>
            {
                if (DataManager.users.Exists((x) => x.Id == textBox_ID.Text))
                    MessageBox.Show("존재하는 ID");
                else // 새로운 아이디 등록
                {
                    User user = new User() { Id = textBox_ID.Text, Name = textBox_Name.Text };
                    DataManager.users.Add(user);
                   // DBHelper.InsertUser(user);

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.users;
                    DataManager.Save();
                }
            };

            button_Modify.Click += delegate (object sender, EventArgs e)
            {
                try
                {
                    User user = DataManager.users.Single((x) => x.Id == textBox_ID.Text);
                    user.Name = textBox_Name.Text;
                    try
                    {
                        Booking booking = DataManager.bookings.Single((x) => x.UserId == textBox_ID.Text); 
                        booking.UserName = textBox_Name.Text;
                    }
                    catch (Exception)
                    {
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(textBox_ID.Text+"는 없는 아이디 입니다.");
                }

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataManager.users;
                DataManager.Save();
            };

            button_Delete.Click += Btn_Delete;
 
        }
        private void Btn_Delete(object sender, EventArgs e)
        {
            try
            {
                User user = DataManager.users.Single((x) => x.Id == textBox_ID.Text);
                DataManager.users.Remove(user);

                dataGridView1.DataSource=null;
                if (DataManager.users.Count > 0)
                    dataGridView1.DataSource = DataManager.users;

                DataManager.Save();
            }
            catch (Exception)
            {
                MessageBox.Show("해당 아이디의 사용자는 없습니다.");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(DataManager.users.Count > 0)
            {
            User user = dataGridView1.CurrentRow.DataBoundItem as User;
            textBox_ID.Text = user.Id.ToString();
            textBox_Name.Text = user.Name.ToString();
            }
        }
    }
}
