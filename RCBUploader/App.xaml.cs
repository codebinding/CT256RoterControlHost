using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

using System.Windows.Threading;

namespace RCBUploader {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public const string gkProgramName = "";

        private void Application_Startup( object sender, StartupEventArgs e ) {

            // Global exception handling 

            Application.Current.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler( AppDispatcherUnhandledException );

        }

        void AppDispatcherUnhandledException( object sender, DispatcherUnhandledExceptionEventArgs e ) {

            ShowUnhandeledException( e );

        }

        void ShowUnhandeledException( DispatcherUnhandledExceptionEventArgs e ) {

            string myMessage = e.Exception.Message;

            e.Handled = true;

            if( e.Exception.InnerException != null ) {

                myMessage = myMessage + " - " + e.Exception.InnerException.Message;

            }

            string myOffendingFunction = e.Exception.TargetSite.ToString();

            if( MessageBox.Show( myMessage, "Application Error", MessageBoxButton.YesNoCancel, MessageBoxImage.Error ) == MessageBoxResult.No ) {

                if( MessageBox.Show( "WARNING: The application will close. Any changes will not be saved!\nDo you really want to close it?", "Close the application!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning ) == MessageBoxResult.Yes ) {

                    Application.Current.Shutdown();

                }

            }

        }

    }
}
