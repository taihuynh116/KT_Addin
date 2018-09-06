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

            Singleton.Instance.WPFData.BeamInputs.Add(new BeamInput()
            {
                Name = "BVH12",
                TopNumber = 3,
                BottomNumber = 2
            });
            Singleton.Instance.WPFData.BeamInputs.Add(new BeamInput()
            {
                Name = "BVH14",
                TopNumber = 2,
                BottomNumber = 5
            });

            Singleton.Instance.WPFData.InputForm.ShowDialog();
            if (!Singleton.Instance.WPFData.IsClosedOK) { return Result.Succeeded; }

            Singleton.Instance.RevitData.Transaction.Start();


            var abc = Singleton.Instance.WPFData.BeamInputs;


            Singleton.Instance.RevitData.Transaction.Commit();
            return Result.Succeeded;
        }
    }
}