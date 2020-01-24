using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {


  
    [SerializeField] PlaySpace playSpace;
    public Location location = new Location(0, 0);
    private ViewController vc;
    private Button button;

    
    public PlaySpace PlaySpace()
    {
        return playSpace;
    }

    public void SetIdentity(ViewController viewController, Location location)
    {
        vc = viewController;
        this.location = location;
    }

    public void MakeMove()
    {
        vc.Select(location);
    }

    public void PressSelf()
    {
        //
        vc.Select(location);
        //playSpace.CycleUnit(forward: true);

     //   playspace.sprite = null;
    }


    public void ToggleActive(bool toggleOn)
    {
        
            button.enabled = toggleOn;
        
    }




     void Start()
    {
        button = gameObject.GetComponent<Button>();
    }

}
