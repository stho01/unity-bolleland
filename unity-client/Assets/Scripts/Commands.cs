using UnityEngine;
using UnityEngine.EventSystems;

public class Commands : MonoBehaviour, IPointerClickHandler
{
    
    
    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        const string CommandButtonTag = "CommandButton";
        var commandButton = eventData.pointerCurrentRaycast.gameObject.transform;
        while (commandButton != null && !commandButton.gameObject.CompareTag(CommandButtonTag))
            commandButton = commandButton.transform.parent;

        if (commandButton != null)
        {
            var commandButtonState = commandButton.GetComponent<CommandButton>();
        }
    }
}
