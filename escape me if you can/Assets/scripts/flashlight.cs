/* for future reference:
 * change the rate to how fast the power wastes according to game mode
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class flashlight : MonoBehaviour
{
    public Image[] indicators;
    public int batterylife = 100;
    public Light torch;
    public bool toggle = true;
    public bool loosingpower = true; //make the ai check this when its near

    private bool check = false;
    public bool outofpower = false;

    public bool refill = false;
    

    // Start is called before the first frame update
    void Start()
    {
        indicators[0].enabled = true;
        toggle = true;
        loosingpower = true;
        check = false;
        refill = false;

        outofpower = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (batterylife > 100)
        {
            batterylife = 100;
        }

        if (batterylife < 0) //out of power
        {
            batterylife = 0;
            outofpower = true; //out of power check
            torch.enabled = false;
        }
        else if (batterylife >0)
        {
            outofpower = false;
        }

        //enabling and disabling the flash light.
        if (Input.GetKeyDown(KeyCode.F) && toggle == true && outofpower == false) //disabling (the flash light must not be out of power)
        {
            toggle = false;
            torch.enabled = false;
            
            loosingpower = false;
        }
        else if (Input.GetKeyDown(KeyCode.F) && toggle == false && outofpower == false)//enabling
        {
            toggle = true;
            torch.enabled = true;
            
            loosingpower = true;
        }

        if (loosingpower == true && check == false)
        {
            check = true;
            StartCoroutine(wastepower());
        }

        if (refill == true)
        {
            batterylife += 25;
            refill = false;
        }

        //ui
        if (batterylife <= 100 && batterylife >= 75) //green
        {
            indicators[0].enabled = true;

            indicators[1].enabled = false;
            indicators[2].enabled = false;
            indicators[3].enabled = false;
            indicators[4].enabled = false;

        }
        else if (batterylife < 75 && batterylife >= 50) //yellow
        {
            indicators[1].enabled = true;

            indicators[0].enabled = false;
            indicators[2].enabled = false;
            indicators[4].enabled = false;
            indicators[3].enabled = false;

        }
        else if (batterylife < 50 && batterylife >= 25) //orange <-- mid
        {
            indicators[2].enabled = true;

            indicators[0].enabled = false;
            indicators[1].enabled = false;
            indicators[4].enabled = false;
            indicators[3].enabled = false;
        }
        else if (batterylife < 25 && batterylife > 0) //red <-- low
        {
            indicators[3].enabled = true;

            indicators[2].enabled = false;
            indicators[0].enabled = false;
            indicators[1].enabled = false;
            indicators[4].enabled = false;
        }
        else if (batterylife == 0)
        {
            indicators[4].enabled = true;

            indicators[2].enabled = false;
            indicators[3].enabled = false;
            indicators[0].enabled = false;
            indicators[1].enabled = false;
        }

    }

    IEnumerator wastepower()
    {
        batterylife--;
        Debug.Log(batterylife.ToString());
        yield return new WaitForSeconds(1); //change speed for battery wastage depending on mode
        check = false;
        
    }
}
