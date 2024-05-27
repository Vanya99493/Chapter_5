using UnityEngine;
using UnityEngine.UI;

namespace UIModule.MessagePanelModule
{
    public class MessagePanel : MonoBehaviour
    {
        [SerializeField] private Text _textField;

        public void UpdateText(string message)
        {
            _textField.text = message;
        }
    }
}