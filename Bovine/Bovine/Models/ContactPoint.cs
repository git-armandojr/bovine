using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Models
{
    public class ContactPoint
    {
        public string email { get; set; }
        public string telephone { get; set; }

        public ContactPoint(string email, string telephone)
        {
            this.email = email;
            this.telephone = telephone;
        }
    }
}
