using InteractionModule;
using InteractionModule.MaterialChangerModule;
using PlayerModule;

namespace TeleportationAreaModule
{
    public class TeleportationArea : MaterialChanger, IInteractByViewHandler
    {
        public void Interact(PlayerInteractor playerInteractor)
        {
            playerInteractor.Move(transform.position);
        }
    }
}