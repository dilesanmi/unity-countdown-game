public enum TabType
{
    ALBUM,
    EMAIL,
    VERIFY
}

[System.Serializable]
public class TaskData
{
    public string taskName;
    public float startTime;
    public float dueTime;
    public TabType taskTab;
    public bool isActive;
    public bool isComplete;
    public bool isFailed;

    public TaskData (string taskName, float startTime, float dueTime, TabType taskTab)
    {
        this.taskName = taskName;
        this.startTime = startTime;
        this.dueTime = dueTime;
        this.taskTab = taskTab;
    }
}