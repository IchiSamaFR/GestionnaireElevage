using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Tools;

public class Showanimals : MonoBehaviour
{
    #region --  initialize  --
    Load                            loadClass;
    [SerializeField] GameObject     linePrefab;
    [SerializeField] Transform      lineContent;
    [SerializeField] List<Sprite>   imgSex = new List<Sprite>();
    [SerializeField] InputField     inputSearch;

    [Header("New")]
    List<string>                    lstRace = new List<string>();
    [SerializeField] GameObject     addNewPanel;

    [Header("Name")]
    [SerializeField] InputField     inputName;
    [SerializeField] Image          mustName;

    [Header("Sex")]
    [SerializeField] Toggle         tglMale;

    [Header("Date")]
    [SerializeField] InputField     inputDay;
    [SerializeField] InputField     inputMonth;
    [SerializeField] InputField     inputYear;
    [SerializeField] Image          mustDate;

    [Header("Race")]
    [SerializeField] Dropdown       dropRace;
    [SerializeField] Image          mustRace;


    [Header("Id")]
    [SerializeField] InputField     inputId;
    [SerializeField] Image          mustId;

    [Header("Others")]
    [SerializeField] InputField     inputLoof;
    [SerializeField] InputField     inputFather;
    [SerializeField] InputField     inputMother;
    #endregion

    void Start()
    {
        loadClass = Load.instance;
        Show();
    }

    #region --  show  --
    public void Show()
    {
        for (int i = 0; i < lineContent.childCount; i++)
        {
            Destroy(lineContent.GetChild(i).gameObject);
        }

        List<Animal> lstAnimals;
        lstAnimals = loadClass.GetCatLst();

        for (int i = 0; i < lstAnimals.Count; i++)
        {
            if(LastEvent(lstAnimals[i].animalIdentification, loadClass.path, true) == "10")
            {
                return;
            }
            GameObject tmpItem = Instantiate(linePrefab, lineContent);

            tmpItem.name = i.ToString();
            tmpItem.transform.GetChild(0).GetComponent<Text>().text = lstAnimals[i].animalName;
            if (lstAnimals[i].animalSex == "M")
            {
                tmpItem.transform.GetChild(1).GetComponent<Image>().sprite = imgSex[0];
            }
            else
            {
                tmpItem.transform.GetChild(1).GetComponent<Image>().sprite = imgSex[1];
            }

            tmpItem.transform.GetChild(2).GetComponent<Text>().text = lstAnimals[i].birthDay + " / " + lstAnimals[i].birthMonth + " / " + lstAnimals[i].birthYear;
            tmpItem.transform.GetChild(3).GetComponent<Text>().text = loadClass.catLstRaces[loadClass.GetArrayId(lstAnimals[i].animalRace)].raceName;
            tmpItem.transform.GetChild(4).GetComponent<Text>().text = LastEvent(lstAnimals[i].animalIdentification, loadClass.path);
            tmpItem.transform.GetChild(5).GetComponent<Text>().text = lstAnimals[i].animalIdentification;
            tmpItem.transform.GetChild(6).GetComponent<Text>().text = lstAnimals[i].animalLoof;

            tmpItem.SetActive(true);
        }
    }
    public void ShowByName()
    {
        string toVerify = inputSearch.text;
        List<Animal> lstAnimals;


        if(toVerify == "")
        {
            Show();
            return;
        }


        for (int i = 0; i < lineContent.childCount; i++)
        {
            Destroy(lineContent.GetChild(i).gameObject);
        }

        lstAnimals = loadClass.GetCatLst();
        
        for (int i = 0; i < lstAnimals.Count; i++)
        {
            string compare = "";

            if (toVerify.Length > loadClass.catLstAnimals[i].animalName.Length)
            {
                return;
            }

            compare = loadClass.catLstAnimals[i].animalName.Substring(0, toVerify.Length);


            if (toVerify == compare)
            {
                GameObject tmpItem = Instantiate(linePrefab, lineContent);

                tmpItem.name = i.ToString();
                tmpItem.transform.GetChild(0).GetComponent<Text>().text = lstAnimals[i].animalName;

                if (lstAnimals[i].animalSex == "M")
                    tmpItem.transform.GetChild(1).GetComponent<Image>().sprite = imgSex[0];
                else
                    tmpItem.transform.GetChild(1).GetComponent<Image>().sprite = imgSex[1];

                tmpItem.transform.GetChild(2).GetComponent<Text>().text = lstAnimals[i].birthDay + " / " + lstAnimals[i].birthMonth + " / " + lstAnimals[i].birthYear;
                tmpItem.transform.GetChild(3).GetComponent<Text>().text = loadClass.catLstRaces[loadClass.GetArrayId(lstAnimals[i].animalRace)].raceName;
                tmpItem.transform.GetChild(4).GetComponent<Text>().text = "";
                tmpItem.transform.GetChild(5).GetComponent<Text>().text = lstAnimals[i].animalIdentification;
                tmpItem.transform.GetChild(6).GetComponent<Text>().text = lstAnimals[i].animalLoof;

                tmpItem.SetActive(true);
            }
        }
    }

    #endregion

    public void     Reset_add_panel()
    {
        lstRace = loadClass.GetRaces();

        inputName.text = "";

        tglMale.isOn = true;

        inputDay.text = "";
        inputMonth.text = "";
        inputYear.text = "";

        dropRace.ClearOptions();
        dropRace.AddOptions(lstRace);
        dropRace.value = 0;

        inputId.text = "";
        inputLoof.text = "";
        inputFather.text = "";
        inputMother.text = "";

        mustName.color = Color.black;
        mustDate.color = Color.black;
        mustId.color = Color.black;
        mustRace.color = Color.black;
    }

    public void     Create_Animal()
    {
        //Si 
        if (inputName.text != "" && inputDay.text != "" && inputMonth.text != "" && inputYear.text != "" && inputId.text != "" && dropRace.value != 0
            && int.Parse(inputDay.text) <= 31 && int.Parse(inputMonth.text) <= 12 && int.Parse(inputYear.text) <= DateTime.Now.Year)
        {
            int count;
            string name;
            string sex;
            int day;
            int month;
            int year;
            string race;
            string id;
            string loof;
            string father;
            string mother;



            name = inputName.text;
            if (tglMale.isOn)
                sex = "M";
            else
                sex = "F";
            day = int.Parse(inputDay.text);
            month = int.Parse(inputMonth.text);
            year = int.Parse(inputYear.text);
            race = loadClass.catLstRaces[dropRace.value].id;
            id = inputId.text;
            loof = inputLoof.text;
            father = inputFather.text;
            mother = inputMother.text;


            
            count = loadClass.catLstAnimals.Count;
            loadClass.CreateNewAnimal(count - 1, "Cat", name, sex, day, month, year, race, id, loof, father, mother);

            addNewPanel.SetActive(false);
            Show();
        } else
        {
            if (inputName.text == "")
                mustName.color = Color.red;
            else
                mustName.color = Color.black;

            if (inputDay.text == "" || inputMonth.text == "" || inputYear.text == "" || int.Parse(inputDay.text) > 31 || int.Parse(inputMonth.text) > 12 || int.Parse(inputYear.text) > DateTime.Now.Year)
                mustDate.color = Color.red;
            else
                mustDate.color = Color.black;

            if (inputId.text == "")
                mustId.color = Color.red;
            else
                mustId.color = Color.black;

            if (dropRace.value == 0)
                mustDate.color = Color.red;
            else
                mustDate.color = Color.black;
        }
    }


}
