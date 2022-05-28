using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpeech : MonoBehaviour
{

    [SerializeField] GameObject speechBubblePrefab;
    GameObject speechBubbleObject = null;
    private void OnMouseDown()
    {
        if (speechBubbleObject != null)
        {
            CancelInvoke("DestroySpeechBubble");
            Destroy(speechBubbleObject);
            speechBubbleObject = null;
        }
        speechBubbleObject = Instantiate(speechBubblePrefab, gameObject.transform);
        Invoke("DestroySpeechBubble", 2);
    }

    void DestroySpeechBubble()
    {
        Destroy(speechBubbleObject);
        speechBubbleObject = null;
    }
}
