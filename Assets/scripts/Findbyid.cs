using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Findbyid : MonoBehaviour
{
    InputField                  idInput;
    [SerializeField] Dropdown   nameDrop;
    Load                        loadClass;
    [SerializeField] string     whatFind = "M";

    List<Animal>                lstAnimals = new List<Animal>();
    bool                        modif;


    void             Awake()
    {
        loadClass = Load.instance;
        idInput = this.GetComponent<InputField>();
        idInput.text = "";
        CheckId();
    }

    public void     CheckId()
    {
        if (modif == true)
        {
            modif = false;
        }
        List<string> lst_name_animals = new List<string>();

        if (whatFind == "M")
            lst_name_animals.Add("Father");
        else if(whatFind == "F")
            lst_name_animals.Add("Mother");

        for (int i = 0; i < loadClass.catLstAnimals.Count; i++)
        {
            string compare = "-";
            if(idInput.text.Length <= loadClass.catLstAnimals[i].animalIdentification.Length)
            {
                compare = loadClass.catLstAnimals[i].animalIdentification.Substring(0, idInput.text.Length);
            }


            if(loadClass.catLstAnimals[i].animalSex == whatFind && idInput.text == compare)
            {
                lstAnimals.Add(loadClass.catLstAnimals[i]);
                lst_name_animals.Add(loadClass.catLstAnimals[i].animalName);
            }
        }
        modif = true;
        nameDrop.ClearOptions();
        nameDrop.AddOptions(lst_name_animals);
    }

    public void     CheckDrop()
    {
        if (modif == true)
        {
            modif = false;
        }

        if (nameDrop.value != 0)
            idInput.text = lstAnimals[nameDrop.value - 1].animalIdentification;
        else
            idInput.text = "";

        modif = true;
    }

    public void Set_drop_value()
    {

    }
}
