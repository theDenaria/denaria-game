using _Project.Shooting.Scripts.Models;
using _Project.Shooting.Scripts.Services;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.Shooting.Scripts.Commands
{
    public class OnTargetHitCommand : Command
    {
        [Inject] public IShootingMechanicService ShootingMechanicService { get; set; }
        [Inject] public HandleTargetHitCommandData HandleTargetHitCommandData { get; set; }
        public override void Execute()
        {
            if (HandleTargetHitCommandData.Hit.collider != null)
            {
                UnityEngine.Debug.Log("COLLIDER!!!");

                if (HandleTargetHitCommandData.Hit.collider.TryGetComponent(out IDamageable damageable))
                {
                    UnityEngine.Debug.Log("HIT!!!");
                    
                    float distance = Vector3.Distance(HandleTargetHitCommandData.StartPoint, HandleTargetHitCommandData.EndPoint);///

                    damageable.TakeDamage(ShootingMechanicService.GetDamageConfiguration().GetDamage(distance));
                }
            }

            HandleSurfaceImpact();
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
    }
}