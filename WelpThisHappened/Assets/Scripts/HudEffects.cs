using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudEffects : MonoBehaviour {
    public Button itemMenuBtn;

    Animator anim;
    bool btnClicked = false;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        itemMenuBtn.onClick.AddListener(() => { ButtonClicked(btnClicked = true); });
    }

    private void ButtonClicked(bool btnClicked)
    {
        //if I add any more button this will have to be upgraded..oh I will have to upgrade it to close the menu ... dumbass
        //anim.SetTrigger("ItemMenuTrig");
        anim.SetBool("ItemMenu", btnClicked);
    }

}
