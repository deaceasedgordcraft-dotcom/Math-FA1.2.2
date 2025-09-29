using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 100f; // adjust in Inspector

    private void Update()
    {
        // Rotate coin every frame
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player touched coin!"); // test in Console if player touches coin
            ScoreManager.instance.AddScore(1);
            Destroy(gameObject);
        }
    }
}
