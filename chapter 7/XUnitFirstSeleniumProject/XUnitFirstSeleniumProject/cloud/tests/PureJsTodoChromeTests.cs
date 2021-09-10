using System.Collections.Generic;
using OpenQA.Selenium;
using Xunit;

//[assembly: CollectionBehavior(CollectionBehavior.CollectionPerClass, MaxParallelThreads = 4)]
namespace XUnitFirstSeleniumProject.cloud
{
    ////[Collection("Chrome Tests")]
    public class PureJsTodoChromeTests : IClassFixture<DriverFixture>
    {
        public PureJsTodoChromeTests(DriverFixture driverFixture)
        {
        }

        [InlineData("Backbone.js")]
        [InlineData("AngularJS")]
        [InlineData("React")]
        [InlineData("Vue.js")]
        [InlineData("CanJS")]
        [InlineData("Ember.js")]
        [InlineData("KnockoutJS")]
        [InlineData("Marionette.js")]
        [InlineData("Polymer")]
        [InlineData("Vanilla JS")]
        [InlineData("jQuery")]
        [InlineData("Dojo")]
        [BrowserRunTheory]
        [Browser(BrowserType.Chrome)]
        public void VerifyTodoListCreatedSuccessfully(string technology)
        {
            var itemsToAdd = new List<string>() { "Clean the car", "Clean the house", "Buy Ketchup" };
            var itemsToCheck = new List<string>() { "Buy Ketchup" };

            ToDoFacade.VerifyTodoListCreatedSuccessfully(technology, itemsToAdd, itemsToCheck, 2);
        }     
    }
}
