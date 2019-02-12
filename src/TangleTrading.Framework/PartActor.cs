using Akka.Actor;
using Akka.Event;
using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TangleTrading.Framework
{
    /// <summary>
    /// 将<see cref="IPart"/>
    /// </summary>
    public class PartActor : ReceiveActor//, IPartContext
    {
        //private EventStream es;

        //readonly WeakReference reference;
        //readonly Dictionary<Type, MethodInfo> supportedHandlers = new Dictionary<Type, MethodInfo>();

        //public ILog Logger { get; protected set; }
        //public ILog GetLogger(string name)
        //{
        //    //return new NLogLogger(NLog.LogManager.GetLogger(name));
        //    return null;
        //}



        //public PartActor(IPart part)
        //{
        //    Logger = LogManager.GetLogger(part.GetType());

        //    es = Context.System.EventStream;
        //    //es.Subscribe(Self, typeof(IDomainEvent));

        //    reference = new WeakReference(part);
        //    part.Context = this;

        //    var interfaces = part.GetType().GetInterfaces()
        //       .Where(x => typeof(IHandle).IsAssignableFrom(x) && x.IsGenericType);

        //    foreach (var @interface in interfaces)
        //    {
        //        var type = @interface.GetGenericArguments()[0];
        //        var method = @interface.GetMethod("Handle", new Type[] { type });

        //        if (method != null)
        //        {
        //            supportedHandlers[type] = method;
        //            es.Subscribe(Self, type);
                    
        //            Receive(type, (msg) => method.Invoke(part, new object[] { msg }));        
        //        }
        //    }


        //    interfaces = part.GetType().GetInterfaces()
        //       .Where(x => typeof(IExecute).IsAssignableFrom(x) && x.IsGenericType);

        //    foreach (var @interface in interfaces)
        //    {
        //        var type = @interface.GetGenericArguments()[0];
        //        var method = @interface.GetMethod("Execute", new Type[] { type });

        //        if (method != null)
        //        {
        //            supportedHandlers[type] = method;
        //            es.Subscribe(Self, type);
                    
        //            Receive(type, (cmd) => method.Invoke(part, new object[] { cmd }));        
        //        }
        //    }
            

            

        //    //    //TODO:
        //    //    // logger.warn("unknown message");

        //    //});
        //}

        //public void Subscribe(Type t)
        //{
        //    es.Subscribe(Self, t);
        //}

        //public void Publish(object message)
        //{
        //    es.Publish(message);
            
        //}

        //public void Schedule(int initialMillisecondsDelay, int millisecondsInterval, object message)
        //{
        //    //(TimeSpan initialDelay, TimeSpan interval, ICanTell receiver, object message, IActorRef sender)
        //    Context.System.Scheduler.ScheduleTellRepeatedly(initialMillisecondsDelay, millisecondsInterval, Self, message, Self);
        //}
    }
}
