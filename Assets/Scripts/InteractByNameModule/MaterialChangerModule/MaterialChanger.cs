using UnityEngine;

namespace InteractByNameModule.MaterialChangerModule
{
    public class MaterialChanger : MonoBehaviour
    {
        [SerializeField] private Material _inactiveMaterial;
        [SerializeField] private Material _activeMaterial;

        private Renderer _renderer;
        
        private void Awake()
        {
            _renderer = gameObject.GetComponent<Renderer>();
            if (_inactiveMaterial == null)
            {
                _inactiveMaterial = _renderer.material;
            }
        }
        
        public virtual void OnPointerEnter()
        {
            Activate();
        }
        
        public virtual void OnPointerExit()
        {
            Deactivate();
        }

        public void Activate()
        {
            gameObject.GetComponent<Renderer>().material = _activeMaterial;
        }

        public void Deactivate()
        {
            gameObject.GetComponent<Renderer>().material = _inactiveMaterial;
        }
    }
}