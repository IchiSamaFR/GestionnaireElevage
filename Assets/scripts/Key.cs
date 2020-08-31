using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using static encryption;

public class Key : MonoBehaviour
{
    string allChar = "\n !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
    string pass = "6g*M='&CwTnij!0?,\"l4L9o7\\SQY</FphHv#AX DxBuz5;k{]Zm-[V2>$|trdWO)e3(%K8:+_c}sUI`~fRqyG.^@P\n1bENaJ";
    string aKey = "key=TEST-FIRST-KEY00-2AA1Z-FAEZF" +
        "\nkey_version=premium" +
        "\nvendor=SimpleBreeding " +
        "\ncreation_date=09/05/2021" +
        "\nvalid_date=10/10/2021" +
        "\ncomplet_name=Jordan DE SOUSA" +
        "\nsiret_number=65465161321" +
        "\nstreet=08 lotissement des comp" +
        "\ncit =Pourt" +
        "\nstate=" +
        "\npost_code=37890" +
        "\ncountry=France" +
        "\nmobile_phone=0789313465";

    public string path = @"C:\ManageBreeding\";

    string get = "";

    // Start is called before the first frame update
    void Start()
    {
        string all = GetKeyFileStr(path, @"\Config\key.txt");
    }

    // Update is called once per frame
    void Update()
    {
        if(get != "")
        {
            print(Decrypt(get, pass));
        }
    }

    public void OpenDir()
    {
        string pathDir = EditorUtility.OpenFilePanel("Selection de la clé d'activation", path, "");
        if (pathDir.Length != 0)
        {
            var fileContent = File.ReadAllBytes(pathDir);
            get = System.Text.Encoding.UTF8.GetString(fileContent, 0, fileContent.Length);
        }
    }
}
