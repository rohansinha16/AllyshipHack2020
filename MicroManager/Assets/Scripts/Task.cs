using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Task : MonoBehaviour
{

    
    public string Name;
    public int Level;
    public bool Assigned = false;
    private int Completion = 0; // 100 is complete
    private bool Completed =false;
    private Task[] DependentTasks;


    public Task(string _name, Task[] _dependentTasks, int _level)
    {
        Name = _name;
        DependentTasks = _dependentTasks;
        Level = _level;
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    }

    /* 
     * Do work on a task given the member who's working on it's morale.
     * Called once per second by Employee update function
     */
    public bool Work(int morale)
    {
        if (!Completed)
        {
            // task progress = morale/10 (=1 if progress is <1)
            if(morale > 100 || morale < 0)
            {
                throw new System.Exception("Morale should not exceed 100 or be below 0. Morale was " + morale + ".");
            }
            int progress = morale / 10;
            if (progress <= 1)
            {
                progress = 1;
            }
            Completion += progress;
            if(Completion >= 100)
            {
                Completion = 100;
                Completed = true;
            }
        }
        return Completed;
    }


}
