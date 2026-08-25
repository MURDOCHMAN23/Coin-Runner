using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerHandler player = other.GetComponent<PlayerHandler>();

        if (player == null)
            return;

        player.ObstacleTrigger();
    }
}