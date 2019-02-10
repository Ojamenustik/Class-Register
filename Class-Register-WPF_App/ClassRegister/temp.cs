using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegister
{
  
    public class temp
    {
       public ClassRegisterLibrary.User user { get; set; }

        public void setusr(ClassRegisterLibrary.User usr)
        {
            user = usr;
        }
        public ClassRegisterLibrary.User Getusr()
        {
            return  user;
        }
    }
}
