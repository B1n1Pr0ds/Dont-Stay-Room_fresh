using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;



//this script is responsable to set the action for the character idepedent on view.
public class Actions : MonoBehaviour
{
    [Header("Change Perpective")]
    [SerializeField] Behaviour[] tPView; //components of the third person view;
    [SerializeField] Behaviour[] fPView; //components of the first person view
    public bool pov;
    

    [SerializeField] private GameManager gameManager;

    [Header("Use Button")]
    [SerializeField] private TextMeshProUGUI useText;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float MaxUseDistance;
    [SerializeField] private LayerMask useLayers;

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            TogglePov();
            if (pov == true)
            {
                DisableComponents(tPView);
                EnableComponents(fPView);
            }
            if (pov == false)
            {
                DisableComponents(fPView);
                EnableComponents(tPView);
            }
        
        }

        if(Input.GetKeyDown(KeyCode.E)) 
        {
            ItemInteract();
        }

        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, MaxUseDistance, useLayers) && hit.collider.TryGetComponent<Door>(out Door _door))
        {
            if(_door.isOpen)
            {
                useText.SetText("Close \"E\"");
            }
            else
            {
                useText.SetText("Open \"E\"");
            }
            useText.gameObject.SetActive(true);
            
        }
        else
        {
            useText.gameObject.SetActive(false);
        }
        if(Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(0);
        }
    }
    void ItemInteract()
    {
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, MaxUseDistance, useLayers))
        {
            if (hit.collider.TryGetComponent<Door>(out Door door))
            {
                if (door.isOpen)
                {
                    door.Close();
                }
                else
                {
                    door.Open(transform.position);
                }
            }
        }

    }



    void DisableComponents(Behaviour[] disableThis)
    {
        for (int i = 0; i < disableThis.Length; i++)
        {
            disableThis[i].enabled = false;
        }
    }
    void EnableComponents(Behaviour[] enableThis)
    {
        for (int i = 0; i < enableThis.Length; i++)
        {
            enableThis[i].enabled = true;
        }
    }

    void TogglePov()
    {
        pov = !pov;
    }

   
}
