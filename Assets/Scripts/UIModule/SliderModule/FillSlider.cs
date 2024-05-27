using UnityEngine;
using UnityEngine.UI;

namespace UIModule.SliderModule
{
    public class FillSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private CanvasGroup _canvasGroup;
        
        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            SetValue(0f);
        }

        public void SetValue(float value)
        {
            if (value <= 0f)
            {
                _canvasGroup.alpha = 0f;
            }
            else
            {
                _canvasGroup.alpha = 0.8f;
                _slider.value = value;
            }
        }
    }
}