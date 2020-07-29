using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee
{
    private string Name;
    private string ImagePath;
    private int Morale = 50; // between 0 and 100
    private int locationX;
    private int locationY;
    private Queue<Task> TaskQueue = new Queue<Task>();
    private double updateInterval = 1;

    public Employee(string _name, string _imagePath)
	{
        Name = _name;
        ImagePath = _imagePath;
	}

    void Update()
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
