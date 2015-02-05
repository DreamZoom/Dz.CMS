using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dz.CMS.Model.Models
{
    public class User : ModelBase
    {
        [DisplayName("编号")]
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [StringLength(11)]
        [DisplayName("用户名")]
        public string UserName { get; set; }


        [StringLength(11)]
        [DisplayName("电话")]
        public string Phone { get; set; }

        [DisplayName("地址")]
        public string Address { get; set; }

        [DisplayName("年龄")]
        public int Age { get; set; }

        [DisplayName("职业")]
        public string Job { get; set; }
    }
}
