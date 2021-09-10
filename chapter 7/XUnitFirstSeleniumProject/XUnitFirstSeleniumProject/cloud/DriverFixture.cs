using System;
using System.Threading;

namespace XUnitFirstSeleniumProject.cloud
{
    public class DriverFixture : IDisposable
    {
        public DriverFixture()
        {
            Driver = Settings.GetExecutionSettings().RunInCloud ?
                new ThreadLocal<IDriverAdapter>(() => new SettingsCloudDriverAdapter()) :
                new ThreadLocal<IDriverAdapter>(() => new DriverAdapter());

#if (!DEBUG)
            Driver = new ThreadLocal<IDriverAdapter>(() => new DriverAdapter());
#elif (!RELEASE)
            Driver = new ThreadLocal<IDriverAdapter>(() => new CloudDriverAdapter());
#else
            throw new ArgumentException("Test environment not supported!");
#endif
        }

        public static ThreadLocal<string> TestName { get; set; }

        public static ThreadLocal<IDriverAdapter> Driver { get; set; }

        public void Dispose()
        {
            Driver.Value.Dispose();
        }
    }
}
