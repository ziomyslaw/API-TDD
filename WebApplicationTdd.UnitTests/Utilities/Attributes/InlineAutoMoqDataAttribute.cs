using AutoFixture.Xunit2;

namespace WebApplicationTdd.UnitTests.Utilities.Attributes;

public class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
{
    public InlineAutoMoqDataAttribute(params object[] objects) : base(new AutoMoqDataAttribute(), objects) { }
}