using Xunit;

namespace Sagara.Core.Tests.ExtensionTests;

public class IEnumerableExtensionsTests
{
    //
    // Number
    //

    [Fact]
    public void Number_NullValue_Throws()
    {
        IEnumerable<int>? source = null;

        // Justification: testing that null throws.
#pragma warning disable CS8604 // Possible null reference argument.
        Assert.Throws<ArgumentNullException>(() => source.Number());
#pragma warning restore CS8604 // Possible null reference argument.
    }

    [Fact]
    public void Number_Returns1BasedNumbers()
    {
        List<string> source = [
            "apple",
            "orange",
            "banana",
            "grapefruit",
            ];

        var expectedNumber = 1;
        foreach (var (number, item) in source.Number())
        {
            Assert.Equal(expected: expectedNumber, actual: number);
            expectedNumber++;
        }
    }
}
