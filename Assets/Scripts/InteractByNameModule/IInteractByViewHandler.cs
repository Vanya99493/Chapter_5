using System;
using PlayerModule;

namespace InteractionModule
{
    public interface IInteractByViewHandler
    {
        public event Action<IInteractByViewHandler> PointerEnterEvent;
        public event Action<IInteractByViewHandler> PointerExitEvent;
        
        public void Interact(PlayerInteractor playerInteractor);
    }
}