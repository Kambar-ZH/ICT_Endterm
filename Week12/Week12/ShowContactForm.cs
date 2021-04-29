using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week12
{
    public partial class ShowContactForm : Form
    {
        public ShowContactForm()
        {
            InitializeComponent();
        }
        public ShowContactForm(CreateContactCommand createContactCommand)
        {
            InitializeComponent();
            this.nameLabel.Text = createContactCommand.Name;
            this.phoneLabel.Text = createContactCommand.Phone;
            this.addressLabel.Text = createContactCommand.Address;
        }
    }
}
