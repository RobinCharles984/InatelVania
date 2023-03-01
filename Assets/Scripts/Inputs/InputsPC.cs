using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsPC : MonoBehaviour
{
    public float h = Input.GetAxis("Horizontal");//Horizontal Movement
    public bool v = Input.GetButtonDown("Jump");//Vertical Movement
    public float stomp = Input.GetAxis("Vertical");//Stopm Button
    public bool dash = Input.GetButtonDown("Dash");//Dash Button
}
