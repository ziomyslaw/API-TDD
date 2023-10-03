using AutoFixture;
using AutoFixture.AutoMoq;

namespace WebApplicationTdd.UnitTests.Utilities.Customizations;

public class DefaultMoqCustomization : CompositeCustomization
{
    public DefaultMoqCustomization() : base(
        new ClaimCustomization(),
        new AutoMoqCustomization
        {
            ConfigureMembers = true,
        },
        new AspNetCustomization())
    { }
}