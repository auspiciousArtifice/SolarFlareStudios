using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink_Effect : MonoBehaviour
{
    // Start is called before the first frame update
    public float blink_speed = .05f;
    private bool subtract = true;
    private float r;
    private float b;
    private float g;

    void Start()
    {
        Text text = this.gameObject.GetComponent<Text>();
        if (text != null)
        {
            r = text.color.r;
            b = text.color.b;
            g = text.color.g;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Text text = this.gameObject.GetComponent<Text>();
        if (text != null)
        {
            if (subtract)
            {
                text.color = new Color(r, b, g, text.color.a - blink_speed);
            } else
            {
                text.color = new Color(r, b, g, text.color.a + blink_speed);
            }
            if (text.color.a <= .25 || text.color.a >= 1)
            {
                subtract = !subtract;
            }
            
        }
    }
}
