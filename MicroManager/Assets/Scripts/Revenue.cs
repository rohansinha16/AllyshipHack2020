using UnityEngine;
using UnityEditor;

public class Revenue : ScriptableObject
{
    private static int revenue { get; set; } = 0;

    public static void Earn(int amount)
    {
        revenue += amount * 9;
    }

    public static void CompleteTask()
    {
        revenue += 1000;
    }

    public static int GetRevenue()
    {
        return revenue;
    }
    
}