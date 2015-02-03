using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.CMS.Services.Services
{
    public class UserService : ServiceBase
    {
        public void AddUser(Model.Models.User user)
        {
            DAO.AddModel(user);
        }

        
    }
}
