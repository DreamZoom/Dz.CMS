using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz.CMS.Services
{
    public class ServiceBase : IService
    {

        protected DAO.DAOBase DAO { get; set; }

        public ServiceBase()
        {
            DAO = new DAO.DAOBase();
        }

        public string GetModelTypeName()
        {
            return this.GetType().Name.Replace("Service", "").Trim();
        }

        public virtual string GetModelAssemblyPath()
        {
            return "Dz.CMS.Model.Models";
        }

        public virtual string GetModelDllName()
        {
            return "Dz.CMS.Model";
        }

        public virtual Model.ModelBase CreateTemplate()
        {

            var model = System.Reflection.Assembly.Load(GetModelDllName()).CreateInstance(GetModelAssemblyPath() + "." + GetModelTypeName(), false) as Model.ModelBase;
            return model;
        }

        public virtual bool AddModel(Model.ModelBase model)
        {
           return DAO.AddModel(model);
        }

        public virtual bool UpdateModel(Model.ModelBase model)
        {
            return DAO.UpdateModel(model);
        }

        public virtual bool DeleteModel(Model.ModelBase model)
        {
            return DAO.DeleteModel(model);
        }

        public virtual Model.ModelBase GetModel(int Id)
        {
            return DAO.GetSingle(this.CreateTemplate(),"ID="+Id);
        }

        public virtual IEnumerable<Model.ModelBase> GetModelList(string where)
        {
            return DAO.GetList(this.CreateTemplate(), where);
        }

        public virtual IEnumerable<Model.ModelBase> List(string where)
        {
            return DAO.GetList(this.CreateTemplate(), where);
        }
    }
}
