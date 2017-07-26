using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using nexus.core.logging;
using Xamarin.Forms;
using Application = Windows.UI.Xaml.Application;
using Frame = Windows.UI.Xaml.Controls.Frame;

namespace ble.net.sampleapp.uwp
{
   /// <summary>
   /// Provides application-specific behavior to supplement the default Application class.
   /// </summary>
   sealed partial class App : Application
   {
      public const Boolean IS_DEBUG =
#if DEBUG
         true;
#else
         false;
#endif

      /// <summary>
      /// Initializes the singleton application object.  This is the first line of authored code
      /// executed, and as such is the logical equivalent of main() or WinMain().
      /// </summary>
      public App()
      {
         UnhandledException += ( sender, e ) =>
         {
            SystemLog.Instance.Error(
               "UNHANDLED EXCEPTION: from={0} message={1}\n{2}",
               sender,
               e.Message,
               e.Exception?.ToString() );
         };
         TaskScheduler.UnobservedTaskException += ( sender, e ) =>
         {
            SystemLog.Instance.Error( "UNOBSERVED TASK EXCEPTION: from={0} {1}", sender, e.Exception?.ToString() );
         };

#pragma warning disable 162
         // ReSharper disable once ConditionIsAlwaysTrueOrFalse
         if(IS_DEBUG)
         {
            SystemLog.Instance.AddSink( entry => { Debug.WriteLine( entry.FormatAsString() ); } );
         }
#pragma warning restore 162

         InitializeComponent();
      }

      /// <summary>
      /// Invoked when the application is launched normally by the end user.  Other entry points
      /// will be used such as when the application is launched to open a specific file.
      /// </summary>
      /// <param name="e">Details about the launch request and process.</param>
      protected override void OnLaunched( LaunchActivatedEventArgs e )
      {
#if DEBUG
         if(Debugger.IsAttached)
         {
            DebugSettings.EnableFrameRateCounter = true;
         }
#endif

         var rootFrame = Window.Current.Content as Frame;

         // Do not repeat app initialization when the Window already has content,
         // just ensure that the window is active
         if(rootFrame == null)
         {
            // Create a Frame to act as the navigation context and navigate to the first page
            rootFrame = new Frame();

            rootFrame.NavigationFailed += OnNavigationFailed;

            Forms.Init( e );

            if(e.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
               //TODO: Load state from previously suspended application
            }

            // Place the frame in the current Window
            Window.Current.Content = rootFrame;
         }

         if(rootFrame.Content == null)
         {
            // When the navigation stack isn't restored navigate to the first page,
            // configuring the new page by passing required information as a navigation
            // parameter
            rootFrame.Navigate( typeof(MainPage), e.Arguments );
         }
         // Ensure the current window is active
         Window.Current.Activate();
      }

      /// <summary>
      /// Invoked when Navigation to a certain page fails
      /// </summary>
      /// <param name="sender">The Frame which failed navigation</param>
      /// <param name="e">Details about the navigation failure</param>
      void OnNavigationFailed( Object sender, NavigationFailedEventArgs e )
      {
         throw new Exception( "Failed to load Page " + e.SourcePageType.FullName );
      }
   }
}