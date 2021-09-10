using System.Collections.Generic;
using Xunit;

//[assembly: CollectionBehavior(CollectionBehavior.CollectionPerClass, MaxParallelThreads = 4)]
namespace XUnitFirstSeleniumProject.third
{
    ////[Collection("Firefox Tests")]
    public class PureJsTodoFirefoxTests : IClassFixture<FirefoxDriverFixture>, IClassFixture<PagesFixture>
    {
        private readonly FirefoxDriverFixture _fixture;
        private readonly PagesFixture _pagesFixture;

        public PureJsTodoFirefoxTests(FirefoxDriverFixture fixture, PagesFixture pagesFixture)
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
