using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    FixedJoystick fixedJoystick;
    public GameObject bodyPart;
    public List<Transform> Babies;
    Vector2 currentDir;
    public float speed = 1f;
    public float rotationspeed = 50;
    private Transform curBodyPart;
    private Transform preBodyPart;
    private float distance;
    private float mindistance = 0.25f;
    public float nitroFactor=1f;
   
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
        if (Input.GetMouseButtonDown(2))
        {
            AddBodyPart();
        }
    }

    public void Move()
    {
       
        float curspeed = speed;
        if (fixedJoystick.Direction != Vector2.zero)
        {
            currentDir = fixedJoystick.Direction;
            Babies[0].Translate(new Vector3(fixedJoystick.Direction.x, 0, fixedJoystick.Direction.y) * nitroFactor*curspeed * Time.smoothDeltaTime, Space.World);
        }
        else
        {
            Babies[0].Translate(new Vector3(currentDir.x, 0, currentDir.y) * curspeed * Time.smoothDeltaTime, Space.World);

        }

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
        GameObject baby= Instantiate(bodyPart, Babies[Babies.Count-1].transform.position,Quaternion.Euler(Vector3.right*90));
        baby.GetComponent<SpriteRenderer>().sprite  = Resources.Load<Sprite>("Sprites/Bodies/body" + PlayerPrefs.GetInt("Selected")); 
        baby.transform.SetParent(transform);
        Babies.Add(baby.transform);
    }
}