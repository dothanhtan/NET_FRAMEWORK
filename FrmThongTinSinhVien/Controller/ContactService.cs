using FrmThongTinSinhVien.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrmThongTinSinhVien.Controller
{
    public class ContactService
    {

        public static List<Contact> GetContact(string pathConTact, string IDContact)
        {
            if (File.Exists(pathConTact))
            {
                var listLines = File.ReadAllLines(pathConTact);
                List<Contact> listContact = new List<Contact>();
                foreach (var line in listLines)
                {
                    var rs = line.Split(new char[] { '#' });
                    Contact contact = new Contact
                    {
                        IDContact = rs[0],
                        Name = rs[1],
                        Phone = rs[2],
                        Email = rs[3] 
                    };
                    if (contact.IDContact == IDContact)
                        listContact.Add(contact);
                }
                return listContact;
            }
            else
                return null;
        }

    }
}
