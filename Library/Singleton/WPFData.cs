using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using BeamAddin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<BeamInput> beamInputs;
        private bool isClosedOK;
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
        public ObservableCollection<BeamInput> BeamInputs
        {
            get
            {
                if (beamInputs == null) beamInputs = new ObservableCollection<BeamInput>();
                return beamInputs;
            }
            set
            {
                beamInputs = value;
                OnPropertyChanged();
            }
        }
        public bool IsClosedOK
        {
            get
            {
                return isClosedOK;
            }
            set
            {
                if (isClosedOK == value) return;
                isClosedOK = value;
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
