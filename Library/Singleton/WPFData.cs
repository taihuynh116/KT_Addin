using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using BeamAddin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Single
{
    public class WPFData : INotifyPropertyChanged
    {
        #region Variables
        private InputForm inputForm;
        private double nameWidth = 50;
        private double numTopLayWidth = 50;
        private double typeTopLayWidth = 50;
        private double numBotLayWidth = 50;
        private double typeBotLayWidth = 50;
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
        public double NameWidth
        {
            get
            {
                return nameWidth;
            }
        }
        public double NumberTopLayerWidth
        {
            get
            {
                return numTopLayWidth;
            }
        }
        public double TypeTopLayerWidth
        {
            get
            {
                return typeTopLayWidth;
            }
        }
        public double NumberBottomLayerWidth
        {
            get
            {
                return numBotLayWidth;
            }
        }
        public double TypeBottomLayerWidth
        {
            get
            {
                return typeBotLayWidth;
            }
        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
