using System;

namespace SandlotWizards.Core.Interfaces.Wrapper
{
    public interface IDateTime
    {
        DateTime Now { get; }
        DateTime Null { get; }
        DateTime UtcNow { get; }
    }
}