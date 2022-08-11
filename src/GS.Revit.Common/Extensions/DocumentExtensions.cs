using System.IO;

namespace Autodesk.Revit.DB
{
    public static class DocumentExtensions
    {
        public static bool IsMetric(this Document doc) => doc.DisplayUnitSystem == DisplayUnit.METRIC;
    }
}
