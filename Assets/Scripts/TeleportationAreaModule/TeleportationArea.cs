using System;
using InteractByNameModule.MaterialChangerModule;
using InteractionModule;
using PlayerModule;

namespace TeleportationAreaModule
{
    public class TeleportationArea : MaterialChanger, IInteractByViewHandler
    {
        public event Action<IInteractByViewHandler> PointerEnterEvent;
        public event Action<IInteractByViewHandler> PointerExitEvent;

        public void Interact(PlayerInteractor playerInteractor)
        {
            playerInteractor.Move(transform.position);
        }

        public override void OnPointerEnter()
        {
            base.OnPointerEnter();
            PointerEnterEvent?.Invoke(this);
        }

        public override void OnPointerExit()
        {
            base.OnPointerExit();
            PointerExitEvent?.Invoke(this);
        }
    }
}