using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Utility.模式
{
    public sealed class H多事件<T>
    {
        // The private dictionary used to maintain EventKey -> Delegate mappings
        private readonly Dictionary<T, Delegate> _缓存 =
            new Dictionary<T, Delegate>();

        // Adds an EventKey -> Delegate mapping if it doesn't exist or 
        // combines a delegate to an existing EventKey
        public void 注册(T __键, Delegate __处理方法)
        {
            Monitor.Enter(_缓存);
            Delegate d;
            _缓存.TryGetValue(__键, out d);
            _缓存[__键] = Delegate.Combine(d, __处理方法);
            Monitor.Exit(_缓存);
        }

        // Removes a delegate from an EventKey (if it exists) and 
        // removes the EventKey -> Delegate mapping the last delegate is removed
        public void 注销(T __键, Delegate __处理方法)
        {
            Monitor.Enter(_缓存);
            // Call TryGetValue to ensure that an exception is not thrown if
            // attempting to remove a delegate from an EventKey not in the set
            Delegate d = null;
            if (_缓存.TryGetValue(__键, out d))
            {
                d = Delegate.Remove(d, __处理方法);

                // If a delegate remains, set the new head else remove the EventKey
                if (d != null) _缓存[__键] = d;
                else _缓存.Remove(__键);
            }
            Monitor.Exit(_缓存);
        }

        public void 注销所有()
        {
            Monitor.Enter(_缓存);
            _缓存.Clear();
            Monitor.Exit(_缓存);
        }

        // Raises the event for the indicated EventKey
        public void 触发(T __键, params object[] __事件参数)
        {
            // Don't throw an exception if the EventKey is not in the set
            Delegate d;
            Monitor.Enter(_缓存);
            _缓存.TryGetValue(__键, out d);
            Monitor.Exit(_缓存);

            if (d != null)
            {
                // Because the dictionary can contain several different delegate types,
                // it is impossible to construct a type-safe call to the delegate at 
                // compile time. So, I call the System.Delegate type抯 DynamicInvoke 
                // method, passing it the callback method抯 parameters as an array of 
                // objects. Internally, DynamicInvoke will check the type safety of the 
                // parameters with the callback method being called and call the method.
                // If there is a type mismatch, then DynamicInvoke will throw an exception.
                d.DynamicInvoke(__事件参数);
            }
        }

        // Raises the event for the indicated EventKey
        public List<object> 触发返回(T __键, params object[] __事件参数)
        {
            // Don't throw an exception if the EventKey is not in the set
            var __结果 = new List<object>();
            Delegate d;
            Monitor.Enter(_缓存);
            _缓存.TryGetValue(__键, out d);
            Monitor.Exit(_缓存);

            if (d != null)
            {
                // Because the dictionary can contain several different delegate types,
                // it is impossible to construct a type-safe call to the delegate at 
                // compile time. So, I call the System.Delegate type抯 DynamicInvoke 
                // method, passing it the callback method抯 parameters as an array of 
                // objects. Internally, DynamicInvoke will check the type safety of the 
                // parameters with the callback method being called and call the method.
                // If there is a type mismatch, then DynamicInvoke will throw an exception.

                __结果.AddRange(d.GetInvocationList().Select(d1 => d1.DynamicInvoke(__事件参数)));
            }
            return __结果;
        }

    }
}



/////////////////////////////////////////////////////////////////////////////////


//// This class exists to provide a bit more type safety and 
//// code maintainability when using EventSet
//public sealed class EventKey : Object
//{
//}


/////////////////////////////////////////////////////////////////////////////////


//public sealed class EventSet
//{
//    // The private dictionary used to maintain EventKey -> Delegate mappings
//    private readonly Dictionary<EventKey, Delegate> m_events =
//        new Dictionary<EventKey, Delegate>();

//    // Adds an EventKey -> Delegate mapping if it doesn't exist or 
//    // combines a delegate to an existing EventKey
//    public void Add(EventKey eventKey, Delegate handler)
//    {
//        Monitor.Enter(m_events);
//        Delegate d;
//        m_events.TryGetValue(eventKey, out d);
//        m_events[eventKey] = Delegate.Combine(d, handler);
//        Monitor.Exit(m_events);
//    }

//    // Removes a delegate from an EventKey (if it exists) and 
//    // removes the EventKey -> Delegate mapping the last delegate is removed
//    public void Remove(EventKey eventKey, Delegate handler)
//    {
//        Monitor.Enter(m_events);
//        // Call TryGetValue to ensure that an exception is not thrown if
//        // attempting to remove a delegate from an EventKey not in the set
//        Delegate d;
//        if (m_events.TryGetValue(eventKey, out d))
//        {
//            d = Delegate.Remove(d, handler);

//            // If a delegate remains, set the new head else remove the EventKey
//            if (d != null) m_events[eventKey] = d;
//            else m_events.Remove(eventKey);
//        }
//        Monitor.Exit(m_events);
//    }

//    // Raises the event for the indicated EventKey
//    public void Raise(EventKey eventKey, Object sender, EventArgs e)
//    {
//        // Don't throw an exception if the EventKey is not in the set
//        Delegate d;
//        Monitor.Enter(m_events);
//        m_events.TryGetValue(eventKey, out d);
//        Monitor.Exit(m_events);

//        if (d != null)
//        {
//            // Because the dictionary can contain several different delegate types,
//            // it is impossible to construct a type-safe call to the delegate at 
//            // compile time. So, I call the System.Delegate type抯 DynamicInvoke 
//            // method, passing it the callback method抯 parameters as an array of 
//            // objects. Internally, DynamicInvoke will check the type safety of the 
//            // parameters with the callback method being called and call the method.
//            // If there is a type mismatch, then DynamicInvoke will throw an exception.
//            d.DynamicInvoke(new Object[] { sender, e });
//        }
//    }
//}

