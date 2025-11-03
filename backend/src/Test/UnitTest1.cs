using System.Linq.Expressions;
using Test;

namespace TitanicExplorer.Test;

public class UnitTest1
{
    [Fact]
    public void IsPrime()
    {
        var value = Expression.Parameter(typeof(int), "value");
        var result = ScriptingEngine.IsPrime(value);

        var expression = Expression.Lambda<Func<int, bool>>(result, value);
        var func = expression.Compile();

        Assert.False(func(0));
        Assert.False(func(1));
        Assert.True(func(2));
        Assert.True(func(3));
        Assert.False(func(4));
        Assert.True(func(5));
        Assert.False(func(144));
    }
}