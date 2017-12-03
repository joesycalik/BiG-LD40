using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{

    public float duration = 2;

    public Player player { get; set; }
    public Monster monster { get; set; } // Hacky, sorry.

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (player != null && collider.gameObject == player.gameObject) return;
        if (monster != null && collider.gameObject == monster.gameObject) return;
        if (collider.gameObject.CompareTag("Player"))
        {
            if (player != null) player.hitsLanded++;
            collider.GetComponent<Player>().GetHit();
        }
        Destroy(gameObject);
    }
}
