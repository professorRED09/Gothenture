using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    private Animator transition;
    // Start is called before the first frame update
    private void Awake()
    {
        transition = GetComponent<Animator>();
    }
    public void ShowPanel()
    {
        transition.SetTrigger("Open");
    }

    public void ClosePanel()
    {
        transition.SetTrigger("Close");
    }
    
}
