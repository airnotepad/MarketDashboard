using Application.Common.Interfaces;
using Common.Parallelism;
using MarketPlugin.Base;
using System.Reflection;

namespace Infrastructure.Services
{
    class MarketsService : IMarketService
    {
        private readonly string pluginPath = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");
        private Dictionary<string, IMarket> plugins = new Dictionary<string, IMarket>();
        private RWLock locker = new RWLock();

        public MarketsService()
        {
            RefreshPlugins();
        }

        public void RefreshPlugins()
        {
            using (locker.WriteLock())
            {
                plugins.Clear();

                DirectoryInfo pluginDirectory = new DirectoryInfo(pluginPath);
                if (!pluginDirectory.Exists)
                    pluginDirectory.Create();

                foreach (var direcory in pluginDirectory.GetDirectories())
                {
                    var pluginFiles = Directory.GetFiles(direcory.FullName, "*.dll");
                    foreach (var file in pluginFiles)
                    {
                        Assembly asm = Assembly.LoadFrom(file);
                        var types = asm.GetTypes()
                            .Where(t => t.GetInterfaces()
                            .Where(i => i.FullName == typeof(IMarket).FullName).Any());

                        foreach (var type in types)
                        {
                            var plugin = asm.CreateInstance(type.FullName) as IMarket;
                            plugins.Add(plugin.Name, plugin);
                        }
                    }
                }
            }
        }

        public IList<string> GetMarketNames() { using (locker.ReadLock()) return plugins.Keys.ToList(); }

        public bool TryGetMarket(string Name, out IMarket Market) { using (locker.ReadLock()) return plugins.TryGetValue(Name, out Market); }

        public bool TryGetMarket(Guid Guid, out IMarket Market)
        {
            using (locker.ReadLock())
            {
                if (plugins.Any(x => x.Value.GetType().GUID == Guid))
                {
                    Market = plugins.FirstOrDefault(x => x.Value.GetType().GUID == Guid).Value;
                    return true;
                }
                else
                {
                    Market = null;
                    return false;
                }
            }
        }
    }
}
