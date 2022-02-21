using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField] private SpriteRenderer backgroundSpriteRenderer;
    [SerializeField] private SpriteRenderer iconSpriteRenderer;
    [SerializeField] private TextMeshPro textMeshPro;

    // the person the speach bubble is pointing at.
    [SerializeField] private Transform target;

    /// <summary>
    /// Sets the text of the Speech Bubble to the given text.
    /// </summary>
    /// <param name="text"></param>
    private void SetText(string text)
    {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate(); // to avoid text textSize returning 0 becase mesh has not updated yet.
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(4f,7f);
        backgroundSpriteRenderer.size = textSize + padding;

        // set the background positon
        Vector3 backgroundOffset = new Vector3(-5f, 0); 
        backgroundSpriteRenderer.transform.localPosition = new Vector3(backgroundSpriteRenderer.size.x * 0.5f, 0f) + backgroundOffset;

        // set the icon postion in the middle of the background
        Vector3 iconOffset = new Vector3(0, 1.7f);
        iconSpriteRenderer.transform.localPosition = (backgroundSpriteRenderer.transform.localPosition * 0.5f) + iconOffset;
    }

    private void SetIcon(Sprite icon)
    {
        iconSpriteRenderer.sprite = icon;
    }


    public void UpdateSpeechBouble(Sprite icon, string text, Transform target)
    {
        this.target = target;
        SetIcon(icon);
        SetText(text);
    }

    private void Update()
    {
        if(target == null)
            return;

        Vector3 moddedPostion = this.transform.position;
        moddedPostion.y = target.transform.position.y;

        Quaternion lookRotation = Quaternion.LookRotation(moddedPostion - target.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5f * Time.deltaTime);
    }
}
