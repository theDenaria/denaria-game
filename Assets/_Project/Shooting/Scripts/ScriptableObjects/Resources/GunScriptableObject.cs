using System;
using System.Collections;
using _Project.Shooting.Scripts.Signals;
using DefaultNamespace;
using LlamAcademy.Guns;
using strange.extensions.injector.impl;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering;

namespace _Project.Shooting.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GunConfiguration", menuName = "Denaria/Guns/Gun Configuration", order = 0)]
    public class GunScriptableObject : ScriptableObject
    {
        //[Inject] public OnTargetHitSignal OnTargetHitSignal { get; set; }

        //public ImpactType ImpactType; //TODO: Enable
        
        public GunType Type;
        public string Name;
        public GameObject GunModelPrefab;
        public Vector3 SpawnPoint;
        public Vector3 SpawnRotation;
        public Vector3 SpawnScale; //TODO: Use later

        public ShootConfigurationScriptableObject ShootConfiguration;
        public TrailConfigurationScriptableObject TrailConfiguration;
        public DamageConfigurationScriptableObject DamageConfiguration;
        
        private MonoBehaviour ActiveMonoBehaviour;
        private GameObject GunModelInstance;
        private Camera ActiveCamera;
        private float LastShootTime;
        private ParticleSystem ShootSystem;
        private UnityEngine.Pool.ObjectPool<TrailRenderer> TrailPool;
        
        public void Spawn(Transform Parent, MonoBehaviour ActiveMonoBehaviour, Camera ActiveCamera = null)
        {
            this.ActiveMonoBehaviour = ActiveMonoBehaviour;

            GetCameraReference(ActiveCamera);
            
            LastShootTime = 0; //In Editor, this will not be properly reset, in build it is fine.
            TrailPool = new UnityEngine.Pool.ObjectPool<TrailRenderer>(CreateTrail); //Or use UnityEngine.Pool.Rendering.ObjectPool?
            
            SetUpGunModel(Parent);
        }
        
        public void Shoot()
        {
            if (Time.time > ShootConfiguration.FireRate + LastShootTime)
            {
                LastShootTime = Time.time;
                ShootSystem.Play();

                Vector3 spreadAmount = ShootConfiguration.GetSpread();
                //GunModelInstance.transform.forward += GunModelInstance.transform.TransformDirection(spreadAmount);

                //TODO: Calculate the needed rotation and apply to the gun. Needs further animating work for arm.
                //Vector3 shootDirection = ShootSystem.transform.forward;
                Vector3 shootDirection = ShootSystem.transform.forward;

                if (ShootConfiguration.IsHitscan)
                {
                    DoHitscanShoot(shootDirection, GetRaycastOrigin(), ShootSystem.transform.position);
                }
                else
                {
                    DoProjectileShoot(shootDirection);
                }
            }
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
        
        private void SetUpGunModel(Transform Parent)
        {
            GunModelInstance = Instantiate(GunModelPrefab, Parent, false);
            GunModelInstance.transform.localPosition = SpawnPoint;
            GunModelInstance.transform.localRotation = Quaternion.Euler(SpawnRotation);
            ShootSystem = GunModelInstance.GetComponentInChildren<ParticleSystem>();
        }
                
        /// <summary>
        /// Returns the proper Origin point for raycasting based on <see cref="ShootConfigScriptableObject.ShootType"/>
        /// </summary>
        /// <returns></returns>
        public Vector3 GetRaycastOrigin()
        {
            if (ActiveCamera == null)//TODO: This should not be needed.
            {
                ActiveCamera = Camera.main;
            }
            
            Vector3 origin = ShootSystem.transform.position;

            if (ShootConfiguration.ShootingType == ShootingType.FromCamera)
            {
                origin = ActiveCamera.transform.position
                         + ActiveCamera.transform.forward * Vector3.Distance(
                             ActiveCamera.transform.position,
                             ShootSystem.transform.position
                         );
            }

            return origin;
        }
        
        /// <summary>
        /// Performs a Raycast to determine if a shot hits something. Spawns a TrailRenderer
        /// and will apply impact effects and damage after the TrailRenderer simulates moving to the
        /// hit point. 
        /// See <see cref="PlayTrail(Vector3, Vector3, RaycastHit)"/> for impact logic.
        /// </summary>
        /// <param name="ShootDirection"></param>
        private void DoHitscanShoot(Vector3 ShootDirection, Vector3 Origin, Vector3 TrailOrigin, int Iteration = 0)
        {
            Camera cam = Camera.main;

            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

            Ray ray = cam.ScreenPointToRay(screenCenter);

            Vector3 direction = ray.direction;

            // (Optional) Visualize the ray in the scene view for debugging
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
            
            RaycastHit hitOfCrosshair;
            bool x = Physics.Raycast(
                ray.origin,
                ray.direction,
                out hitOfCrosshair,
                float.MaxValue,
                ShootConfiguration.HitMask);

            if (x)
            {
                ShootDirection = hitOfCrosshair.point - TrailOrigin;
                //GunModelInstance.transform.forward += Model.transform.TransformDirection(spreadAmount);
                
                //GunModelInstance.transform.forward = ShootDirection;
                //OnTargetHitSignal.Dispatch();
            }
            else
            {
                ShootDirection = ray.direction;
                //GunModelInstance.transform.forward = ShootDirection;
            }
            
            if (Physics.Raycast(
                    TrailOrigin,
                    ShootDirection,
                    out RaycastHit hit,
                    float.MaxValue,
                    ShootConfiguration.HitMask
                ))
            {
                ActiveMonoBehaviour.StartCoroutine(
                    PlayTrail(
                        TrailOrigin,
                        hit.point,
                        hit
                        //,
                        //Iteration
                    )
                );
            }
            else
            {
                ActiveMonoBehaviour.StartCoroutine(
                    PlayTrail(
                        TrailOrigin,
                        ShootDirection,//TrailOrigin + (ShootDirection * TrailConfiguration.MissDistance),
                        new RaycastHit()
                        //,
                        //Iteration
                    )
                );
            }
        }
        
        /// <summary>
        /// Generates a live Bullet instance that is launched in the <paramref name="ShootDirection"/> direction
        /// with velocity from <see cref="ShootConfigScriptableObject.BulletSpawnForce"/>.
        /// </summary>
        /// <param name="ShootDirection"></param>
        private void DoProjectileShoot(Vector3 ShootDirection)
        {
            return;
            /*
            Bullet bullet = BulletPool.Get();
            bullet.gameObject.SetActive(true);
            bullet.OnCollision += HandleBulletCollision;

            // We have to ensure if shooting from the camera, but shooting real proejctiles, that we aim the gun at the hit point
            // of the raycast from the camera. Otherwise the aim is off.
            // When shooting from the gun, there's no need to do any of this because the recoil is already handled in TryToShoot
            if (ShootConfiguration.ShootingType == ShootingType.FromCamera
                && Physics.Raycast(
                    GetRaycastOrigin(),
                    ShootDirection,
                    out RaycastHit hit,
                    float.MaxValue,
                    ShootConfiguration.HitMask
                ))
            {
                Vector3 directionToHit = (hit.point - ShootSystem.transform.position).normalized;
                GunModelInstance.transform.forward = directionToHit;
                ShootDirection = directionToHit;
            }

            bullet.transform.position = ShootSystem.transform.position;
            bullet.Spawn(ShootDirection * ShootConfiguration.BulletSpawnForce);

            TrailRenderer trail = TrailPool.Get();
            if (trail != null)
            {
                trail.transform.SetParent(bullet.transform, false);
                trail.transform.localPosition = Vector3.zero;
                trail.emitting = true;
                trail.gameObject.SetActive(true);
            }
            */
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

        private void UpdateCamera(Camera ActiveCamera)
        {
            this.ActiveCamera = ActiveCamera;
        }

    }
}