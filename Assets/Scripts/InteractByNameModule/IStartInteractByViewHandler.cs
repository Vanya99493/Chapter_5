using System;

namespace InteractionModule
{
    public interface IStartInteractByViewHandler
    {
        public event Action<IInteractByViewHandler> PointerEnterEvent;
        
        public void StartInteract();
    }
}