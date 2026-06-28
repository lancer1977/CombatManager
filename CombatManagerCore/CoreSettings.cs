using System;
using CombatManager.LocalService;
#if MONO
using Xamarin.Essentials;
#else
#endif

namespace CombatManager
{

    //these settings are set in a platform appropriate manner in the core
    //the settings are automatically stored when changed

    public class CoreSettings : SimpleNotifyClass
    {
        public IPreferences Preferences { get; }
        private static CoreSettings _instance;

        private CoreSettings()
        {
            Preferences = new WindowsPreferences();
        }

        public static CoreSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CoreSettings();
                }

                return _instance;
            }
        }

        public bool RunLocalService
        {
            get => Preferences.LoadBoolValue("RunLocalService", false);
            set
            {
                if (RunLocalService != value)
                {
                    Preferences.SaveBoolValue("RunLocalService", value);
                    Notify("RunLocalService");
                }
            }
        }


        public int LocalServicePort
        {
            get => Preferences.LoadIntValue("LocalServicePort", LocalCombatManagerService.DefaultPort);
            set
            {
                if (value > 0 && value < 32778 && LocalServicePort != value)
                {

                    Preferences.SaveIntValue("LocalServicePort", value);
                    Notify("LocalServicePort");
                }
            }
        }

        public string LocalServicePasscode
        {

            get => Preferences.LoadStringValue("LocalServicePasscode", "");
            set
            {
                if (LocalServicePasscode == value) return;
                Preferences.SaveStringValue("LocalServicePasscode", value);
                Notify("LocalServicePasscode");
            }
        }

        public bool AutomaticStabilization
        {
            get => Preferences.LoadBoolValue("AutomaticStabilization", false);
            set
            {
                if (AutomaticStabilization == value) return;
                Preferences.SaveBoolValue("AutomaticStabilization", value);
                Notify("AutomaticStabilization");
            }
        }



    }

#if MONO
    public class MonoPreferences : IPreferences
    {
        public   void SaveBoolValue(String name, bool value)
        {
            Preferences.Set(name, value);
        }


        public   bool LoadBoolValue(String name, bool def)
        {
            return Preferences.Get(name, def);

        }
        public   void SaveStringValue(String name, string value)
        {
            Preferences.Set(name, value);
        }


        public   String LoadStringValue(String name, string def)
        {
            return Preferences.Get(name, (string)def);
        }
        public   void SaveIntValue(String name, int value)
        {
            Preferences.Set(name, value);
        }


        public   int LoadIntValue(String name, int def)
        {
            return Preferences.Get(name, def);
        }
    }
#endif
}
