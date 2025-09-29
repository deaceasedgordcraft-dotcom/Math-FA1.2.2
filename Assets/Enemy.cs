using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Reward Settings")]
    public int coinReward = 1;

    [Header("Movement Settings")]
    public float moveDistance = 3f;
    public float moveSpeed = 2f; 

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance;
        transform.position = startPos + new Vector3(offset, 0, 0);
    }

    public void Die()
    {
        ScoreManager.instance.AddScore(coinReward);
        Destroy(gameObject);
    }
}