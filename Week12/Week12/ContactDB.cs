using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace Week12
{
    class ContactDB : DataAccessLayer, IDisposable
    {

        SQLiteConnection con = default(SQLiteConnection);
        string cs = @"URI=file:test2.db";
        public ContactDB()
        {
            con = new SQLiteConnection(cs);
            con.Open();
            PrepareDB();
        }
        public void Dispose()
        {
            con.Close();
        }
        public void ExecuteNonQuery(string commandText)
        {
            SQLiteCommand cmd = new SQLiteCommand(con);
            cmd.CommandText = commandText;
            cmd.ExecuteNonQuery();
        }
        private void PrepareDB()
        {
            //SQLiteConnection.CreateFile("test2.db");
            //ExecuteNonQuery("DROP TABLE IF EXISTS contacts");
            //ExecuteNonQuery("CREATE TABLE contacts(id TEXT PRIMARY KEY, name TEXT, phone TEXT, address TEXT)");
        }
        public string CreateContact(ContactDTO contact)
        {
            string text = string.Format("INSERT INTO contacts(id, name, phone, address) VALUES('{0}', '{1}', '{2}', '{3}')", 
                contact.Id, 
                contact.Name, 
                contact.Phone, 
                contact.Address);
            ExecuteNonQuery(text);
            return contact.Id;
        }

        public bool DeleteContactById(string id)
        {
            string text = string.Format("DELETE FROM contacts WHERE id = \"{0}\"", id);
            ExecuteNonQuery(text);
            return true;
        }

        public bool UpdateContact(ContactDTO contact)
        {
            string text = string.Format("UPDATE contacts SET id = \"{0}\", name = \"{1}\", phone = \"{2}\", address = \"{3}\"  WHERE id = \"{0}\"", 
                contact.Id,
                contact.Name,
                contact.Phone,
                contact.Address);
            MessageBox.Show(text);
            ExecuteNonQuery(text);
            return true;
        }

        public List<ContactDTO> GetAllContactsInPage(int offset, string property, string pattern, bool sort)
        {
            string sortBy = "NULL";
            if (sort) sortBy = property;
            string selectSQL = string.Format("SELECT * FROM contacts WHERE {0} LIKE '%{2}%' ORDER BY (SELECT {3}) LIMIT 10 OFFSET {1}", property, offset, pattern, sortBy);
            return GetContacts(selectSQL);
        }

        public List<ContactDTO> GetContacts(string selectSQL)
        {
            List<ContactDTO> res = new List<ContactDTO>();
            using (SQLiteCommand command = new SQLiteCommand(selectSQL, con))
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var item = new ContactDTO
                    {
                        Id = reader.GetString(0),
                        Name = reader.GetString(1),
                        Phone = reader.GetString(2),
                        Address = reader.GetString(3)
                    };
                    res.Add(item);
                }
            }
            return res;
        }
    }
}
