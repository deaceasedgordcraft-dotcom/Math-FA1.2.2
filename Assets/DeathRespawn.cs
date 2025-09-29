using UnityEngine;

public class DeathRespawn : MonoBehaviour
{
    [Tooltip("Assign a Transform from the Scene (not a prefab).")]
    public Transform respawnPoint;

    [Tooltip("Optional: movement scripts to disable while respawning (e.g. PlayerController).")]
    public MonoBehaviour[] disableOnRespawn;

    Rigidbody rb;
    CharacterController cc;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Killer"))
        {
            Debug.Log($"Killed by {collision.gameObject.name} (Collision) on {gameObject.name}");
            Respawn();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Killer"))
        {
            Debug.Log($"Killed by {other.gameObject.name} (Trigger) on {gameObject.name}");
            Respawn();
        }
    }

    void Respawn()
    {
        if (respawnPoint == null)
        {
            Debug.LogWarning("Respawn Point not assigned in Inspector!");
            return;
        }

        Vector3 target = respawnPoint.position;
        Debug.Log($"Respawning {gameObject.name} to {target} (before: {transform.position})");


        if (disableOnRespawn != null)
        {
            foreach (var m in disableOnRespawn)
                if (m != null) m.enabled = false;
        }

        if (cc != null)
        {
            cc.enabled = false;
            transform.position = target;
            cc.enabled = true;
        }
        else if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
#if UNITY_2020_1_OR_NEWER
            rb.angularVelocity = Vector3.zero;
#endif

            rb.position = target;
            transform.position = target;
        }
        else
        {
            transform.position = target;
        }


        if (animator != null && animator.applyRootMotion)
        {
            animator.applyRootMotion = false;

        }

        if (disableOnRespawn != null)
        {
            foreach (var m in disableOnRespawn)
                if (m != null) m.enabled = true;
        }

        Debug.Log($"Respawn complete. New position: {transform.position}");
    }
}