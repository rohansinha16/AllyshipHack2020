using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TaskManager : MonoBehaviour
{
    static List<Employee> Employees = new List<Employee>();
    static Dictionary<string, Task> Tasks = new Dictionary<string, Task>();
    static Dictionary<string, GameObject> Buttons = new Dictionary<string, GameObject>();
    static List<List<Task>> TasksByLevel = new List<List<Task>>();
    static string ActiveButton;

    // Employee Buttons
    public GameObject James;
    public GameObject Monique;
    public GameObject Kelly;
    public GameObject Pallavi;
    public GameObject HyunJin;

    public GameObject TaskCanvas;
    public GameObject ButtonPrefab;
    public Text ActiveTaskText;


    // Use this for initialization
    void Start()
    {
        James.name = "James";
        James.GetComponentInChildren<Text>().text = "James";
        Monique.name = "Monique";
        Kelly.name = "Kelly";
        Pallavi.name = "Pallavi";
        HyunJin.name = "HyunJin";
        readTaskList(Application.dataPath + @"/Scripts/TaskLists/graph1.txt");
        DrawTaskButtons(ButtonPrefab, TaskCanvas);
        Employee e = ScriptableObject.CreateInstance<Employee>();
        e.AssignEmployee("James");
        Employees.Add(e);
        James.GetComponent<Button>().onClick.AddListener(() => {
            e.AssignTask(Tasks[ActiveButton]);
            if (!string.IsNullOrEmpty(ActiveButton))
            {
                ActiveButton += " to " + EventSystem.current.currentSelectedGameObject.name;
            }
        });

        e = ScriptableObject.CreateInstance<Employee>();
        e.AssignEmployee("Monique");
        Employees.Add(e);
        Monique.GetComponent<Button>().onClick.AddListener(() => {
            e.AssignTask(Tasks[ActiveButton]);
            if (!string.IsNullOrEmpty(ActiveButton))
            {
                ActiveButton += " to " + EventSystem.current.currentSelectedGameObject.name;
            }
        });

        e = ScriptableObject.CreateInstance<Employee>();
        e.AssignEmployee("Kelly");
        Employees.Add(e);
        Kelly.GetComponent<Button>().onClick.AddListener(() => {
            e.AssignTask(Tasks[ActiveButton]);
            if (!string.IsNullOrEmpty(ActiveButton))
            {
                ActiveButton += " to " + EventSystem.current.currentSelectedGameObject.name;
            }
        });

        e = ScriptableObject.CreateInstance<Employee>();
        e.AssignEmployee("Pallavi");
        Employees.Add(e);
        Pallavi.GetComponent<Button>().onClick.AddListener(() => {
            e.AssignTask(Tasks[ActiveButton]);
            if (!string.IsNullOrEmpty(ActiveButton))
            {
                ActiveButton += " to " + EventSystem.current.currentSelectedGameObject.name;
            }
        });

        e = ScriptableObject.CreateInstance<Employee>();
        e.AssignEmployee("HyunJin");
        Employees.Add(e);
        HyunJin.GetComponent<Button>().onClick.AddListener(() => {
            e.AssignTask(Tasks[ActiveButton]);
            if (!string.IsNullOrEmpty(ActiveButton))
            {
                ActiveButton += " to " + EventSystem.current.currentSelectedGameObject.name;
            }
        });

        /*
        Employees.Add(new Employee("Monique"));
        Monique.GetComponent<Button>().onClick.AddListener(() => Employees[Employees.Count - 1].AssignTask(Tasks[ActiveButton]));
        Employees.Add(new Employee("Kelly"));
        Kelly.GetComponent<Button>().onClick.AddListener(() => Employees[Employees.Count - 1].AssignTask(Tasks[ActiveButton]));
        Employees.Add(new Employee("Pallavi"));
        Pallavi.GetComponent<Button>().onClick.AddListener(() => Employees[Employees.Count - 1].AssignTask(Tasks[ActiveButton]));
        Employees.Add(new Employee("HyunJin"));
        HyunJin.GetComponent<Button>().onClick.AddListener(() => Employees[Employees.Count - 1].AssignTask(Tasks[ActiveButton]));
        */
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTaskButtons();
        foreach(Employee e in Employees)
        {
            e.Update();
        }
        if (!string.IsNullOrEmpty(ActiveButton) && ActiveButton.Contains("to"))
        {
            ActiveTaskText.text = "Assigning Task: " + ActiveButton;
        }
        else if (!string.IsNullOrEmpty(ActiveButton) && !Tasks[ActiveButton].Assigned)
        {
            ActiveTaskText.text = "Assigning Task: " + ActiveButton;
        }
        else
        {
            ActiveTaskText.text = "";
        }

    }

    public static void DrawTaskButtons(GameObject _buttonPrefab, GameObject _taskCanvas)
    {
        int previousShift = 0;
        for (int i = TasksByLevel.Count - 1; i >= 0; i--)
        {
            int shift = 0;
            if (i < TasksByLevel.Count - 1)
            {
                shift = (TasksByLevel[i + 1].Count - TasksByLevel[i].Count) * 45;
            }
            previousShift += shift;
            for (int j = 0; j < TasksByLevel[i].Count; j++)
            {
                Buttons.Add(TasksByLevel[i][j].Name, CreateTaskButton(TasksByLevel[i][j], _buttonPrefab, _taskCanvas, previousShift + j * 90 + 20, (Screen.height - 40 - TasksByLevel.Count * 90) + i * 90 + 110));
                Buttons[TasksByLevel[i][j].Name].GetComponent<Button>().onClick.AddListener(() => {
                    ActiveButton = EventSystem.current.currentSelectedGameObject.name;
                });
            }
        }
    }

    public static void UpdateTaskButtons()
    {
        for (int i = TasksByLevel.Count - 1; i >= 0; i--)
        {
            for (int j = 0; j < TasksByLevel[i].Count; j++)
            {
                Sprite buttonAssigned = Resources.Load<Sprite>("Pictures/buttonAssigned");
                Sprite buttonAvailable = Resources.Load<Sprite>("Pictures/buttonAvailable");
                Sprite buttonComplete = Resources.Load<Sprite>("Pictures/buttonComplete");
                Task t = TasksByLevel[i][j];
                t.Update();
                if (t.Completed)
                {
                    Buttons[t.Name].GetComponent<Image>().sprite = buttonComplete;
                }
                else if (t.Assigned)
                {
                    Buttons[t.Name].GetComponent<Image>().sprite = buttonAssigned;
                }
                else if (t.DependentCompleted)
                {
                    Buttons[t.Name].GetComponent<Image>().sprite = buttonAvailable;
                }
            }
        }
    }

    public static GameObject CreateTaskButton(Task task, GameObject _buttonPrefab, GameObject _taskCanvas, int x, int y)
    {
        GameObject buttonObject = Instantiate(_buttonPrefab, new Vector2(x, y), Quaternion.identity);
        buttonObject.name = task.Name;
        buttonObject.GetComponentInChildren<Text>().text = task.Name;

        Sprite buttonAvailable = Resources.Load<Sprite>("Pictures/buttonAvailable");
        Sprite buttonComplete = Resources.Load<Sprite>("Pictures/buttonComplete");
        if (task.Completed)
        {
            buttonObject.GetComponent<Image>().sprite = buttonComplete;
        }
        if (task.DependentCompleted)
        {
            buttonObject.GetComponent<Image>().sprite = buttonAvailable;
        }

        buttonObject.transform.SetParent(_taskCanvas.transform);
        return buttonObject;
    }

    public void readTaskList(string filename)
    {
        List<string> incompleteLines = new List<string>();
        List<string> completeLines = new List<string>();
        incompleteLines.AddRange(System.IO.File.ReadAllLines(filename));
        while (incompleteLines.Count != 0)
        {
            foreach (string line in incompleteLines)
            {
                string[] ids = line.Split(' ');
                List<Task> dependencies = new List<Task>();
                int level = 0;
                if (Tasks.ContainsKey(ids[0]))
                {
                    throw new System.Exception("Cannot have tasks with identical ids.");
                }
                for (int i = 0; i < ids.Length - 1; i++)
                {
                    if (Tasks.ContainsKey(ids[i + 1]))
                    {
                        dependencies.Add(Tasks[ids[i + 1]]);
                        if (dependencies[i].Level >= level)
                        {
                            level = dependencies[i].Level + 1;
                        }
                    }
                    else
                    {
                        // come back to it later
                        break;
                    }
                }
                Task thisTask = ScriptableObject.CreateInstance<Task>();
                thisTask.AssignTask(ids[0], dependencies, level);
                // add to dictionary to fetch by name
                Tasks.Add(ids[0], thisTask);
                // add to nested list to organize by level
                while (TasksByLevel.Count <= level)
                {
                    TasksByLevel.Add(new List<Task>());
                }
                TasksByLevel[level].Add(thisTask);
                completeLines.Add(line);
            }
            foreach (string line in completeLines)
            {
                incompleteLines.Remove(line);
            }
        }
    }

    public static Text AddTextToCanvas(string textString, GameObject canvasGameObject, int x, int y)
    {
        GameObject newText = new GameObject(textString.Replace(" ", "-"), typeof(RectTransform));
        Text text = newText.AddComponent<Text>();
        text.text = textString;

        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        text.font = ArialFont;
        text.color = Color.black;
        text.material = ArialFont.material;
        text.fontSize = 100;

        // Text position
        RectTransform rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector2(x, y);
        //.anchoredPosition = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(400, 200);
        text.transform.SetParent(canvasGameObject.transform);
        return text;
    }

    public static Button AddButtonToCanvas(string textString, GameObject canvasGameObject, int x, int y)
    {
        GameObject newButton = new GameObject(textString.Replace(" ", "-"), typeof(RectTransform));
        Button button = newButton.AddComponent<Button>();
        button.GetComponentInChildren<Text>().text = textString;
        button.onClick.AddListener(delegate { });

        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        button.GetComponentInChildren<Text>().font = ArialFont;
        button.GetComponentInChildren<Text>().color = Color.black;
        button.GetComponentInChildren<Text>().material = ArialFont.material;
        button.GetComponentInChildren<Text>().fontSize = 50;

        // Text position
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector2(x, y);
        button.transform.SetParent(canvasGameObject.transform);
        return button;
    }
}
