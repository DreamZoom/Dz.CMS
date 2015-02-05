using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dz.CMS.Model;
namespace Dz.Web.Module
{
    public interface IAdmin
    {
        IEnumerable<ModelBase> List(string where ,string order , int page,int pagesize);

        void AddModel()
        
    }
}
