using InteractionModule;
using InteractionModule.MaterialChangerModule;
using PlayerModule;
using UnityEngine;

namespace InteractableObjectsModule
{
    public class BarrelView : MaterialChanger, IInteractByViewHandler
    {
        [SerializeField] private ParticleSystem _explosionParticleSystem;
        [SerializeField] private float _destroyParticlesDelay;
        
        public void Interact(PlayerInteractor playerInteractor)
        {
            var particleSystem = Instantiate(_explosionParticleSystem, gameObject.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            Destroy(gameObject);
            Destroy(particleSystem, _destroyParticlesDelay);
        }
    }
}