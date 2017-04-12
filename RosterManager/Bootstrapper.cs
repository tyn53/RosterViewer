using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using Gmi.RosterManager.Controllers;
using Gmi.RosterManager.DataAccess;

namespace Gmi.RosterManager
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IImageRepository, ImageRepository>();
            container.RegisterType<IStatRepository, StatRepository>();
            container.RegisterType<ITeamRepository, TeamRepository>();
            container.RegisterType<IPlayerRepository, PlayerRepository>();
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();            

            return container;
        }
    }
}