using System.Collections;
using TMPro;
using UnityEngine;

public class TextFadeOut : MonoBehaviour
{
    private float textShownTime = 3f;

    private void Start()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(textShownTime);
        this.GetComponent<TextMeshProUGUI>().CrossFadeColor(new Vector4(255, 255, 255, 0), 1f, false, true);
    }
}
