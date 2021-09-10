using System.Collections.Generic;
using Xunit;

namespace XUnitFirstSeleniumProject.cloud
{
    ////[Collection("Edge Tests")]
    public class CompileToJsTodoEdgeTests
    {
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
        [BrowserRunTheory]
        [Browser(BrowserType.Edge)]
        public void VerifyTodoListCreatedSuccessfully(string technology)
        {
            var itemsToAdd = new List<string>() { "Clean the car", "Clean the house", "Buy Ketchup" };
            var itemsToCheck = new List<string>() { "Buy Ketchup" };

            ToDoFacade.VerifyTodoListCreatedSuccessfully(technology, itemsToAdd, itemsToCheck, 2);
        }    
    }
}
