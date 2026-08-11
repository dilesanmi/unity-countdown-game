using System.Collections;
using System.Collections.Generic;
using SoundControl;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    private GameManager gameManager;

    [Header("UI")]
    [SerializeField] private TMP_Text tasksLeftText;
    [SerializeField] private GameObject taskNotification;

    [Header("Parameters")]
    public int failCount;
    public int maxFailures=1;
    public int notificationDisplayLength;
    private int tasksLeft;
    float initialX;
    [SerializeField] LeanTweenType easeType;


    private List<TaskData> designatedTasks=new();


    void Start()
    {
        GameObject GameController = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = GameController.GetComponent<GameManager>();

        //Set up tasks for this level or whatever
        TaskData testTask= new TaskData("eat",2.00f,120.00f,TabType.ALBUM);
        TaskData testTaskB = new TaskData("whine", 12.00f, 120.00f, TabType.ALBUM);
        designatedTasks.Add(testTask);
        designatedTasks.Add(testTaskB);

        tasksLeft = designatedTasks.Count;

        initialX = taskNotification.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (TaskData task in designatedTasks)
        {
            //Activate tasks when it's time to be assigned
            if (gameManager.timer > task.startTime && !task.isActive)
            {
                task.isActive = true;
                Debug.Log("NEEWWWWWW TASK! :>");
                StartCoroutine(ShowTaskNotification());


            }
            //Complete tasks
            if (task.isComplete)
            {
                SoundEffectManager.Play("sfx_taskcomplete");
                designatedTasks.Remove(task);
                tasksLeft--;
                Debug.Log("Done task!");
            }
            //Fail tasks
            if (task.isFailed)
            {
                designatedTasks.Remove(task);
                failCount++;
            }
        }

        //Win and lose
        if (tasksLeft <= 0){

            gameManager.GameWon();
        }
        if (failCount >= maxFailures)
        {
            gameManager.GameOver();
        }
        SetTasksLeft();
    }

    //NOT CURRENTLY USED BUT SHOULD BE TBH
    public void AddTask(TaskData taskData)
    {
        designatedTasks.Add(taskData);
    }

    public TaskData GetActiveTasks(TabType tab)
    {
        return designatedTasks.Find(t => t.taskTab == tab && t.isActive);
    }

    //NOT CURRENTLY USED BUT SHOULD BE TBH
    public void CompleteTask(TaskData task)
    {
        task.isComplete = true;
        designatedTasks.Remove(task);
        Debug.Log(task.taskName + " completed!");    
    }

    private void SetTasksLeft()
    {
        tasksLeftText.text = "Tasks Left:" + tasksLeft;
    }

    private IEnumerator ShowTaskNotification()
    {
        


        LeanTween.moveX(taskNotification, 5, 0.5f).setEase(easeType);
        yield return new WaitForSeconds(notificationDisplayLength);
        LeanTween.moveX(taskNotification,initialX, 0.5f).setEase(easeType);

    }
}
