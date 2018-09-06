using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Test1;

namespace Single
{
    public class WPFData : INotifyPropertyChanged
    {
        #region Variables
        private InputForm inputForm;
        private InputColumnForm inputColumnForm;
        private string prefix = "";
        private string direction = "";
        private string name ="";
        private string type = "";
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
        public InputColumnForm InputColumnForm
        {
            get
            {
                if (inputColumnForm == null) inputColumnForm = new InputColumnForm();
                return inputColumnForm;
            }
        }
        public string Prefix
        {
            get
            {
                return prefix;
            }
            set
            {
                if (prefix == value) return;
                prefix = value; OnPropertyChanged();
            }
        }
        public string Direction
        {
            get
            {
                return direction;
            }
            set
            {
                if (direction == value) return;
                direction = value; OnPropertyChanged();
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name == value) return;
                name = value; OnPropertyChanged();
            }
        }
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                if (type == value) return;
                type = value; OnPropertyChanged();
            }
        }
        public string Value
        {
            get
            {
                return $"{Prefix}{Direction}{Name}";
            }
        }
        public string WallValue
        {
            get
            {
                return $"{Prefix}-{Name}";
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
