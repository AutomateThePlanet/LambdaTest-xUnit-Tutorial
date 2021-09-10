using System.Collections.Generic;

namespace XUnitFirstSeleniumProject.third
{
    public class ToDoFacade
    {
        private readonly MainPage _mainPage;
        private readonly ToDoAppPage _toDoAppPage;

        public ToDoFacade(MainPage mainPage, ToDoAppPage toDoAppPage)
        {
            _mainPage = mainPage;
            _toDoAppPage = toDoAppPage;
        }

        public void VerifyTodoListCreatedSuccessfully(string technology, List<string> itemsToAdd, List<string> itemsToCheck, int expectedItemsLeft)
        {
            _mainPage.Open();
            _mainPage.OpenTechnologyApp(technology);

            itemsToAdd.ForEach(item => _toDoAppPage.AddNewToDoItem(item));
            itemsToCheck.ForEach(item => _toDoAppPage.GetItemCheckBox(item).Click());

            _toDoAppPage.AssertLeftItems(expectedItemsLeft);
        }
    }
}
