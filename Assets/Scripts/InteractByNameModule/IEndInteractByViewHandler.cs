using System;

namespace InteractionModule
{
    public interface IEndInteractByViewHandler
    {
        public event Action<IInteractByViewHandler> PointerExitEvent;
        
        public void EndInteract();
    }
}