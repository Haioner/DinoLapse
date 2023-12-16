using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [Header("CACHE")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private EnemyController enemy;
    private LayerMask layer = ~0;

    [Header("Values")]
    [SerializeField] private float radiusInteresting = 15f;
    [SerializeField] private float radiusNearDistance = 5f;
    [SerializeField] private float minAngle = -110f;
    [SerializeField] private float maxAngle = 110f;

    private void Update()
    {
        CheckTarget();
        CheckNearDistance();
        SetPitchMusicByDistance();
    }

    private void CheckTarget()
    {
        if (CheckFaceDistance() || CheckNearDistance())
            RayCastPlayer();
        else
        {
            transform.localRotation = Quaternion.identity;
            DisableTrigger();
        }
    }

    private void SetPitchMusicByDistance()
    {
        float maxDistance = 10f;
        float minPitch = 1f;
        float maxPitch = 1.5f;

        float pitchDistance = Vector3.Distance(transform.position, playerController.transform.position);
        float t = Mathf.InverseLerp(0f, maxDistance, pitchDistance);
        float pitch = Mathf.Lerp(maxPitch, minPitch, t);
        enemy.SetChaseMusicPitch(pitch);
    }


    private bool CheckNearDistance()
    {
        if (Vector3.Distance(transform.position, playerController.transform.position) <= radiusNearDistance
            && playerController.InputMovement.magnitude > 0
            && playerController.playerStance == Models.PlayerStance.Stand)
            return true;
        else
            return false;
    }

    private bool CheckFaceDistance()
    {
        Vector3 toPlayer = playerController.transform.position - transform.position;
        float distance = toPlayer.magnitude;
        float angle = Vector3.SignedAngle(transform.forward, toPlayer, transform.up);

        float weightTarget = CalculateWeight(distance, angle);
        if(weightTarget > 0.5f)
            return true;
        else
            return false;
    }

    private float CalculateWeight(float distance, float angle)
    {
        return (distance <= radiusInteresting && angle >= minAngle && angle <= maxAngle) ? 1f : 0f;
    }

    public void RayCastPlayer()
    {
        transform.LookAt(playerController.transform.position);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, transform.forward, out hit, 500, layer, QueryTriggerInteraction.Ignore))
        {
            Debug.DrawLine(transform.position, hit.point, Color.green);
            if (hit.transform.CompareTag("Player"))
                enemy.isTrigger = true;
            else
                enemy.isTrigger = false;
        }
    }

    private void DisableTrigger()
    {
        enemy.isTrigger = false;
        enemy.SetChaseMusicPitch(1);
    }
}
