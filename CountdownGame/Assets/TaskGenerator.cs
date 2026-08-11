using System.Collections.Generic;
using Message;
using UnityEngine;

public class TaskGenerator : MonoBehaviour
{

    [SerializeField] private TaskManager taskManager;


    [Header("Possible Tasks")]
    [SerializeField] private List<GameObject> availableTasks;

    [SerializeField] private List<TaskData> designatedTasks;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GenerateTasks(int numTasks)
    {

        for (int i= 0; i < numTasks; i++){

            TaskData randomTask =
                        designatedTasks[Random.Range(0, availableTasks.Count)];

            taskManager.AddTask(randomTask);
            Debug.Log("Task Added");
        }
        Debug.Log("Tasks successfully generated");

        return true; 
    }
}
