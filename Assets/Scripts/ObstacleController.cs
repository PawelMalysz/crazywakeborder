using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public int multi = 5;
    private Engine engine;
    private Rigidbody rb;
    public float force = 0;
    public Vector3 upLift;



    void Start()
    {
        engine = GameObject.FindGameObjectWithTag("GM").GetComponent<Engine>();
        
        rb = this.GetComponent<Rigidbody>();
        force = 6f;
    }


    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (this.transform.position.y < engine.waterLevel + 0.7f)
            {
                rb.AddForce(transform.up - Physics.gravity * force, ForceMode.Force);
            }

            this.transform.position += new Vector3(0, 0, -1) * Time.deltaTime * multi;

            if (this.transform.position.z < -engine.maxZ / 2)
            {
                if (gameObject.tag == "Bonus")
                {
                    engine.bonuses.Remove(gameObject);
                }
                GameObject.Destroy(this.gameObject);
            }
        }
        
    }


}
