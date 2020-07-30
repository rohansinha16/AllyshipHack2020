using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Task : ScriptableObject
{

    
    public string Name;
    public int Level;
    public bool Assigned = false;
    public bool DependentCompleted = false;
    private int Completion = 0; // 100 is complete
    public bool Completed = false;
    public List<Task> DependentTasks { get; set; }


    public void AssignTask(string _name, List<Task> _dependentTasks, int _level)
    {
        Name = _name;
        DependentTasks = _dependentTasks;
        Level = _level;
        if(_dependentTasks.Count == 0)
        {
            DependentCompleted = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        List<Task> completedTasks = new List<Task>();
        foreach(Task task in DependentTasks)
        {
            if (task.Completed)
            {
                completedTasks.Add(task);
            }
        }
        foreach(Task task in completedTasks)
        {
            DependentTasks.Remove(task);
        }
        if(DependentTasks.Count == 0)
        {
            DependentCompleted = true;
        }
    }

    /* 
     * Do work on a task given the member who's working on it's morale.
     * Called once per second by Employee update function
     */
    public bool Work(int morale)
    {
        if (DependentCompleted && !Completed)
        {
            // task progress = morale/5 (=1 if progress is <1)
            if(morale > 100 || morale < 0)
            {
                throw new System.Exception("Morale should not exceed 100 or be below 0. Morale was " + morale + ".");
            }
            int progress = morale / 5;
            if (progress <= 1)
            {
                progress = 1;
            }
            Completion += progress;
            if(Completion >= 100)
            {
                Completion = 100;
                Completed = true;
                Revenue.CompleteTask();
            }
        }
        return Completed;
    }

}
