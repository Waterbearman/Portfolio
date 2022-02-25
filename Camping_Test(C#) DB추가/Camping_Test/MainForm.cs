using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Camping_Test
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            label_allBookingCount.Text = "";
            label_allUserCount.Text = "";
            label_allSeatCount.Text = "";

            label_allBookingCount.Text = DataManager.bookings.Count.ToString();
            label_allUserCount.Text = DataManager.users.Count.ToString();
            label_allSeatCount.Text = DataManager.bookings.Where( x=> x.Checking).Count().ToString();
            

            if(DataManager.bookings.Count > 0)
                dataGridView1.DataSource = DataManager.bookings;

            if(DataManager.users.Count > 0)
                dataGridView2.DataSource = DataManager.users;

            dataGridView1.CellClick += Book_GridView_CellClick;
        
        }

        private void Book_GridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(DataManager.bookings.Count > 0)
            {
                Booking booking = dataGridView1.CurrentRow.DataBoundItem as Booking;
                textBox_AreaNum.Text = booking.Area;
                textBox_Booking.Text = booking.BookingNum;
            }
            
        }

        private void 예약현황ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new BookingForm().ShowDialog(); // 모달
            DataManager.Load();
            dataGridView1.DataSource = null;
            if (DataManager.bookings.Count > 0)
                dataGridView1.DataSource = DataManager.bookings;
        }

        private void 회원관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new UserForm().ShowDialog();
            DataManager.Load();
            dataGridView2.DataSource = null;
            if (DataManager.users.Count > 0)
                dataGridView2.DataSource = DataManager.users;
        }

        private void timer_Now_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel_Now.Text = DateTime.Now.ToString("yyyy년 MM월 dd일 HH:mm:ss");
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            User user = dataGridView2.CurrentRow.DataBoundItem as User;
            textBox_Id.Text = user.Id.ToString();
        }

        private void button_Booking_Click(object sender, EventArgs e)
        {
            if(textBox_Booking.Text.Trim() == "")
                MessageBox.Show("예약번호를 입력하세요");
            else if(textBox_Id.Text.Trim() == "")
                MessageBox.Show("사용자 id를 입력하세요.");
            else
            {
                try
                {
                    Booking booking = DataManager.bookings.Single((x) => x.BookingNum == textBox_Booking.Text);
                    if(booking.Checking && booking.Area == textBox_AreaNum.Text)
                        MessageBox.Show("이미 예약 했습니다.");
                    else
                    {
                        User user = DataManager.users.Single((x) => x.Id.ToString() == textBox_Id.Text);
                        booking.BookingNum = textBox_Booking.Text;
                        booking.UserId = user.Id;
                        booking.UserName = user.Name;
                        booking.Checking = true;
                        booking.BookingDate = DateTime.Now;

                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = DataManager.bookings;
                        DataManager.Save();
                        MessageBox.Show($"{booking.Area}구역이 {user.Name}님께 예약 되었습니다.");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("존재하지 않는 예약번호 또는 사용자 입니다.");
                }
            }
        }

        private void button_Return_Click(object sender, EventArgs e)
        {
            if(textBox_Booking.Text.Trim() == "")
                MessageBox.Show("예약 번호를 입력하세요");
            else
            {
                try
                {
                    Booking booking = DataManager.bookings.Single((x) => x.Area == textBox_AreaNum.Text);
                    if (booking.Checking)
                    {
                        DateTime oldDay = booking.BookingDate;
                        booking.UserId = "";
                        booking.UserName = "";
                        booking.Checking = false;
                        booking.BookingDate = new DateTime();

                        dataGridView1.DataSource=null;
                        dataGridView1.DataSource=DataManager.bookings;
                        DataManager.Save();

                        TimeSpan timeDiff = DateTime.Now - oldDay; // 예약일 예약한날 빼기
                        if(timeDiff.Days > 7)
                            MessageBox.Show(booking.Area + "은 시간 지나서 체크아웃");
                        else
                            MessageBox.Show(booking.Area + "구역 정상 체크아웃");
                    }
                    else
                        MessageBox.Show("예약 상태가 아닙니다.");
                }
                catch (Exception)
                {
                    MessageBox.Show("존재 하지 않는 구역 입니다.");
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private Point mousePoint;
        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = new Point(e.X, e.Y);
        }

        private void menuStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Location = new Point(this.Left - (mousePoint.X - e.X),
                    this.Top - (mousePoint.Y - e.Y));
            }
        }
    }
}
