using System.Security.Claims;
using AutoFixture;

namespace WebApplicationTdd.UnitTests.Utilities.Customizations;

public class ClaimCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Register(() => new Claim("myRoleClaim", "admin"));
    }
}