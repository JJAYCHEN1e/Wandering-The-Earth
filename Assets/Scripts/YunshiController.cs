using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class YunshiController : MonoBehaviour
{

    public GameObject Target;
    public Rigidbody YS;

    public int ifgo;



    int Random01()
    {
        int a = Random.Range(0, 2);
        if (a == 0)
        { return -1; }
        else
        { return 1; }
    }


    Vector3 start_position()
    {
        Vector3 t = Target.transform.position;

        t.x += Random.Range(100, 200) * Random01();
        t.y += Random.Range(50, 100) * Random01();
        t.z += Random.Range(100, 200) * Random01();
        return t;
    }


    void Start()
    {
        Target = GameObject.Find("Earth");

    }


    void Update()
    {


    }

    int count = 0;

    void FixedUpdate()
    {
        count++;
            if (count % 50 == 0)
            {
                ifgo = Random.Range(0, 11);
                if (ifgo % 2 == 0)
                {
                    int f = Random.Range(2, 4);
                    GameObject ys = GameObject.Instantiate(Resources.Load<GameObject>("yunshi"));
                    YS = ys.GetComponent<Rigidbody>();
                    ys.transform.position = start_position();
                    Destroy(ys, 15);
                    Vector3 initialVelocityDirection = (Target.transform.position - ys.transform.position).normalized;
                    YS.velocity = initialVelocityDirection * 10 * f;
                }

            }
        

    }
}
