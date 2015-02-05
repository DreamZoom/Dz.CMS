using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;

namespace Dz.CMS.Web.Areas.Admin.Controllers
{
    public class ModuleController : Controller
    {
        //
        // GET: /Admin/Module/

        public ActionResult Index(string serviceName, string cmd)
        {

            //获取服务对象
            Dz.CMS.Services.ServiceBase Service = Services.ServiceFactory.Create(serviceName) as Dz.CMS.Services.ServiceBase;

            //获取方法
            MethodInfo method = Service.GetType().GetMethod(cmd);

            //绑定参数
            ParameterInfo[] parms = method.GetParameters();

            List<object> parameters = new List<object>();
            foreach (var p in parms)
            {
                if (Request.Params[p.Name] != null)
                {
                    try
                    {
                        var o = Convert.ChangeType(Request.Params[p.Name], p.ParameterType);
                        parameters.Add(o);
                    }
                    catch
                    {
                        parameters.Add(null);
                    }
                }
                else
                {
                    parameters.Add(null);
                }
            }
            //执行方法

            var list = method.Invoke(Service, parameters.ToArray());

            return View(cmd, list);
        }


        public ActionResult List(string serviceName)
        {
            //获取服务对象
            Dz.CMS.Services.ServiceBase Service = Services.ServiceFactory.Create(serviceName) as Dz.CMS.Services.ServiceBase;
            //获取方法
            MethodInfo method = Service.GetType().GetMethod("List");

            //绑定参数
            ParameterInfo[] parms = method.GetParameters();
            List<object> parameters = new List<object>();
            foreach (var p in parms)
            {
                if (Request.Params[p.Name] != null)
                {
                    try
                    {
                        var o = Convert.ChangeType(Request.Params[p.Name], p.ParameterType);
                        parameters.Add(o);
                    }
                    catch
                    {
                        parameters.Add(null);
                    }
                }
                else
                {
                    parameters.Add(null);
                }
            }
            //执行方法
            var list = method.Invoke(Service, parameters.ToArray());
            return View(list);
        }

        #region 添加数据
        public ActionResult Add(string serviceName)
        {
            Model.ModelBase model = Dz.CMS.Model.ModelFactory.Create(serviceName);



            return View(model);
        }

        [HttpPost]
        public ActionResult Add(string serviceName,bool b=false)
        {
            Model.ModelBase model = Dz.CMS.Model.ModelFactory.Create(serviceName);

            if (!TryModel(model)) return null;

            //获取服务对象
            Dz.CMS.Services.ServiceBase Service = Services.ServiceFactory.Create(serviceName) as Dz.CMS.Services.ServiceBase;
            //获取方法
            MethodInfo method = Service.GetType().GetMethod("AddModel");

            try
            {
                method.Invoke(Service, new object[] { model });//执行添加操作
            }
            catch
            {
            }

            return View(model);
        }
        #endregion


        #region 修改数据
        public ActionResult Edit(string serviceName,int Id)
        {
            //获取服务对象
            Dz.CMS.Services.ServiceBase Service = Services.ServiceFactory.Create(serviceName) as Dz.CMS.Services.ServiceBase;
            //获取方法
            MethodInfo method = Service.GetType().GetMethod("GetModel");

            var model = method.Invoke(Service, new object[] { Id });//执行添加操作

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(string serviceName, int Id, bool isPost = false)
        {

            Model.ModelBase model = Dz.CMS.Model.ModelFactory.Create(serviceName);
       
            if (!TryModel(model)) return null;

            //获取服务对象
            Dz.CMS.Services.ServiceBase Service = Services.ServiceFactory.Create(serviceName) as Dz.CMS.Services.ServiceBase;
            //获取方法
            MethodInfo method = Service.GetType().GetMethod("UpdateModel");

            try
            {
                method.Invoke(Service, new object[] { model });//执行添加操作
            }
            catch
            {
            }

            return View(model);
        }
        #endregion


        #region 删除数据
        public ActionResult Delete(string serviceName, int Id)
        {
            //获取服务对象
            Dz.CMS.Services.ServiceBase Service = Services.ServiceFactory.Create(serviceName) as Dz.CMS.Services.ServiceBase;
            //获取方法
            MethodInfo method = Service.GetType().GetMethod("GetModel");

            var model = method.Invoke(Service, new object[] { Id });//执行添加操作

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(string serviceName, int Id, bool isPost = false)
        {
            Model.ModelBase model = Dz.CMS.Model.ModelFactory.Create(serviceName);

            if (!TryModel(model)) return null;

            //获取服务对象
            Dz.CMS.Services.ServiceBase Service = Services.ServiceFactory.Create(serviceName) as Dz.CMS.Services.ServiceBase;
            //获取方法
            MethodInfo method = Service.GetType().GetMethod("DeleteModel");

            try
            {
                method.Invoke(Service, new object[] { model });//执行添加操作
            }
            catch
            {
            }

            return View(model);
        }
        #endregion


        public bool TryModel(object model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            var propertys = model.GetType().GetProperties();
            foreach (var p in propertys)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.Request.Params[p.Name]))
                {
                    try
                    {
                        p.SetValue(model, Convert.ChangeType(HttpContext.Request.Params[p.Name], p.PropertyType));
                    }
                    catch
                    {
                    }
                }
            }
            
            return TryValidateModel(model);
        }
    }
}
