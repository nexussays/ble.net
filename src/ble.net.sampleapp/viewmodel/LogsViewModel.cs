// Copyright Malachi Griffie
// 
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using ble.net.sampleapp.util;
using nexus.core;
using nexus.core.logging;
using Xamarin.Forms;

namespace ble.net.sampleapp.viewmodel
{
   /// <summary>
   /// Acts as a log sink to display logs within app without requiring a debugger connected or DEBUG mode to be active
   /// </summary>
   public sealed class LogsViewModel
      : BaseViewModel,
        ILogSink
   {
      private const Int32 LOG_BUFFER_MAX_SIZE = 100;
      private readonly Object m_lock = new Object();

      private readonly Queue<String> m_logEntries;

      public LogsViewModel()
      {
         m_logEntries = new Queue<String>( LOG_BUFFER_MAX_SIZE );
      }

      public String LogBuffer
      {
         get
         {
            lock(m_lock)
            {
               return m_logEntries.Join( "\n" );
            }
         }
      }

      public String PageTitle => "Logs";

      /// <inheritdoc />
      public void Handle( ILogEntry entry, Int32 sequenceNumber )
      {
         lock(m_lock)
         {
            while(m_logEntries.Count >= LOG_BUFFER_MAX_SIZE)
            {
               m_logEntries.Dequeue();
            }

            m_logEntries.Enqueue(
               sequenceNumber + " [" + entry.Severity.ToString().ToUpperInvariant() + "] " +
               entry.FormatMessageAndArguments() + " " + entry.Data.Select( x => x?.ToString() + "" ).Join( " " ) );
         }
         Device.BeginInvokeOnMainThread( () => RaisePropertyChanged( nameof(LogBuffer) ) );
      }
   }
}