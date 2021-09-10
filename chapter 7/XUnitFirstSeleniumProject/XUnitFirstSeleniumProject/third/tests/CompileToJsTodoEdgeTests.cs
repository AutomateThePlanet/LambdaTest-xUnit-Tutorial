using System.Collections.Generic;
using Xunit;

namespace XUnitFirstSeleniumProject.third
{
    ////[Collection("Edge Tests")]
    public class CompileToJsTodoEdgeTests : IClassFixture<EdgeDriverFixture>, IClassFixture<PagesFixture>
    {
        private readonly DriverFixture _fixture;
        private readonly PagesFixture _pagesFixture;

        public CompileToJsTodoEdgeTests(EdgeDriverFixture fixture, PagesFixture pagesFixture)
        {
            _fixture = fixture;
            _pagesFixture = pagesFixture;
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
            var itemsToAdd = new List<string>() { "Clean the car", "Clean the house", "Buy Ketchup" };
            var itemsToCheck = new List<string>() { "Buy Ketchup" };

            _pagesFixture.ToDoFacade.VerifyTodoListCreatedSuccessfully(technology, itemsToAdd, itemsToCheck, 2);
        }    
    }
}
