using UnityEngine;
using TMPro;

public class BlinkText : MonoBehaviour
{
    public float blinkSpeed = 3f; // más bajo = parpadeo más lento
    private TMP_Text textComponent;

    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    void Update()
    {
        float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1f);
        Color color = textComponent.color;
        color.a = alpha;
        textComponent.color = color;
    }
}
