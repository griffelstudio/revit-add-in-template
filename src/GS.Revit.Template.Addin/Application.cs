using Autodesk.Revit.UI;

namespace GS.Revit.Template.Addin
{
    public sealed class Application : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
