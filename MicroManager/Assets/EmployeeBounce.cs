using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EmployeeBounce : MonoBehaviour
{

    public GameObject EmployeeCanvas;
    public double bounceTime = 0.05;
    public int pose = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bounceTime -= Time.deltaTime;
        if (bounceTime <= 0)
        {
            pose += 1;
            
            if (pose > 3)
            {
                pose = 0;
            }
            switch (pose)
            {
                case 0:
                    EmployeeCanvas.transform.Translate(new Vector2(0, -8));
                    bounceTime = 0.1;
                    break;
                case 1:
                    EmployeeCanvas.transform.Translate(new Vector2(0, -2));
                    bounceTime = 0.1;
                    break;
                case 2:
                    EmployeeCanvas.transform.Translate(new Vector2(0, 2));
                    bounceTime = 0.3;
                    break;
                case 3:
                    EmployeeCanvas.transform.Translate(new Vector2(0, 8));
                    bounceTime = 0.3;
                    break;
            }
        }
    }
}
