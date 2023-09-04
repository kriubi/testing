using FluentAssertions;

namespace testing.Sku;

public sealed class SkuTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("     ")]
    public void Create_Should_ReturnNull_WhenValueIsNullOrWhiteSpace(string? value)
    {
        var sku = Sku.Create(value);

        Assert.Null(sku);

        // FluentAssertions
        sku.Should().BeNull();
    }

    [Theory]
    [InlineData("---------------")]
    public void Create_Should_ThrowArgumentException_WhenValueIsDashes(string? value)
    {
        Action act = () => Sku.Create(value);

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage(nameof(value));
    }

    [Theory]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("123")]
    [InlineData("1234")]
    [InlineData("12345")]
    [InlineData("123456")]
    [InlineData("1234567")]
    [InlineData("12345678")]
    [InlineData("123456789")]
    [InlineData("1234567890")]
    [InlineData("12345678901")]
    [InlineData("123456789012")]
    [InlineData("1234567890123")]
    [InlineData("12345678901234")]
    public void Create_Should_ReturnNull_WhenValueLengthIsInvalid(string value)
    {
        var sku = Sku.Create(value);

        Assert.Null(sku);

        // FluentAssertions
        sku.Should().BeNull();
    }

    [Theory]
    [ClassData(typeof(Skus))]
    public void Create_Should_ReturnASku_WhenValueLengthIsValid(string value)
    {
        var sku = Sku.Create(value);

        Assert.NotNull(sku);
        Assert.IsType<Sku>(sku);

        // FluentAssertions
        sku.Should().NotBeNull();
        sku.Should().BeOfType<Sku>();
    }

    public class Skus : TheoryData<string>
    {
        public Skus()
        {
            Add("123456789012345");
            Add("ABCDEFGHIJKLMNO");
            Add("'!@#$%¨&*()_+=-");
        }
    }
}
