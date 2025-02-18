using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
public class Chair : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform LockPoint;
    public Transform StandPoint;
    public GameObject SitButton;
    public GameObject StandButton;
    public CharacterController playercontroller;
    public bool sit = false;
    public bool stand = false;
    public bool Assigned = false;
    void Start()
    {
       
    }
    //public void 
    // Update is called once per frame
    void Update()
    {

        if (InputBridge.Instance.AButtonUp || Input.GetKeyDown(KeyCode.B))
        {
           
            if ( playercontroller!=null&&sit == false)
            {
                SitFunction();
            }
            else
            {
                StandFuntion();
            }
           
        }
    }
    public void SitFunction()
    {
       
            sit = true;
            stand = false;
            playercontroller.transform.position = LockPoint.position;
            //  playercontroller.transform.rotation = LockPoint.rotation;
            playercontroller.enabled = false;
          //  Assigned = true;
        
      
    }
    public void StandFuntion()
    {
        sit = false;
        stand = true;
        if (playercontroller != null)
        {
            playercontroller.transform.position = StandPoint.position;
            playercontroller.enabled = true;
        }
 
       // Assigned = false;
      //  playercontroller.transform.rotation = StandPoint.rotation;
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && playercontroller ==null ) 
        {
            playercontroller = other.GetComponent<CharacterController>();
           // SitButton.SetActive(true);
        }
   
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && playercontroller != null)
        {
            playercontroller = null;
          //  SitButton.SetActive(true);
        }
    }
}
