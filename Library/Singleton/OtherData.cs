using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using BeamAddin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Single
{
    public class OtherData
    {
        #region Variables
        private InputForm inputForm;
        #endregion

        #region Properties
        public InputForm InputForm
        {
            get
            {
                if (inputForm == null) inputForm = new InputForm();
                return inputForm;
            }
        }
        #endregion
    }
}
