using UnityEngine;
using UnityEngine.Events;

public class ScaleTween : MonoBehaviour
{
    public UnityEvent onCompleteCallback;

    public void OnEnable()
    {
        transform.localScale = new Vector2(0, 0);

        CanvasGroup group = GetComponent<CanvasGroup>();
        group.alpha = 0.2f;

        LeanTween.scale(gameObject, new Vector2(1, 1), 0.1f);
        LeanTween.alphaCanvas(group, 1, 0.1f);
    }

    public void OnDisable()
    {
        CanvasGroup group = GetComponent<CanvasGroup>();
        group.alpha = 1f;

        LeanTween.alphaCanvas(group, 0.2f, 0.2f);

        LeanTween.scale(gameObject, new Vector2(0, 0), 0.1f);
    }

    public void OnComplete()
    {
        if (onCompleteCallback != null)
        {
            onCompleteCallback.Invoke();
        }
    }

    // When the close button is pressed
    public void OnClose()
    {
        LeanTween.scale(gameObject, new Vector2(0, 0), 0.5f)
                 .setOnComplete(DestroyMe);
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }
}