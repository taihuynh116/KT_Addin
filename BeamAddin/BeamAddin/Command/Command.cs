using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeamAddin
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Singleton.Instance = new Singleton();
            Singleton.Instance.RevitData.UIApplication = commandData.Application;

            Singleton.Instance.RevitData.Transaction.Start();

            Singleton.Instance.WPFData.InputForm.ShowDialog();

            Singleton.Instance.RevitData.Transaction.Commit();
            return Result.Succeeded;
        }
    }
}
