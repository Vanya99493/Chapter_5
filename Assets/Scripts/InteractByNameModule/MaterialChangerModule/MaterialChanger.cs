using UnityEngine;

namespace InteractionModule.MaterialChangerModule
{
    public class MaterialChanger : MonoBehaviour, IStartInteractByViewHandler, IEndInteractByViewHandler
    {
        [SerializeField] private Material _activeMaterial;

        private Material _inactiveMaterial;

        private void Awake()
        {
            _inactiveMaterial = gameObject.GetComponent<Renderer>().material;
        }

        public void StartInteract()
        {
            gameObject.GetComponent<Renderer>().material = _activeMaterial;
        }

        public void EndInteract()
        {
            gameObject.GetComponent<Renderer>().material = _inactiveMaterial;
        }
    }
}