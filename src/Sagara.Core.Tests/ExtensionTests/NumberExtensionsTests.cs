using Sagara.Core.Extensions;
using Xunit;

namespace Sagara.Core.Tests.ExtensionTests;

public class NumberExtensionsTests
{
    //
    // To Null
    //

    [Fact]
    public void ToNullIfZero_Byte_0ReturnsNull()
    {
        Assert.Null(((Byte)0).ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_SByte_0ReturnsNull()
    {
        Assert.Null(((SByte)0).ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_Int16_0ReturnsNull()
    {
        Assert.Null(((Int16)0).ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_UInt16_0ReturnsNull()
    {
        Assert.Null(((UInt16)0).ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_Int32_0ReturnsNull()
    {
        Assert.Null(0.ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_UInt32_0ReturnsNull()
    {
        Assert.Null(((UInt32)0).ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_Int64_0ReturnsNull()
    {
        Assert.Null(0L.ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_UInt64_0ReturnsNull()
    {
        Assert.Null(0UL.ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_IntPtr_0ReturnsNull()
    {
        Assert.Null(((IntPtr)0L).ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_UIntPtr_0ReturnsNull()
    {
        Assert.Null(((UIntPtr)0L).ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_Single_0ReturnsNull()
    {
        Assert.Null(0.0f.ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_Double_0ReturnsNull()
    {
        Assert.Null(0.0.ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_Decimal_0ReturnsNull()
    {
        Assert.Null(0m.ToNullIfZero());
    }


    //
    // Unchanged
    //

    [Fact]
    public void ToNullIfZero_Byte_42Returns42()
    {
        Byte value = 42;
        Assert.Equal(value, value.ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_SByte_42Returns42()
    {
        SByte value = 42;
        Assert.Equal(value, value.ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_Int16_42Returns42()
    {
        Int16 value = 42;
        Assert.Equal(value, value.ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_UInt16_42Returns42()
    {
        UInt16 value = 42;
        Assert.Equal(value, value.ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_Int32_42Returns42()
    {
        Int32 value = 42;
        Assert.Equal(value, value.ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_UInt32_42Returns42()
    {
        UInt32 value = 42;
        Assert.Equal(value, value.ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_Int64_42Returns42()
    {
        Int64 value = 42L;
        Assert.Equal(value, value.ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_UInt64_42Returns42()
    {
        UInt64 value = 42UL;
        Assert.Equal(value, value.ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_IntPtr_42Returns42()
    {
        IntPtr value = 42;
        Assert.Equal(value, value.ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_UIntPtr_42Returns42()
    {
        UIntPtr value = 42;
        Assert.Equal(value, value.ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_Single_42Returns42()
    {
        Single value = 42.0f;
        Assert.Equal(value, value.ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_Double_42Returns42()
    {
        Double value = 42.0;
        Assert.Equal(value, value.ToNullIfZero());
    }

    [Fact]
    public void ToNullIfZero_Decimal_42Returns42()
    {
        Decimal value = 42.0m;
        Assert.Equal(value, value.ToNullIfZero());
    }
}
