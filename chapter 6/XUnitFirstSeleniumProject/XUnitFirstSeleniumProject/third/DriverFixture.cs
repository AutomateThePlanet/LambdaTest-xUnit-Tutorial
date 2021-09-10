using System;
using System.Threading;

namespace XUnitFirstSeleniumProject.third
{
    public abstract class DriverFixture : IDisposable
    {
        private const int WAIT_FOR_ELEMENT_TIMEOUT = 30;

        protected DriverFixture()
        {
            Driver = new ThreadLocal<DriverAdapter>(() => new DriverAdapter());
            IntializeDriver();
        }

        protected abstract void IntializeDriver();
        public virtual int WaitForElementTimeout { get; set; } = WAIT_FOR_ELEMENT_TIMEOUT;

        public static ThreadLocal<DriverAdapter> Driver { get; set; }

        public void Dispose()
        {
            Driver.Value.Dispose();
        }
    }
}
