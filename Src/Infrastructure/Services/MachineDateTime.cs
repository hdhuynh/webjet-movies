using Webjet.Application.Common.Interfaces;

namespace Webjet.Infrastructure.Services;

public class MachineDateTime : IDateTime
{
    public DateTime Now => DateTime.Now;

    public int CurrentYear => DateTime.Now.Year;
}
