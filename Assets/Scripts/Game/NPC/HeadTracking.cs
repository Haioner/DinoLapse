using UnityEngine.Animations.Rigging;
using UnityEngine;

public class HeadTracking : MonoBehaviour
{
    [Header("CACHE")]
    [SerializeField] private Transform targetHead;
    [SerializeField] private Transform playerPos;
    [SerializeField] private MultiAimConstraint constraint;

    [Header("Values")]
    [SerializeField] private float radiusInteresting = 5f;
    [SerializeField, Range(1f, 10f)] private float lerpSpeed = 5f;
    [SerializeField] private float minAngle = -110f;
    [SerializeField] private float maxAngle = 110f;

    private void Update()
    {
        CalculateDistance();
    }

    private void CalculateDistance()
    {
        Vector3 toPlayer = playerPos.position - transform.position;
        float distance = toPlayer.magnitude;
        float angle = Vector3.SignedAngle(transform.forward, toPlayer, transform.up);

        float weightTarget = CalculateWeight(distance, angle);
        constraint.weight = Mathf.Lerp(constraint.weight, weightTarget, lerpSpeed * Time.deltaTime);

        HandleTargetPosition(angle);
    }

    private float CalculateWeight(float distance, float angle)
    {
        return (distance <= radiusInteresting && angle >= minAngle && angle <= maxAngle) ? 1f : 0f;
    }

    private void HandleTargetPosition(float angle)
    {
        if (constraint.weight > 0.5f && (angle < minAngle || angle > maxAngle))
        {
            float smoothWeight = Mathf.Lerp(constraint.weight, 0f, lerpSpeed * Time.deltaTime);
            constraint.weight = smoothWeight;
        }

        if (constraint.weight > 0.5f)
        {
            MoveTargetHead();
        }
    }

    private void MoveTargetHead()
    {
        targetHead.position = playerPos.position;
    }
}
