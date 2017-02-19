using UnityEngine;
using System.Collections;

public class Hilbert : MonoBehaviour {

    public int n;
    public int initialRotation;
    public const int
        UP = 0,
        RIGHT = 1,
        DOWN = 2,
        LEFT = 3;

	// Use this for initialization
	void Start () {
        Generate(n, 0, 0, initialRotation);
	}

    void Generate(int n, int x, int y, float r)
    {
        if(r < 0) { r += 4; }
        if(r >= 4) { r -= 4; }
        if(n == 0) { return; }
        int s = (int)Mathf.Pow(2, n - 1) - 1; // width of *smaller* curve
        int o = s + 1;

        int w = (int)Mathf.Pow(2, n) - 1; // width of *this* curve

        Vector2 up = new Vector2(0, 1);
        Vector2 right = new Vector2(1, 0);

        if (r == UP)
        {
            Generate(n - 1, x, y + o, r); // top left
            Generate(n - 1, x + o, y + o, r); // top right
            Generate(n - 1, x, y, r + 1); // bottom left
            Generate(n - 1, x + o, y, r - 1); // bottom right

            Line(new Vector2(x, y + s), up); // left line
            Line(new Vector2(x + s, y + o), right); // mid line
            Line(new Vector2(x + w, y + o), -up); // right line
        }

        else if (r == RIGHT)
        {
            Generate(n - 1, x, y + o, r + 1); // top left
            Generate(n - 1, x + o, y + o, r); // top right
            Generate(n - 1, x, y, r - 1); // bottom left
            Generate(n - 1, x + o, y, r); // bottom right

            Line(new Vector2(x + o, y + w), -right); // top line
            Line(new Vector2(x + o, y + o), -up); // mid line
            Line(new Vector2(x + o, y), -right); // bottom line
        }

        else if (r == DOWN)
        {
            Generate(n - 1, x, y + o, r - 1); // top left
            Generate(n - 1, x + o, y + o, r + 1); // top right
            Generate(n - 1, x, y, r); // bottom left
            Generate(n - 1, x + o, y, r); // bottom right

            Line(new Vector2(x + w, y + s), up); // right line
            Line(new Vector2(x + o, y + s), -right); // mid line
            Line(new Vector2(x, y + s), up); // left line
        }

        else if(r == LEFT)
        {
            Generate(n - 1, x, y + o, r); // top left
            Generate(n - 1, x + o, y + o, r - 1); // top right
            Generate(n - 1, x, y, r); // bottom left
            Generate(n - 1, x + o, y, r + 1); // bottom right

            Line(new Vector2(x + s, y), right); // bottom line
            Line(new Vector2(x + s, y + s), up); // mid line
            Line(new Vector2(x + s, y + w), right); // top line
        }
        
    }

    void Line(Vector2 start, Vector2 length)
    {
        GameObject go = new GameObject();
        LineRenderer r = go.AddComponent<LineRenderer>();
        r.SetPositions(new Vector3[]{start, start + length});
        r.SetWidth(0.1f, 0.1f);
    }
}
