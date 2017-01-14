using System;
using System.Linq;
using nexus.core;
using nexus.core.logging;
using UIKit;

namespace ble.net.sampleapp.ios
{
   public class Application
   {
      public const Boolean IS_DEBUG = 
#if DEBUG
         true;
#else
         false;
#endif

      internal static void Main( String[] args )
      {
         // ReSharper disable once ConditionIsAlwaysTrueOrFalse
         if(IS_DEBUG)
         {
            // dump all entries to Android system log in debug mode
            SystemLog.Instance.AddSink(
                        entry =>
                        {
                           var message = entry.FormatMessageAndArguments() + " " +
                                         entry.Data.Select( x => x?.ToString() + "" ).Join( " " );
                           if(entry.Severity == LogLevel.Error)
                           {
                              Console.Error.WriteLine( message );
                           }
                           else
                           {
                              Console.Out.WriteLine( message );
                           }
                        } );
         }

         UIApplication.Main( args, null, nameof( AppDelegate ) );
      }
   }
}