using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;

namespace XUnitFirstSeleniumProject.third
{
    public class TestDataFixture : IDisposable
    {
        public TestDataFixture()
        {
            var fixture = new Fixture();

            ItemsToAdd = fixture.CreateMany<string>(5).ToList();
            ItemsToCheck = ItemsToAdd.Skip(3).ToList();
            ExpectedItemsLeft = 3;
        }

        public List<string> ItemsToAdd { get; set; }
        public List<string> ItemsToCheck { get; set; }
        public int ExpectedItemsLeft { get; set; }

        public void Dispose() { }
    }
}
