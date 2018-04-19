using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspetosManager : MonoBehaviour {

    private SliderJoint2D espeto;
    private JointMotor2D aux;

	// Use this for initialization
	void Start () {
        espeto = GetComponent<SliderJoint2D>();
        aux = espeto.motor;

	}
	
	// Update is called once per frame
	void Update () {
		if(espeto.limitState == JointLimitState2D.UpperLimit)
        {
            aux.motorSpeed = Random.Range(-1, -5);
            espeto.motor = aux;
        }
        if (espeto.limitState == JointLimitState2D.LowerLimit)
        {
            aux.motorSpeed = Random.Range(1, 5);
            espeto.motor = aux;
        }
        /* tentando fazer os espetos fazer curva tipo um U
        if (espeto.motor.motorSpeed > 0)
            espeto.angle += 10*Time.deltaTime;
        if(espeto.motor.motorSpeed < 0)
            espeto.angle -= 10 * Time.deltaTime;
        */
    }
}
