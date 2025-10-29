using UnityEngine;

public class coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInventory inventory = collision.gameObject.GetComponent<playerInventory>();
            inventory.AddCoins(1);


            Destroy(gameObject);
        }

    }

}
