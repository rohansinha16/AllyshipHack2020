using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewBehaviourScript1 : MonoBehaviour
{
    List<Employee> Employees;
    Dictionary<string, Task> Tasks = new Dictionary<string, Task>();
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void readTaskList(string filename)
    {
        string[] lines = System.IO.File.ReadAllLines(filename);
        foreach (string line in lines)
        {
            string[] ids = line.Split(' ');
            Task[] dependencies = new Task[ids.Length - 1];
            if (dependencies.Length == 0)
            {
                if (Tasks.ContainsKey(ids[0]))
                {
                    throw new System.Exception("Cannot have tasks with identical ids.");
                }
                Tasks.Add(ids[0], new Task(ids[0], dependencies, 0));
            }
            else
            {
                int level = 0;
                // add tasks from dictionary to dependency list and determine level for this task
                for (int i = 0; i < dependencies.Length; i++)
                {
                    if(Tasks.ContainsKey(ids[i + 1]))
                    {
                        dependencies[i] = Tasks[ids[i + 1]];
                        if(dependencies[i].Level >= level)
                        {
                            level = dependencies[i].Level + 1;
                        }
                    }
                    else
                    {
                        // throw?
                    }
                }
                Tasks.Add(ids[0], new Task(ids[0], dependencies, level));
            }
        }
    }
}
