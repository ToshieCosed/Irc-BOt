using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConsoleApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void send_beeps(string beeps_string)
        {

            if (!COM17.IsOpen)
            {



                try
                {
                    COM17.Open();
                    COM17.WriteLine(beeps_string);
                        COM17.Close();
                }
                
                catch
                {
                    MessageBox.Show("There was an error. Please make sure that the correct port was selected, and the device, plugged in.");
                }
            }
            

        }
    }
}

