using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{

    public GameObject gameObject2; 
    private LineRenderer line;
    
    void Start()
    {
        line = this.gameObject.GetComponent<LineRenderer>();
    }
    
    void Update()
    {
        if (gameObject2 != null)
        {
            line.SetPosition(0, this.transform.position);
            line.SetPosition(1, gameObject2.transform.position);
        }
    }
}
