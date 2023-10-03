using AutoFixture;
using AutoFixture.Xunit2;
using WebApplicationTdd.UnitTests.Utilities.Customizations;

namespace WebApplicationTdd.UnitTests.Utilities.Attributes;

public class AutoMoqDataAttribute : AutoDataAttribute
{
    public AutoMoqDataAttribute() : base(() => new Fixture().Customize(new DefaultMoqCustomization())) { }
}