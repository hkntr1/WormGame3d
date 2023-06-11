using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AIController : MonoBehaviour
{
    FixedJoystick fixedJoystick;
    public GameObject bodyPart;
    public List<Transform> Babies;
    Vector3 currentDir;
    public float speed = 1f;
    public float rotationspeed = 50;
    private Transform curBodyPart;
    private Transform preBodyPart;
    private float distance;
    private float mindistance = 0.25f;
    public int timerLimit;
    private float timerCounter;

    void Start()
    {
        fixedJoystick = FindObjectOfType<FixedJoystick>();
        Init();
    }
    private void Init()
    {
        foreach (Transform baby in transform)
        {
            Babies.Add(baby);
        }
    }

    void Update()
    {
        Move();
        
    }
   
    public void Move()
    {
        
        if (timerCounter<timerLimit)
        {
            timerCounter += Time.deltaTime;
           
        }
        else
        {
     
            currentDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            
            timerCounter = 0;
        }

        if (currentDir == Vector3.zero)
        {
            currentDir = Vector3.left;
        }
        float curspeed = speed;        
        Babies[0].Translate(currentDir * curspeed * Time.smoothDeltaTime, Space.World);
      

        for (int i = 1; i < Babies.Count; i++)
        {
            curBodyPart = Babies[i];
            preBodyPart = Babies[i - 1];
            distance = Vector3.Distance(preBodyPart.position, curBodyPart.position);
            Vector3 newpos = preBodyPart.position;
            newpos.y = Babies[0].position.y;
            float T = Time.deltaTime * distance / mindistance * curspeed;
            if (T > 0.5f)
                T = 0.5f;
            curBodyPart.position = Vector3.Slerp(curBodyPart.position, newpos, T);
            curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, preBodyPart.rotation, T);
        }
    }
    public void AddBodyPart()
    {
        GameObject baby = Instantiate(bodyPart, Babies[Babies.Count - 1].transform.position, Quaternion.Euler(Vector3.right * 90));
        baby.transform.SetParent(transform);
        Babies.Add(baby.transform);
    }
}