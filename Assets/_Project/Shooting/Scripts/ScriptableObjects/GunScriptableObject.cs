using System.Collections;
using DefaultNamespace;
using LlamAcademy.Guns;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering;

namespace _Project.Shooting.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GunConfiguration", menuName = "Denaria/Guns/Gun Configuration", order = 0)]
    public class GunScriptableObject : ScriptableObject
    {
        //public ImpactType ImpactType; //TODO: Enable
        
        public GunType Type;
        public string Name;
        public GameObject ModelPrefab;
        public Vector3 SpawnPoint;
        public Vector3 SpawnRotation;
        public Vector3 SpawnScale; //TODO: Use later

        public ShootConfigurationScriptableObject ShootConfiguration;
        public TrailConfigurationScriptableObject TrailConfiguration;

        public DamageConfigurationScriptableObject DamageConfiguration;
        
        private MonoBehaviour ActiveMonoBehaviour;
        private GameObject Model;
        private float LastShootTime;
        private ParticleSystem ShootSystem;
        private UnityEngine.Pool.ObjectPool<TrailRenderer> TrailPool;

        public void Spawn(Transform Parent, MonoBehaviour ActiveMonoBehaviour)
        {
            Debug.Log("uuu entered spawn");

            this.ActiveMonoBehaviour = ActiveMonoBehaviour;
            LastShootTime = 0; //In Editor, this will not be properly reset, in build it is fine.
            TrailPool = new UnityEngine.Pool.ObjectPool<TrailRenderer>(CreateTrail); //Or use UnityEngine.Pool.Rendering.ObjectPool?
            Model = Instantiate(ModelPrefab, Parent, false);
            //Model.transform.SetParent(Parent, false);
            Model.transform.localPosition = SpawnPoint;
            Model.transform.localRotation = Quaternion.Euler(SpawnRotation);

            ShootSystem = Model.GetComponentInChildren<ParticleSystem>();
            Debug.Log("uuu exiting spawn");
        }
        
        public void Shoot()
        {
            if (Time.time > ShootConfiguration.FireRate + LastShootTime)
            {
                LastShootTime = Time.time;
                ShootSystem.Play();
                
                //TODO: Calculate the needed rotation and apply to the gun. Needs further animating work for arm.
                Vector3 shootDirection = ShootSystem.transform.forward +
                                         new Vector3(
                                             Random.Range(-ShootConfiguration.Spread.x, ShootConfiguration.Spread.x), 
                                             Random.Range(-ShootConfiguration.Spread.y, ShootConfiguration.Spread.y),                                         
                                             Random.Range(-ShootConfiguration.Spread.z, ShootConfiguration.Spread.z)
                                             );
                shootDirection.Normalize();
                if (Physics.Raycast(ShootSystem.transform.position,
                        shootDirection,
                        out RaycastHit hit,
                        float.MaxValue,//TODO: Customize later for max range of gun.
                        ShootConfiguration.HitMask))
                {
                    ActiveMonoBehaviour.StartCoroutine(
                        PlayTrail(
                            ShootSystem.transform.position,
                            hit.point,
                            hit));

                }
                else
                {
                    ActiveMonoBehaviour.StartCoroutine(
                        PlayTrail(
                            ShootSystem.transform.position,
                            ShootSystem.transform.position + (shootDirection * TrailConfiguration.MissDistance), 
                            new RaycastHit()));
                }
            }
        }


        public void StopShooting()
        {
            if (ShootSystem != null)
            {
                ShootSystem.Stop();
            }
        }

        private IEnumerator PlayTrail(Vector3 StartPoint, Vector3 EndPoint, RaycastHit Hit)
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
            
            yield return new WaitForSeconds(TrailConfiguration.Duration);
            yield return null;
            instance.emitting = false;
            instance.gameObject.SetActive(false);
            TrailPool.Release(instance);
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

    }
}