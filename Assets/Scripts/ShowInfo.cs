using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string Text;
    public Text Info;
    //private Highlighter _highliter = null;
    // Start is called before the first frame update
    void Start()
    {
       // _highliter = GetComponent<Highlighter>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Info.text = Text;
        //_highliter.ConstantOn();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Info.text = "";
        //_highliter.ConstantOff();
    }
}
