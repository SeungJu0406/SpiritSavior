using UnityEngine;

namespace Project.ParkJunMin.Scripts.States.Switch
{
    public abstract class SwichInteractable : MonoBehaviour
    {
        protected Switch _switch;

        public abstract void Interact();

        public void SetSwitch(Switch thisSwitch)
        {
            _switch = thisSwitch;
        }
    }
}
