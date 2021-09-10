using Xunit;

namespace XUnitFirstSeleniumProject.cloud
{
    ////[Collection("Chrome Tests")]
    public class CompileToJsTodoChromeTests : IClassFixture<DriverFixture>, IClassFixture<TestDataFixture>
    {
        private readonly TestDataFixture _testDataFixture;

        // initialize fixture browser but not start the browser
        public CompileToJsTodoChromeTests(DriverFixture driverFixture, TestDataFixture testDataFixture)
        {
            _testDataFixture = testDataFixture;
        }

        [InlineData("Closure")]
        [InlineData("Dart")]
        [InlineData("Elm")]
        [InlineData("cujoJS")]
        [InlineData("Spine")]
        [InlineData("Angular 2.01")]
        [InlineData("Mithril")]
        [InlineData("Kotlin + React")]
        [InlineData("Firebase + AngularJS")]
        [InlineData("Vanilla ES6")]
        [BrowserRunTheory]
        [Browser(BrowserType.Chrome)]
        public void VerifyTodoListCreatedSuccessfully(string technology)
        {
            ToDoFacade.VerifyTodoListCreatedSuccessfully(technology, _testDataFixture.ItemsToAdd, _testDataFixture.ItemsToCheck, _testDataFixture.ExpectedItemsLeft);
        }     
    }
}
