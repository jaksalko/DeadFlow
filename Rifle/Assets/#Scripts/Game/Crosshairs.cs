using UnityEngine;
using System.Collections;

public class Crosshairs : MonoBehaviour
{

    public LayerMask targetMask;
    public SpriteRenderer dot;
    public Color dotHighlightColour;
    SpriteRenderer crossSprite;
    Color originalDotColour;

    void Start()
    {
        Cursor.visible = false;
        crossSprite = GetComponent<SpriteRenderer>();
        originalDotColour = dot.color;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * -40 * Time.deltaTime);
    }

    public void DetectTargets(Ray ray)
    {
        if (Physics.Raycast(ray, 100, targetMask))
        {
            crossSprite.color = dotHighlightColour;
            dot.color = dotHighlightColour;
        }
        else
        {
            crossSprite.color = Color.black;
            dot.color = originalDotColour;
        }
    }
}