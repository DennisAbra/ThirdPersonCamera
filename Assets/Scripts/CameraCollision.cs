using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public float minDist = 1.0f;
    public float maxDist = 1.0f;
    public float smooth = 10.0f;
    public float dist;

    public Vector3 dollyDirAdjusted;
    
    Vector3 dollyDir;
    CameraZoom camZoom;

    private void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        dist = transform.localPosition.magnitude;
        camZoom = GetComponent<CameraZoom>();
    }

    private void Update()
    {
        Vector3 desiredCamPos = transform.parent.TransformPoint(dollyDir * camZoom.currentZoom);
        RaycastHit hit;

        Debug.DrawLine(transform.parent.position, desiredCamPos);

        if(Physics.Linecast(transform.parent.position, desiredCamPos, out hit))
        {
            dist = Mathf.Clamp((hit.distance * 0.9f), minDist, maxDist);
        }
        else
        {
            dist = camZoom.currentZoom;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * dist, Time.deltaTime * smooth);
    }
}
