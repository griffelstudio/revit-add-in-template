using System;

namespace GS.Revit.Common.Licensing
{

    [Serializable]
    internal class EntitlementResponse
    {
        public string UserID { get; set; }

        public string AppID { get; set; }

        public bool IsValid { get; set; }

        public string Message { get; set; }
    }
}
