using Supermarker.mvp._Repositories;
using Supermarker.mvp.Models;
using Supermarker.mvp.Presenters;
using Supermarker.mvp.Properties;
using Supermarker.mvp.Views;

namespace Supermarker.mvp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            string sqlConnectionString = Settings.Default.SqlConnection;
            IMainView view = new MainView();
            new MainPresenter(view, sqlConnectionString);
            Application.Run((Form)view);

        }
    }
}