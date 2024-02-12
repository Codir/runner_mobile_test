using UnityEngine;

namespace Utils
{
    public static class ConfigLoader
    {
        //Load ScriptableObject from Assets/Configs/Resources
        public static T GetConfig<T>(string name)
            where T : ScriptableObject
        {
            return Resources.Load<T>(name);
        }
    }
}