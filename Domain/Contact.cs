using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }


        private string _fullName;
        public string FullName
        {
            get
            {
                if (_fullName == null)
                {
                    var first = string.IsNullOrWhiteSpace(FirstName) ? "" : FirstName;
                    var last = string.IsNullOrWhiteSpace(LastName) ? "" : LastName;
                    return first.Trim() + " " + last.Trim();
                }
                return _fullName;

            }
            set { _fullName = value; }
        }
    }
}