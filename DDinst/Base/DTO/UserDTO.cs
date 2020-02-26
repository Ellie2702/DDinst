using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDinst.Base.DTO
{
    public class UserDTO
    {
      
        public int User_Id { get; set; }
  
        public string Nickname { get; set; }
   
        public string Email { get; set; }
        public DateTime Birth { get; set; }
        public DateTime RegDate { get; set; }
        public bool IsDeleted { get; set; }

        public string PassHash { get; set; }
    
        public string Salt { get; set; }

        
    }
}