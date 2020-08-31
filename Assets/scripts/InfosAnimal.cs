using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Tools;

public class InfosAnimal : MonoBehaviour
{
    #region --  initialize  --
    [Header("Class")]
    Load                            loadClass;
    [SerializeField] Showanimals    showClass;
    string                          path;

    [Header("Items")]
    [SerializeField] GameObject     itemNew;
    [SerializeField] GameObject     itemRearing;
    [SerializeField] GameObject     itemMedic;

    [SerializeField] Transform      content;
    Animal                          animal;
    AnimalEvent                     eventHere;

    [Header("Show")]
    [SerializeField] GameObject     eventPanel;
    [SerializeField] GameObject     modEventPanel;
    [SerializeField] GameObject     showPanel;
    [SerializeField] GameObject     modifPanel;

    [Header("Show")]
    [SerializeField] List<Sprite>   imgSex;

    [SerializeField] Text           animalName;
    [SerializeField] Text           animalSex;
    [SerializeField] Image          animalSexImg;
    [SerializeField] Text           animalDate;
    [SerializeField] Text           animalRace;
    [SerializeField] Text           animalIdent;
    [SerializeField] Text           animalLoof;
    [SerializeField] Text           animalFather;
    [SerializeField] Text           animalMother;

    [Header("Modif")]
    [SerializeField] InputField     modAnimalName;
    [SerializeField] Toggle         modAnimalSex;
    [SerializeField] Toggle         modAnimalSexF;
    [SerializeField] InputField     modAnimalDay;
    [SerializeField] InputField     modAnimalMonth;
    [SerializeField] InputField     modAnimalYear;
    [SerializeField] Dropdown       modAnimalRace;
    [SerializeField] InputField     modAnimalIdent;
    [SerializeField] InputField     modAnimalLoof;
    [SerializeField] InputField     modAnimalFather;
    [SerializeField] InputField     modAnimalMother;


    [Header("Modif must img")]
    [SerializeField] Image          mustName;
    [SerializeField] Image          mustDate;
    [SerializeField] Image          mustRace;
    [SerializeField] Image          mustIdent;


    [Header("Event")]
    [SerializeField] Dropdown       eventPossible;
    [SerializeField] GameObject     eventDate;
    [SerializeField] InputField     eventDay;
    [SerializeField] InputField     eventMonth;
    [SerializeField] InputField     eventYear;
    [SerializeField] InputField     eventInformations;
    [SerializeField] InputField     eventFather;

    [Header("ShowEvent")]
    [SerializeField] InputField     modifEventName;
    [SerializeField] InputField     modifEventDay;
    [SerializeField] InputField     modifEventMonth;
    [SerializeField] InputField     modifEventYear;
    [SerializeField] InputField     modifEventInformations;
    [SerializeField] InputField     modifEventFather;

    List<AnimalEvent>               eventLst = new List<AnimalEvent>();
    List<EventType>                 eventTypeLst;

    #endregion

    void Awake()
    {
        loadClass = Load.instance;
        path = loadClass.path;
        LoadEventsType();
        CheckEvent();
    }

    #region -------- ANIMAL --------

    //      Affiche les informations de l'animal
    public void LoadAnimal(GameObject itemAnimal)
    {
        eventPanel.SetActive(false);
        modEventPanel.SetActive(false);
        showPanel.SetActive(true);
        modifPanel.SetActive(false);

        int idArray = int.Parse(itemAnimal.name);
        animal = loadClass.catLstAnimals[idArray];
        
        //      Appel des stats
        LoadAnimalStats(animal);

        //      Appel des evenements
        LoadAnimalEvents(animal);
    }

    //      Appel des stats, Nom, date etc...
    void LoadAnimalStats(Animal animalHere)
    {
        animalName.text = animalHere.animalName;
        if (animalHere.animalSex == "M"){
            animalSex.text = "Male";
            animalSexImg.sprite = imgSex[0];
        } else {
            animalSex.text = "Female";
            animalSexImg.sprite = imgSex[1];
        }
        animalDate.text = animalHere.birthDay + " / " + animalHere.birthMonth + " / " + animalHere.birthYear;
        animalRace.text = loadClass.GetRacesNames(animalHere.animalRace);

        animalIdent.text = animalHere.animalIdentification;
        animalLoof.text = animalHere.animalLoof;
        animalFather.text = animalHere.animalFather;
        animalMother.text = animalHere.animalMother;
    }

    //      Affiche les evenements liés à l'animal
    void LoadAnimalEvents(Animal animalHere)
    {
        string[] lines;
        int x;

        x = 0;
        eventLst = new List<AnimalEvent>();

        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }

        if (!File.Exists(path + @"\Events\" + animalHere.animalIdentification + ".txt"))
        {
            File.Create(path + @"\Events\" + animalHere.animalIdentification + ".txt");
        } else
        {
            lines = File.ReadAllLines(path + @"\Events\" + animalHere.animalIdentification + ".txt");

            foreach (string line in lines)
            {
                var data_values = line.Split(new char[] { ';' });

                eventLst.Add(new AnimalEvent());

                DateTime date = DateTime.ParseExact(data_values[1], "ddMMyyyy",
                                                System.Globalization.CultureInfo.InvariantCulture);

                eventLst[x].Set(data_values[0], date, data_values[2]);
                x++;
            }
        }
        eventLst.Sort((AnimalEvent c1, AnimalEvent c2) => c1.date.CompareTo(c2.date));


        GameObject tmp_item = Instantiate(itemNew, content);

        tmp_item.name = "newEvent";
        tmp_item.SetActive(true);

        for (int i = 0; i < eventLst.Count; i++)
        {
            if(eventLst[i].idEvent == "8")
            {
                GameObject item = Instantiate(itemMedic, content);

                item.name = i.ToString();
                item.transform.GetChild(1).GetComponent<Text>().text = eventLst[i].date.ToString("dd / MM / yyyy");
                item.transform.GetChild(2).GetComponent<Text>().text = GetEventNameById(eventLst[i].idEvent, eventTypeLst);
                item.SetActive(true);
            }
            else
            {
                GameObject item = Instantiate(itemRearing, content);

                item.name = i.ToString();
                item.transform.GetChild(1).GetComponent<Text>().text = eventLst[i].date.ToString("dd / MM / yyyy");
                item.transform.GetChild(2).GetComponent<Text>().text = GetEventNameById(eventLst[i].idEvent, eventTypeLst);
                item.SetActive(true);
            }
        }

    }

    //      Reset du panel de modifications
    public void SetModifPanel()
    {
        List<String> lstRace = loadClass.GetRaces();

        modAnimalName.text = animal.animalName;

        if (animal.animalSex == "M")
        {
            modAnimalSex.isOn = true;
            modAnimalSexF.isOn = false;
        }
        else
        {
            modAnimalSex.isOn = false;
            modAnimalSexF.isOn = true;
        }


        modAnimalDay.text = animal.birthDay.ToString();
        modAnimalMonth.text = animal.birthMonth.ToString();
        modAnimalYear.text = animal.birthYear.ToString();

        modAnimalRace.ClearOptions();
        modAnimalRace.AddOptions(lstRace);
        modAnimalRace.value = loadClass.GetArrayId(animal.animalRace);

        modAnimalIdent.text = animal.animalIdentification;
        modAnimalLoof.text = animal.animalLoof;
        modAnimalFather.text = animal.animalFather;
        modAnimalMother.text = animal.animalMother;
    }

    //          Valide les modifications apporté lorsque l'on clique sur "Applied"
    public void ValidModif()
    {
        //          Si les données minimales sont correctes alors modifier les valeurs
        if (modAnimalName.text != "" && modAnimalDay.text != "" && modAnimalDay.text != "" && modAnimalDay.text != ""
            && modAnimalIdent.text != "" && modAnimalRace.value != 0
            && int.Parse(modAnimalDay.text) <= 31 && int.Parse(modAnimalDay.text) <= 12 && int.Parse(modAnimalDay.text) <= DateTime.Now.Year)
        {
            animal.animalName = modAnimalName.text;

            if (modAnimalSex.isOn)
                animal.animalSex = "M";
            else
                animal.animalSex = "F";

            animal.birthDay = int.Parse(modAnimalDay.text);
            animal.birthMonth = int.Parse(modAnimalMonth.text);
            animal.birthYear = int.Parse(modAnimalYear.text);

            animal.animalRace = loadClass.catLstRaces[modAnimalRace.value].id;


            if (animal.animalIdentification != modAnimalIdent.text)
            {
                loadClass.ChangeIdentNumber(animal.animalIdentification, modAnimalIdent.text);
                animal.animalIdentification = modAnimalIdent.text;
            }

            animal.animalLoof = modAnimalLoof.text;
            animal.animalFather = modAnimalFather.text;
            animal.animalMother = modAnimalMother.text;

            showClass.Show();
            LoadAnimalStats(animal);
            loadClass.SaveAnimals();
            showPanel.SetActive(true);
            modifPanel.SetActive(false);
        }
        //          Sinon affiché le soucis
        else
        {
            if (modAnimalName.text == "")
                mustName.color = Color.red;
            else
                mustName.color = Color.black;

            if (modAnimalDay.text == "" || modAnimalMonth.text == "" || modAnimalYear.text == ""
                || int.Parse(modAnimalDay.text) > 31 || int.Parse(modAnimalMonth.text) > 12 || int.Parse(modAnimalYear.text) > DateTime.Now.Year)
                mustDate.color = Color.red;
            else
                mustDate.color = Color.black;

            if (modAnimalIdent.text == "")
                mustIdent.color = Color.red;
            else
                mustIdent.color = Color.black;

            if (modAnimalRace.value == 0)
                mustDate.color = Color.red;
            else
                mustDate.color = Color.black;
        }
    }

    #endregion

    //                  EVENTS
    #region -------- Events -------
    //      Permet de get les differents events possibles
    void LoadEventsType()
    {
        string[] lines;
        int x;


        x = 0;
        eventTypeLst = new List<EventType>();

        lines = File.ReadAllLines(path + @"\Config\event_list_fr.txt");


        foreach (string line in lines)
        {
            var dataValues = line.Split(new char[] { ';' });

            eventTypeLst.Add(new EventType());
            eventTypeLst[x].Set(dataValues[0], dataValues[1]);
            x++;
        }
    }

    public void RefreshEventDrop()
    {
        bool find = false;
        List<string> lstEvents = new List<string>();

        eventPossible.ClearOptions();

        if(animal.animalSex == "F")
        {
            
            for (int i = eventLst.Count - 1; i >= 0; i--)
            {
                lstEvents.Add(GetEventNameById("2", eventTypeLst));
                lstEvents.Add(GetEventNameById("3", eventTypeLst));
                lstEvents.Add(GetEventNameById("4", eventTypeLst));
                lstEvents.Add(GetEventNameById("7", eventTypeLst));
                lstEvents.Add(GetEventNameById("8", eventTypeLst));
                /*
                if (eventLst[i].idEvent == "1")
                {
                    lstEvents.Add(GetEventNameById("2", eventTypeLst));
                    lstEvents.Add(GetEventNameById("3", eventTypeLst));
                    lstEvents.Add(GetEventNameById("4", eventTypeLst));
                    lstEvents.Add(GetEventNameById("7", eventTypeLst));
                    lstEvents.Add(GetEventNameById("8", eventTypeLst));
                    find = true;
                    break;
                }
                else if (eventLst[i].idEvent == "2")
                {
                    lstEvents.Add(GetEventNameById("3", eventTypeLst));
                    lstEvents.Add(GetEventNameById("4", eventTypeLst));
                    lstEvents.Add(GetEventNameById("7", eventTypeLst));
                    lstEvents.Add(GetEventNameById("8", eventTypeLst));
                    find = true;
                    break;
                }
                else if (eventLst[i].idEvent == "3")
                {
                    lstEvents.Add(GetEventNameById("2", eventTypeLst));
                    lstEvents.Add(GetEventNameById("3", eventTypeLst));
                    lstEvents.Add(GetEventNameById("4", eventTypeLst));
                    lstEvents.Add(GetEventNameById("7", eventTypeLst));
                    lstEvents.Add(GetEventNameById("8", eventTypeLst));
                    find = true;
                    break;
                }
                else if (eventLst[i].idEvent == "4")
                {
                    lstEvents.Add(GetEventNameById("2", eventTypeLst));
                    lstEvents.Add(GetEventNameById("3", eventTypeLst));
                    lstEvents.Add(GetEventNameById("4", eventTypeLst));
                    lstEvents.Add(GetEventNameById("5", eventTypeLst));
                    lstEvents.Add(GetEventNameById("7", eventTypeLst));
                    lstEvents.Add(GetEventNameById("8", eventTypeLst));
                    find = true;
                    break;
                }
                else if (eventLst[i].idEvent == "5")
                {
                    lstEvents.Add(GetEventNameById("6", eventTypeLst));
                    lstEvents.Add(GetEventNameById("8", eventTypeLst));
                    find = true;
                    break;
                }
                else if (eventLst[i].idEvent == "6")
                {
                    lstEvents.Add(GetEventNameById("2", eventTypeLst));
                    lstEvents.Add(GetEventNameById("3", eventTypeLst));
                    lstEvents.Add(GetEventNameById("8", eventTypeLst));
                    find = true;
                    break;
                }
                else if (eventLst[i].idEvent == "7")
                {
                    lstEvents.Add(GetEventNameById("8", eventTypeLst));
                    find = true;
                    break;
                }*/
            }
        }
        else if (animal.animalSex == "M")
        {
            
            for (int i = eventLst.Count - 1; i >= 0; i--)
            {
                lstEvents.Add(GetEventNameById("7", eventTypeLst));
                lstEvents.Add(GetEventNameById("8", eventTypeLst));
                /*
                if (eventLst[i].idEvent == "1")
                {
                    lstEvents.Add(GetEventNameById("7", eventTypeLst));
                    lstEvents.Add(GetEventNameById("8", eventTypeLst));
                    find = true;
                    break;
                } 
                else if (eventLst[i].idEvent == "7" || eventLst[i].idEvent == "7")
                {
                    lstEvents.Add(GetEventNameById("8", eventTypeLst));
                    find = true;
                    break;
                }*/
            }
        }
        if(find == false)
        {
            lstEvents.Add(GetEventNameById("1", eventTypeLst));
        }

        lstEvents.Insert(0, "Event");
        eventPossible.AddOptions(lstEvents);
        CheckEvent();
    }

    //      Permet de creer un nouvel evenement
    public void CreateNewEvent()
    {
        // Si la date n'est pas null
        if(eventDay.text != "" && eventMonth.text != "" && eventYear.text != "")
        {
            // Verification si les dates ne sont pas null
            if (int.Parse(eventDay.text) == 0 || int.Parse(eventMonth.text) == 0 || int.Parse(eventYear.text) == 0)
            {
                return;
            }

            // Get l'id de l'event
            string id = GetEventIdByName(eventPossible.captionText.text, eventTypeLst);


            if (eventDay.text.Length != 2)
            {
                eventDay.text = "0" + eventDay.text;
            }
            if (eventMonth.text.Length != 2)
            {
                eventMonth.text = "0" + eventMonth.text;
            }


            DateTime date = DateTime.ParseExact(eventDay.text + eventMonth.text + eventYear.text,
                "MMddyyyy", System.Globalization.CultureInfo.InvariantCulture);
            string info = eventInformations.text;
            string father = eventFather.text;
            eventLst.Add(new AnimalEvent());
            eventLst[eventLst.Count - 1].Set(id, date, info, father);
            eventPanel.SetActive(false);

            SaveEvent();
            LoadAnimalEvents(animal);
        }
    }
    

    public void ShowEventCreated(GameObject obj)
    {
        string id = obj.name;
        AnimalEvent thisEvent = eventLst[int.Parse(id)];
        eventHere = thisEvent;

        modifEventName.text = GetEventNameById(thisEvent.idEvent, eventTypeLst);
        modifEventDay.text = thisEvent.date.ToString("dd");
        modifEventMonth.text = thisEvent.date.ToString("MM");
        modifEventYear.text = thisEvent.date.ToString("yyyy");
        modifEventInformations.text = thisEvent.informations.Replace("&n", "\n");

        if(thisEvent.idEvent != "")
        {
            modifEventFather.text = thisEvent.father;
            modifEventFather.gameObject.SetActive(true);
        } else
        {
            modifEventFather.gameObject.SetActive(false);
        }
    }



    //      Check quel est l'event et affiché quelques choses en fonction
    public void CheckEvent()
    {
        ResetEvent();

        if (eventPossible.value != 0)
        {
            if (GetEventIdByName(eventPossible.captionText.text, eventTypeLst) == "4")
            {
                eventDate.SetActive(true);
                eventInformations.gameObject.SetActive(true);
                eventFather.gameObject.SetActive(true);
            }
            else
            {
                eventDate.SetActive(true);
                eventInformations.gameObject.SetActive(true);
            }
        }
    }
    public void ResetEvent()
    {
        eventDay.text = DateTime.Now.Day.ToString();
        eventMonth.text = DateTime.Now.Month.ToString();
        eventYear.text = DateTime.Now.Year.ToString();
        eventInformations.text = "";
        eventFather.text = "";

        eventDate.SetActive(false);
        eventInformations.gameObject.SetActive(false);
        eventFather.gameObject.SetActive(false);
    }

    public void SaveEvent()
    {
        StreamWriter file = new StreamWriter(path + @"\Events\" + animal.animalIdentification + ".txt");

        for (int i = 0; i < eventLst.Count; i++)
        {
            file.Write(eventLst[i].GetValues() + "\n");
        }
        file.Close();
    }

    public void ModifEvent()
    {
        DateTime date = DateTime.ParseExact(modifEventDay.text + modifEventMonth.text + modifEventYear.text, "ddMMyyyy",
                                        System.Globalization.CultureInfo.InvariantCulture);
        eventHere.date = date;
        eventHere.informations = modifEventInformations.text.Replace("\n", "&n");
        eventHere.father = modifEventFather.text;

        modEventPanel.SetActive(false);
        SaveEvent();
        LoadAnimalEvents(animal);
    }
    #endregion



}

public class AnimalEvent
{
    public string       idEvent;
    public DateTime     date;
    public string       informations;
    public string       father;
    public void Set(string id, DateTime d, string inf)
    {
        idEvent = id;
        date = d;
        informations = inf.Replace("\n", "&n");
    }
    public void Set(string id, DateTime d, string inf, string fath)
    {
        idEvent = id;
        date = d;
        informations = inf.Replace("\n", "&n");
        father = fath;
    }
    public string GetValues()
    {
        return idEvent + ";" + date.ToString("ddMMyyyy") + ";" + informations + ";" + father;
    }
}

public class EventType
{
    public string   name;
    public string   id;

    public void Set(string Name, string Id)
    {
        name = Name;
        id = Id;
    }
}
