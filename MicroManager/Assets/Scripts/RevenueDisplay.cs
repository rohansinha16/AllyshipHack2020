using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevenueDisplay : MonoBehaviour
{

    private int revenue = 0;
    public Text revenueText;

    // Start is called before the first frame update
    void Start()
    {
        revenueText.text = GetRevenueText();
    }

    // Update is called once per frame
    void Update()
    {
        revenueText.text = GetRevenueText();
        if(Input.GetMouseButtonUp(0))
        {
            CompleteTask();
        }
    }

    private string GetRevenueText()
    {
        return "Revenue: $" + revenue;
    }

    public void CompleteTask()
    {
        revenue += 100;
    }
}
