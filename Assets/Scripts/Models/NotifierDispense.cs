using System;
using System.Collections.Generic;

namespace Models
{
    public  abstract class EventNotify
    {

    }
    public abstract class NotifierDispense
    {
        private static readonly Dictionary<string, Action<EventNotify>> Dictionary = new();

        public static void Attach(string command,Action<EventNotify> eventNotify)
        {
            //如果获取到与指定命令关联的方法，将返回添加到方法中，并将其放入到列表中；否则直接将命令添加到列表中
            if (Dictionary.TryGetValue(command,out _))
            {
                Dictionary[command] = eventNotify;
            }
            else
            {
                Dictionary.Add(command,eventNotify);
            }
        }
        //如果字典中包含了指定命令，则将其注册方法在指定命令列表中删除
        public static void Detach(string command,Action<EventNotify> eventNotify)
        {
            if (Dictionary.ContainsKey(command))
            {
                Dictionary[command] -= eventNotify;
            }
        }

        //通知事件 action 起到回调函数的作用
        public static void Notify(string command,EventNotify eventNotify)
        {
            if (Dictionary.TryGetValue(command,out var action))
            {
                action(eventNotify);
            }
        }
    }
}