using SoundControl;
using UnityEngine;

public class Icon : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public void TogglePanel()
    {
        SoundEffectManager.Play("sfx_click");
        panel.SetActive(!panel.activeSelf);
    }
}