using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace WebApplicationTdd.UnitTests.Utilities.Customizations;

public class DefaultNSubstituteCustomization : CompositeCustomization
{
    public DefaultNSubstituteCustomization() : base(
        new AutoNSubstituteCustomization
        {
            ConfigureMembers = true,
        },
        new ClaimCustomization(),
        new AspNetCustomization(),
        new SupportMutableValueTypesCustomization()) 
    { }
}