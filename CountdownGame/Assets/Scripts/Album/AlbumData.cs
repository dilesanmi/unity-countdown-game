using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Album", menuName = "Albums/Album Data")]
public class AlbumData : ScriptableObject
{
    [Header("Album Info")]
    public string albumName;
    public AlbumCategory category;
    
    [Header("Photos in this Album")]
    public List<PhotoData> photos = new List<PhotoData>();
}

public enum AlbumCategory
{
    Animal,
    Food,
    Landscape,
    Work
}