using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    int dnaLength = 2;
    public DNA dna;
    public GameObject eyes;
    bool alive = true;
    bool seeWall = true;
    Vector3 startPosition;
    public float distanceTravelled = 0;


    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "dead")
        {
            alive = false;
            distanceTravelled = 0;
        }
    }
    public void Init()
    {
        //0 forward
        //1 Angle turn

        dna = new DNA(dnaLength, 360);
        startPosition = this.transform.position;
    }

    private void Update()
    {
        if (!alive) return;

        seeWall = false;
        RaycastHit hit;
        Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 0.5f, Color.red, 10);
        if (Physics.SphereCast(eyes.transform.position, 0.1f,eyes.transform.forward, out hit,0.5f))
        {
            if (hit.collider.gameObject.tag == "wall")
            {
                seeWall = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (!alive) return;

        float h = 0;
        float v = dna.GetGene(0);

        if (seeWall)
        {
            h = dna.GetGene(1);
        }

        this.transform.Translate(0, 0, 0.1f);
        this.transform.Rotate(0, h, 0);
        distanceTravelled = Vector3.Distance(startPosition, this.transform.position);
    }
}
