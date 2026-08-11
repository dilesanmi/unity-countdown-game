using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Photo", menuName = "Albums/Photo Data")]
public class PhotoData : ScriptableObject
{
    public Sprite photoImage;
    public AlbumCategory category;
}
