using AutoFixture;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApplicationTdd.UnitTests.Utilities.Customizations;

public class AspNetCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());
        fixture.Customizations.Add(new ControllerBasePropertyOmitter());
    }
}