using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCrm.Infrastructure
{
   public class ConfigManager
    {
        private static ConfigManager instance;

        private Configuration configFile;
        private KeyValueConfigurationCollection settings;
        public DateTime OrdersStartDate {
            get
            {
                DateTime result;
                DateTime.TryParse(GetSetting(nameof(OrdersStartDate)), out result);
                return result;
            }
            set { AddUpdateAppSettings(nameof(OrdersStartDate),value.ToShortDateString()); }
        }
        public DateTime OrdersEndDate {
            get
            {
                DateTime result;
                DateTime.TryParse(GetSetting(nameof(OrdersEndDate)), out result);
                return result;
            }
            set { AddUpdateAppSettings(nameof(OrdersEndDate), value.ToShortDateString()); }
        }

        public DateTime ReportsStartDate
        {
            get
            {
                DateTime result =DateTime.MinValue;
                DateTime.TryParse(GetSetting(nameof(ReportsStartDate)), out result);
                return result;
            }
            set { AddUpdateAppSettings(nameof(ReportsStartDate), value.ToShortDateString()); }
        }
        public DateTime ReportsEndDate
        {
            get
            {
                DateTime result = DateTime.MinValue;
                DateTime.TryParse(GetSetting(nameof(ReportsEndDate)), out result);
                return result;
            }
            set { AddUpdateAppSettings(nameof(ReportsEndDate), value.ToShortDateString()); }
        }


        private ConfigManager()
        {
            configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            settings = configFile.AppSettings.Settings;
        }

        public static ConfigManager getInstance()
        {
            if (instance == null)
                instance = new ConfigManager();
            return instance;
        }



        private string GetSetting(string key)
        {
            try
            {
                return settings[key]==null?"":settings[key].Value ?? "";
               
            }
            catch (ConfigurationErrorsException)
            {
                return "";
            }
        }

        private void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                
            }
        }


    }
}
