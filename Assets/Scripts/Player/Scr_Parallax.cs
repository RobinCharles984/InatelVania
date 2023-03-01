using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Parallax : MonoBehaviour
{
    //Components
    [SerializeField] Transform background;
    public Transform cam;
    private Vector3 previewPosition;
    
    //Variables
    public float speed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        previewPosition = cam.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float paralaxX = previewPosition.x - cam.position.x;
        float bgTargetX = background.position.x + paralaxX;

        Vector3 bgPostiion = new Vector3(bgTargetX, background.position.y, background.position.z);
        background.position = Vector3.Lerp(background.position, bgPostiion, speed * Time.deltaTime);

        previewPosition = cam.position;

    }
}
