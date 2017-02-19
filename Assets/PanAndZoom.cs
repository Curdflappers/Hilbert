using UnityEngine;

/// <summary>
/// Makes the camera pan and zoom around the mouse.
/// Right-click and drag to pan
/// Scroll to zoom
/// </summary>
public class PanAndZoom : MonoBehaviour
{
    private Vector3 Origin;
    private const float DELTA_SCROLL = 4,
        MAX_SIZE = 128,
        MIN_SIZE = 4;

    void Update()
    {
        Zoom();
        Pan();
    }

    void Pan()
    {
        if (Input.GetMouseButtonDown(1)) { Origin = MousePos(); }
        else if (Input.GetMouseButton(1))
        {
            transform.position += Origin - MousePos();
        }
    }

    void Zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float size = Camera.main.orthographicSize;
        if (scroll > 0)
        {
            Camera.main.orthographicSize =
                Mathf.Max(MIN_SIZE, size - DELTA_SCROLL);
        }
        else if (scroll < 0)
        {
            Camera.main.orthographicSize =
                Mathf.Min(MAX_SIZE, size + DELTA_SCROLL);
        }
    }

    public static Vector3 MousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}