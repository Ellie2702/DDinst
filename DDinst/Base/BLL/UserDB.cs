using AutoMapper;
using DDinst.Base.DAL.Entity;
using DDinst.Base.DTO;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;


namespace DDinst.Base.BLL
{
    public class UserDB
    {
        public static UserDTO GetUser(string Email, int? id = null)
        {
            if (!id.HasValue && string.IsNullOrEmpty(Email))
                return null;
            try
            {
                using (DataContextcs db = new DataContextcs())
                {
                    var dbUser = db.Users.Where(x => x.Email == Email || (id.HasValue && x.User_Id == id)).FirstOrDefault();
                    var user = Mapper.Map<UserDTO>(dbUser);
              
                    if (user != null)
                     return user;

                    throw new Exception($"Not found User with ID:{id}");
                }
            }
            catch (Exception ex)
            {
                //throw;
                return null;
            }
        }
    }
}