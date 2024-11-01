using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace LlamAcademy.Guns
{
    [CreateAssetMenu(fileName = "DamageConfiguration", menuName = "Denaria/Guns/DamageConfiguration", order = 1)]
    public class DamageConfigurationScriptableObject : ScriptableObject, System.ICloneable
    {
        public MinMaxCurve DamageCurve;

        private void Reset()
        {
            DamageCurve.mode = ParticleSystemCurveMode.Curve;
        }

        public int GetDamage(float Distance = 0, float DamageMultiplier = 1)
        {
            return Mathf.CeilToInt(
                DamageCurve.Evaluate(Distance, Random.value) * DamageMultiplier
            );
        }

        public object Clone()
        {
            DamageConfigurationScriptableObject config = CreateInstance<DamageConfigurationScriptableObject>();

            config.DamageCurve = DamageCurve;
            return config;
        }
    }
}