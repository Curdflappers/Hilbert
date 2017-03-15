using UnityEngine;
using System.Collections;

/// <summary>
/// Generate a Hilbert Curve attached to whatever gameobject this is attached to
/// Mostly an exercise in direction, recursion, and programming
/// </summary>
public class Hilbert : MonoBehaviour {

    public int size;
    public int initialRotation;
    private Vector3[] points;
    private int i;

    public const int
        UP = 0,
        RIGHT = 1,
        DOWN = 2,
        LEFT = 3;

	// Use this for initialization
	void Start () {
        points = new Vector3[(int)Mathf.Pow(4, size)];
        i = 0;
        Generate(size, 0, 0, initialRotation);
	}

    void Generate(int n, int x, int y, int r)
    {
        if(n == 0)
        {
            points[i++] = new Vector2(x, y);
            return;
        }

        int o = (int)Mathf.Pow(2, n - 1); // width of *smaller* curve

        switch (r) {
            case (UP):
                Generate(n - 1, x, y, RIGHT); // bottom left
                Generate(n - 1, x, y + o, UP); // top left
                Generate(n - 1, x + o, y + o, UP); // top right
                Generate(n - 1, x + o, y, LEFT); // bottom right
                break;
            case (RIGHT):
                Generate(n - 1, x, y, UP); // bottom left
                Generate(n - 1, x + o, y, RIGHT); // bottom right
                Generate(n - 1, x + o, y + o, RIGHT); // top right
                Generate(n - 1, x, y + o, DOWN); // top left
                break;
            case (DOWN):
                Generate(n - 1, x + o, y + o, LEFT); // top right
                Generate(n - 1, x + o, y, DOWN); // bottom right
                Generate(n - 1, x, y, DOWN); // bottom left
                Generate(n - 1, x, y + o, RIGHT); // top left
                break;
            case (LEFT):
                Generate(n - 1, x + o, y + o, DOWN); // top right
                Generate(n - 1, x, y + o, LEFT); // top left
                Generate(n - 1, x, y, LEFT); // bottom left
                Generate(n - 1, x + o, y, UP); // bottom right
                break;
        }

        if (n == size) { CreateObject(); }
    }

    private void CreateObject()
    {
        GameObject go = new GameObject();
        LineRenderer render = go.AddComponent<LineRenderer>();
        render.SetVertexCount(points.Length);
        render.SetPositions(points);
        render.SetWidth(0.1f, 0.1f);
    }
}
