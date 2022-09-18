using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace GS.Revit.Template.API
{
    /// <summary>
    /// Revit creates an instance of this class.
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class TestCommand : BaseExternalCommand
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        /// <returns>Command execution result.</returns>
        public override Result Execute()
        {
            return Result.Succeeded;
        }
    }
}
