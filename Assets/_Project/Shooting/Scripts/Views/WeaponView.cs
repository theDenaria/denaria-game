using System.Collections;
using _Project.Shooting.Scripts.ScriptableObjects;
using _Project.StrangeIOCUtility.Scripts.Views;
using LlamAcademy.Guns;
using UnityEngine;
using UnityEngine.Rendering;

namespace _Project.Shooting.Scripts.Views
{
    public class WeaponView : ViewZeitnot
    {
        [field: SerializeField] private ParticleSystem ParticleSystem { get; set; }
        
        private Camera ActiveCamera;
        private float LastShootTime;
        private ParticleSystem ShootSystem;
        private UnityEngine.Pool.ObjectPool<TrailRenderer> TrailPool;
        
        
        public TrailConfigurationScriptableObject TrailConfiguration { get; set; }
        public DamageConfigurationScriptableObject DamageConfiguration { get; set; }

        internal void Init()
        {
            TrailPool = new UnityEngine.Pool.ObjectPool<TrailRenderer>(CreateTrail); //Or use UnityEngine.Pool.Rendering.ObjectPool?
        }

        private void OnDisable()
        {
            
        }

        private void GetCameraReference(Camera activeCamera)
        {
            if (ActiveCamera != null)
            {
                ActiveCamera = activeCamera;
            }
            else
            {
                ActiveCamera = Camera.main;
            }
        }
        
        private TrailRenderer CreateTrail()
        {
            GameObject instance = new GameObject("BulletTrail");
            TrailRenderer trail = instance.AddComponent<TrailRenderer>();
            trail.colorGradient = TrailConfiguration.Color;
            UnityEngine.Debug.Log("xxx TrailConfiguration.Material: " + TrailConfiguration.Material.name);
            trail.material = TrailConfiguration.Material;
            trail.widthCurve = TrailConfiguration.WidthCurve;
            trail.time = TrailConfiguration.Duration;
            trail.minVertexDistance = TrailConfiguration.MinimumVertexDistance;
            
            trail.emitting = false;
            trail.shadowCastingMode = ShadowCastingMode.Off;

            return trail;
        }
        
        internal IEnumerator PlayTrail(Vector3 StartPoint, Vector3 EndPoint, RaycastHit Hit)
        {
            TrailRenderer instance = TrailPool.Get();
            instance.gameObject.SetActive(true);
            instance.transform.position = StartPoint;

            instance.emitting = true;

            float distance = Vector3.Distance(StartPoint, EndPoint);
            float remainingDistance = distance;

            while (remainingDistance > 0)
            {
                
                instance.transform.position = Vector3.Lerp(
                    StartPoint,
                    EndPoint,
                    Mathf.Clamp01(1 - (remainingDistance / distance)));

                remainingDistance -= TrailConfiguration.SimulationSpeed * Time.deltaTime;
                yield return null;
            }

            instance.transform.position = EndPoint;

            if (Hit.collider != null)
            {
                UnityEngine.Debug.Log("COLLIDER!!!");

                if (Hit.collider.TryGetComponent(out IDamageable damageable))
                {
                    UnityEngine.Debug.Log("HIT!!!");
                    damageable.TakeDamage(DamageConfiguration.GetDamage(distance));
                }
            }

            HandleSurfaceImpact();
            
            yield return new WaitForSeconds(TrailConfiguration.Duration);
            yield return null;
            instance.emitting = false;
            instance.gameObject.SetActive(false);
            TrailPool.Release(instance);
        }

        private void HandleSurfaceImpact()
        {
            //TODO: Use after you add SurfaceManager
            /*if (Hit.collider != null)
            {
                SurfaceManager.Instance.HandleImpact(
                    Hit.transform.gameObject,
                    EndPoint,
                    Hit.normal,
                    ImpactType,
                    0);
            }*/
        }
        
        
        internal void PlayParticleSystem()
        {
            ParticleSystem.Play();
        }        
        
        internal void StopParticleSystem()
        {
            ParticleSystem.Stop();
        }
        
    }
}