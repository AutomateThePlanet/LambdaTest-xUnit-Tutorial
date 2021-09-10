using System.Collections.Generic;

namespace XUnitFirstSeleniumProject.cloud
{
    public static class ToDoFacade
    {

        public static void VerifyTodoListCreatedSuccessfully(string technology, List<string> itemsToAdd, List<string> itemsToCheck, int expectedItemsLeft)
        {
            MainPage.Open();
            MainPage.OpenTechnologyApp(technology);

            itemsToAdd.ForEach(item => ToDoAppPage.AddNewToDoItem(item));
            itemsToCheck.ForEach(item => ToDoAppPage.GetItemCheckBox(item).Click());

            ToDoAppPage.AssertLeftItems(expectedItemsLeft);
        }
    }
}
