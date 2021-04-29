using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class Form1 : Form
    {
        Brain brain;
        public Form1()
        {
            InitializeComponent();
            DisplayMessage displayMessage = new DisplayMessage(SetDisplayMessage);
            brain = new Brain(displayMessage);
        }

        public void SetDisplayMessage(string text)
        {
            textBox1.Text = text;
        }

        public void BtnPressedEvent(object sender, EventArgs args)
        {
            Button btn = sender as Button;
            brain.ProcessSignal(btn.Text);
        }

        public void Form1_Load(object sender, EventArgs args)
        {

        }
<<<<<<< HEAD

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
=======
    }
}
>>>>>>> a5c073f9fef23fd2f9fddcc5f3b52027be21a0a1
