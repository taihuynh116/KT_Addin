using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI.Selection;
using Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    public class StirrupSelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem is Rebar)
            {
                Rebar rb = elem as Rebar;
                if (rb.LookupParameter("Style").AsInteger() == 1) return true;
            }
            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            throw new NotImplementedException();
        }
    }
    public class StirrupAndLockheadRebarSelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem is Rebar)
            {
                Rebar rb = elem as Rebar;
                if (rb.LookupParameter("Style").AsInteger() == 1 || rb.LookupParameter("Type").AsString() == "Lockhead") return true;
            }
            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            throw new NotImplementedException();
        }
    }
    public class LockheadRebarSelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem is Rebar)
            {
                Rebar rb = elem as Rebar;
                if (rb.LookupParameter("Type").AsString() == "Lockhead") return true;
            }
            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            throw new NotImplementedException();
        }
    }
    public class AddinRebarSelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem is Rebar)
            {
                Rebar rb = elem as Rebar;
                if (rb.LookupParameter("Comments").AsString() == "add-in")
                {
                    foreach (string level in ConstantValue.Levels)
                    {
                        if (rb.LookupParameter("Level").AsString() == level) return true;
                    }
                }
            }
            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            throw new NotImplementedException();
        }
    }
    public class BeamSelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem.Category.Id.IntegerValue == (int)BuiltInCategory.OST_StructuralFraming) return true;
            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}
