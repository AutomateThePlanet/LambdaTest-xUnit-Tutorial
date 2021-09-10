using System;

namespace XUnitFirstSeleniumProject.second
{
    public abstract class DriverFixture : IDisposable
    {
        private const int WAIT_FOR_ELEMENT_TIMEOUT = 30;

        protected DriverFixture()
        {
            Driver = new DriverAdapter();
            IntializeDriver();
        }

        protected abstract void IntializeDriver();
        public virtual int WaitForElementTimeout { get; set; } = WAIT_FOR_ELEMENT_TIMEOUT;

        public DriverAdapter Driver { get; set; }

        public void Dispose()
        {
            Driver.Dispose();
        }
    }
}
