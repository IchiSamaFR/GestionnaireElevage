using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class Tools
{
    #region ---- getEvents ----
    public static string GetEventNameById(string getId, List<EventType> lst)
    {
        for (int i = 0; i < lst.Count; i++)
        {
            if (lst[i].id == getId)
            {
                return lst[i].name;
            }
        }
        return null;
    }
    public static string GetEventIdByName(string getName, List<EventType> lst)
    {
        for (int i = 0; i < lst.Count; i++)
        {
            if (lst[i].name == getName)
            {
                return lst[i].id;
            }
        }
        return null;
    }

    public static string LastEvent(string idAnimal, string path)
    {
        string[] lines;
        string last = "";

        if (!File.Exists(path + @"\Events\" + idAnimal + ".txt"))
        {
            File.Create(path + @"\Events\" + idAnimal + ".txt");
        }
        else
        {
            lines = File.ReadAllLines(path + @"\Events\" + idAnimal + ".txt");

            foreach (string line in lines)
            {
                var data_values = line.Split(new char[] { ';' });

                last = data_values[0];
            }
        }
        List<EventType> eventTypeLst = new List<EventType>();
        eventTypeLst = LoadEventsType(path);
        
        return GetEventNameById(last, eventTypeLst);
    }
    public static string LastEvent(string idAnimal, string path, bool id)
    {
        string[] lines;
        string last = "";

        if (!File.Exists(path + @"\Events\" + idAnimal + ".txt"))
        {
            File.Create(path + @"\Events\" + idAnimal + ".txt");
        }
        else
        {
            lines = File.ReadAllLines(path + @"\Events\" + idAnimal + ".txt");

            foreach (string line in lines)
            {
                var data_values = line.Split(new char[] { ';' });

                last = data_values[0];
            }
        }
        List<EventType> eventTypeLst = new List<EventType>();
        eventTypeLst = LoadEventsType(path);
        if (id)
        {
            return last;
        }
        else
        {
            return GetEventNameById(last, eventTypeLst);
        }
    }
    #endregion

    #region ---- Load ----
    public static void SavePath(string path, string list)
    {
        StreamWriter file = new StreamWriter(path);

        string[] lines = list.Split(new char[] { ';' });
        int x = 1;
        foreach (string line in lines)
        {
            file.Write(line + ";" + x + "\n");
            x++;
        }
        file.Close();
    }
    public static void SavePath(string path, List<Animal> list)
    {
        StreamWriter file = new StreamWriter(path + @"\cat_list.txt");

        for (int i = 0; i < list.Count; i++)
        {
            file.Write(list[i].GetValues() + "\n");
        }
        file.Close();
    }

    public static void SavePath(string path, List<AnimalEvent> list)
    {
        StreamWriter file = new StreamWriter(path);

        for (int i = 0; i < list.Count; i++)
        {
            file.Write(list[i].GetValues() + "\n");
        }
        file.Close();
    }

    public static List<Animal> GetValuesAnimals(string path)
    {
        string[] lines;
        int x;

        x = 0;
        List<Animal> lst = new List<Animal>();
        lines = File.ReadAllLines(path + @"\cat_list.txt");

        foreach (string line in lines)
        {
            var data_values = line.Split(new char[] { ';' });

            lst.Add(new Animal());
            lst[x].SetInit(x, data_values[0], data_values[1], data_values[2], int.Parse(data_values[3]), int.Parse(data_values[4]), int.Parse(data_values[5]), data_values[6], data_values[7], data_values[8]);
            lst[x].SetFather(data_values[9]);
            lst[x].SetMother(data_values[10]);
            x++;
        }
        return lst;
    }
    public static List<Race> GetValuesRace(string path)
    {
        string[] lines;
        int x;

        x = 0;
        List<Race> lst = new List<Race>();
        lines = System.IO.File.ReadAllLines(path + @"\Config\race_list_fr.txt");
        foreach (string line in lines)
        {
            var data_values = line.Split(new char[] { ';' });

            lst.Add(new Race());
            lst[x].Set(data_values[0], data_values[1]);
            x++;
        }
        lst.Sort((Race c1, Race c2) => c1.raceName.CompareTo(c2.raceName));
        lst.Insert(0, new Race());
        lst[0].Set("Race", "0");

        return lst;
    }
    public static List<EventType> LoadEventsType(string path)
    {
        string[] lines;
        int x;


        x = 0;
        List<EventType> eventTypeLst = new List<EventType>();

        lines = File.ReadAllLines(path + @"\Config\event_list_fr.txt");


        foreach (string line in lines)
        {
            var dataValues = line.Split(new char[] { ';' });

            eventTypeLst.Add(new EventType());
            eventTypeLst[x].Set(dataValues[0], dataValues[1]);
            x++;
        }

        return eventTypeLst;
    }

    #endregion
}
