#nullable enable
using System;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Utilities
{
    public static class JSONUtilityZeitnot
    {
        public static T? DeserializeObject<T>(string value, JsonSerializerSettings? settings)
        {
            T? result = JsonConvert.DeserializeObject<T>(value, settings);
            return result;
        }
        
        public static T? TryDeserializeObject<T>(string value, JsonSerializerSettings? settings)
        {
            try
            {
                T? result = JsonConvert.DeserializeObject<T>(value, settings);
                return result;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                T? result = default(T);
                return result;
                //throw; //We don't throw and break the game, but instead give the player something to continue playing.
            }
        }
        
        public static T? DeserializeObject<T>(string value)
        {
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.All;
            T? result = JsonConvert.DeserializeObject<T>(value, settings);
            return result;
        }

        public static T? TryDeserializeObject<T>(string value)
        {
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.All;
            try
            {
                T? result = JsonConvert.DeserializeObject<T>(value, settings);
                return result;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                T? result = default(T);
                return result;
                //throw; //We don't throw and break the game, but instead give the player something to continue playing.
            }
        }
        
        public static string SerializeObject(object? value, Type? type, JsonSerializerSettings? settings)
        {
            string JSONstring = JsonConvert.SerializeObject(value, type, settings);
            return JSONstring;
        }
        
        public static string TrySerializeObject(object? value, Type? type, JsonSerializerSettings? settings)
        {
            try
            {
                string JSONstring = JsonConvert.SerializeObject(value, type, settings);
                return JSONstring;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                // Return an empty JSON string with the specified type
                return JsonConvert.SerializeObject(Activator.CreateInstance(type), settings);
                //throw; //We don't throw and break the game, but instead give the player something to continue playing.
            }
        }
        
        public static string SerializeObject(object? value, Type? type)
        {
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.All;
            string JSONstring = JsonConvert.SerializeObject(value, type, settings);
            return JSONstring;
        }
        
        public static string TrySerializeObject(object? value, Type? type)
        {
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.All;
            try
            {
                string JSONstring = JsonConvert.SerializeObject(value, type, settings);
                return JSONstring;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                // Return an empty JSON string with the specified type
                return JsonConvert.SerializeObject(Activator.CreateInstance(type), settings);
                //throw; //We don't throw and break the game, but instead give the player something to continue playing.
            }
        }
        
    }
}