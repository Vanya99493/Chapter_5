using System.Collections;
using InteractionModule;
using UnityEngine;

namespace PlayerModule
{
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private Transform _headTransform;
        [SerializeField] private float _raycastFrequency;
        [SerializeField] private float _gazeTime = 2.0f;

        private IInteractByViewHandler _interactByViewObject;
        private float _gazeCounter = 0.0f;
        private bool _isGazing = false;

        private void Start()
        {
            StartCoroutine(RaycastDraw());
        }

        public void Move(Vector3 newPosition)
        {
            gameObject.transform.position = newPosition;
        }

        private IEnumerator RaycastDraw()
        {
            float delay = 1f / _raycastFrequency;

            while (true)
            {
                Ray ray = new Ray(_headTransform.position, _headTransform.forward);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.transform.gameObject.TryGetComponent(out IInteractByViewHandler teleportationArea))
                    {
                        if (!_isGazing)
                        {
                            _isGazing = true;
                            _gazeCounter = 0.0f;
                            _interactByViewObject = teleportationArea;
                            (_interactByViewObject as IStartInteractByViewHandler)?.StartInteract();
                        }
                        else
                        {
                            _gazeCounter += delay;
                            if (_gazeCounter >= _gazeTime)
                            {
                                Interact();
                            }
                        }
                    }
                    else
                    {
                        ResetGaze();
                    }
                }
                else
                {
                    ResetGaze();
                }
                
                yield return new WaitForSeconds(delay);
            }
        }

        void ResetGaze()
        {
            _isGazing = false;
            _gazeCounter = 0.0f;
            (_interactByViewObject as IEndInteractByViewHandler)?.EndInteract();
        }

        void Interact()
        {
            _interactByViewObject.Interact(this);
            ResetGaze();
        }
    }
}