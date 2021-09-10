using System.Collections.Generic;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Xunit;

namespace XUnitFirstSeleniumProject.third
{
    ////[Collection("Chrome Tests")]
    public class CompileToJsTodoChromeTests : IClassFixture<ChromeDriverFixture>, IClassFixture<PagesFixture>, IClassFixture<TestDataFixture>
    {
        private readonly ChromeDriverFixture _fixture;
        private readonly PagesFixture _pagesFixture;
        private readonly TestDataFixture _testDataFixture;

        public CompileToJsTodoChromeTests(ChromeDriverFixture fixture, PagesFixture pagesFixture, TestDataFixture testDataFixture)
        {
            _fixture = fixture;
            _pagesFixture = pagesFixture;
            _testDataFixture = testDataFixture;
        }

        [Theory]
        [InlineData("Closure")]
        [InlineData("Dart")]
        [InlineData("Elm")]
        [InlineData("cujoJS")]
        [InlineData("Spine")]
        [InlineData("Angular 2.0")]
        [InlineData("Mithril")]
        [InlineData("Kotlin + React")]
        [InlineData("Firebase + AngularJS")]
        [InlineData("Vanilla ES6")]
        public void VerifyTodoListCreatedSuccessfully(string technology)
        {
            _pagesFixture.ToDoFacade.VerifyTodoListCreatedSuccessfully(technology, _testDataFixture.ItemsToAdd, _testDataFixture.ItemsToCheck, _testDataFixture.ExpectedItemsLeft);
        }     
    }
}
