using AutoFixture;
using AutoFixture.Xunit2;
using WebApplicationTdd.UnitTests.Utilities.Customizations;

namespace WebApplicationTdd.UnitTests.Utilities.Attributes;

public class AutoNSubstituteDataAttribute : AutoDataAttribute
{
    public AutoNSubstituteDataAttribute() : base(() => new Fixture().Customize(new DefaultNSubstituteCustomization())) { }
}