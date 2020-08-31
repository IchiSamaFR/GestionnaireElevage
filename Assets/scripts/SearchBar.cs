using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchBar : MonoBehaviour
{
    Load                            loadClass;
    [SerializeField] GameObject     item;
    [SerializeField] Transform      content;
    [SerializeField] InputField     inputSearch;



    // Start is called before the first frame update
    void Start()
    {
        loadClass = Load.instance;
        Search();
    }

    public void Search()
    {
        string toVerify = inputSearch.text;

        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }


        for (int i = 0; i < loadClass.catLstAnimals.Count; i++)
        {
            string compare = "";

            if (toVerify.Length > loadClass.catLstAnimals[i].animalName.Length)
            {
                return;
            }

            compare = loadClass.catLstAnimals[i].animalName.Substring(0, toVerify.Length);


            if (toVerify == compare)
            {
                GameObject tmpItem = Instantiate(item, content);

                tmpItem.name = i.ToString();
                tmpItem.transform.GetChild(0).GetComponent<Text>().text = loadClass.catLstAnimals[i].animalName;
                tmpItem.GetComponent<CopyId>().toCopy = loadClass.catLstAnimals[i].animalIdentification;
                
                tmpItem.SetActive(true);
            }
        }
    }
}
