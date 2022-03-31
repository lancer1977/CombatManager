/*
 *  WeaponSpecialAbility.cs
 *
 *  Copyright (C) 2010-2012 Kyle Olson, kyle@kyleolson.com
 *
 *  This program is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU General Public License
 *  as published by the Free Software Foundation; either version 2
 *  of the License, or (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 * 
 *  You should have received a copy of the GNU General Public License
 *  along with this program; if not, write to the Free Software
 *  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 *
 */

 using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CombatManager
{
    public class WeaponSpecialAbility : INotifyPropertyChanged
    {



        public event PropertyChangedEventHandler PropertyChanged;

        private string _Minor;
        private string _Medium;
        private string _Major;
        private string _RangedMinor;
        private string _RangedMedium;
        private string _RangedMajor;
        private string _Name;
        private string _AltName;
        private int _BasePriceMod;
        private bool _Melee;
        private bool _Ranged;
        private string _Plus;
        private string _Text;

        private static List<WeaponSpecialAbility> abilities;
        static WeaponSpecialAbility()
        {

            Load();

        }

        static void Load()
        {
            abilities = XmlListLoader<WeaponSpecialAbility>.Load("WeaponSpecialAbility.xml");

        }

        public static List<WeaponSpecialAbility> SpecialAbilities
        {
            get
            {
                return new List<WeaponSpecialAbility>(abilities);
            }
        }

        public static List<WeaponSpecialAbility> RangedAbilities
        {
            get
            {
                List<WeaponSpecialAbility> list = new List<WeaponSpecialAbility>();

                foreach (WeaponSpecialAbility w in SpecialAbilities)
                {
                    if (w.Ranged)
                    {
                        list.Add(w);
                    }
                }

                return list;
            }
        }



        public static List<WeaponSpecialAbility> MeleeAbilities
        {
            get
            {
                List<WeaponSpecialAbility> list = new List<WeaponSpecialAbility>();

                foreach (WeaponSpecialAbility w in SpecialAbilities)
                {
                    if (w.Melee)
                    {
                        list.Add(w);
                    }
                }

                return list;
            }
        }


        public string Minor
        {
            get { return _Minor; }
            set
            {
                if (_Minor != value)
                {
                    _Minor = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Minor")); }
                }
            }
        }
        public string Medium
        {
            get { return _Medium; }
            set
            {
                if (_Medium != value)
                {
                    _Medium = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Medium")); }
                }
            }
        }
        public string Major
        {
            get { return _Major; }
            set
            {
                if (_Major != value)
                {
                    _Major = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Major")); }
                }
            }
        }
        public string RangedMinor
        {
            get { return _RangedMinor; }
            set
            {
                if (_RangedMinor != value)
                {
                    _RangedMinor = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("RangedMinor")); }
                }
            }
        }
        public string RangedMedium
        {
            get { return _RangedMedium; }
            set
            {
                if (_RangedMedium != value)
                {
                    _RangedMedium = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("RangedMedium")); }
                }
            }
        }
        public string RangedMajor
        {
            get { return _RangedMajor; }
            set
            {
                if (_RangedMajor != value)
                {
                    _RangedMajor = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("RangedMajor")); }
                }
            }
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
        public string AltName
        {
            get { return _AltName; }
            set
            {
                if (_AltName != value)
                {
                    _AltName = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("AltName")); }
                }
            }
        }
        public int BasePriceMod
        {
            get { return _BasePriceMod; }
            set
            {
                if (_BasePriceMod != value)
                {
                    _BasePriceMod = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("BasePriceMod")); }
                }
            }
        }
        public bool Melee
        {
            get { return _Melee; }
            set
            {
                if (_Melee != value)
                {
                    _Melee = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Melee")); }
                }
            }
        }
        public bool Ranged
        {
            get { return _Ranged; }
            set
            {
                if (_Ranged != value)
                {
                    _Ranged = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Ranged")); }
                }
            }
        }
        public string Plus
        {
            get { return _Plus; }
            set
            {
                if (_Plus != value)
                {
                    _Plus = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Plus")); }
                }
            }
        }

        public string Text
        {
            get { return _Text; }
            set
            {
                if (_Text != value)
                {
                    _Text = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Text")); }
                }
            }
        }
    }
}
