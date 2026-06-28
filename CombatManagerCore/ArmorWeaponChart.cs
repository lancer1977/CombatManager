/*
 *  ArmorWeaponChart.cs
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
    public class ArmorWeaponChart : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _Weight;
        private string _Name;
        private string _Cost;
        private string _Materials;
        private string _Type;

        private static List<ArmorWeaponChart> _Chart;

        private static Dictionary<string, int> _TotalWeights;

        static ArmorWeaponChart()
        {
            try
            {
                _Chart = XmlListLoader<ArmorWeaponChart>.Load("ArmorWeaponChart.xml");

                _TotalWeights = new Dictionary<string, int>();

                foreach (ArmorWeaponChart chart in _Chart)
                {
                    int weight = int.Parse(chart._Weight);

                    if (!_TotalWeights.ContainsKey(chart._Type))
                    {
                        _TotalWeights[chart._Type] = weight;
                    }
                    else
                    {
                        _TotalWeights[chart.Type] = _TotalWeights[chart.Type] + weight;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public string Weight
        {
            get { return _Weight; }
            set
            {
                if (_Weight != value)
                {
                    _Weight = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Weight")); }
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
        public string Cost
        {
            get { return _Cost; }
            set
            {
                if (_Cost != value)
                {
                    _Cost = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Cost")); }
                }
            }
        }
        public string Materials
        {
            get { return _Materials; }
            set
            {
                if (_Materials != value)
                {
                    _Materials = value;
                    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("Materials")); }
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

        public static List<ArmorWeaponChart> Chart
        {
            get
            {
                return _Chart;
            }
        }

        public static Dictionary<string, int> TotalWeights
        {
            get
            {
                return _TotalWeights;
            }
        }

    }
}
