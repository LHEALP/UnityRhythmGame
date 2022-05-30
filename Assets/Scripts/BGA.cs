using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGA : MonoBehaviour
{
    public float speed1 = 0.2f;
    public float speed2 = 1.2f;
    
    Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        StartCoroutine(IEParty());
    }

    IEnumerator IEParty()
    {
        float t = 0f;
        float speed = Random.Range(speed1, speed2);

        Color s = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        Color d = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        Color i = Calc(s, d);

        while (true)
        {
            if (image != null)
                image.color = new Color(s.r + i.r * t, s.g + i.g * t, s.b + i.b * t);

            t += Time.deltaTime * speed;
            if (t > 1f)
            {
                t = 0f;
                speed = Random.Range(speed1, speed2);
                s = d;
                d = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                i = Calc(s, d);
            }
            yield return null;
        }
    }

    Color Calc(Color start, Color dest)
    {
        return new Color((dest.r - start.r), (dest.g - start.g), (dest.b - start.b), 255);
    }
}
