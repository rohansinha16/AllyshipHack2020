using UnityEngine;
using UnityEditor;

public class Revenue : ScriptableObject
{
    private static int revenue { get; set; } = 0;

    public static void Earn(int amount)
    {
        System.Random rand = new System.Random();
        revenue += (int) (amount * (rand.NextDouble()/2 + .5) * 10);
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