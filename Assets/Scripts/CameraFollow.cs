using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    private new Camera camera;
    private new Transform transform;

    public Transform target;
    public BoxCollider2D constrainArea;

    [Range(0.75f, 1)] public float smoothing = 0.85f;

    private Vector3 boundsMin, boundsMax;

    private void Start()
    {
        camera = GetComponent<Camera>();
        transform = gameObject.transform;

        GameObject taggedTarget = GameObject.FindGameObjectWithTag("Camera Target");
        if (!target || taggedTarget) target = taggedTarget.transform;

        Bounds b = constrainArea.bounds;
        boundsMin = b.min;
        boundsMax = b.max;

        constrainArea.transform.parent = null;
        constrainArea.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        Vector3 currentPos = transform.position;
        Vector3 targetPos = target.position;

        // TODO Make it frame independent
        float x = Mathf.Lerp(currentPos.x, targetPos.x, smoothing);
        float y = Mathf.Lerp(currentPos.y, targetPos.y, smoothing);


        float cameraHalfWidth = camera.orthographicSize * ((float)(Screen.width) / Screen.height);
        targetPos.x = Mathf.Clamp(x, boundsMin.x + cameraHalfWidth, boundsMax.x - cameraHalfWidth);
        targetPos.y = Mathf.Clamp(y, boundsMin.y + camera.orthographicSize, boundsMax.y - camera.orthographicSize);
        targetPos.z = -10;
        transform.position = targetPos;
    }

}
