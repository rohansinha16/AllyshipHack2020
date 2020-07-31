using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : ScriptableObject
{
    public string Name;
    private int Morale = 50; // between 0 and 100
    public Queue<Task> TaskQueue = new Queue<Task>();
    private double updateInterval = 1;

    public void AssignEmployee(string _name)
    {
        Name = _name;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Update()
    {
        updateInterval -= Time.deltaTime;
        if (updateInterval <= 0)
        {
            if (Morale < 20)
            {
                // random chance to quit?
            }
            if (TaskQueue.Count != 0)
            {
                Task cur = TaskQueue.Peek();
                bool isComplete = cur.Work(Morale);
                if (isComplete)
                {
                    TaskQueue.Dequeue();
                }
            }
            updateInterval = 1;
        }
        // draw employee image at locationX locationY
       
    }

    public void AssignTask(Task t)
    {
        if (!t.Assigned)
        {
            TaskQueue.Enqueue(t);
            t.Assigned = true;
        }
    }

}
