using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Single
{
    public class Singleton
    {
        #region Variables
        private RevitData revitData;
        private WPFData wpfData;
        private ModelData modelData;
        #endregion

        #region Properties
        public static Singleton Instance { get; set; }
        public RevitData RevitData
        {
            get
            {
                if (revitData == null) revitData = new RevitData();
                return revitData;
            }
        }
        public WPFData WPFData
        {
            get
            {
                if (wpfData == null) wpfData = new WPFData();
                return wpfData;
            }
        }
        public ModelData ModelData
        {
            get
            {
                if (modelData == null) modelData = new ModelData();
                return modelData;
            }
        }
        #endregion
    }
}
