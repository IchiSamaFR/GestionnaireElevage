using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static Tools;

public class Load : MonoBehaviour
{
    public static Load instance;

    public List<Animal>     catLstAnimals = new List<Animal>();
    public List<Race>       catLstRaces = new List<Race>();

    public string path = @"C:\ManageBreeding";
    readonly string races_fr = "Abyssin;American Bobtail;Balinais;Bengal;Birman;American Curl;American Shorthair;American Wirehair;Angora Turc;Bleu Russe;Bombay;British Longhair;British Shorthair;Burmese;Burmilla;California Spangled;Californian Rex;Ceylan;Chartreux;Chausie;Chinchilla Persan;Cornish Rex;Cymric;Devon Rex;Donskoy;Européen;Exotic;German Rex;Gouttièrre;Havana Brown;Highland Straight;Himalayen;Japanese Bobtail;Javanais;Khao Manee;Korat;LaPerm;Maine Coon;Manx;Mau Egyptien;Munchkin;Nebelung;Norvégien;Ocicat;Oriental;Persan;Peterbald;Pixiebob;Ragamuffin;Ragdoll;Safari;Savannah;Scottish Fold;Scottish Fold poil long;Selkirk";
    readonly string events_fr = "Entrer élevage;Repos;Chaleur;Saillie;Gestation;Mise bas;Retraite";

    void                    Start()
    {
        instance = this;
        Init_directory();
        LoadAnimals();
        LoadRaces();
    }

    void Init_directory()
    {
        //Create root directory
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);


        //Create main directory
        if (!Directory.Exists(path + @"\Events"))
            Directory.CreateDirectory(path + @"\Events");
        
        if (!File.Exists(path + @"\cat_list.txt"))
            File.Create(path + @"\cat_list.txt");


        //Config directory and files
        //
        if (!Directory.Exists(path + @"\Config"))
            Directory.CreateDirectory(path + @"\Config");

        //Races
        if (!File.Exists(path + @"\Config\race_list_fr.txt"))
        {
            SavePath(path + @"\Config\race_list_fr.txt", races_fr);
        }

        //Events type
        if (!File.Exists(path + @"\Config\event_list_fr.txt"))
        {
            SavePath(path + @"\Config\event_list_fr.txt", events_fr);
        }

        
    }


    #region ---- ANIMALS ----

    //                  SAVE ANIMALS
    public void             SaveAnimals()
    {
        SavePath(path, catLstAnimals);
    }

    //                  Load animal
    public void             LoadAnimals()
    {
        catLstAnimals = GetValuesAnimals(path);
    }

    //                  Create new animals
    public void             CreateNewAnimal(int id, string type, string name, string sex, int day, int month, int year, string race, string identification, string loof, string father, string mother)
    {
        int count;

        catLstAnimals.Add(new Animal());
        count = catLstAnimals.Count;
        catLstAnimals[count - 1].SetInit(count - 1, "Cat", name, sex, day, month, year, race, identification, loof);
        catLstAnimals[count - 1].SetFather(father);
        catLstAnimals[count - 1].SetMother(mother);
        SaveAnimals();
    }

    //                  Return the cat list
    public List<Animal>     GetCatLst()
    {
        return catLstAnimals;
    }

    public void             ChangeIdentNumber(string ancientId, string newId)
    {
        for (int i = 0; i < catLstAnimals.Count; i++)
        {
            if (catLstAnimals[i].animalIdentification == ancientId)
                catLstAnimals[i].animalIdentification = newId;
            if (catLstAnimals[i].animalFather == ancientId)
                catLstAnimals[i].animalFather = newId;
            if (catLstAnimals[i].animalMother == ancientId)
                catLstAnimals[i].animalMother = newId;
        }

        File.Move(path + @"\Events\" + ancientId + ".txt", path + @"\Events\" + newId + ".txt");
    }
    #endregion



    #region ---- RACES ----
    public void LoadRaces()
    {
        catLstRaces = GetValuesRace(path);
    }

    //      Recupere le nom grace à l'id
    public string GetRacesNames(string id)
    {
        string to_return = "";
        for (int i = 0; i < catLstRaces.Count; i++)
        {
            if (id == catLstRaces[i].id)
            {
                to_return = catLstRaces[i].raceName;
                break;
            }
        }
        return to_return;
    }

    //      Recupere l'id grace au nom
    public string GetRacesId(string name)
    {
        string to_return = "";
        for (int i = 0; i < catLstRaces.Count; i++)
        {
            if (name == catLstRaces[i].raceName)
            {
                to_return = catLstRaces[i].id;
                break;
            }
        }
        return to_return;
    }

    //      Recupere le numero dans la liste des races grace à l'ID
    public int GetArrayId(string id)
    {
        int to_return = 0;
        for (int i = 0; i < catLstRaces.Count; i++)
        {
            if (id == catLstRaces[i].id)
            {
                to_return = i;
                break;
            }
        }
        return to_return;
    }
    public List<string> GetRaces()
    {
        List<string> toReturn = new List<string>();
        for (int i = 0; i < catLstRaces.Count; i++)
        {
            toReturn.Add(catLstRaces[i].raceName);
        }
        return toReturn;
    }

    #endregion





    void OnApplicationQuit()
    {

    }

}
