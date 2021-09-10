using System;

namespace XUnitFirstSeleniumProject.third
{
    public class PagesFixture : IDisposable
    {
        public PagesFixture()
        {
            MainPage = new MainPage(DriverFixture.Driver.Value);
            ToDoAppPage = new ToDoAppPage(DriverFixture.Driver.Value);
            ToDoFacade = new ToDoFacade(MainPage, ToDoAppPage);
        }

        public MainPage MainPage { get; set; }
        public ToDoAppPage ToDoAppPage { get; set; }
        public ToDoFacade ToDoFacade { get; set; }

        public void Dispose()
        {
        }
    }
}
