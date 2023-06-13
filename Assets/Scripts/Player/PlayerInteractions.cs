using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteractions : MonoBehaviour 
{
    [Header("Components")]
    [SerializeField] bool inTransition;

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !inTransition)
        {
            StartCoroutine(ElevatorInteract());
        }
    }

    IEnumerator ElevatorInteract()
    {
        inTransition = true;
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, 10000))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null )
            {
                interactable.Interact();
            }
        }
        yield return new WaitForSeconds(1f);
        inTransition = false;
    }


}
