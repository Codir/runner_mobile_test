using Configs;
using UnityEngine;
using Utils;

namespace CoreGameplay.Controllers
{
    //Base class for controllers with using configs from Assets/Configs/Resources
    public abstract class BaseControllerWithConfig<T>
        where T : BaseConfig
    {
        public GameObject View { get; private set; }

        protected T Config;

        public BaseControllerWithConfig()
        {
            Init();
        }

        protected virtual string GetConfigName()
        {
            return typeof(T).Name;
        }

        private void Init()
        {
            LoadConfig();
            CreateView();
        }

        private void LoadConfig()
        {
            Config = ConfigLoader.GetConfig<T>(GetConfigName());
        }

        private void CreateView()
        {
            if (Config == null || Config.View == null) return;

            View = Object.Instantiate(Config.View);
        }
    }
}