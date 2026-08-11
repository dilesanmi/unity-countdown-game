using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CleanupTask : Task
{

    [SerializeField] private GameObject iconPrefab;

    [Header("Icon Spawn Config")]
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;
    [SerializeField] private int minIcons;
    [SerializeField] private int maxIcons;
    [SerializeField] private float radius = 2f;

    [Header("UI")]
    public TMP_Text instructionText;
    public TMP_Text statusText;

    public int count;
    public int numIcons;
    private List<GameObject> icons;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        SetStatusText();  
    }

    public override void SetUpTask()
    {
        icons = new List<GameObject>();
        numIcons = Random.Range(minIcons, maxIcons);

        SpawnIcons();
        Debug.Log("Spawn complete");
    }

    public override void OnTaskComplete()
    {
        isComplete = true;
    }

    private void SpawnIcons()
    {
        

        for (int i = 0; i < numIcons; i++)
        {


            float x = Random.Range(minBounds.x, maxBounds.x);
            float y = Random.Range(minBounds.y, maxBounds.y);

            Vector3 position= new Vector3(x, y, 99);

            GameObject icon = Instantiate(iconPrefab, position, Quaternion.identity);
            icons.Add(icon);

            
        }

    }

    public void SetStatusText()
    {
        statusText.text = "Icons Remaining: " + numIcons.ToString();

        if (numIcons == 0)
        {
            Debug.Log("You win the task");
            OnTaskComplete();
        }
    }
}
