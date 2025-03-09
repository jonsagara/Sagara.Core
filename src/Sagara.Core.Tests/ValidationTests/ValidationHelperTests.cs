using Sagara.Core.Validation;
using Xunit;

namespace Sagara.Core.Tests.ValidationTests;

public class ValidationHelperTests
{
    public class TestClass
    {
        public string? FirstName { get; set; }
        public int? Age { get; set; }
        public Employer? Employer { get; set; }
    }

    public class ValidatableClass
    {
        public ValidatableProperty<string?> FirstName { get; set; }
        public ValidatableProperty<int?> Age { get; set; }
        public ValidatableProperty<Employer?> Employer { get; set; }
    }

    public class Employer
    {
        public string? ManagerName { get; set; }
    }


    //
    // string?
    //

    [Fact]
    public void CheckRequiredField_String_ReturnsErrorForNull()
    {
        TestClass testClass = new()
        {
            FirstName = null,
        };

        ValidatableClass valClass = new()
        {
            FirstName = new(testClass.FirstName, nameof(testClass.FirstName), null),
        };

        List<RequestError> errors = [];

        ValidationHelper.CheckRequiredField(valClass.FirstName, errors);

        Assert.Single(errors);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\r")]
    [InlineData("\n")]
    [InlineData("\r\n")]
    public void CheckRequiredField_String_ReturnsErrorForWhiteSpace(string whiteSpace)
    {
        TestClass testClass = new()
        {
            FirstName = whiteSpace,
        };

        ValidatableClass valClass = new()
        {
            FirstName = new(testClass.FirstName, nameof(testClass.FirstName), null),
        };

        List<RequestError> errors = [];

        ValidationHelper.CheckRequiredField(new ValidatableProperty<string?>(testClass.FirstName, nameof(testClass.FirstName), null), errors);

        Assert.Single(errors);
    }


    //
    // Reference types
    //

    [Fact]
    public void CheckRequiredField_ReferenceType_ReturnsErrorForNull()
    {
        TestClass testClass = new()
        {
            Employer = null,
        };

        ValidatableClass valClass = new()
        {
            Employer = new(testClass.Employer, nameof(testClass.Employer), null),
        };

        List<RequestError> errors = [];

        ValidationHelper.CheckRequiredField(valClass.Employer, errors);

        Assert.Single(errors);
    }

    [Fact]
    public void CheckRequiredField_ReferenceType_ReturnsNoErrorForNonNull()
    {
        TestClass testClass = new()
        {
            Employer = new Employer(),
        };

        ValidatableClass valClass = new()
        {
            Employer = new(testClass.Employer, nameof(testClass.Employer), null),
        };

        List<RequestError> errors = [];

        ValidationHelper.CheckRequiredField(valClass.Employer, errors);

        Assert.Empty(errors);
    }


    //
    // Value types
    //

    [Fact]
    public void CheckRequiredField_ValueType_ReturnsErrorForNull()
    {
        TestClass testClass = new()
        {
            Age = null,
        };

        ValidatableClass valClass = new()
        {
            Age = new(testClass.Age, nameof(testClass.Age), null),
        };

        List<RequestError> errors = [];

        ValidationHelper.CheckRequiredField(valClass.Age, errors);

        Assert.Single(errors);
    }

    [Fact]
    public void CheckRequiredField_ValueType_ReturnsNoErrorForNonNull()
    {
        TestClass testClass = new()
        {
            Age = 42,
        };

        ValidatableClass valClass = new()
        {
            Age = new(testClass.Age, nameof(testClass.Age), null),
        };

        List<RequestError> errors = [];

        ValidationHelper.CheckRequiredField(valClass.Age, errors);

        Assert.Empty(errors);
    }


    //
    // Max/Min string length
    //

    private const int MaxLength = 7;
    private const int MinLength = 5;

    [Theory]
    [InlineData(null)]
    [InlineData("123456")]
    [InlineData("1234567")]
    public void CheckStringMaxLength_NullLessThanOrEqual_ReturnsNoError(string? firstName)
    {
        TestClass testClass = new()
        {
            FirstName = firstName,
        };

        ValidatableClass valClass = new()
        {
            FirstName = new(testClass.FirstName, nameof(testClass.FirstName), null),
        };

        List<RequestError> errors = [];
        ValidationHelper.CheckStringMaxLength(valClass.FirstName, MaxLength, errors);

        Assert.Empty(errors);
    }

    [Theory]
    [InlineData("12345678")]
    [InlineData("123456789")]
    [InlineData("1234567891")]
    public void CheckStringMaxLength_GreaterThan_ReturnsError(string firstName)
    {
        TestClass testClass = new()
        {
            FirstName = firstName,
        };

        ValidatableClass valClass = new()
        {
            FirstName = new(testClass.FirstName, nameof(testClass.FirstName), null),
        };

        List<RequestError> errors = [];
        ValidationHelper.CheckStringMaxLength(valClass.FirstName, MaxLength, errors);

        Assert.Single(errors);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("123456")]
    [InlineData("1234567")]
    public void CheckStringMinLength_NullGreaterThanEqual_ReturnsNoError(string? firstName)
    {
        TestClass testClass = new()
        {
            FirstName = firstName,
        };

        ValidatableClass valClass = new()
        {
            FirstName = new(testClass.FirstName, nameof(testClass.FirstName), null),
        };

        List<RequestError> errors = [];
        ValidationHelper.CheckStringMinLength(valClass.FirstName, MinLength, errors);

        Assert.Empty(errors);
    }

    [Theory]
    [InlineData("12")]
    [InlineData("123")]
    [InlineData("1234")]
    public void CheckStringMinLength_LessThan_ReturnsError(string firstName)
    {
        TestClass testClass = new()
        {
            FirstName = firstName,
        };

        ValidatableClass valClass = new()
        {
            FirstName = new(testClass.FirstName, nameof(testClass.FirstName), null),
        };

        List<RequestError> errors = [];
        ValidationHelper.CheckStringMinLength(valClass.FirstName, MinLength, errors);

        Assert.Single(errors);
    }


    //
    // GetDisplayName
    //

    [Fact]
    public void GetDisplayName_WithDisplayName_ReturnsDisplayName()
    {
        TestClass testClass = new()
        {
            FirstName = "Jon",
        };

        const string expectedDisplayName = "First Name";

        ValidatableClass valClass = new()
        {
            FirstName = new(testClass.FirstName, nameof(testClass.FirstName), expectedDisplayName),
        };

        var actualDisplayName = valClass.FirstName.GetDisplayName();

        Assert.Equal(expectedDisplayName, actualDisplayName);
    }

    [Fact]
    public void GetDisplayName_NoDisplayName_ReturnsPropertyName()
    {
        TestClass testClass = new()
        {
            FirstName = "Jon",
        };

        ValidatableClass valClass = new()
        {
            FirstName = new(testClass.FirstName, nameof(testClass.FirstName), null),
        };

        var actualDisplayName = valClass.FirstName.GetDisplayName();

        Assert.Equal(nameof(testClass.FirstName), actualDisplayName);
    }
}
