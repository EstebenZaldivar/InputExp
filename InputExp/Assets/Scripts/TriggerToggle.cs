using UnityEngine;

public class TriggerToggle : MonoBehaviour
{
    [Tooltip("Object to toggle when triggered.")]
    public GameObject targetObject;

    [Tooltip("Tag of the object that can activate the trigger.")]
    public string triggeringTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggeringTag) && targetObject != null)
        {
            bool isActive = targetObject.activeSelf;
            targetObject.SetActive(!isActive);
            Debug.Log($"{targetObject.name} toggled to {!isActive} by {other.name}");
        }
    }
}
