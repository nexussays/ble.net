// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
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
      public void Handle( params ILogEntry[] entries )
      {
         lock(m_lock)
         {
            foreach(var entry in entries)
            {
               m_logEntries.Enqueue( entry.SequenceId + " " + entry.FormatAsString() );
            }

            while(m_logEntries.Count > LOG_BUFFER_MAX_SIZE)
            {
               m_logEntries.Dequeue();
            }
         }
         Device.BeginInvokeOnMainThread( () => RaisePropertyChanged( nameof(LogBuffer) ) );
      }
   }
}
