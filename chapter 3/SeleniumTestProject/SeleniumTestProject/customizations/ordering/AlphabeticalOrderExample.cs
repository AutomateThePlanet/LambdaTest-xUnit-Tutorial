﻿using Xunit;

[TestCaseOrderer("SeleniumTestProject.TestCaseOrdering.AlphabeticalOrderer", "SeleniumTestProject")]
public class AlphabeticalOrderExample
{
    public static bool Test1Called;
    public static bool Test2Called;
    public static bool Test3Called;

    [Fact]
    public void Test1()
    {
        Test1Called = true;

        Assert.False(Test2Called);
        Assert.False(Test3Called);
    }

    [Fact]
    public void Test2()
    {
        Test2Called = true;

        Assert.True(Test1Called);
        Assert.False(Test3Called);
    }

    [Fact]
    public void Test3()
    {
        Test3Called = true;

        Assert.True(Test1Called);
        Assert.True(Test2Called);
    }
}
