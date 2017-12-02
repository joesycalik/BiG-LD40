using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{

    public float duration = 2;

    public GameObject player { get; set; }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject != player)
        {
            Debug.Log("BOOM");
            Destroy(gameObject);
        }
    }
}
