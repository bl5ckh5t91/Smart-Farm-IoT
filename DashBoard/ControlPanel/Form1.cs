using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Drawing;
using ICOMA;



namespace ControlPanel
{

    public partial class Form1 : Form
    {
        

        String[] ports;
        public string ComPort;
       
        bool isConnected = false;
        bool isclick6 = false;
        bool isclick7 = false;
        bool isclick8 = false;
        bool isclick9 = false;
        bool isclick10 = false;
        bool TLflag1 = true;
        bool TLflag = true;
        bool ISflag = true;

        public string RR = "";
        public string BB = "";
        public string GG = "";

        string INpin2 = "0", INpin3 = "0", INpin4 = "0", INpin5="0";
        string A0value = "0";
        string A1value = "0";
        string A2value = "0";
        string A3value = "0";
        string Sonar = "0";
        string Humidity = "0";
        string Temperature = "0";

        Arduino cal = new Arduino();

        public Form1()
        {
            InitializeComponent();
        }

        

        private void button17_Click(object sender, EventArgs e)
        {
            ComPort = cal.DetectArduinoPort(); // Automatically detect Arduino Uno connected port 
            comboBox1.Text = ComPort;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ComPort = cal.DetectArduinoPort();
            comboBox1.Text = ComPort;
        }

        // To list available Comm port
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

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (isConnected)
                {         
                    cal.Disconnect();           // Disconnect from Arduino Board Comm port
                    isConnected = false;
                    button1.Text = "Connect";
                    button1.BackColor = Color.Blue;
                    TLflag1 = false;
                }
                else
                {
                   cal.Connect(comboBox1.Text); //Connect to Arduino Board Comm port
                    button1.Text = "One Moment..";
                    await Task.Delay(2000);
                    if (cal.CheckBoard())
                    {
                        textBox20.Text = "FlowLogic 6 OK";
                        isConnected = true;
                        button1.Text = "Disconnect";
                        button1.BackColor = Color.LightGreen;
                        TLflag1 = true;
                        SerialData();           // Read Arduino board Digital and Analog IO
                    }
                    else
                    {
                        textBox20.Text = "Board Not OK";
                        button1.Text = "Connect";
                        cal.Disconnect();       // Disconnect from Arduino Board Comm port
                        MessageBox.Show("Please Activate your Arduino Board with Board Activator" + "\r" + "If your Board is already activated, try connecting again", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        button1.BackColor = Color.LightGreen;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Possible Port number invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                button1.Text = "Connect";
            }
        }

        
        // Servo motor connected to Pin D8 to D13
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            textBox14.Text = hScrollBar1.Value.ToString();
            //cal.Apin6(textBox14.Text);
            cal.PwmWrite(6, textBox14.Text);
        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            textBox15.Text = hScrollBar2.Value.ToString();
            //cal.Apin9(textBox15.Text);
            cal.PwmWrite(9, textBox15.Text);
        }

        private void hScrollBar3_Scroll(object sender, ScrollEventArgs e)
        {
            textBox16.Text = hScrollBar3.Value.ToString();
           // cal.Apin10(textBox16.Text);
            cal.PwmWrite(10, textBox16.Text);
        }

        private void circularButton1_Click(object sender, EventArgs e)
        {
            string Tmelody = textBox12.Text;
            string Tduration = textBox13.Text;
            cal.Tone(Tmelody, Tduration);
        }

        private void hScrollBar4_Scroll(object sender, ScrollEventArgs e)
        {
            SSpeed.Text = hScrollBar4.Value.ToString();
        }

        private void hScrollBar8_Scroll(object sender, ScrollEventArgs e)
        {
            Servo8.Text = hScrollBar8.Value.ToString();
            //cal.Servo8(Servo8.Text, SSpeed.Text);
            cal.ServoMotor(8, Servo8.Text, SSpeed.Text);
        }

        private void hScrollBar9_Scroll(object sender, ScrollEventArgs e)
        {
            Servo9.Text = hScrollBar9.Value.ToString();
           // cal.Servo9(Servo9.Text, SSpeed.Text);
            cal.ServoMotor(9, Servo9.Text, SSpeed.Text);
        }

        private void hScrollBar10_Scroll(object sender, ScrollEventArgs e)
        {
            Servo10.Text = hScrollBar10.Value.ToString();
            //cal.Servo10(Servo10.Text, SSpeed.Text);
            cal.ServoMotor(10, Servo10.Text, SSpeed.Text);
        }

        private void hScrollBar11_Scroll(object sender, ScrollEventArgs e)
        {
            Servo11.Text = hScrollBar11.Value.ToString();
            //cal.Servo11(Servo11.Text, SSpeed.Text);
            cal.ServoMotor(11, Servo11.Text, SSpeed.Text);
        }

        private void hScrollBar12_Scroll(object sender, ScrollEventArgs e)
        {
            Servo12.Text = hScrollBar12.Value.ToString();
            //cal.Servo12(Servo12.Text, SSpeed.Text);
            cal.ServoMotor(12, Servo12.Text, SSpeed.Text);
        }

        private void hScrollBar13_Scroll(object sender, ScrollEventArgs e)
        {
            Servo13.Text = hScrollBar13.Value.ToString();
            //cal.Servo13(Servo13.Text, SSpeed.Text);
            cal.ServoMotor(13, Servo13.Text, SSpeed.Text);
        }

        //Shift registor connected to Pin D7,D8,D9
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (isConnected)
            {
                if (checkBox1.Checked)
                {
                    cal.ShiftWrite(0, "1");
                }
                else
                {
                    cal.ShiftWrite(0, "0");
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (isConnected)
            {
                if (checkBox2.Checked)
                {
                    cal.ShiftWrite(1, "1");
                }
                else
                {
                    cal.ShiftWrite(1, "0");
                }
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (isConnected)
            {
                if (checkBox3.Checked)
                {
                    cal.ShiftWrite(2, "1");
                }
                else
                {
                    cal.ShiftWrite(2, "0");
                }
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (isConnected)
            {
                if (checkBox4.Checked)
                {
                    cal.ShiftWrite(3, "1");
                }
                else
                {
                    cal.ShiftWrite(3, "0");
                }
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (isConnected)
            {
                if (checkBox5.Checked)
                {
                    cal.ShiftWrite(4, "1");
                }
                else
                {
                    cal.ShiftWrite(4, "0");
                }
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (isConnected)
            {
                if (checkBox6.Checked)
                {
                    cal.ShiftWrite(5, "1");
                }
                else
                {
                    cal.ShiftWrite(5, "0");
                }
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (isConnected)
            {
                if (checkBox7.Checked)
                {
                    cal.ShiftWrite(6, "1");
                }
                else
                {
                    cal.ShiftWrite(6, "0");
                }
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (isConnected)
            {
                if (checkBox8.Checked)
                {
                    cal.ShiftWrite(7, "1");
                }
                else
                {
                    cal.ShiftWrite(7, "0");
                }
            }
        }

        // Traffic Light Demo
        private async void button10_Click(object sender, EventArgs e)
        {
            TLflag = true;
            while (TLflag)
            {
                cal.DigitalWrite(6, "ON");    // Turn ON Pin D6
                circularButton2.BackColor = Color.Red;
                await Task.Delay(2000);
                circularButton2.BackColor = Color.LightPink;
                cal.DigitalWrite(6, "OFF");   // Turn OFF Pin D6
                await Task.Delay(10);
                circularButton3.BackColor = Color.Yellow;
                cal.DigitalWrite(7, "ON");              // Turn ON Pin D7
                for (var i = 0; i < 5; i++)
                {
                    await Task.Delay(200);
                    circularButton3.BackColor = Color.Yellow;
                    cal.DigitalWrite(7, "ON");          // Turn ON Pin D7
                    await Task.Delay(200);
                    circularButton3.BackColor = Color.LightYellow;
                    cal.DigitalWrite(7, "OFF");        // Turn OFF Pin D7
                }
                await Task.Delay(100);
                circularButton4.BackColor = Color.LawnGreen;
                cal.DigitalWrite(9, "ON");            // Turn ON Pin D9
                await Task.Delay(2000);
                circularButton4.BackColor = Color.LightGreen;
                cal.DigitalWrite(9, "OFF");           // Turn OFF Pin D9
                await Task.Delay(100);
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            TLflag = false;
        }

        //Intrusion Detection Demo
        private async void button2_Click_1(object sender, EventArgs e)
        {
            ISflag = true;
            bool p6flagON = true;
            while (ISflag)
            {               
                textBox17.Text = Sonar;
                await Task.Delay(100);

                if (Sonar != "")
                {
                    if (Convert.ToInt32(Sonar) <= 10)
                    {
                        circularButton5.BackColor = Color.Red;
                        await Task.Delay(100);
                        if (!p6flagON)
                        {
                            cal.DigitalWrite(6, "ON");
                            await Task.Delay(100);                           
                            p6flagON = true;
                        }
                    }
                    else
                    {
                        circularButton5.BackColor = Color.LightPink;
                        await Task.Delay(100);
                        if (p6flagON)
                        {
                            cal.DigitalWrite(6, "OFF");
                            await Task.Delay(100);                            
                            p6flagON = false;
                        }
                    }
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ISflag = false;
        }

        private async void button13_Click(object sender, EventArgs e)
        {
            await Task.Delay(100);
            int T1 = textBox18.Text.Length;
            cal.LCDline1(textBox18.Text.PadRight((16 + T1 - T1), ' '));
        }

        //LCD SDA/SCL or Pin A4/A5
        private async void button14_Click(object sender, EventArgs e)
        {
            await Task.Delay(100);
            int T2 = textBox19.Text.Length;
            cal.LCDline2(textBox19.Text.PadRight((16 + T2 - T2), ' '));
        }

        private void button15_Click(object sender, EventArgs e)
        {
            // Line 1
            int T1 = textBox18.Text.Length;
            cal.LCDline1(textBox18.Text.PadRight((16 + T1 - T1), ' '));
            // Line 2
            int T2 = textBox19.Text.Length;
            cal.LCDline2(textBox19.Text.PadRight((16 + T2 - T2), ' '));
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                if (!isclick6)
                {
                    button5.BackColor = Color.GreenYellow;
                    isclick6 = true;
                    cal.DigitalWrite(6, "ON");
                    await Task.Delay(10);
                }
                else
                {
                    button5.BackColor = Color.FromArgb(250, 245, 235); ;
                    isclick6 = false;
                    cal.DigitalWrite(6, "OFF");
                    await Task.Delay(10);
                }
            }
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                if (!isclick7)
                {
                    button6.BackColor = Color.GreenYellow;
                    isclick7 = true;
                    cal.DigitalWrite(7, "ON");
                    await Task.Delay(10);
                }
                else
                {
                    button6.BackColor = Color.FromArgb(250, 245, 235); ;
                    isclick7 = false;
                    cal.DigitalWrite(7, "OFF");
                    await Task.Delay(10);
                }
            }
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            if (!isclick8)
            {
                button7.BackColor = Color.GreenYellow;
                isclick8 = true;
                cal.DigitalWrite(8, "ON");
                await Task.Delay(10);
            }
            else
            {
                button7.BackColor = Color.FromArgb(250, 245, 235); ;
                isclick8 = false;
                cal.DigitalWrite(8, "OFF");
                await Task.Delay(10);
            }
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            if (!isclick9)
            {
                button8.BackColor = Color.GreenYellow;
                isclick9 = true;
                cal.DigitalWrite(9, "ON");
                await Task.Delay(10);
            }
            else
            {
                button8.BackColor = Color.FromArgb(250, 245, 235); ;
                isclick9 = false;
                cal.DigitalWrite(9, "OFF");
                await Task.Delay(10);
            }
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            if (!isclick10)
            {
                button9.BackColor = Color.GreenYellow;
                isclick10 = true;
                cal.DigitalWrite(10, "ON");
                await Task.Delay(10);
            }
            else
            {
                button9.BackColor = Color.FromArgb(250, 245, 235); ;
                isclick10 = false;
                cal.DigitalWrite(10, "OFF");
                await Task.Delay(10);
            }
        }

        private async void SerialData()
        {
            TLflag1 = true;
            while (TLflag1)
            {
                try
                {
                    await Task.Delay(10);

                    cal.ReadIOs();
                    // Analog Input Status - Pin A0 to pin A3
                    A0value = cal.AnalogRead(0);    // Pin AO
                    A1value = cal.AnalogRead(1);    // Pin A1
                    A2value = cal.AnalogRead(2);    // Pin A2
                    A3value = cal.AnalogRead(3);    // Pin A3
                    textBox1.Text = A0value;
                    textBox2.Text = A1value;
                    textBox3.Text = A2value;
                    textBox4.Text = A3value;
                    // Sensor Input Status 
                    Sonar = cal.Sonar();        //Pin 2 and 3     
                    Humidity = cal.Humidity();  //DHT1 Sensor Pin 5
                    Temperature = cal.Temperature(); //DHT1 Sensor Pin 5
                    textBox6.Text = Sonar;          
                    textBox7.Text = Humidity;       
                    textBox8.Text = Temperature;    

                    if (A3value != "")
                    {
                        if (checkBox9.Checked)
                        {

                            circularProgressBar1.Value = Convert.ToInt32(A0value);
                            circularProgressBar1.Text = A0value;
                            circularProgressBar1.Update();
                        }

                        if (checkBox10.Checked)
                        {
                            circularProgressBar2.Value = Convert.ToInt32(A1value);
                            circularProgressBar2.Text = A1value;
                            circularProgressBar2.Update();
                        }

                        if (checkBox11.Checked)
                        {
                            circularProgressBar3.Value = Convert.ToInt32(A2value);
                            circularProgressBar3.Text = A2value;
                            circularProgressBar3.Update();
                        }

                        if (checkBox12.Checked)
                        {
                            circularProgressBar4.Value = Convert.ToInt32(A3value);
                            circularProgressBar4.Text = A3value;
                            circularProgressBar4.Update();
                        }
                    }

                    // Digital Input Status - Pin 2 to pin 5 
                    if (checkBox13.Checked)
                    {
                        INpin2 = cal.DigitalRead(2); // Pin D2
                    }

                    if (checkBox14.Checked)
                    {
                        INpin3 = cal.DigitalRead(3); // Pin D3
                    }

                    if (checkBox15.Checked)
                    {
                        INpin4 = cal.DigitalRead(4); // Pin D4
                    }

                    if (checkBox16.Checked)
                    {
                       INpin5 = cal.DigitalRead(5); // Pin D5
                    }

                    if (INpin2 == "1")
                    {
                        textBox5.BackColor = Color.LightGreen;
                        textBox5.Text = INpin2;
                    }
                    else
                    {
                        textBox5.BackColor = Color.Magenta;
                        textBox5.Text = INpin2;
                    }

                    if (INpin3 == "1")
                    {
                        textBox9.BackColor = Color.LightGreen;
                        textBox9.Text = INpin3;
                    }
                    else
                    {
                        textBox9.BackColor = Color.Magenta;
                        textBox9.Text = INpin3;
                    }

                    if (INpin4 == "1")
                    {
                        textBox10.BackColor = Color.LightGreen;
                        textBox10.Text = INpin4;
                    }
                    else
                    {
                        textBox10.BackColor = Color.Magenta;
                        textBox10.Text = INpin4;
                    }

                    if (INpin5 == "1")
                    {
                        textBox11.BackColor = Color.LightGreen;
                        textBox11.Text = INpin4;
                    }
                    else
                    {
                        textBox11.BackColor = Color.Magenta;
                        textBox11.Text = INpin5;
                    }
                }
                catch { }
            }
        }

        Point lastPoint;

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                MessageBox.Show("Please Disconnect from Serial port First", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Application.Exit();
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                MessageBox.Show("Please click Disconnet to disconnect from port First before exit", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Application.Exit();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}  

   