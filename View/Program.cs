using DAL.Interfaces;
using ImplementDataBase;
using ImplementDataBase.Implementations;
using ImplementsServiceList.Implementations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace View
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormArticles>());
        }
        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, AbstractDbContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IArticleService, ArticleServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IAuthorService, AuthorServiceDB>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
