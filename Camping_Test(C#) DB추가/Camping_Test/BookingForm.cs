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
    public partial class BookingForm : Form
    {
        public BookingForm()
        {
            InitializeComponent();
            Text = "예약 현황";
            if(DataManager.bookings.Count > 0)
                dataGridView1.DataSource = DataManager.bookings;
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            bool existBooking = false;

            foreach(var item in DataManager.bookings)
            {
                if(item.Area == textBox_Area.Text && item.BookingNum == textBox_Booking.Text)
                {
                    existBooking = true;
                    break;
                }
            }
            if(existBooking)
                MessageBox.Show("이미 존재하는 예약 및 자리 입니다.");
            else
            {
                Booking booking = new Booking();
                booking.BookingNum = textBox_Booking.Text;
                booking.Area = textBox_Area.Text;
                DataManager.bookings.Add(booking);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DataManager.bookings;
                DataManager.Save();
            }
        }

        private void button_Modify_Click(object sender, EventArgs e)
        {
            Booking booking = null;
            for(int i = 0; i<DataManager.bookings.Count; i++)
            {
                if (DataManager.bookings[i].BookingNum == textBox_Booking.Text)
                {
                    booking = DataManager.bookings[i];
                    booking.BookingNum = textBox_Booking.Text;
                    booking.Area = textBox_Area.Text;

                    dataGridView1.DataSource= null;
                    dataGridView1.DataSource= DataManager.bookings;
                    DataManager.Save();
                }
            }
            if(booking ==null)
                MessageBox.Show("존재 하지 않는 구역입니다.");
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            bool existBooking = false;
            for(int i = 0; i<DataManager.bookings.Count; i++)
            {
                if(DataManager.bookings[i].BookingNum == textBox_Booking.Text)
                {
                    DataManager.bookings.RemoveAt(i);
                    existBooking = true;
                }
            }
            if(existBooking == false)
                MessageBox.Show("없는 예약 입니다.");
            else
            {
                dataGridView1.DataSource = null;
                if(DataManager.bookings.Count > 0)
                {
                    dataGridView1.DataSource = DataManager.bookings;
                }
                DataManager.Save();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(DataManager.bookings.Count > 0)
            {
            Booking booking = dataGridView1.CurrentRow.DataBoundItem as Booking;
            textBox_Booking.Text = booking.BookingNum;
            textBox_Area.Text = booking.Area;
            }
        }
    }
}
