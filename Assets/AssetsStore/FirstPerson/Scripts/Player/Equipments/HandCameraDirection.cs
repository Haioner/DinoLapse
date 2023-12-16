using UnityEngine;


public enum AxisToRotate
{
    X,Y,Z
}

public class HandCameraDirection : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float numberDifference = 107f;
    [SerializeField] private AxisToRotate axisToRotate = AxisToRotate.Z;

    void Update()
    {
        FollowCamera();
    }

    private void FollowCamera()
    {
        float cameraRotationX = target.eulerAngles.x;
        switch (axisToRotate)
        {
            case AxisToRotate.X:
                transform.rotation = Quaternion.Euler(cameraRotationX + numberDifference, transform.eulerAngles.y, transform.eulerAngles.z);
                break;
            case AxisToRotate.Y:
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, cameraRotationX + numberDifference, transform.eulerAngles.z);
                break;
            case AxisToRotate.Z:
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, cameraRotationX + numberDifference);
                break;
        }
    }
}
