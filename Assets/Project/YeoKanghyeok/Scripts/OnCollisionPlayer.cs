using UnityEngine;

public class OnCollisionPlayer : MonoBehaviour
{
    [SerializeField] int attackPower = 1;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == Manager.Game.Player.gameObject)
        {
            Manager.Game.Player.playerModel.TakeDamageEvent(attackPower);
        }
    }
}

