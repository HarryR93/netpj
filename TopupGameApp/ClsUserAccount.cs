using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TopupGameApp
{
    public class UserAccount
    {
        public static string CurrentUsername = "";
        public static string CurrentPassword = "";
        public static void setCurrentPassword(string currentPassword)
        {
            CurrentPassword = currentPassword;
        }
        public static void setCurrentUsername(string currentUsername)
        {
            CurrentUsername = currentUsername;
        }
    }

}
