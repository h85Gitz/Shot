using System;
using Utilities;

namespace Test.SubscriberModel
{
    internal delegate void StackEventHandler<in T, U>(T sender, U eventArgs);
     class Stackle<T> : Singleton<Stackle<T>>
    {
        public class StackEventArgs : EventArgs { string a; }
        public event StackEventHandler<Stackle<T>, StackEventArgs> stackleEvent;
        protected virtual void OnStackChanged(StackEventArgs a) { stackleEvent(this, a); }

        //引发事件 条件
        public void add(T a)
        {
            var se = new StackEventArgs();
            OnStackChanged(se);
        }
    }

    //练习委托

}

