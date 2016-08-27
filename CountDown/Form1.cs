using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CountDown
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int hour, minute, second;
        private void btnStart_Click(object sender, EventArgs e)
        {
            int.TryParse(txtHour.Text, out hour);
            int.TryParse(txtMin.Text, out minute);
            int.TryParse(txtSec.Text, out second);

            //değerlerden negatif olan varsa pozitife çevir
            if (second < 0 || minute<0 || hour<0)
            {
                second = (int)Math.Abs(second);
                minute = (int)Math.Abs(minute);
                hour = (int)Math.Abs(hour);
            }

            //dakika ve saniye eğer 60dan fazla ise dakika veya saat arttırılsın
            if (second > 60)
            {
                int plus = second / 60;
                minute += plus;
                second %= 60;
            }
            if (minute > 60)
            {
                int plus = minute / 60;
                hour += plus;
                minute %= 60;
            }
            //değerler asıl durumlarına dönsün
            txtHour.Text = hour.ToString();
            txtMin.Text = minute.ToString();
            txtSec.Text = second.ToString();
            //txt leri kullanıma kapatalım
            txtHour.Enabled=false;
            txtMin.Enabled=false;
            txtSec.Enabled = false;

            //gecikme olmaması için değerler hemen gösterilsin
            lblHour.Text = hour.ToString();
            lblMin.Text = minute.ToString();
            lblSec.Text = second.ToString();

            timer1.Start();
            btnStart.Enabled = false;//eski değerlerle yeniden geri sayımı başlatmasın

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
          
            if (second==0)
            {
                if (minute > 0)
                {
                    minute--;
                    second = 59;
                }
                else
                {
                    if (hour > 0)
                    {
                        hour--;
                        minute = 59;
                        second = 59;
                    }
                }
            }
            second--;


            lblHour.Text = hour.ToString();
            lblMin.Text = minute.ToString();
            lblSec.Text = second.ToString();

            if (hour == 0 && minute == 0 && second == 0)
            {
                timer1.Stop();
                //result kullanıcının yes veya no butonunun değerini alır
                DialogResult result= MessageBox.Show("Do you want to start a new countdown?", "Time Is Out!..",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if (result==DialogResult.Yes)
                {//kullanıcı yeniden geri sayım yapılmasını isterse
                    btnStart.Enabled = true;
                    txtHour.Enabled = true;
                    txtMin.Enabled = true;
                    txtSec.Enabled = true;
                    txtHour.Clear();
                    txtMin.Clear();
                    txtSec.Clear();
                }
                else
                {//hayır derse uygulamayı sonlandır
                    Thread.Sleep(3000);
                    Application.Exit();
                }
            }
           

        }
    }
}
