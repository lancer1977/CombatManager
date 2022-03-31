using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace CombatManager
{
    public class ActiveResource : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private string _Name;
        private string _Uses;
        private string _Type;
        private int _Min;
        private int _Max;
        private int _Current;

        public ActiveResource()
        {
        }

        public ActiveResource(ActiveResource resource)
        {
            _Name = resource.Name;
            _Uses = resource.Uses;
            _Type = resource._Type;
            _Min = resource._Min;
            _Max = resource._Max;
            _Current = resource.Current;
        }

        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Name")); }
                }
            }
        }
        public string Uses
        {
            get { return _Uses; }
            set
            {
                if (_Uses != value)
                {
                    _Uses = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Uses")); }
                }
            }
        }
        public string Type
        {
            get { return _Type; }
            set
            {
                if (_Type != value)
                {
                    _Type = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Type")); }
                }
            }
        }
        public int Min
        {
            get { return _Min; }
            set
            {
                if (_Min != value)
                {
                    _Min = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Min")); }
                }
            }
        }
        public int Max
        {
            get { return _Max; }
            set
            {
                if (_Max != value)
                {
                    _Max = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Max")); }
                }
            }
        }
        public int Current
        {
            get { return _Current; }
            set
            {
                if (_Current != value)
                {
                    _Current = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Current")); }
                }
            }
        }

    }
}
