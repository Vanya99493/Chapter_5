using System.Collections;
using InteractByNameModule.MaterialChangerModule;
using InteractionModule;
using UIModule.SliderModule;
using UnityEngine;

namespace PlayerModule
{
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private MaterialChanger[] _interactableObjects;
        
        [SerializeField] private FillSlider _fillSlider;
        //[SerializeField] private Transform _headTransform;
        [SerializeField] private float _lookTime = 2.0f;
        /*[Header("Spherecast settings")]
        [SerializeField] private float _radius = 1f;
        [SerializeField] private float _maxDistance = 10f;
        [SerializeField] private LayerMask _layerMask;*/

        
        private IInteractByViewHandler _interactableObject;
        private Coroutine _interactionCountDownCoroutine;
        [SerializeField] private float _interactionCoroutineFrequency = 30f;
        
        
        private float _lookCounter = 0.0f;
        private bool _isLooking = false;
        
        /*public GameObject _currentHitObject;
        private float _currentHitDistance;
        private Vector3 _origin;
        private Vector3 _direction;*/

        
        
        private void Start()
        {
            for (int i = 0; i < _interactableObjects.Length; i++)
            {
                var interactableObject = _interactableObjects[i] as IInteractByViewHandler;
                interactableObject.PointerEnterEvent += OnPointerEnterEvent;
                interactableObject.PointerExitEvent += OnPointerExitEvent;
            }
            
            //StartCoroutine(RaycastDraw());
            //StartCoroutine(SpherecastDraw());
        }

        public void Move(Vector3 newPosition)
        {
            gameObject.transform.position = newPosition;
        }

        /*private IEnumerator RaycastDraw()
        {
            float delay = 1f / _raycastFrequency;

            while (true)
            {
                Ray ray = new Ray(_headTransform.position, _headTransform.forward);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.transform.gameObject.TryGetComponent(out IInteractByViewHandler teleportationArea))
                    {
                        if (!_isLooking)
                        {
                            _isLooking = true;
                            _lookCounter = 0.0f;
                            _interactByViewObject = teleportationArea;
                            (_interactByViewObject as IStartInteractByViewHandler)?.StartInteract();
                        }
                        else
                        {
                            _lookCounter += delay;
                            _fillSlider.SetValue(_lookCounter / _lookTime);
                            if (_lookCounter >= _lookTime)
                            {
                                Interact();
                            }
                        }
                    }
                    else
                    {
                        ResetLook();
                    }
                }
                else
                {
                    ResetLook();
                }
                
                yield return new WaitForSeconds(delay);
            }
        }*/
        
        /*private IEnumerator SpherecastDraw()
        {
            float delay = 1f / _raycastFrequency;

            while (true)
            {
                _origin = _headTransform.position; 
                if (Physics.SphereCast(_origin, _radius, _direction, out RaycastHit hit, _maxDistance, _layerMask))
                {
                    _currentHitObject = hit.transform.gameObject;
                    _currentHitDistance = hit.distance;
                    Debug.Log(_currentHitDistance);
                }
                else
                {
                    _currentHitObject = null;
                    _currentHitDistance = _maxDistance;
                }

                if (_currentHitObject != null)
                {
                    if (hit.transform.gameObject.TryGetComponent(out IInteractByViewHandler teleportationArea))
                    {
                        if (!_isLooking)
                        {
                            _isLooking = true;
                            _lookCounter = 0.0f;
                            _interactByViewObject = teleportationArea;
                            (_interactByViewObject as IStartInteractByViewHandler)?.StartInteract();
                        }
                        else
                        {
                            _lookCounter += delay;
                            _fillSlider.SetValue(_lookCounter / _lookTime);
                            if (_lookCounter >= _lookTime)
                            {
                                Interact();
                            }
                        }
                    }
                    else
                    {
                        ResetLook();
                    }
                }
                
                yield return new WaitForSeconds(delay);
            }
        }*/

        private void OnPointerEnterEvent(IInteractByViewHandler interactableObject)
        {
            if (_interactableObject != null)
            {
                return;
            }
            _interactableObject = interactableObject;
            _interactionCountDownCoroutine = StartCoroutine(StartInteractionCountDown());
        }

        private void OnPointerExitEvent(IInteractByViewHandler interactableObject)
        {
            _interactableObject = null;
            ResetLook();
            StopCoroutine(_interactionCountDownCoroutine);
            _interactionCountDownCoroutine = null;
        }

        private IEnumerator StartInteractionCountDown()
        {
            float delay = 1f / _interactionCoroutineFrequency;
            ResetLook();
            
            while (true)
            {
                _lookCounter += delay;
                _fillSlider.SetValue(_lookCounter / _lookTime);

                if (_lookCounter >= _lookTime)
                {
                    Interact();
                    _interactableObject = null;
                    _interactionCountDownCoroutine = null;
                    yield break;
                }

                yield return new WaitForSeconds(delay);
            }
        }

        private void ResetLook()
        {
            _lookCounter = 0.0f;
            _fillSlider.SetValue(0f);
        }

        private void Interact()
        {
            _interactableObject.Interact(this);
            ResetLook();
        }
        
        /*private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Debug.DrawLine(_origin, _origin + _direction * _currentHitDistance);
            Gizmos.DrawWireSphere(_origin + _direction * _currentHitDistance, _radius);
        }*/
    }
}