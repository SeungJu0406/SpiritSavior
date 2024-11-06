using UnityEngine;

public abstract class Item : Trap
{
    PlayerController player;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            Active();
            Delete();
        }
    }

    protected override void ProcessActive()
    {
        PlayerController playerController = player;
        Use(playerController);
    }

    protected abstract void Use(PlayerController player);

    void Delete()
    {
        Destroy(gameObject);
    }
}
