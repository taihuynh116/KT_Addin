using Autodesk.Revit.DB.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeamAddin
{
    public class BeamInput
    {
        public string Name { get; set; }
        public int TopNumber { get; set; }
        public RebarBarType TopBarType { get; set; }
        public int BottomNumber { get; set; }
        public RebarBarType BottomBarType { get; set; }
    }
}
