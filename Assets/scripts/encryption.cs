using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class encryption
{
    public static void openInExplorer(string path)
    {
        string cmd = "explorer.exe";
        string arg = "/select, " + path;
        System.Diagnostics.Process.Start(cmd, arg);
    }
    public static void save(string path, string toSave)
    {
        StreamWriter file = new StreamWriter(path + @"\Config\key.txt");

        file.Write(toSave);
        file.Close();
    }


    public static string Encrypt(string toEncrypt, string pass)
    {
        string result = "";
        int x = toEncrypt.Length - 1;
        int y;
        while (x >= 0)
        {
            y = 0;
            while (true)
            {
                if(toEncrypt[x] == GetAllChar()[y])
                {
                    result += pass[y];
                    break;
                }
                y++;
            }
            x--;
        }
        return(result);
    }
    public static string Decrypt(string toDecrypt, string pass)
    {
        string result = "";
        int x = toDecrypt.Length - 1;
        int y;
        while (x >= 0)
        {
            y = 0;
            while (true)
            {
                if (toDecrypt[x] == pass[y])
                {
                    result += GetAllChar()[y];
                    break;
                }
                y++;
            }
            x--;
        }
        return (result);
    }


    #region ---- tools ----
    public static string GetKeyFileStr(string path, string nextPath)
    {
        string[] lines;
        lines = File.ReadAllLines(path + nextPath);

        string all = "";
        for (int i = 0; i < lines.Length; i++)
        {
            all += lines[i] + "\n";
        }

        return all;
    }
    public static string[] GetKeyFileLst(string path, string nextPath)
    {
        string[] lines;
        lines = File.ReadAllLines(path + nextPath);

        return lines;
    }


    public static void GetAlphabet()
    {
        string toShow = "";
        for (int i = 0; i < 26; i++)
        {
            char test = (char)(65 + i);
            toShow += test;
        }
    }
    public static string GetAllChar()
    {
        /*
        string result = "";
        for (int i = 33; i < 127; i++)
        {
            char test = (char)(i);
            result += test;
        }*/
        return("\n !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~");
    }

    public static void genKeyPass()
    {
        string get = GetAllChar();
        string result = "";
        System.Random rnd = new System.Random();

        int length = get.Length;

        while (get.Length > 0)
        {
            int k = rnd.Next(get.Length);

            char key = get[k];
            get = get.Remove(k, 1);
            result += key;
        }
    }
    #endregion
}
