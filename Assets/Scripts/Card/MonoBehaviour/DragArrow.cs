using UnityEngine;

public class DragArrow : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 mousePos;
    public int pointCount; //number of points in the curve
    public float arcModifier;

    private void Awake() 
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update() 
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        SetArrowPosition();
    }

    public void SetArrowPosition()
    {
        Vector3 cardPosition = transform.position;
        Vector3 normalizedDirection = (mousePos - cardPosition).normalized;

        Vector3 perpendicularDirection = new (-normalizedDirection.y, normalizedDirection.x, normalizedDirection.z);
        Vector3 offset = perpendicularDirection * arcModifier;
        Vector3 controlPoint = (cardPosition + mousePos) / 2 + offset;
        lineRenderer.positionCount = pointCount;
        for(int i = 0; i < pointCount; i++)
        {
            float t = i / (float)(pointCount - 1);
            Vector3 point = CalculateQuadraticBezierPoint(t, cardPosition, controlPoint, mousePos);
            lineRenderer.SetPosition(i, point);
        }
    }


    private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0; //first term
        p += 2 * u * t * p1; //second term
        p += tt * p2; //third term
        return p;
    }
}
