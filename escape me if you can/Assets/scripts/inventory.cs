using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class inventory : MonoBehaviour
{
    public GameObject main;
    public GameObject options;
    public GameObject torch;

    public GameObject shedtrigger;
    public GameObject boxkeytrigger;
    public GameObject exitkeytrigger;

    //spawn object prefab
    public GameObject batteryprefab;
    public GameObject keyprefab;
    public GameObject torchprefab;
    public GameObject crowbarprefab;
    public GameObject boxkeyprefab;
    public GameObject exitkeyprefab;
    public GameObject mapprefab;

    public GameObject torchreal;
    public GameObject crowbarreal;
    GameObject torchprefabcol;

    public Image[] boxes;
    public RawImage[] optionindicator;
    //public RawImage[] image_boxes; //the indicators 
    public int index = 0;
    public int newindex = 0;

    public int length;
    public bool trigger = false;
    public bool optionsenabled = false;
    private bool havetorch = false;
    public bool usekey = false;
    public bool useboxkey = false;
    public bool useexitkey = false;

    bool setimageactive = false;

    public GameObject imageviewer;

    public Sprite torchs;
    public Sprite battery;
    public Sprite Key;
    public Sprite crowbarpic;
    public Sprite boxkey;
    public Sprite exitkey;
    public Sprite map;

    public GameObject power;
    public Text infoline;
    public Text shedtext;

    FirstPersonController controller;

    public bool[] indexSlots;
    public int toolindex = 0;
    int torchtool = 1;
    int crowbartool = 2;

    //items
    public bool torchItem = false;
    public bool crowbaritem = false;
    public bool addBattery = false;
    public bool addKey = false;
    public bool addboxkey = false;
    public bool addexitkey = false;
    public bool addmap = false;

    public bool picked = false;
    private bool newindexenabled = false;

    public Text title;
    public Text info;

    public bool shouldshedtextshow = false;

    public GameObject canvas;

    public string[] thisitem;
    public string[] iteminfo;
    private const string torchtext = "Torch";
    private const string Keytext = "Key";
    private const string batterytext = "Battery";
    private const string crowbartext = "Crowbar";
    private const string boxKeytext = "Box Key";
    private const string exitKeytext = "Exit key";
    private const string maptext = "Map";
    private const string TorchInfo = "A battery powered torch, useful for emmiting light. Requires batteries.";
    private const string crowbarInfo = "A tool found in the shed, could be useful.";
    private const string batteryInfo = "1.5 volts battery, can be used on a Torch to refill some power.";
    private const string keyInfo = "A key to some kind of storage shed.";
    private const string boxkeyInfo = "A key for a box.";
    private const string exitkeyInfo = "The key to the exit.";
    private const string mapInfo = "A roughly sketched map.";

    public int optionsint;

    public bool full = false;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        newindex = 0;
        Time.timeScale = 1f;
        trigger = false;

        torchtool = 1;
        crowbartool = 2;
        toolindex = 0;

        length = indexSlots.Length;
        optionsint = optionindicator.Length;

        for (int i = 0; i < length; i++)
        {
            indexSlots[i] = false;
        }

        for (int i = 0; i < length; i++)
        {
            thisitem[i] = "Empty";
        }

        for (int i = 0; i < length; i++) //info
        {
            iteminfo[i] = "";
        }

        for (int i = 0; i < optionsint; i++)
        {
            optionindicator[i].enabled = false;
        }

        optionindicator[0].enabled = true;

        picked = false;
        main.SetActive(false);
        havetorch = false;

        options.SetActive(false);
        newindexenabled = false;

        imageviewer.SetActive(false);
        setimageactive = false;

        usekey = false;
        useboxkey = false;
        useexitkey = false;

        torchreal.SetActive(false);
        crowbarreal.SetActive(false);

        shouldshedtextshow = false;

        torchItem = false;
        crowbaritem = false;
        addBattery = false;
        addKey = false;
        addboxkey = false;
        addexitkey = false;
        addmap = false;
        optionsenabled = false;

        controller = GetComponent<FirstPersonController>();

        full = false;
        
      
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas.GetComponent<menu>().paused == false)
        {
            if (picked == false) //disabling or enabling ui
            {
                power.SetActive(false);
            }
            else
            {
                power.SetActive(true);
                toolindex = torchtool; //torch is equipped
            }

            //enabling the inventory
            if (Input.GetKeyDown(KeyCode.I) && trigger == false && gameObject.GetComponent<healthscript>().deadd1 == false)
            {
                Debug.Log("inventory enabled");
                trigger = true;
                shouldshedtextshow = true; //false

                main.SetActive(true);
                controller.enabled = false;
                Time.timeScale = 0f; //freezes time 
            }
            else if (Input.GetKeyDown(KeyCode.I) && trigger == true)
            {
                if (setimageactive == true)
                {
                    setimageactive = false;
                    imageviewer.SetActive(false);
                }
                trigger = false;
                main.SetActive(false);
                shouldshedtextshow = false; //false
                controller.enabled = true;
                Time.timeScale = 1f;
            }

            if (toolindex == torchtool) //torch is equipped
            {
                torchreal.SetActive(true);
                picked = true;
                crowbarreal.SetActive(false);

                //Debug.Log("torch equipped");
            }
            else if (toolindex == crowbartool)
            {
                //Debug.Log("crowbarworks");
                torchreal.SetActive(false);
                //picked = false;
                crowbarreal.SetActive(true);
                //Debug.Log("crowbar equipped");
            }
            else if (toolindex == 0)
            {
                torchreal.SetActive(false);
                crowbarreal.SetActive(false);
                //Debug.Log("none equipped");
            }


            if (full == true)
            {
                //
            }


            if (indexSlots[0] == false)
            {
                //slot 0 is empty
                if (torchItem == true) { boxes[0].GetComponent<slot>().slotTorch = true; indexSlots[0] = true; torchItem = false; thisitem[0] = torchtext; iteminfo[0] = TorchInfo; }
                else if (crowbaritem == true) { boxes[0].GetComponent<slot>().slotcrowbar = true; indexSlots[0] = true; crowbaritem = false; thisitem[0] = crowbartext; iteminfo[0] = crowbarInfo; }
                else if (addBattery == true && indexSlots[0] == false) { boxes[0].GetComponent<slot>().slotbattery = true; indexSlots[0] = true; addBattery = false; thisitem[0] = batterytext; iteminfo[0] = batteryInfo; } //battery
                else if (addKey == true && indexSlots[0] == false) { boxes[0].GetComponent<slot>().slotKey = true; indexSlots[0] = true; addKey = false; thisitem[0] = Keytext; iteminfo[0] = keyInfo; } //key
                else if (addboxkey == true && indexSlots[0] == false) { boxes[0].GetComponent<slot>().slotboxKey = true; indexSlots[0] = true; addboxkey = false; thisitem[0] = boxKeytext; iteminfo[0] = boxkeyInfo; } //boxkey
                else if (addexitkey == true && indexSlots[0] == false) { boxes[0].GetComponent<slot>().slotexitkey = true; indexSlots[0] = true; addexitkey = false; thisitem[0] = exitKeytext; iteminfo[0] = exitkeyInfo; } //boxkey
                else if (addmap == true && indexSlots[0] == false) { boxes[0].GetComponent<slot>().slotmap = true; indexSlots[0] = true; addmap = false; thisitem[0] = maptext; iteminfo[0] = mapInfo; } //map
                else { thisitem[0] = "Empty"; }
                //add else if for more items : if (item == true && torchItem == false){}

            }
            else if (indexSlots[1] == false)
            {
                //slot 1 is empty
                if (torchItem == true) { boxes[1].GetComponent<slot>().slotTorch = true; indexSlots[1] = true; torchItem = false; thisitem[1] = torchtext; iteminfo[1] = TorchInfo; }
                else if (crowbaritem == true) { boxes[1].GetComponent<slot>().slotcrowbar = true; indexSlots[1] = true; crowbaritem = false; thisitem[1] = crowbartext; iteminfo[1] = crowbarInfo; }
                else if (addBattery == true && indexSlots[1] == false) { boxes[1].GetComponent<slot>().slotbattery = true; indexSlots[1] = true; addBattery = false; thisitem[1] = batterytext; iteminfo[1] = batteryInfo; } //battery
                else if (addKey == true && indexSlots[1] == false) { boxes[1].GetComponent<slot>().slotKey = true; indexSlots[1] = true; addKey = false; thisitem[1] = Keytext; iteminfo[1] = keyInfo; } //key
                else if (addboxkey == true && indexSlots[1] == false) { boxes[1].GetComponent<slot>().slotboxKey = true; indexSlots[1] = true; addboxkey = false; thisitem[1] = boxKeytext; iteminfo[1] = boxkeyInfo; } //boxkey
                else if (addexitkey == true && indexSlots[1] == false) { boxes[1].GetComponent<slot>().slotexitkey = true; indexSlots[1] = true; addexitkey = false; thisitem[1] = exitKeytext; iteminfo[1] = exitkeyInfo; } //boxkey
                else if (addmap == true && indexSlots[1] == false) { boxes[1].GetComponent<slot>().slotmap = true; indexSlots[1] = true; addmap = false; thisitem[1] = maptext; iteminfo[1] = mapInfo; } //map
                else { thisitem[1] = "Empty"; }

            }
            else if (indexSlots[2] == false)
            {
                //slot 2 is empty
                if (torchItem == true) { boxes[2].GetComponent<slot>().slotTorch = true; indexSlots[2] = true; torchItem = false; thisitem[2] = torchtext; iteminfo[2] = TorchInfo; }
                else if (crowbaritem == true) { boxes[2].GetComponent<slot>().slotcrowbar = true; indexSlots[2] = true; crowbaritem = false; thisitem[2] = crowbartext; iteminfo[2] = crowbarInfo; }
                else if (addBattery == true && indexSlots[2] == false) { boxes[2].GetComponent<slot>().slotbattery = true; indexSlots[2] = true; addBattery = false; thisitem[2] = batterytext; iteminfo[2] = batteryInfo; } //battery
                else if (addKey == true && indexSlots[2] == false) { boxes[2].GetComponent<slot>().slotKey = true; indexSlots[2] = true; addKey = false; thisitem[2] = Keytext; iteminfo[2] = keyInfo; } //key
                else if (addboxkey == true && indexSlots[2] == false) { boxes[2].GetComponent<slot>().slotboxKey = true; indexSlots[2] = true; addboxkey = false; thisitem[2] = boxKeytext; iteminfo[2] = boxkeyInfo; } //boxkey
                else if (addexitkey == true && indexSlots[2] == false) { boxes[2].GetComponent<slot>().slotexitkey = true; indexSlots[2] = true; addexitkey = false; thisitem[2] = exitKeytext; iteminfo[2] = exitkeyInfo; } //boxkey
                else if (addmap == true && indexSlots[2] == false) { boxes[2].GetComponent<slot>().slotmap = true; indexSlots[2] = true; addmap = false; thisitem[2] = maptext; iteminfo[2] = mapInfo; } //map
                else { thisitem[2] = "Empty"; }

            }
            else if (indexSlots[3] == false)
            {
                //slot 3 is empty
                if (torchItem == true) { boxes[3].GetComponent<slot>().slotTorch = true; indexSlots[3] = true; torchItem = false; thisitem[3] = torchtext; iteminfo[3] = TorchInfo; }
                else if (crowbaritem == true) { boxes[3].GetComponent<slot>().slotcrowbar = true; indexSlots[3] = true; crowbaritem = false; thisitem[3] = crowbartext; iteminfo[3] = crowbarInfo; }
                else if (addBattery == true && indexSlots[3] == false) { boxes[3].GetComponent<slot>().slotbattery = true; indexSlots[3] = true; addBattery = false; thisitem[3] = batterytext; iteminfo[3] = batteryInfo; } //battery
                else if (addKey == true && indexSlots[3] == false) { boxes[3].GetComponent<slot>().slotKey = true; indexSlots[3] = true; addKey = false; thisitem[3] = Keytext; iteminfo[3] = keyInfo; } //key
                else if (addboxkey == true && indexSlots[3] == false) { boxes[3].GetComponent<slot>().slotboxKey = true; indexSlots[3] = true; addboxkey = false; thisitem[3] = boxKeytext; iteminfo[3] = boxkeyInfo; } //boxkey
                else if (addexitkey == true && indexSlots[3] == false) { boxes[3].GetComponent<slot>().slotexitkey = true; indexSlots[3] = true; addexitkey = false; thisitem[3] = exitKeytext; iteminfo[3] = exitkeyInfo; } //boxkey
                else if (addmap == true && indexSlots[3] == false) { boxes[3].GetComponent<slot>().slotmap = true; indexSlots[3] = true; addmap = false; thisitem[3] = maptext; iteminfo[3] = mapInfo; } //map
                else { thisitem[3] = "Empty"; }

            }
            else if (indexSlots[4] == false)
            {
                //slot 4 is empty
                if (torchItem == true) { boxes[4].GetComponent<slot>().slotTorch = true; indexSlots[4] = true; torchItem = false; thisitem[4] = torchtext; iteminfo[4] = TorchInfo; }
                else if (crowbaritem == true) { boxes[4].GetComponent<slot>().slotcrowbar = true; indexSlots[4] = true; crowbaritem = false; thisitem[4] = crowbartext; iteminfo[4] = crowbarInfo; }
                else if (addBattery == true && indexSlots[4] == false) { boxes[4].GetComponent<slot>().slotbattery = true; indexSlots[4] = true; addBattery = false; thisitem[4] = batterytext; iteminfo[4] = batteryInfo; } //battery
                else if (addKey == true && indexSlots[4] == false) { boxes[4].GetComponent<slot>().slotKey = true; indexSlots[4] = true; addKey = false; thisitem[4] = Keytext; iteminfo[4] = keyInfo; } //key
                else if (addboxkey == true && indexSlots[4] == false) { boxes[4].GetComponent<slot>().slotboxKey = true; indexSlots[4] = true; addboxkey = false; thisitem[4] = boxKeytext; iteminfo[4] = boxkeyInfo; } //boxkey
                else if (addexitkey == true && indexSlots[4] == false) { boxes[4].GetComponent<slot>().slotexitkey = true; indexSlots[4] = true; addexitkey = false; thisitem[4] = exitKeytext; iteminfo[4] = exitkeyInfo; } //boxkey
                else if (addmap == true && indexSlots[4] == false) { boxes[4].GetComponent<slot>().slotmap = true; indexSlots[4] = true; addmap = false; thisitem[4] = maptext; iteminfo[4] = mapInfo; } //map
                else { thisitem[4] = "Empty"; }

            }
            else if (indexSlots[5] == false)
            {
                //slot 5 is empty

                if (torchItem == true) { boxes[5].GetComponent<slot>().slotTorch = true; indexSlots[5] = true; torchItem = false; thisitem[5] = torchtext; iteminfo[5] = TorchInfo; }
                else if (crowbaritem == true) { boxes[5].GetComponent<slot>().slotcrowbar = true; indexSlots[5] = true; crowbaritem = false; thisitem[5] = crowbartext; iteminfo[5] = crowbarInfo; }
                else if (addBattery == true && indexSlots[5] == false) { boxes[5].GetComponent<slot>().slotbattery = true; indexSlots[5] = true; addBattery = false; thisitem[5] = batterytext; iteminfo[5] = batteryInfo; } //battery
                else if (addKey == true && indexSlots[5] == false) { boxes[5].GetComponent<slot>().slotKey = true; indexSlots[5] = true; addKey = false; thisitem[5] = Keytext; iteminfo[5] = keyInfo; } //key
                else if (addboxkey == true && indexSlots[5] == false) { boxes[5].GetComponent<slot>().slotboxKey = true; indexSlots[5] = true; addboxkey = false; thisitem[5] = boxKeytext; iteminfo[5] = boxkeyInfo; } //boxkey
                else if (addexitkey == true && indexSlots[5] == false) { boxes[5].GetComponent<slot>().slotexitkey = true; indexSlots[5] = true; addexitkey = false; thisitem[5] = exitKeytext; iteminfo[5] = exitkeyInfo; } //boxkey
                else if (addmap == true && indexSlots[5] == false) { boxes[5].GetComponent<slot>().slotmap = true; indexSlots[5] = true; addmap = false; thisitem[5] = maptext; iteminfo[5] = mapInfo; } //map
                else { thisitem[5] = "Empty"; }

            }

            //full = false;
            for (int i = 0; i < length; i++)
            {
                if (indexSlots[i])
                {
                    full = true;
                }
                else
                {
                    full = false;
                    break;

                }


            }


            if (setimageactive == false)
            {
                var d = Input.GetAxis("Mouse ScrollWheel");
                if (d > 0f && optionsenabled == false)
                {
                    // scroll up
                    index++;
                    if (index >= 6)
                    {
                        index = 0;
                    }
                }
                else if (d < 0f && optionsenabled == false)
                {
                    // scroll down
                    index--;
                    if (index < 0)
                    {
                        index = 5;
                    }
                }

                if (d > 0f && optionsenabled == true)
                {
                    newindex--;
                    if (newindex < 0)
                    {
                        newindex = 2;
                    }
                }
                else if (d < 0f && optionsenabled == true)
                {
                    // scroll down
                    newindex++;
                    if (newindex >= 3)
                    {
                        newindex = 0;
                    }
                }
            }



            if (!optionsenabled)
            {
                title.text = thisitem[index];
                info.text = iteminfo[index];
            }

            //Debug.Log(index);

            if (Input.GetMouseButtonDown(0) && newindexenabled == false && optionsenabled == false && thisitem[index] != "Empty")
            {

                optionsenabled = true;
                options.SetActive(true);
                StartCoroutine(waiter());
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                optionsenabled = false;
                options.SetActive(false);
                newindexenabled = false;
            }

            if (gameObject.GetComponent<itemPickup>().triggered == true)
            {
                infoline.text = "";
            }

            if (setimageactive == true && Input.GetKeyDown(KeyCode.E))
            {
                setimageactive = false;
                imageviewer.SetActive(false);
            }

            if (Input.GetMouseButtonDown(0) && optionsenabled == true && newindexenabled == true && setimageactive == false) //left mouse click when the options are enabled
            {
                if (optionindicator[newindex].GetComponent<optionindication>().id == 0)//use ============================================================================================
                {
                    //use ite
                    //Debug.Log("using: " + thisitem[index]);
                    if (thisitem[index] == batterytext) // if selected item is a battery
                    {
                        //use battery
                        for (int i = 0; i < length; i++)
                        {
                            if (boxes[i].GetComponent<slot>().slotTorch == true)
                            {
                                Debug.Log("have torch");
                                havetorch = true;
                            }


                        }

                        if (havetorch == false)
                        {
                            StopCoroutine(wait());
                            infoline.text = "I can't use this on anything.";
                            StartCoroutine(wait());
                        }

                        if (havetorch == true) //the slots contain a torch
                        {
                            if (torch.GetComponent<flashlight>().batterylife <= 75) //if battery is less than 75%
                            {
                                remove();

                                torch.GetComponent<flashlight>().refill = true;
                                havetorch = false;

                            }
                            else
                            {
                                StopCoroutine(wait());
                                infoline.text = "The torch already has full power";
                                StartCoroutine(wait());
                            }


                        }
                    }
                    else if (thisitem[index] == torchtext && trigger == true) //if it is a torch
                    {
                        //torch
                        if(toolindex == torchtool)
                        {
                            StopCoroutine(wait());
                            infoline.text = "Torch already in use";
                            StartCoroutine(wait());
                        }
                        toolindex = torchtool; //torch is being used

                    }
                    else if (thisitem[index] == crowbartext && trigger == true)//if its a crowbar
                    {
                        //Debug.Log("crowbar");
                        if (toolindex == crowbartool)
                        {
                            StopCoroutine(wait());
                            infoline.text = "Crowbar already in use";
                            StartCoroutine(wait());
                        }
                        toolindex = crowbartool; //crowbar is being used
                        picked = false;
                        //ps fix the shed ui thing and this.
                    }
                    else if (thisitem[index] == Keytext) // if its a key
                    {
                        if (shedtrigger.GetComponent<shedtrigger>().havekey == true) //if player is close to the shed
                        {
                            usekey = true; //use key
                            remove();
                        }
                        else
                        {
                            StopCoroutine(wait());
                            infoline.text = "I can't use this on anything.";
                            StartCoroutine(wait());
                        }

                    }
                    else if (thisitem[index] == boxKeytext) // if its a box key
                    {
                        if (boxkeytrigger.GetComponent<boxtrigger>().havekey == true) //if player is close to the box
                        {
                            useboxkey = true; //use key
                            remove();
                        }
                        else
                        {
                            StopCoroutine(wait());
                            infoline.text = "I can't use this on anything.";
                            StartCoroutine(wait());
                        }

                    }
                    else if (thisitem[index] == exitKeytext) // if its a exit key
                    {
                        if (exitkeytrigger.GetComponent<exittrigger>().havekey == true) //if player is close to the exit
                        {
                            useexitkey = true; //use key
                            remove();
                        }
                        else
                        {
                            StopCoroutine(wait());
                            infoline.text = "I can't use this on anything.";
                            StartCoroutine(wait());
                        }

                    }
                    else if (thisitem[index] == maptext) //map
                    {
                        //map
                        imageviewer.SetActive(true);
                        setimageactive = true;

                    }


                }
                else if (optionindicator[newindex].GetComponent<optionindication>().id == 1)//drop ===================================================================================
                {
                    //drop
                    Debug.Log("dropping: " + thisitem[index]);
                    if (thisitem[index] == batterytext) // if dropped object is a battery <-- given that we have a battery
                    {
                        Instantiate(batteryprefab, this.transform.position + transform.forward * 0.5f, this.transform.rotation);
                        remove();
                    }
                    else if (thisitem[index] == torchtext) // if dropped object is a torch <-- given that we have a torch
                    {
                        bool havecrowbar = false;
                        for (int i = 0; i < length; i++) //when dropping the crowbar, check if there is a torch so the equipped tool becomes that
                        {
                            if (boxes[i].GetComponent<slot>().slotcrowbar == true)
                            {
                                havecrowbar = true;
                            }
                        }

                        if (havecrowbar == true)
                        {
                            toolindex = crowbartool;
                            havecrowbar = false;
                        }
                        else if (havecrowbar == false)
                        {
                            toolindex = 0;
                        }

                        Instantiate(torchprefab, this.transform.position + transform.forward * 0.5f, this.transform.rotation);
                        picked = false;
                        torchreal.SetActive(false);
                        remove();
                    }
                    else if (thisitem[index] == crowbartext)//if its a crowbar
                    {
                        bool havetorchs = false;
                        for (int i = 0; i < length; i++) //when dropping the crowbar, check if there is a torch so the equipped tool becomes that
                        {
                            if (boxes[i].GetComponent<slot>().slotTorch == true)
                            {
                                havetorchs = true;
                            }
                        }

                        if (havetorchs == true)
                        {
                            toolindex = torchtool;
                            havetorchs = false;
                        }
                        else if (havetorchs == false)
                        {
                            toolindex = 0;
                        }
                        Instantiate(crowbarprefab, this.transform.position + transform.forward * 0.5f, this.transform.rotation);
                        crowbarreal.SetActive(false);
                        remove();
                    }
                    else if (thisitem[index] == Keytext) //if its a key
                    {
                        Instantiate(keyprefab, this.transform.position + transform.forward * 0.5f, Quaternion.Euler(-90, 0, 0));
                        remove();
                    }
                    else if (thisitem[index] == boxKeytext) //if its a box key
                    {
                        Instantiate(boxkeyprefab, this.transform.position + transform.forward * 0.5f, Quaternion.Euler(0, 0, 0));
                        remove();
                    }
                    else if (thisitem[index] == exitKeytext) //if its a exit key
                    {
                        Instantiate(exitkeyprefab, this.transform.position + transform.forward * 0.5f, Quaternion.Euler(0, 0, 0));
                        remove();
                    }
                    else if (thisitem[index] == maptext) //map
                    {
                        //map
                        Instantiate(mapprefab, this.transform.position + transform.forward * 0.5f, Quaternion.Euler(0, 0, 0));
                        remove();
                    }


                }
                else if (optionindicator[newindex].GetComponent<optionindication>().id == 2)//cancel
                {
                    //cancel
                    optionsenabled = false;
                    newindexenabled = false;
                    options.SetActive(false);
                }

            }
        }
        


    }

    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Debug.Log("waited");
        newindexenabled = true;

    }

    IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(5);
        
        infoline.text = "";
    }

    void remove()
    {
        thisitem[index] = "Empty";
        indexSlots[index] = false;
        iteminfo[index] = "";
        title.text = thisitem[index];
        info.text = iteminfo[index];

        boxes[index].GetComponent<slot>().removesprite = true;
    }


}
