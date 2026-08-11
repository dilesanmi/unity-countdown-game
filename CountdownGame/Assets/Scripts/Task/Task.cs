using UnityEngine;

public  abstract class Task : MonoBehaviour
{
    public string name;
    string instruction;
    public bool isComplete;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void SetUpTask();

    public abstract void OnTaskComplete();
    
}
