using System;
using InteractByNameModule.MaterialChangerModule;
using InteractionModule;
using PlayerModule;
using UnityEngine;

namespace InteractableObjectsModule
{
    public class BarrelView : MaterialChanger, IInteractByViewHandler
    {
        public event Action<IInteractByViewHandler> PointerEnterEvent;
        public event Action<IInteractByViewHandler> PointerExitEvent;
        
        [SerializeField] private ParticleSystem _explosionParticleSystem;
        [SerializeField] private float _destroyParticlesDelay;

        public void Interact(PlayerInteractor playerInteractor)
        {
            var particleSystem = Instantiate(_explosionParticleSystem, gameObject.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            Destroy(particleSystem.gameObject, _destroyParticlesDelay);
            Destroy(gameObject, _destroyParticlesDelay);
        }

        public override void OnPointerEnter()
        {
            base.OnPointerEnter();
            PointerEnterEvent?.Invoke(this);
        }

        public override void OnPointerExit()
        {
            base.OnPointerExit();
            PointerExitEvent?.Invoke(this);
        }
    }
}