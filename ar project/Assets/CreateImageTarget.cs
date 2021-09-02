using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class CreateImageTarget : MonoBehaviour
{
    public GameObject augmentationObject;
    // Start once Vuforia is initialized
    void Start()
    {
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
    }
    void OnVuforiaStarted()
    {
        int counter = 0;
        // Loop through all activated trackables
        IEnumerable<TrackableBehaviour> tbs = TrackerManager.Instance.GetStateManager().GetTrackableBehaviours();
        foreach (TrackableBehaviour tb in tbs)
        {
            // This will filter by trackables that haven't been defined previously in the scene.
            if (tb.name == "New Game Object")
            {
                // Change generic name to include trackable name
                tb.gameObject.name = ++counter + ":DynamicImageTarget-" + tb.TrackableName;
                // Add additional script components for trackable
                tb.gameObject.AddComponent<DefaultTrackableEventHandler>();
                tb.gameObject.AddComponent<TurnOffBehaviour>();
                // This section will add an augmentation based off the GameObject defined on the script.
                // Replace this with whatever you prefer to augment
                if (augmentationObject != null)
                {
                    // instantiate augmentation object and parent to trackable
                    GameObject augmentation = (GameObject)GameObject.Instantiate(augmentationObject);
                    augmentation.transform.parent = tb.gameObject.transform;
                    augmentation.transform.localPosition = new Vector3(0f, 0f, 0f);
                    augmentation.transform.localRotation = Quaternion.identity;
                    augmentation.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    augmentation.gameObject.SetActive(true);
                }
                else
                {
                    Debug.Log("<color=yellow>Warning: No augmentation object specified for: " + tb.TrackableName + "</color>");
                }
            }
        }
    }
}
