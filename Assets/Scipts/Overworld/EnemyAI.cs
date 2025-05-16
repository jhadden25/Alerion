using UnityEngine;
using System.Collections;
public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 1.0F;
    [SerializeField] private float rotateSpeed = 0.3F;
    public bool inCombat = false;

    private bool isMoving = false;
    private float randomAngle;
    private void Start()
    {
    }

    private void Update()
    {
        if (!isMoving)
            Movement();
    }

    private void Movement()
    {
        isMoving = true;
        StartCoroutine(MovementRoutine());
    }

    private IEnumerator MovementRoutine()
    {
        // Step 1: Rotate to a random direction
        randomAngle = Random.Range(0f, 360f);
        Quaternion targetRotation = Quaternion.Euler(0, randomAngle, 0);

        // Gradually rotate to the target rotation
        float rotationProgress = 0f;
        while (rotationProgress < 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed);
            rotationProgress += Time.deltaTime;
            yield return null;
        }

        // Ensure we're exactly at the target rotation
        transform.rotation = targetRotation;

        // Step 2: Walk forward for 4 seconds
        float timer = 0f;
        while (timer < 4f)
        {
            transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        // Step 3: Turn around (rotate 180 degrees)
        targetRotation = Quaternion.Euler(0, randomAngle + 180f, 0);
        rotationProgress = 0f;
        while (rotationProgress < 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed);
            rotationProgress += Time.deltaTime;
            yield return null;
        }

        // Ensure we're exactly at the target rotation
        transform.rotation = targetRotation;

        // Step 4: Walk back for 4 seconds
        timer = 0f;
        while (timer < 4f)
        {
            transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        // Movement sequence complete
        isMoving = false;
    }

}
