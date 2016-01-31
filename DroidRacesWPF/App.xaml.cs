using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace DroidRacesWPF
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			this.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
		}

		void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			// Process unhandled exception
			MessageBox.Show(e.Exception.InnerException.Message, "Unexpected Error", MessageBoxButton.OK, MessageBoxImage.Error);
			e.Handled = true;
			Shutdown();
			//Process.GetCurrentProcess().Close();
		}
	}
}
