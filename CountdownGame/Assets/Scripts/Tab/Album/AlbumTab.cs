using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AlbumTab : MonoBehaviour
{
    [SerializeField] private TaskManager taskManager;

    [Header("Prefabs")]
    [SerializeField] private GameObject photoPrefab;
    [SerializeField] private GameObject albumPrefab;

    [Header("UI")]
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private TMP_Text incorrectSortsText;

    [SerializeField] private List<PhotoData> availablePhotos;

    [Header("Parameters")]
    public int numPhotos=10;
    public int numAlbums = 2;
    public int maxWrongPlaces;
    public int photosLeft;
    public int wrongPlaces;
    bool taskSet;

    private GameObject currentPhotoObject;
    private TaskData currentTask;

    private List<AlbumCategory> chosenCategories = new();
    private Queue<PhotoData> chosenPhotos = new();
    private List<GameObject> albumObjects = new();
 
    // Update is called once per frame
    void Update()
    {
        SetText();

        if (photosLeft == 0 && taskSet)
        {
            Debug.Log("You win the task");
            OnTaskComplete();
        }
        if (wrongPlaces >= maxWrongPlaces)
        {
            Debug.Log("You somehow failed the task?");
            OnTaskFailed();
        }
        if (currentPhotoObject == null && taskSet)
        {
            SpawnPhoto();
        }
    }

    private void OnEnable()
    {
        currentTask = taskManager.GetActiveTasks(TabType.ALBUM);

        if (currentTask != null && !taskSet)
        {
            statusText.text = "";
            Debug.Log("Setting up task");
            SetUpTask();
        }
    }

    private void SetUpTask()
    {
        taskSet = true;
        photosLeft = numPhotos;
        SetAlbumFolders();
        GeneratePhotoList();
    }

    private void SetAlbumFolders()
    {
        List<AlbumCategory> categories = System.Enum.GetValues(typeof(AlbumCategory))
            .Cast<AlbumCategory>()
            .ToList();

        // Shuffle categories
        for (int i = 0; i < categories.Count; i++)
        {
            int random = Random.Range(i, categories.Count);
            (categories[i], categories[random]) = (categories[random], categories[i]);
        }

        // Take the first numAlbums unique categories
        for (int i = 0; i < numAlbums; i++)
        {
            chosenCategories.Add( categories[i]);

            //Create album folder object
            GameObject albumObject = Instantiate(albumPrefab, this.transform);
            albumObject.transform.SetLocalPositionAndRotation(new Vector2((150*i)-100,100), Quaternion.identity);

            AlbumFolder curAlbumFolder = albumObject.GetComponent<AlbumFolder>();
            curAlbumFolder.SetFolder(categories[i]);

            albumObjects.Add(albumObject);
        }

    }

    //Spawns a photo for the player to drag into a folder if there is no photo available
    private void GeneratePhotoList()
    {
        for (int i = 0; i < numPhotos; i++)
        {
            // Pick one of the active folder categories
            AlbumCategory category = chosenCategories[Random.Range(0, numAlbums)];

            List<PhotoData> validPhotos = availablePhotos.FindAll(p => p.category == category);

            PhotoData photo = validPhotos[Random.Range(0, validPhotos.Count)];

            chosenPhotos.Enqueue(photo);
        }
    }

    private void SpawnPhoto()
    {
        PhotoData currentPhotoData= chosenPhotos.Dequeue();

        currentPhotoObject = Instantiate(photoPrefab, this.transform);
        DraggablePhoto photoUI = currentPhotoObject.GetComponent<DraggablePhoto>();
        photoUI.SetPhoto(currentPhotoData);
    }

    public void OnTaskComplete()
    {
        currentTask.isComplete = true;
        ResetTask();
    }

    public void OnTaskFailed()
    {
        currentTask.isFailed= true;
        ResetTask();
    }

    public void ResetTask()
    {
        taskSet = false;

        foreach (GameObject album in albumObjects)
        {
            Destroy(album);
        }
        chosenPhotos.Clear();
        chosenCategories.Clear();
    }

    public void SetText()
    {
        incorrectSortsText.text = "Incorrect Sorts: " + wrongPlaces; 
        //statusText.text = "Photos Remaining: " + numPhotos;

        if (!taskSet)
        {
            statusText.text = "No files to sort!";
        }      
    }
}