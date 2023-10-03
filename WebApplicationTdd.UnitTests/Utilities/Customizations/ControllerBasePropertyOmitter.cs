using AutoFixture.Kernel;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTdd.UnitTests.Utilities.Customizations;

public class ControllerBasePropertyOmitter : Omitter
{
    public ControllerBasePropertyOmitter() : base(new OrRequestSpecification(GetPropertySpecifications()))
    {
    }

    private static IEnumerable<IRequestSpecification> GetPropertySpecifications() =>
        typeof(ControllerBase)
            .GetProperties()
            .Where(x => x.CanWrite)
            .Select(x => new PropertySpecification(x.PropertyType, x.Name));
}