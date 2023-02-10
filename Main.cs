using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3Task81
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            using (var ts = new Transaction(doc, "Export ifc"))
            {
                ts.Start();

                View3D view3D = new FilteredElementCollector(doc)
                    .OfClass(typeof(View3D))
                    .Cast<View3D>()
                    .FirstOrDefault(v => v.ViewType == ViewType.ThreeD && v.Name.Equals("{3D}"));

                var ifcOption = new IFCExportOptions();

                doc.Export(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "export.ifc", ifcOption);

                ts.Commit();
            }

                return Result.Succeeded;
        }
    }
}
