using System.Numerics;
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

        ValidationHelper.CheckRequiredField(valClass.FirstName, errors);

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


    //
    // Greater Than
    //

    [Theory]
    [InlineData((byte)1, (byte)0)]
    public void CheckGreaterThan_Byte_Success(byte value, byte threshold)
    {
        var property = CreateValidatableProperty<byte>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Empty(errors);
    }

    [Theory]
    [InlineData((byte)0, (byte)1)]
    [InlineData(1, 1)]
    public void CheckGreaterThan_Byte_Failure(byte value, byte threshold)
    {
        var property = CreateValidatableProperty<byte>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Single(errors);
    }

    [Theory]
    [InlineData((sbyte)1, (sbyte)0)]
    [InlineData((sbyte)-1, (sbyte)-5)]
    public void CheckGreaterThan_SByte_Success(sbyte value, sbyte threshold)
    {
        var property = CreateValidatableProperty<sbyte>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Empty(errors);
    }

    [Theory]
    [InlineData((sbyte)0, (sbyte)1)]
    [InlineData((sbyte)1, (sbyte)1)]
    [InlineData((sbyte)-5, (sbyte)-1)]
    public void CheckGreaterThan_SByte_Failure(sbyte value, sbyte threshold)
    {
        var property = CreateValidatableProperty<sbyte>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Single(errors);
    }

    [Theory]
    [InlineData((short)1, (short)0)]
    [InlineData((short)-1, (short)-5)]
    public void CheckGreaterThan_Int16_Success(short value, short threshold)
    {
        var property = CreateValidatableProperty<short>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Empty(errors);
    }

    [Theory]
    [InlineData((short)0, (short)1)]
    [InlineData((short)1, (short)1)]
    [InlineData((short)-5, (short)-1)]
    public void CheckGreaterThan_Int16_Failure(short value, short threshold)
    {
        var property = CreateValidatableProperty<short>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Single(errors);
    }

    [Theory]
    [InlineData((ushort)1, (ushort)0)]
    public void CheckGreaterThan_UInt16_Success(ushort value, ushort threshold)
    {
        var property = CreateValidatableProperty<ushort>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Empty(errors);
    }

    [Theory]
    [InlineData((ushort)0, (ushort)1)]
    [InlineData((ushort)1, (ushort)1)]
    public void CheckGreaterThan_UInt16_Failure(ushort value, ushort threshold)
    {
        var property = CreateValidatableProperty<ushort>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Single(errors);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(-1, -5)]
    public void CheckGreaterThan_Int32_Success(int value, int threshold)
    {
        var property = CreateValidatableProperty<int>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Empty(errors);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(-5, -1)]
    public void CheckGreaterThan_Int32_Failure(int value, int threshold)
    {
        var property = CreateValidatableProperty<int>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Single(errors);
    }

    [Theory]
    [InlineData((uint)1, (uint)0)]
    public void CheckGreaterThan_UInt32_Success(uint value, uint threshold)
    {
        var property = CreateValidatableProperty<uint>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Empty(errors);
    }

    [Theory]
    [InlineData((uint)0, (uint)1)]
    [InlineData((uint)1, (uint)1)]
    public void CheckGreaterThan_UInt32_Failure(uint value, uint threshold)
    {
        var property = CreateValidatableProperty<uint>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Single(errors);
    }

    [Theory]
    [InlineData(1L, 0L)]
    [InlineData(-1L, -5L)]
    public void CheckGreaterThan_Int64_Success(long value, long threshold)
    {
        var property = CreateValidatableProperty<long>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Empty(errors);
    }

    [Theory]
    [InlineData(0L, 1L)]
    [InlineData(1L, 1L)]
    [InlineData(-5L, -1L)]
    public void CheckGreaterThan_Int64_Failure(long value, long threshold)
    {
        var property = CreateValidatableProperty<long>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Single(errors);
    }

    [Theory]
    [InlineData((ulong)1L, (ulong)0L)]
    public void CheckGreaterThan_UInt64_Success(ulong value, ulong threshold)
    {
        var property = CreateValidatableProperty<ulong>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Empty(errors);
    }

    [Theory]
    [InlineData((ulong)0L, (ulong)1L)]
    [InlineData((ulong)1L, (ulong)1L)]
    public void CheckGreaterThan_UInt64_Failure(ulong value, ulong threshold)
    {
        var property = CreateValidatableProperty<ulong>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Single(errors);
    }

    [Fact]
    public void CheckGreaterThan_IntPtr_Success()
    {
        var property = CreateValidatableProperty<nint>((nint)1L);
        var threshold = (nint)0L;
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Empty(errors);
    }

    [Fact]
    public void CheckGreaterThan_IntPtr_Failure()
    {
        var property = CreateValidatableProperty<nint>((nint)0L);
        var threshold = (nint)1L;
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Single(errors);
    }

    [Fact]
    public void CheckGreaterThan_UIntPtr_Success()
    {
        var property = CreateValidatableProperty<nuint>((nuint)1L);
        var threshold = (nuint)0L;
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Empty(errors);
    }

    [Fact]
    public void CheckGreaterThan_UIntPtr_Failure()
    {
        var property = CreateValidatableProperty<nuint>((nuint)0L);
        var threshold = (nuint)1L;
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Single(errors);
    }

    [Theory]
    [InlineData(1f, 0f)]
    [InlineData(-1f, -5f)]
    public void CheckGreaterThan_Single_Success(float value, float threshold)
    {
        var property = CreateValidatableProperty<float>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Empty(errors);
    }

    [Theory]
    [InlineData(0f, 1f)]
    [InlineData(1f, 1f)]
    [InlineData(-5f, -1f)]
    public void CheckGreaterThan_Single_Failure(float value, float threshold)
    {
        var property = CreateValidatableProperty<float>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Single(errors);
    }

    [Theory]
    [InlineData(1.0f, 0.0f)]
    [InlineData(-1.0f, -5.0f)]
    public void CheckGreaterThan_Single_0Point0_Success(float value, float threshold)
    {
        var property = CreateValidatableProperty<float>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Empty(errors);
    }

    [Theory]
    [InlineData(0.0f, 1.0f)]
    [InlineData(1.0f, 1.0f)]
    [InlineData(-5.0f, -1.0f)]
    public void CheckGreaterThan_Single_0Point0_Failure(float value, float threshold)
    {
        var property = CreateValidatableProperty<float>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Single(errors);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(-1, -5)]
    public void CheckGreaterThan_Double_Success(double value, double threshold)
    {
        var property = CreateValidatableProperty<double>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Empty(errors);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(-5, -1)]
    public void CheckGreaterThan_Double_Failure(double value, double threshold)
    {
        var property = CreateValidatableProperty<double>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Single(errors);
    }

    [Theory]
    [InlineData(1.0, 0.0)]
    [InlineData(-1.0, -5.0)]
    public void CheckGreaterThan_Double_0Point0_Success(double value, double threshold)
    {
        var property = CreateValidatableProperty<double>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Empty(errors);
    }

    [Theory]
    [InlineData(0.0, 1.0)]
    [InlineData(1.0, 1.0)]
    [InlineData(-5.0, -1.0)]
    public void CheckGreaterThan_Double_0Point0_Failure(double value, double threshold)
    {
        var property = CreateValidatableProperty<double>(value);
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Single(errors);
    }

    [Fact]
    public void CheckGreaterThan_Decimal_Success()
    {
        var property = CreateValidatableProperty<decimal>(1m);
        var threshold = 0m;
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Empty(errors);
    }

    [Fact]
    public void CheckGreaterThan_Decimal_Failure()
    {
        var property = CreateValidatableProperty<decimal>(0m);
        var threshold = 1m;
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Single(errors);
    }

    [Fact]
    public void CheckGreaterThan_Decimal_0Point0_Success()
    {
        var property = CreateValidatableProperty<decimal>(1.0m);
        var threshold = 0.0m;
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Empty(errors);
    }

    [Fact]
    public void CheckGreaterThan_Decimal_0Point0_Failure()
    {
        var property = CreateValidatableProperty<decimal>(0.0m);
        var threshold = 1m;
        List<RequestError> errors = [];

        ValidationHelper.CheckGreaterThan(property, threshold, errors);

        Assert.Single(errors);
    }


    //
    // Private methods
    //

    private ValidatableProperty<TNumber?> CreateValidatableProperty<TNumber>(TNumber? value)
        where TNumber : struct, INumber<TNumber>
        => new(value, "property", "display");
}
