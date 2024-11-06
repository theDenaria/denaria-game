using System.Collections;
using _Project.Shooting.Scripts.Commands;
using _Project.Shooting.Scripts.Models;
using _Project.Shooting.Scripts.ScriptableObjects;
using _Project.Shooting.Scripts.Signals;
using _Project.StrangeIOCUtility.Scripts.Utilities;
using DefaultNamespace;
using LlamAcademy.Guns;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace _Project.Shooting.Scripts.Services
{
    public class ShootingMechanicService : IShootingMechanicService
    {
        [Inject] public OnTargetHitSignal OnTargetHitSignal { get; set; }
        [Inject] public PlayShootingParticleSystemSignal PlayShootingParticleSystemSignal { get; set; }
        [Inject] public PlayTrailEffectSignal PlayTrailEffectSignal { get; set; }
        [Inject] public StopPlayingShootingParticleSystemSignal StopPlayingShootingParticleSystemSignal { get; set; }
        [Inject] public IGunsModel GunsModel { get; set; }
        public GunScriptableObject ActiveGun { get; set; }
        
        private MonoBehaviour ActiveMonoBehaviour;
        private GameObject GunModelInstance;
        private Camera ActiveCamera;
        private float LastShootTime;
        //private ParticleSystem ShootSystem;

        Vector3 shootDirection;
        private ParticleSystem ShootSystem;
        
        [Inject] public IRoutineRunner RoutineRunner { get; set; }

        public void SetUpShootingMechanicService()
        {
            if (!ActiveGun)
            {
                ActiveGun = GunsModel.GetGunList()[0];
            }
            
            //ShootSystem = GunModelInstance.GetComponentInChildren<ParticleSystem>(); //TODO: Do it once
        }

        public DamageConfigurationScriptableObject GetDamageConfiguration()
        {
            return ActiveGun.DamageConfiguration;
        }

        public void SetGunModel(GameObject spawnedGunModelInstance)
        {
            GunModelInstance = spawnedGunModelInstance;
        }

        public void Shoot()
        {
            if (!ShootSystem)
            {
                ShootSystem = GunModelInstance.GetComponentInChildren<ParticleSystem>();//TODO: Do it once
            }
            
            if (Time.time > ActiveGun.ShootConfiguration.FireRate + LastShootTime)
            {
                LastShootTime = Time.time;

                PlayShootingParticleSystemSignal.Dispatch();

                Vector3 spreadAmount = ActiveGun.ShootConfiguration.GetSpread();
                //GunModelInstance.transform.forward += GunModelInstance.transform.TransformDirection(spreadAmount);

                //TODO: Calculate the needed rotation and apply to the gun. Needs further animating work for arm.
                //Vector3 shootDirection = ShootSystem.transform.forward;
                shootDirection = ShootSystem.transform.forward;

                if (ActiveGun.ShootConfiguration.IsHitscan)
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

            if (ActiveGun.ShootConfiguration.ShootingType == ShootingType.FromCamera)
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
                ActiveGun.ShootConfiguration.HitMask);

            if (x)
            {
                ShootDirection = hitOfCrosshair.point - TrailOrigin;
                //GunModelInstance.transform.forward += Model.transform.TransformDirection(spreadAmount);
                
                //GunModelInstance.transform.forward = ShootDirection;
                HandleTargetHitCommandData handleTargetHitCommandData = new HandleTargetHitCommandData(hitOfCrosshair, Origin, hitOfCrosshair.point);
                OnTargetHitSignal.Dispatch(handleTargetHitCommandData);
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
                    ActiveGun.ShootConfiguration.HitMask
                ))
            {
                PlayTrailEffectSignal.Dispatch(new PlayTrailEffectCommandData(TrailOrigin, hit.point));//hit ,Iteration
            }
            else
            {
                PlayTrailEffectSignal.Dispatch(new PlayTrailEffectCommandData(TrailOrigin, TrailOrigin + ShootDirection));// new RaycastHit() ,Iteration
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
            StopPlayingShootingParticleSystemSignal.Dispatch();
            /*if (ShootSystem != null)
            {
                ShootSystem.Stop();
            }*/
        }
        

        private void UpdateCamera(Camera ActiveCamera)
        {
            this.ActiveCamera = ActiveCamera;
        }
        
    }
}