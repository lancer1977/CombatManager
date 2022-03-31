/*
 *  MagicItem.cs
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
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Xml;
using System.Globalization;
using System.IO;
using System.Xml.Linq;

namespace CombatManager
{


    public enum ItemLevel
    {
        Minor,
        Medium,
        Major
    }

    public class MagicItem : BaseDBClass
    {

        private string _Name;
        private string _Aura;
        private int _CL;
        private string _Slot;
        private string _Price;
        private string _Weight;
        private string _Description;
        private string _Requirements;
        private string _Cost;
        private string _Group;
        private string _Source;
        private string _FullText;
        private string _Destruction;
        private string _MinorArtifactFlag;
        private string _MajorArtifactFlag;
        private string _Abjuration;
        private string _Conjuration;
        private string _Divination;
        private string _Enchantment;
        private string _Evocation;
        private string _Necromancy;
        private string _Transmutation;
        private string _AuraStrength;
        private string _WeightValue;
        private string _PriceValue;
        private string _CostValue; 
        private string _AL;
        private string _Int;
        private string _Wis;
        private string _Cha;
        private string _Ego;
        private string _Communication;
        private string _Senses;
        private string _Powers;
        private string _MagicItems;
        private string _DescHTML;
        private string _Mythic;
        private string _LegendaryWeapon;




        private static Dictionary<string, MagicItem> itemMap;
        private static SortedDictionary<string, string> groups;
        private static SortedDictionary<int, int> cls;

        private static Dictionary<int, MagicItem> _MagicItemsByDetailsID;

        private static bool _MagicItemsLoaded;

        public static void LoadMagicItems()
        {

            List<MagicItem> set = LoadMagicItemsFromXml("MagicItemsShort.xml");
            _MagicItemsByDetailsID = new Dictionary<int, MagicItem>();


            groups = new SortedDictionary<string, string>();
            cls = new SortedDictionary<int, int>();
            itemMap = new Dictionary<string, MagicItem>(new InsensitiveEqualityCompararer());

            foreach (MagicItem item in set)
            {
                itemMap[item.Name] = item;

                groups[item.Group] = item.Group;
                cls[item.CL] = item.CL;

                _MagicItemsByDetailsID[item._DetailsID] = item;
            }

            _MagicItemsLoaded = true;
        }

        public static bool MagicItemsLoaded
        {
            get
            {
                return _MagicItemsLoaded;
            }
        }

        public static List<MagicItem> LoadMagicItemsFromXml(string filename)
        {
            XElement last = null;  
            try
            {

                List<MagicItem> magicItems = new List<MagicItem>();
    #if ANDROID
                XDocument doc = XDocument.Load(new StreamReader(CoreContext.Context.Assets.Open(filename)));
    #elif MONO

                XDocument doc = XDocument.Load(Path.Combine(XmlLoader<MagicItem>.AssemblyDir, filename));
                         

    #else
                XDocument doc = XDocument.Load(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filename));
    #endif
               
                foreach (var v in doc.Descendants("MagicItem"))
                {
                    last = v;
                    MagicItem m = new MagicItem();

                    m._DetailsID = v.ElementIntValue("id");

                    Debug.Assert(m._DetailsID != 0);

                    m._Name = v.ElementValue("Name");
                    if (v.ElementValue("CL") != "-")
                    {
                        m._CL = v.ElementIntValue("CL");
                    }
                    else
                    {
                        m._CL = 0;
                    }
                    m._Group = v.ElementValue("Group");
                    m._Source = v.ElementValue("Source");
                    m._MagicItems = v.ElementValue("MagicItems");

                    magicItems.Add(m);
                }
                return magicItems;
            }
            catch (Exception)
            {
                throw;
            }

        
        }

        public static List<string> DetailsFields
        {
            get
            {
                return new List<string>() {
                    "Aura",
                    "Slot",
                    "Price",
                    "Weight",
                    "Description",
                    "Requirements",
                    "Cost",
                    "FullText",
                    "Destruction",
                    "MinorArtifactFlag",
                    "MajorArtifactFlag",
                    "Abjuration",
                    "Conjuration",
                    "Divination",
                    "Enchantment",
                    "Evocation",
                    "Necromancy",
                    "Transmutation",
                    "AuraStrength",
                    "WeightValue",
                    "PriceValue",
                    "CostValue", 
                    "AL",
                    "Int",
                    "Wis",
                    "Cha",
                    "Ego",
                    "Communication",
                    "Senses",
                    "Powers",
                    "MagicItems",
                    "DescHTML",
                    "Mythic",
                    "LegendaryWeapon",};
            }
        }

        public MagicItem()
        {
        }

        public MagicItem(MagicItem m)
        {
            CopyFrom(m);
        }

        public object Clone()
        {
            return new MagicItem(this);
        }

        public void CopyFrom(MagicItem magicItem)
        {
            if (magicItem == null)
            {
                return;
            }
            _DetailsID = magicItem._DetailsID;
            _Name = magicItem._Name;
            _Aura = magicItem._Aura;
            _CL = magicItem._CL;
            _Slot = magicItem._Slot;
            _Price = magicItem._Price;
            _Weight = magicItem._Weight;
            _Description = magicItem._Description;
            _Requirements = magicItem._Requirements;
            _Cost = magicItem._Cost;
            _Group = magicItem._Group;
            _Source = magicItem._Source;
            _FullText = magicItem._FullText;
            _Destruction = magicItem._Destruction;
            _MinorArtifactFlag = magicItem._MinorArtifactFlag;
            _MajorArtifactFlag = magicItem._MajorArtifactFlag;
            _Abjuration = magicItem._Abjuration;
            _Conjuration = magicItem._Conjuration;
            _Divination = magicItem._Divination;
            _Enchantment = magicItem._Enchantment;
            _Evocation = magicItem._Evocation;
            _Necromancy = magicItem._Necromancy;
            _Transmutation = magicItem._Transmutation;
            _AuraStrength = magicItem._AuraStrength;
            _WeightValue = magicItem._WeightValue;
            _PriceValue = magicItem._PriceValue;
            _CostValue = magicItem._CostValue;
            _AL = magicItem._AL;
            _Int = magicItem._Int;
            _Wis = magicItem._Wis;
            _Cha = magicItem._Cha;
            _Ego = magicItem._Ego;
            _Communication = magicItem._Communication;
            _Senses = magicItem._Senses;
            _Powers = magicItem._Powers;
            _MagicItems = magicItem._MagicItems;
            _DescHTML = magicItem._DescHTML;
            _Mythic = magicItem._Mythic;
            _LegendaryWeapon = magicItem._LegendaryWeapon;
            _DBLoaderID = magicItem._DBLoaderID;
        }

        void UpdateFromDetailsDB()
        {
            if (_DetailsID != 0)
            {
                //perform updating from DB
                var list = DetailsDB.LoadDetails(_DetailsID.ToString(), "MagicItems", DetailsFields);

                _Aura = list["Aura"];
                _Slot = list["Slot"];
                _Price = list["Price"];
                _Weight = list["Weight"];
                _Description = list["Description"];
                _Requirements = list["Requirements"];
                _Cost = list["Cost"];
                _FullText = list["FullText"];
                _Destruction = list["Destruction"];
                _MinorArtifactFlag = list["MinorArtifactFlag"];
                _MajorArtifactFlag = list["MajorArtifactFlag"];
                _Abjuration = list["Abjuration"];
                _Conjuration = list["Conjuration"];
                _Divination = list["Divination"];
                _Enchantment = list["Enchantment"];
                _Evocation = list["Evocation"];
                _Necromancy = list["Necromancy"];
                _Transmutation = list["Transmutation"];
                _AuraStrength = list["AuraStrength"];
                _WeightValue = list["WeightValue"];
                _PriceValue = list["PriceValue"];
                _CostValue = list["CostValue"]; 
                _AL = list["AL"];
                _Int = list["Int"];
                _Wis = list["Wis"];
                _Cha = list["Cha"];
                _Ego = list["Ego"];
                _Communication = list["Communication"];
                _Senses = list["Senses"];
                _Powers = list["Powers"];
                _MagicItems = list["MagicItems"];
                _DescHTML = list["DescHTML"];
                _Mythic = list["Mythic"];
                _LegendaryWeapon = list["LegendaryWeapon"];
                _DBLoaderID = 0;

                _DetailsID = 0;
            }
        }

        public static Dictionary<string, MagicItem> Items
        {
            get
            {
                if (itemMap == null)
                {
                    LoadMagicItems();
                }
                return itemMap;
            }
        }


        public static MagicItem ByDetailsID(int id)
        {
            
            if (_MagicItemsByDetailsID == null)
            {
                LoadMagicItems();
            }
            MagicItem m;
            _MagicItemsByDetailsID.TryGetValue(id, out m);
            return m;
        }

        public static MagicItem ByDBLoaderID(int id)
        {
            return null;
        }

        public static MagicItem ByID(bool custom, int id)
        {
            if (custom)
            {
                return ByDBLoaderID(id); ;
            }
            else
            {
                return ByDetailsID(id);
            }
        }

        public static bool TryByID(bool custom, int id, out MagicItem s)
        {
            s = ByID(custom, id);
            return s != null;
        }

        public static ICollection<string> Groups
        {
            get
            {
                if (itemMap == null)
                {
                    LoadMagicItems();
                }
                return groups.Values;
            }
        }
        public static ICollection<int> CLs
        {
            get
            {
                if (itemMap == null)
                {
                    LoadMagicItems();
                }
                return cls.Values;
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
                    Notify("Name");                }
            }
        }
        public string Aura
        {
            get { UpdateFromDetailsDB(); 
                return _Aura; }
            set
            {
                if (_Aura != value)
                {
                    _Aura = value;
                    Notify("Aura");                }
            }
        }
        public int CL
        {
            get { return _CL; }
            set
            {
                if (_CL != value)
                {
                    _CL = value;
                    Notify("CL");                }
            }
        }
        public string Slot
        {
            get
            {
                UpdateFromDetailsDB();
                return _Slot;
            }
            set
            {
                if (_Slot != value)
                {
                    _Slot = value;
                    Notify("Slot");                }
            }
        }
        public string Price
        {
            get
            {
                UpdateFromDetailsDB();
                return _Price;
            }
            set
            {
                if (_Price != value)
                {
                    _Price = value;
                    Notify("Price");                }
            }
        }
        public string Weight
        {
            get
            {
                UpdateFromDetailsDB();
                return _Weight;
            }
            set
            {
                if (_Weight != value)
                {
                    _Weight = value;
                    Notify("Weight");                }
            }
        }
        public string Description
        {
            get {
                UpdateFromDetailsDB();

                return _Description; 
            }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    Notify("Description");                }
            }
        }
        public string Requirements
        {
            get
            {
                UpdateFromDetailsDB();

                return _Requirements;
            }
            set
            {
                if (_Requirements != value)
                {
                    _Requirements = value;
                    Notify("Requirements");                }
            }
        }
        public string Cost
        {
            get
            {
                UpdateFromDetailsDB();
                return _Cost;
            }
            set
            {
                if (_Cost != value)
                {
                    _Cost = value;
                    Notify("Cost");                }
            }
        }
        public string Group
        {
            get { return _Group; }
            set
            {
                if (_Group != value)
                {
                    _Group = value;
                    Notify("Group");                }
            }
        }
        public string Source
        {
            get { return _Source; }
            set
            {
                if (_Source != value)
                {
                    _Source = value;
                    Notify("Source");                }
            }
        }
        public string FullText
        {
            get
            {
                UpdateFromDetailsDB();

                return _FullText;
            }
            set
            {
                if (_FullText != value)
                {
                    _FullText = value;
                    Notify("FullText");                }
            }
        }
        public string Destruction
        {
            get
            {
                UpdateFromDetailsDB();

                return _Destruction;
            }
            set
            {
                if (_Destruction != value)
                {
                    _Destruction = value;
                    Notify("Destruction");                }
            }
        }
        public string MinorArtifactFlag
        {
            get
            {
                UpdateFromDetailsDB();
                return _MinorArtifactFlag;
            }
            set
            {
                if (_MinorArtifactFlag != value)
                {
                    _MinorArtifactFlag = value;
                    Notify("MinorArtifactFlag");                }
            }
        }
        public string MajorArtifactFlag
        {
            get
            {
                UpdateFromDetailsDB();
                return _MajorArtifactFlag;
            }
            set
            {
                if (_MajorArtifactFlag != value)
                {
                    _MajorArtifactFlag = value;
                    Notify("MajorArtifactFlag");                }
            }
        }
        public string Abjuration
        {
            get
            {
                UpdateFromDetailsDB();
                return _Abjuration;
            }
            set
            {
                if (_Abjuration != value)
                {
                    _Abjuration = value;
                    Notify("Abjuration");                }
            }
        }
        public string Conjuration
        {
            get
            {
                UpdateFromDetailsDB();
                return _Conjuration;
            }
            set
            {
                if (_Conjuration != value)
                {
                    _Conjuration = value;
                    Notify("Conjuration");                }
            }
        }
        public string Divination
        {
            get
            {
                UpdateFromDetailsDB();
                return _Divination;
            }
            set
            {
                if (_Divination != value)
                {
                    _Divination = value;
                    Notify("Divination");                }
            }
        }
        public string Enchantment
        {
            get
            {
                UpdateFromDetailsDB();
                return _Enchantment;
            }
            set
            {
                if (_Enchantment != value)
                {
                    _Enchantment = value;
                    Notify("Enchantment");                }
            }
        }
        public string Evocation
        {
            get
            {
                UpdateFromDetailsDB();
                return _Evocation;
            }
            set
            {
                if (_Evocation != value)
                {
                    _Evocation = value;
                    Notify("Evocation");                }
            }
        }
        public string Necromancy
        {
            get
            {
                UpdateFromDetailsDB();
                return _Necromancy;
            }
            set
            {
                if (_Necromancy != value)
                {
                    _Necromancy = value;
                    Notify("Necromancy");                }
            }
        }
        public string Transmutation
        {
            get
            {
                UpdateFromDetailsDB();
                return _Transmutation;
            }
            set
            {
                if (_Transmutation != value)
                {
                    _Transmutation = value;
                    Notify("Transmutation");                }
            }
        }
        public string AuraStrength
        {
            get
            {
                UpdateFromDetailsDB();
                return _AuraStrength;
            }
            set
            {
                if (_AuraStrength != value)
                {
                    _AuraStrength = value;
                    Notify("AuraStrength");                }
            }
        }
        public string WeightValue
        {
            get
            {
                UpdateFromDetailsDB();
                return _WeightValue;
            }
            set
            {
                if (_WeightValue != value)
                {
                    _WeightValue = value;
                    Notify("WeightValue");                }
            }
        }
        public string PriceValue
        {
            get
            {
                UpdateFromDetailsDB();
                return _PriceValue;
            }
            set
            {
                if (_PriceValue != value)
                {
                    _PriceValue = value;
                    Notify("PriceValue");                }
            }
        }
        public string CostValue
        {
            get
            {
                UpdateFromDetailsDB();
                return _CostValue;
            }
            set
            {
                if (_CostValue != value)
                {
                    _CostValue = value;
                    Notify("CostValue");                }
            }
        }
        public string AL
        {
            get
            {
                UpdateFromDetailsDB();
                return _AL;
            }
            set
            {
                if (_AL != value)
                {
                    _AL = value;
                    Notify("AL");                }
            }
        }
        public string Int
        {
            get
            {
                UpdateFromDetailsDB();
                return _Int;
            }
            set
            {
                if (_Int != value)
                {
                    _Int = value;
                    Notify("Int");                }
            }
        }
        public string Wis
        {
            get
            {
                UpdateFromDetailsDB();
                return _Wis;
            }
            set
            {
                if (_Wis != value)
                {
                    _Wis = value;
                    Notify("Wis");                }
            }
        }
        public string Cha
        {
            get
            {
                UpdateFromDetailsDB();
                return _Cha;
            }
            set
            {
                if (_Cha != value)
                {
                    _Cha = value;
                    Notify("Cha");                }
            }
        }
        public string Ego
        {
            get
            {
                UpdateFromDetailsDB();
                return _Ego;
            }
            set
            {
                if (_Ego != value)
                {
                    _Ego = value;
                    Notify("Ego");                }
            }
        }
        public string Communication
        {
            get
            {
                UpdateFromDetailsDB();
                return _Communication;
            }
            set
            {
                if (_Communication != value)
                {
                    _Communication = value;
                    Notify("Communication");                }
            }
        }
        public string Senses
        {
            get
            {
                UpdateFromDetailsDB();
                return _Senses;
            }
            set
            {
                if (_Senses != value)
                {
                    _Senses = value;
                    Notify("Senses");                }
            }
        }
        public string Powers
        {
            get
            {
                UpdateFromDetailsDB();
                return _Powers;
            }
            set
            {
                if (_Powers != value)
                {
                    _Powers = value;
                    Notify("Powers");                }
            }
        }

        public string MagicItems
        {
            get { return _MagicItems; }
            set
            {
                UpdateFromDetailsDB();
                if (_MagicItems != value && value != "NULL")
                {
                    _MagicItems = value;
                    Notify("MagicItem");                }
            }
        }

        public string DescHTML
        {
            get
            {
                UpdateFromDetailsDB();
                return _DescHTML;
            }
            set
            {
                if (_DescHTML != value)
                {
                    _DescHTML = value;
                    Notify("DescHTML");                }
            }
        }

        public string Mythic
        {
            get
            {
                UpdateFromDetailsDB();
                return _Mythic;
            }
            set
            {
                if (_Mythic != value)
                {
                    _Mythic = value;
                    Notify("Mythic");                }
            }
        }
        public string LegendaryWeapon
        {
            get
            {
                UpdateFromDetailsDB();
                return _LegendaryWeapon;
            }
            set
            {
                if (_LegendaryWeapon != value)
                {
                    _LegendaryWeapon = value;
                    Notify("LegendaryWeapon");                }
            }
        }


        public static MagicItem ByName(string name)
        {
            MagicItem item = null;
            if (itemMap.TryGetValue(name, out item))
            {
                return item;
            }
            return null;
        }

    }
}
