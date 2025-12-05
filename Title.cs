using UnityEngine;
using TMPro;

public class TitleJitter : MonoBehaviour
{
    public TMP_Text titleText;          // drag your title TMP_Text here
    public TMP_FontAsset[] fonts;       // drag multiple fonts into this array
    public float interval = 0.03f;      // how fast to swap (seconds)

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            int randomIndex = Random.Range(0, fonts.Length);
            titleText.font = fonts[randomIndex];
        }
    }
}
