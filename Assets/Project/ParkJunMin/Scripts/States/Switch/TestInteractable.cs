using UnityEngine;

namespace Project.ParkJunMin.Scripts.States.Switch
{
    public class TestInteractable : SwichInteractable
    {
        [SerializeField] GameObject gameObject;
        private GameObject interactiveObject;
        public override void Interact()
        {
            interactiveObject = Instantiate(gameObject);
            interactiveObject.SetActive(true);
        }
    }
}