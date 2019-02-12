﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangle.PluginModel
{
    /// <summary>
    ///   A marker interface for classes that subscribe to messages.
    /// </summary>
    public interface IHandle
    {
    }


    /// <summary>
    ///   Denotes a class which can handle a particular type of message.
    /// </summary>
    /// <typeparam name = "TMessage">The type of message to handle.</typeparam>
    public interface IHandle<in TEvent> : IHandle
        //where TEvent : IDomainEvent
    { //don't use contravariance here
        /// <summary>
        ///   Handles the message.
        /// </summary>
        /// <param name = "message">The message.</param>
        void Handle(TEvent e);
    }
}
