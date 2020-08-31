using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal
{
    [Header("stats")]

    public int      animalId;
    public string   animalType;
    public string   animalName;
    public string   animalSex;

    public int      birthDay;
    public int      birthMonth;
    public int      birthYear;

    public string   animalRace;

    public string   animalIdentification;
    public string   animalLoof;

    public string   animalFather;
    public string   animalMother;


    public void     SetInit(int id, string type, string name, string sex, int day, int month, int year, string race, string identification)
    {
        animalId               = id;

        animalType             = type;
        animalName             = name;
        animalSex              = sex;

        birthDay               = day;
        birthMonth             = month;
        birthYear              = year;

        animalRace             = race;

        animalIdentification   = identification;
    }

    public void     SetInit(int id, string type, string name, string sex, int day, int month, int year, string race, string identification, string loof)
    {
        animalId               = id;

        animalType             = type;
        animalName             = name;
        animalSex              = sex;

        birthDay               = day;
        birthMonth             = month;
        birthYear              = year;

        animalRace             = race;

        animalIdentification   = identification;
        animalLoof             = loof;
    }

    public void     SetFather(string id)
    {
        animalFather = id;
    }

    public void     SetMother(string id)
    {
        animalMother = id;
    }

    public string   GetValues()
    {
        string line;

        line = animalType + ";" + animalName + ";" + animalSex + ";" + birthDay + ";" + birthMonth + ";" + birthYear + ";" + animalRace + ";" + animalIdentification + ";" + animalLoof + ";" + animalFather + ";" + animalMother;
        return (line);
    }
}
