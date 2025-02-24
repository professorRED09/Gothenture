using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBG : MonoBehaviour
{
    public float maxX;
    public float speed;
    public float currentX;
    
    // Update is called once per frame
    void Update()
    {
        currentX += speed * Time.deltaTime;
        this.gameObject.transform.position = new Vector3(currentX, transform.position.y, 0);

        if(currentX > maxX) {            
            this.gameObject.transform.position = new Vector3(0, transform.position.y, 0);
            currentX = 0;
        }
    }
}
