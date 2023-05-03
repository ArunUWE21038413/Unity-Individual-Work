using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Camera_Manager : MonoBehaviour
{
    private Vector3 cameraFollowVelocity = Vector3.zero;

    Input_Manager input_Manager;

    public Transform targetTransform;

    public float cameraFollowSpeed = 0.2f;

    public float cameraPivotSpeed = 2;

    public Transform cameraPivot;

    public float minimumPivotAngle = -25;

    public float maximumPivotAngle = -25;

    public float cameraLookSpeed = 2;

    private float defaultPosition;

    private Vector3 cameraVectorPosition;

    public Transform cameraTransform;

    public float cameraCollisionradius = 2;

    public LayerMask collisionLayers;

    public float cameraCollisionOffset = 0.2f;

    public float minimumCollisionOffset = 0.2f;


    public float lookAngle;  //To get Camera to look up and down
    public float pivotAngle; //To get Camera to look left and right

    private void Awake()
    {
        input_Manager = FindObjectOfType<Input_Manager>();
        targetTransform = FindObjectOfType<Player_Manager>().transform;
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
    }

    public void FollowTarget() 
    {
        Vector3 targetPosition = Vector3.SmoothDamp
            (transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);

        transform.position = targetPosition;
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        //HandleCameraCollisions();
    }

    private void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetrotation;

        lookAngle = lookAngle + (input_Manager.cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle - (input_Manager.cameraInputY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetrotation = Quaternion.Euler(rotation);
        transform.rotation = targetrotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetrotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetrotation;
    }

    public void HandleCameraCollisions() 
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast
            (cameraPivot.transform.position, cameraCollisionradius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers)) 
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition = - (distance - cameraCollisionOffset);
        }

        if(Mathf.Abs(targetPosition) < minimumCollisionOffset) 
        {
            targetPosition = targetPosition - minimumCollisionOffset;
        }

        //cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    }
}
