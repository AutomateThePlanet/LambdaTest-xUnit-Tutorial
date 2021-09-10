using System.Collections.Generic;
using OpenQA.Selenium;
using Xunit;

//[assembly: CollectionBehavior(CollectionBehavior.CollectionPerClass, MaxParallelThreads = 4)]
namespace XUnitFirstSeleniumProject.third
{
    ////[Collection("Chrome Tests")]
    public class PureJsTodoChromeTests : IClassFixture<ChromeDriverFixture>, IClassFixture<PagesFixture>
    {
        private readonly ChromeDriverFixture _fixture;
        private readonly PagesFixture _pagesFixture;

        public PureJsTodoChromeTests(ChromeDriverFixture fixture, PagesFixture pagesFixture)
        {
            _fixture = fixture;
            _pagesFixture = pagesFixture;
        }

        [Theory]
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
        public void VerifyTodoListCreatedSuccessfully(string technology)
        {
            var itemsToAdd = new List<string>() { "Clean the car", "Clean the house", "Buy Ketchup" };
            var itemsToCheck = new List<string>() { "Buy Ketchup" };

            _pagesFixture.ToDoFacade.VerifyTodoListCreatedSuccessfully(technology, itemsToAdd, itemsToCheck, 2);
        }     
    }
}
