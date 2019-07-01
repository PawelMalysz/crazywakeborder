using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float bPosX;
    float bPosY;
    float bPosZ;
    public float maxX;
    float maxY;
    float maxZ;
    float tmp;
    int counter = 0;

    public int multi = 100;
    public float timeToBack = 2;
    float currentTime;
    float effectDuration;
    int pillSpeedUp;

    public Rigidbody rb;
    public Transform animTransform;
    public Vector3 rotationA, rotationB, rotationC;
    public Engine engine;
    public AudioSource audio;
    public AudioClip coinSound;
    public AudioClip speedUpSound;
    public AudioClip starUpSound;
    public AudioClip armorSound;

    public GameObject armor;

    public int Multi
    {
        get
        {
            return this.multi;
        }
        set
        {
            if (value > 0)
            {
                this.multi = value;
            }
        }
    }

    void Start()
    {
        engine = GameObject.FindGameObjectWithTag("GM").GetComponent<Engine>();
        rb = this.GetComponent<Rigidbody>();
        animTransform = GameObject.Find("Anim").GetComponentInChildren<Transform>();
        audio = GetComponent<AudioSource>();
        bPosX = this.transform.position.x;
        bPosY = this.transform.position.y;
        bPosZ = this.transform.position.z;
        maxX = 8f;
        maxY = 0f;
        maxZ = bPosZ;
        tmp = 0;
        effectDuration = 0;

        rotationA = new Vector3(90, 30, 0);
        rotationB = new Vector3(90, -30, 0);
        rotationC = new Vector3(90, 0, 0);
    }
    
    void Update()
    {

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            
            if (this.transform.position.x > -maxX)
            {
                if (this.transform.position.x <= 0)
                {
                    rb.AddForce(Vector3.left * Time.deltaTime * multi);
                }
                else if (this.transform.position.x > 0)
                {
                    rb.AddForce(Vector3.left * Time.deltaTime * multi);
                }

                if (animTransform.localPosition.x > -2 && animTransform.localPosition.x < 2)
                {
                    animTransform.localPosition = new Vector3(animTransform.localPosition.x + Time.deltaTime/10, animTransform.localPosition.y, animTransform.localPosition.z);
                }

                if (animTransform.rotation.eulerAngles.y < 45 && animTransform.rotation.eulerAngles.y > -45)
                {
                    animTransform.Rotate(new Vector3(0, -Time.deltaTime * 5, 0));
                }

                animTransform.rotation = Quaternion.Lerp(animTransform.rotation, Quaternion.Euler(rotationB), 0.01f);
            }
            else
            {
                this.transform.Translate(Vector3.zero);
            }

            currentTime = timeToBack;
        }

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (this.transform.position.x < maxX)
            {
                if (this.transform.position.x < 0)
                {
                    rb.AddForce(Vector3.right * Time.deltaTime * multi);
                }
                else if (this.transform.position.x >= 0)
                {
                    rb.AddForce(Vector3.right * Time.deltaTime * multi);
                }

                if (animTransform.localPosition.x > -2 && animTransform.localPosition.x < 2)
                {
                    animTransform.localPosition = new Vector3(animTransform.localPosition.x - Time.deltaTime/10, animTransform.localPosition.y, animTransform.localPosition.z);
                }

                if (animTransform.rotation.eulerAngles.y < 45 && animTransform.rotation.eulerAngles.y > -45)
                {
                    animTransform.Rotate(new Vector3(0, Time.deltaTime * 5, 0));
                }

                animTransform.rotation = Quaternion.Lerp(animTransform.rotation, Quaternion.Euler(rotationA), 0.01f);
            }
            else
            {
                this.transform.Translate(Vector3.zero);
            }

            currentTime = timeToBack;
        }
        else
        {
            if (animTransform.localPosition.x > 0)
            {
                animTransform.localPosition = new Vector3(animTransform.localPosition.x - Time.deltaTime / 10, animTransform.localPosition.y, animTransform.localPosition.z);
            }
            else if(animTransform.localPosition.x < 0)
            {
                animTransform.localPosition = new Vector3(animTransform.localPosition.x + Time.deltaTime / 10, animTransform.localPosition.y, animTransform.localPosition.z);
            }

            currentTime -= Time.deltaTime;
        }

        animTransform.rotation = Quaternion.Lerp(animTransform.rotation, Quaternion.Euler(rotationC),0.01f);

        if (this.transform.position.x > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Pow((this.transform.position.x), 2) / 20);
        }
        else if (this.transform.position.x < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Pow((this.transform.position.x), 2) / 20);
        }

        if (currentTime < 0)
        {
            if (transform.position.x > 0)
            {
                rb.AddForce(Vector3.left * Time.deltaTime * multi/2);
            }
            else if (transform.position.x < 0)
            {
                rb.AddForce(Vector3.right * Time.deltaTime * multi/2);
            }
        }

        if(effectDuration > 0)
        {
            effectDuration -= Time.deltaTime;
        }
        else if(effectDuration < 0)
        {
            
            this.Multi -= pillSpeedUp*counter;
            effectDuration = 0;
            counter = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Obstacle")
        {

            if (armor.activeSelf)
            {
                armor.SetActive(false);
            }
            else
            {
                engine.GameOver();
            }
            GameObject.Destroy(other.gameObject);
        }

        if(other.tag == "Bonus")
        {
            if(other.GetComponent<SpeedUpPill>())
            {
                other.GetComponent<SpeedUpPill>().GiveEffect(this);
                pillSpeedUp = other.GetComponent<SpeedUpPill>().speedUp;
                effectDuration = other.GetComponent<SpeedUpPill>().duration;
                counter++;
                audio.PlayOneShot(speedUpSound);

            }
            else if(other.GetComponent<StarUp>())
            {
                other.GetComponent<StarUp>().GiveEffect(this);
                audio.PlayOneShot(starUpSound);
            }
            else if(other.GetComponent<Coin>())
            {
                other.GetComponent<Coin>().GiveEffect(this);
                audio.PlayOneShot(coinSound);
            }
            else if (other.GetComponent<Armor>())
            {
                other.GetComponent<Armor>().GiveEffect(this);
                audio.PlayOneShot(armorSound);
            }

            engine.bonuses.Remove(other.gameObject);
            GameObject.Destroy(other.gameObject);
        }
    }
    


}
