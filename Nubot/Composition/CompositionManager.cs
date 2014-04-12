﻿namespace Nubot.Composition
{
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.IO;
    using System.Reflection;
    using System.Text;

    public class CompositionManager
    {
        private readonly Robot _robot;
        private static readonly string ExecutingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private readonly string _pluginsDirectory = string.Format("{0}\\plugins\\", ExecutingDirectory);
        private readonly string _adaptersDirectory = string.Format("{0}\\adapters\\", ExecutingDirectory);
        private DirectoryCatalog _pluginsdirectoryCatalog;

        public CompositionManager(Robot robot)
        {
            _robot = robot;
        }

        public void Compose()
        {
            if (!Directory.Exists(_pluginsDirectory))
            {
                Directory.CreateDirectory(_pluginsDirectory);
            }

            LoadPlugins();
        }

        private void LoadPlugins()
        {
            _pluginsdirectoryCatalog = new DirectoryCatalog(_pluginsDirectory);
            var adapterdirectoryCatalog = new DirectoryCatalog(_adaptersDirectory);
            var applicationCatalog = new ApplicationCatalog();
            var catalog = new AggregateCatalog(applicationCatalog, _pluginsdirectoryCatalog, adapterdirectoryCatalog);

            var container = new CompositionContainer(catalog);
            container.ComposeParts(_robot);

            ShowLoadedPlugins(applicationCatalog, "Loaded the following Nubot plugins");
            ShowLoadedPlugins(_pluginsdirectoryCatalog, "Loaded the following plugins");
            ShowLoadedPlugins(adapterdirectoryCatalog, "Loaded the following adapter");
        }

        private void ShowLoadedPlugins(ComposablePartCatalog catalog, string message)
        {
            var builder = new StringBuilder();
            builder.AppendLine(message);
            foreach (var part in catalog.Parts)
            {
                builder.AppendFormat("\t{0}\n", part);
            }

            _robot.Logger.WriteLine(builder.ToString());
        }

        public void Refresh()
        {
            _pluginsdirectoryCatalog.Refresh();
        }
    }
}