using UnityEngine;

public class MoveTween : MonoBehaviour
{

    [SerializeField] Transform targetTransform;
    [SerializeField] GameObject target;
    [SerializeField] float duration;
    [SerializeField] LeanTweenType easeType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move()
    {
        LeanTween.moveX(gameObject, 233, duration).setEase(easeType);
    }
}
