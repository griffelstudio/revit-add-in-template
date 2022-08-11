using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Autodesk.Revit.UI
{
    /// <summary>
    /// Inherit this class to quickly start implementing Revit commands.
    /// Revit document, UI application and UI document are already there.
    /// </summary>
    public abstract class BaseExternalCommand : IExternalCommand
    {
        /// <summary>
        /// Revit UI Application.
        /// </summary>
        public UIApplication UIApp { get; private set; }

        /// <summary>
        /// Revit UI Document from which command was launched.
        /// </summary>
        public UIDocument UIDoc { get; private set; }

        /// <summary>
        /// Revit project from which command was launched.
        /// </summary>
        public Document Doc { get; private set; }

        /// <summary>
        /// Elements which were selected before the command has started.
        /// </summary>
        public ICollection<Element> SelectedElements { get; protected set; }

        public virtual Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApp = commandData?.Application;
            UIDoc = UIApp?.ActiveUIDocument;
            Doc = UIDoc?.Document;

            SelectedElements = new Collection<Element>();
            if (Doc != null)
            {
                foreach (var id in UIDoc.Selection.GetElementIds())
                {
                    SelectedElements.Add(Doc.GetElement(id));
                }
            }

            try
            {
                return Execute();
            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }
        }

        /// <summary>
        /// Override this method and do all command related stuff there.
        /// </summary>
        /// <returns>Result of command run.</returns>
        public abstract Result Execute();
    }
}
