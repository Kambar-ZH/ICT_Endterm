using System;
using System.Linq;
using System.Windows.Forms;

namespace Week12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadContacts();
        }

        private BLL bll = default(BLL);
        private int currentPage = 0;
        private string searchFor = "name";
        private bool toSort = false;

        private void LoadContacts()
        {
            ContactDB contacts2 = new ContactDB();

            bll = new BLL(contacts2);
            refresh();

            bindingNavigator1.BindingSource = bindingSource1;
            dataGridView1.DataSource = bindingSource1;

        }

        private void refresh()
        {
            string text = toolStripTextBox1.Text;
            toolStripStatusLabel1.Text = bll.GetContactsInPage(currentPage, searchFor, text, toSort).Count().ToString();
            bindingSource1.DataSource = bll.GetContactsInPage(currentPage, searchFor, text, toSort);
        }

        private void addClient(object sender, EventArgs e)
        {
            CreateContactForm createContactForm = new CreateContactForm();
            if (createContactForm.ShowDialog() == DialogResult.OK)
            {
                CreateContactCommand command = new CreateContactCommand();
                command.Name = createContactForm.nameTxtBx.Text;
                command.Phone = createContactForm.phoneTxtBx.Text;
                command.Address = createContactForm.addressTxtBx.Text;
                bll.CreateContact(command);
                refresh();
            }
        }

        private void removeContact(object sender, EventArgs e)
        {
            int row = Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1;
            var text = dataGridView1.Rows[row].Cells[0].Value;
            bll.RemoveContact(text.ToString());
            bindingSource1.RemoveCurrent();
            refresh();
        }

        private void searchContacts(object sender, EventArgs e)
        {
            currentPage = 0;
            refresh();
        }

        private void updateContact(object sender, EventArgs e)
        {
            int row = Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1;
            var text = dataGridView1.Rows[row];
            ContactDTO contact = new ContactDTO();
            contact.Id = text.Cells[0].Value.ToString();
            contact.Name = text.Cells[1].Value.ToString();
            contact.Phone = text.Cells[2].Value.ToString();
            contact.Address = text.Cells[3].Value.ToString();
            bll.UpdateContact(contact);
        }

        private void previousPage(object sender, EventArgs e)
        {
            currentPage = Math.Max(0, currentPage - 10);
            refresh();
        }

        private void nextPage(object sender, EventArgs e)
        {
            string text = toolStripTextBox1.Text;
            currentPage = Math.Max(0, Math.Min(bll.GetContactsInPage(currentPage, searchFor, text, toSort).Count / 10 * 10, currentPage + 10));
            refresh();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int row = Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1;
            var text = dataGridView1.Rows[row];
            CreateContactCommand contact = new CreateContactCommand();
            contact.Name = text.Cells[1].Value.ToString();
            contact.Phone = text.Cells[2].Value.ToString();
            contact.Address = text.Cells[3].Value.ToString();
            ShowContactForm showContactForm = new ShowContactForm(contact);
            showContactForm.ShowDialog();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            searchFor = button.Text;
            refresh();
        }

        private void sort_Click(object sender, EventArgs e)
        {
            toSort = !toSort;
            label3.Text = toSort.ToString();
            refresh();
        }
    }
}
