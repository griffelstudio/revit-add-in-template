using Autodesk.Revit.UI;

namespace GS.Revit.Template.Addin
{
    /// <summary>
    /// Revit creates an instance of this class.
    /// </summary>
    public sealed class Application : IExternalApplication
    {
        /// <summary>
        /// Revit calls this method on start up.
        /// </summary>
        /// <param name="application">Revit application.</param>
        /// <returns>Add-in start up result.</returns>
        public Result OnStartup(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        /// <summary>
        /// Revit calls this method on shut down.
        /// </summary>
        /// <param name="application">Revit application.</param>
        /// <returns>Add-in shut down result.</returns>
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
