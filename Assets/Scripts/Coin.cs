using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerHandler player = other.GetComponent<PlayerHandler>();

        if (player == null)
            return;

        player.CoinTrigger();
        Destroy(gameObject);
    }
}