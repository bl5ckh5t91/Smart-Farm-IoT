using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Drawing;
using ICOMA;
using System.Threading;
using System.Data.SqlClient;
namespace ControlPanel
{
    public partial class MainForm : Form
    {

        String[] ports;
        static SerialPort _serialPort;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            _serialPort = new SerialPort();
            _serialPort.PortName = comboBox1.Text;//Set your board COM
            _serialPort.BaudRate = 9600;
            _serialPort.Open();
            timer1.Enabled = true;
            if (true)
                MessageBox.Show("تم الاتصال بنجاح", "معلومة", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);



        }

        private void button17_Click(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {
            ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);

                if (ports[0] != null)
                {
                    comboBox1.SelectedItem = ports[0];
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            _serialPort.Close();
            MessageBox.Show("تم قطع الاتصال", "معلومة", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string aW = "NaN", aT = "NaN", aSH = "NaN", aH = "NaN";
            string a = _serialPort.ReadExisting();
            //textBox1.Text = a;
            int numericValue;
             bool isNumber = int.TryParse(a, out numericValue);
             if (isNumber==true)
            {
                if ((Convert.ToInt32(a) < 100) & (Convert.ToInt32(a) >= 0))
                {
                    circularProgressBar4.Value = Convert.ToInt32(a);
                     aW = circularProgressBar4.Value.ToString();
                    circularProgressBar4.Text = aW;
                    circularProgressBar4.Update();
                }
                if ((Convert.ToInt32(a) < 10000) & (Convert.ToInt32(a) > 100))
                {
                    circularProgressBar3.Value = Convert.ToInt32(a) / 100;
                     aSH = circularProgressBar3.Value.ToString();
                    circularProgressBar3.Text = aSH;
                    circularProgressBar3.Update();
                }
                if ((Convert.ToInt32(a) < 1000000) & (Convert.ToInt32(a) > 10000))
                {
                    circularProgressBar2.Value = Convert.ToInt32(a) / 10000;
                     aH = circularProgressBar2.Value.ToString();
                    circularProgressBar2.Text = aH;
                    circularProgressBar2.Update();
                }
                if ((Convert.ToInt32(a) < 100000000) & (Convert.ToInt32(a) > 1000000))
                {
                    circularProgressBar1.Value = (Convert.ToInt32(a)) / 1000000;
                     aT = circularProgressBar1.Value.ToString();
                    circularProgressBar1.Text = aT;
                    circularProgressBar1.Update();
                }
                string P="NaN";
                string F="NaN";
                if ((circularProgressBar1.Value / 1000000 > 35) || (circularProgressBar2.Value / 10000 > 50))
                {
                     F = "ON";
                    panel4.BackColor = Color.Green;
                }
                else
                {
                     F = "OFF";
                    panel4.BackColor = Color.Red;
                }

                if (circularProgressBar4.Value >= 20)
                {
                     P = "ON";
                    panel2.BackColor = Color.Green;
                }
                else
                {
                     P = "OFF";
                    panel2.BackColor = Color.Red;
                }

                if (circularProgressBar4.Value < 20)
                    panel3.BackColor = Color.Green;
                else
                    panel3.BackColor = Color.Red;
                string connetionString = null;
                SqlConnection cnn;
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = null;
                connetionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Frame\frame.mdf;Integrated Security=True";
                cnn = new SqlConnection(connetionString);
                string dd = DateTime.Today.ToShortDateString() + DateTime.Today.ToShortTimeString();
                sql = "insert into info(H,T,W,S_H,F,P,DateR) Values('" + aH + "','" + aT + "','" + aW + "','" + aSH + "','" + F + "','" + P + "','" + dd + "')";
                cnn.Open();
                adapter.InsertCommand = new SqlCommand(sql, cnn);
                adapter.InsertCommand.ExecuteNonQuery();
                cnn.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form frm = new info();
            frm.Show();
        }
    }
}
