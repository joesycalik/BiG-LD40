using UnityEngine;

public class Gem : MonoBehaviour
{

    public bool isAvailable = true;

    public void HoldBy(Transform holder)
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
        var collider = GetComponent<Collider2D>();
        collider.enabled = false;
        transform.SetParent(holder);
        transform.localPosition = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(0, 0.1f), 0);
        isAvailable = false;
    }

    public void Release()
    {
        transform.SetParent(null);
        var rb = GetComponent<Rigidbody2D>();
        rb.simulated = true;
        rb.AddForce(new Vector3(Random.Range(-100f, 100f), 100f, 0));
        var collider = GetComponent<Collider2D>();
        collider.enabled = true;
        Invoke("MakeAvailable", 2);
    }

    private void MakeAvailable()
    {
        isAvailable = true;
    }
}
