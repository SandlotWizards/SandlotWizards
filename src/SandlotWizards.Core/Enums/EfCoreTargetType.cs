namespace SandlotWizards.Core.Enums
{
    /// <summary>
    /// Indicates which EF Core migration target to resolve: the application database or MobileFrame database.
    /// </summary>
    public enum EfCoreTargetType
    {
        /// <summary>
        /// Targets the solution's main application database.
        /// </summary>
        Application,

        /// <summary>
        /// Targets the solution's MobileFrame database.
        /// </summary>
        MobileFrame
    }
}
