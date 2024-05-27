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
        [SerializeField] private float _lookTime = 2.0f;
        [SerializeField] private float _interactionCoroutineFrequency = 30f;
        
        private IInteractByViewHandler _interactableObject;
        private Coroutine _interactionCountDownCoroutine;
        private float _lookCounter = 0.0f;
        private bool _isLooking = false;
        
        private void Start()
        {
            for (int i = 0; i < _interactableObjects.Length; i++)
            {
                var interactableObject = _interactableObjects[i] as IInteractByViewHandler;
                interactableObject.PointerEnterEvent += OnPointerEnter;
                interactableObject.PointerExitEvent += OnPointerExit;
            }
        }

        public void Move(Vector3 newPosition)
        {
            gameObject.transform.position = newPosition;
        }

        private void OnPointerEnter(IInteractByViewHandler interactableObject)
        {
            if (_interactableObject != null)
            {
                return;
            }
            _interactableObject = interactableObject;
            _interactionCountDownCoroutine = StartCoroutine(StartInteractionCountDown());
        }

        private void OnPointerExit(IInteractByViewHandler interactableObject)
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
                    ResetLook();
                    _interactableObject = null;
                    _interactionCountDownCoroutine = null;
                    yield break;
                }

                yield return new WaitForSeconds(delay);
            }
        }

        private void Interact()
        {
            _interactableObject.Interact(this);
        }

        private void ResetLook()
        {
            _lookCounter = 0.0f;
            _fillSlider.SetValue(0f);
        }
    }
}