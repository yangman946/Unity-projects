using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class options : MonoBehaviour
{
    public GameObject optionspanel;
    public bool isToggle = false;
    public GameObject shaderObj;

    public Slider symmetry;
    public Slider color;

    public Material rend;
    // Start is called before the first frame update
    void Start()
    {
       
        optionspanel.gameObject.SetActive(false);
        isToggle = false;
        color.value = 0.5f;
        symmetry.value = 0f;
        setValues();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Escape) && isToggle == false)
        {
            isToggle = true;
            optionspanel.gameObject.SetActive(true);
        }
        else if(Input.GetKey(KeyCode.Escape) && isToggle == true)
        {
            isToggle = false;
            optionspanel.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        setValues();
    }

    public void setValues()
    {
        rend.SetFloat("_Color", color.value);
        rend.SetFloat("_Symmetry", symmetry.value);
    }

    public void quit()
    {
        Application.Quit();
    }
}
