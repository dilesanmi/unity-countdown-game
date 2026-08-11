using Relationship;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource notification;

    [Header("References")]
    [SerializeField] private RelationshipManager relationshipManager;
    [SerializeField] private GameManager gameManager;

    [Header("Pitch")]
    [SerializeField] private float normalPitch = 1f;
    [SerializeField] private float maxPitch = 3f;

    [Header("Thresholds")]
    [SerializeField] private float lowTimeThreshold = 60f;
    [SerializeField] private float lowAffectionThreshold = 150f;

    void Update()
    {
        float targetPitch = normalPitch;

        //if (gameManager.timer <= lowTimeThreshold)
        //{
        //    float t = 1f - (gameManager.timer / lowTimeThreshold);
        //    targetPitch = Mathf.Max(targetPitch,
        //        Mathf.Lerp(normalPitch, maxPitch, t));
        //}

        if (relationshipManager.currentAffection <= lowAffectionThreshold)
        {
            float t = 1f - ((float)relationshipManager.currentAffection / lowAffectionThreshold);
            targetPitch = Mathf.Max(targetPitch,
                Mathf.Lerp(normalPitch, maxPitch, t));
        }

        music.pitch = Mathf.Lerp(
            music.pitch,
            targetPitch,
            Time.deltaTime * 3f);
    }

    public void PlayNotification()
    {
        notification.Play();
    }
}