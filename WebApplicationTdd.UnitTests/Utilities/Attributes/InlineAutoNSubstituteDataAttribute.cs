using AutoFixture.Xunit2;

namespace WebApplicationTdd.UnitTests.Utilities.Attributes;

public class InlineAutoNSubstituteDataAttribute : CompositeDataAttribute
{
    public InlineAutoNSubstituteDataAttribute(params object[] values) : base(new InlineDataAttribute(values), new AutoNSubstituteDataAttribute()) { }
}