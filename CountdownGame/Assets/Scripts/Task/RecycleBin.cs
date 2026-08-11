using Unity.VisualScripting;
using UnityEngine;

public class RecycleBin : MonoBehaviour
{

    [SerializeField] private CleanupTask cleanupManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Icon"))
        {
            Debug.Log("Icon detected!!!");
            other.gameObject.SetActive(false);
            cleanupManager.numIcons--;
            Destroy(other.gameObject);
   
        }
    }
}
