using Autofac;
using LocalCrm.DataAccess;
using LocalCrm.Model;
using LocalCrm.Startup;
using LocalCrm.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LocalCrm
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainViewModel _mainViewModel;
        private ReferencesViewModel _referencesViewModel;

        protected override void OnStartup(StartupEventArgs e)
        {

            var pathToMdfFileDirectory = Directory.GetCurrentDirectory();// @"d:\temp\";
AppDomain.CurrentDomain.SetData("DataDirectory", pathToMdfFileDirectory);

            try
            {
base.OnStartup(e);
            var bootstrapper = new Bootstrapper();
            IContainer container = bootstrapper.Bootstrap();

            _mainViewModel = container.Resolve<MainViewModel>();
            _referencesViewModel = container.Resolve<ReferencesViewModel>();
            
            MainWindow = new MainWindow(_mainViewModel, _referencesViewModel);
            //MainWindow = new MainWindow();
            MainWindow.Show();
            _mainViewModel.Load();
            _referencesViewModel.Load();
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.StackTrace);
               
            }
            
        }
    }
}
