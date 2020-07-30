using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevenueDisplay : MonoBehaviour
{
    public Text RevenueText;

    // Start is called before the first frame update
    void Start()
    {
        RevenueText.text = GetRevenueText();
    }

    // Update is called once per frame
    void Update()
    {
        RevenueText.text = GetRevenueText();
    }

    private string GetRevenueText()
    {
        return "Revenue: $" + Revenue.GetRevenue();
    }
}
