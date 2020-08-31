using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyId : MonoBehaviour
{

    [SerializeField] Image          objCopy;

    public string                   toCopy;
    bool                            isOn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn && Input.GetMouseButtonDown(0))
            objCopy.color = Color.white;

    }

    public void Copy()
    {
        var textEditor = new TextEditor();
        textEditor.text = toCopy;
        textEditor.SelectAll();
        textEditor.Copy();

        objCopy.color = Color.red;

        isOn = true;
    }
}
