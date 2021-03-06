﻿namespace Nubot.Abstractions
{
    using System.IO;
    using System.Reflection;

    public abstract class AdapterBase : IAdapter
    {
        protected readonly IRobot Robot;

        protected AdapterBase(string adapterName, IRobot robot)
        {
            Name = adapterName;

            Robot = robot;
        }

        static AdapterBase()
        {
            ExecutingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            BaseAdapterDirectory = Path.Combine(ExecutingDirectory, "adapters");
        }

        public string Name { get; private set; }

        public static string ExecutingDirectory { get; private set; }

        public static string BaseAdapterDirectory { get; private set; }

        public virtual void Start()
        {
        }

        public abstract void Send(Envelope envelope, params string[] messages);

        public virtual void Message(string message)
        {
        }

        public virtual bool SendNotification(string roomName, string authToken, string htmlMessage, bool notify = false)
        {
            return false;
        }

        public virtual string MakeConfigFileName()
        {
            var adapterName = Name;
            var file = string.Format("{0}.config", adapterName);

            return Path.Combine(BaseAdapterDirectory, file);
        }
    }
}
