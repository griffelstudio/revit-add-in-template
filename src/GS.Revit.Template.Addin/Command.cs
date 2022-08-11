using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace GS.Revit.GltfExporter.API
{
    [Transaction(TransactionMode.Manual)]
    public class TestCommand : BaseExternalCommand
    {
        public override Result Execute()
        {
            return Result.Succeeded;
        }
    }
}
