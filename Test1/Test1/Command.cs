using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Singleton.Instance = new Singleton();
            Singleton.Instance.RevitData.UIApplication = commandData.Application;

            Singleton.Instance.RevitData.Transaction.Start();

            Singleton.Instance.WPFData.InputForm.Show();
            while (true)
            {
                try
                {
                    Element elem = Singleton.Instance.RevitData.Document.GetElement(Singleton.Instance.RevitData.Selection.PickObject(ObjectType.Element, new BeamSelectionFilter()));
                    elem.LookupParameter("ElementName").Set(Singleton.Instance.WPFData.Value);

                    Singleton.Instance.RevitData.Transaction.Commit();
                    Singleton.Instance.RevitData.Transaction.Start();
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException)
                {
                    Singleton.Instance.WPFData.InputForm.Close();
                    break;
                }
            }

            Singleton.Instance.RevitData.Transaction.Commit();
            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    public class CreateWall : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Singleton.Instance = new Singleton();
            Singleton.Instance.RevitData.UIApplication = commandData.Application;

            Singleton.Instance.RevitData.Transaction.Start();

            List<string> sLevels = new List<string> { "L7", "L8", "L9", "L10", "L11", "L12", "L13" };

            Singleton.Instance.WPFData.InputColumnForm.Show();
            while (true)
            {
                try
                {
                    Curve curve = Line.CreateBound(Singleton.Instance.RevitData.Selection.PickPoint(), Singleton.Instance.RevitData.Selection.PickPoint());
                    for (int i = 0; i < sLevels.Count-1; i++)
                    {
                        Level level1 = Singleton.Instance.RevitData.Levels.Where(x => x.Name == sLevels[i]).First();
                        Level level2 = Singleton.Instance.RevitData.Levels.Where(x => x.Name == sLevels[i+1]).First();

                        Wall wall = Wall.Create(Singleton.Instance.RevitData.Document, curve, level1.Id, true);
                        WallType wtype = Singleton.Instance.RevitData.WallTypes.Where(x => x.Name == Singleton.Instance.WPFData.Type).First();

                        wall.WallType = wtype;
                        wall.LookupParameter("ElementName").Set(Singleton.Instance.WPFData.WallValue);
                        wall.LookupParameter("Top Constraint").Set(level2.Id);
                    }
                    Singleton.Instance.RevitData.Transaction.Commit();
                    Singleton.Instance.RevitData.Transaction.Start();
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException)
                {
                    Singleton.Instance.WPFData.InputColumnForm.Close();
                    break;
                }
            }

            Singleton.Instance.RevitData.Transaction.Commit();
            return Result.Succeeded;
        }
    }
}
