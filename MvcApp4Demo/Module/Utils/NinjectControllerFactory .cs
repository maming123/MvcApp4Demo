using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcApp4Demo.Module.Email;
using Ninject;
namespace Module.Utils
{
    public class NinjectControllerFactory:DefaultControllerFactory
    {
        //  Ninject中用来管理接口和实例的核心Ninject接口
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            // 通过new StandardKernel得到
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        //  通过重写此方法实现用Ninject提供接口的实例
        protected override IController GetControllerInstance(RequestContext requestContext,
        Type controllerType)
        {
            return controllerType == null
            ? null
            : (IController)ninjectKernel.Get(controllerType);
        }

        //  实现接口和实例类绑定的代码均在此方法中
        private void AddBindings()
        {
            //因为ninject不支持从配置文件配置 也可以通过ninject扩展xml从文件读取配置

            //可以通过此处绑定，更改接口不同的实现方法，用来完成后台不同处理的实现
             ninjectKernel.Bind<IEmailHelper>().To<EmailHelper>();

            //实现当MvcApp4Demo.Controllers.HomeController类中需要该接口的实例时，会把EmailHelper163绑定到IEmailHelper接口，解决统一接口的不同类实现到不同的接口的问题
            //ninjectKernel.Bind<IEmailHelper>().To<EmailHelper163>().WhenInjectedInto< MvcApp4Demo.Controllers.HomeController>();
        }
    }
}