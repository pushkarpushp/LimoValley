using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelectionScreen : MonoBehaviour
{
    #region Menu switch Toggle
    public UnityEngine.UI.ToggleGroup ToggleGroup; // Drag & drop the desired ToggleGroup in the inspector
    [SerializeField]
    GameObject[] ToggleScreen;
    GameObject currentToggleScreen;

    [SerializeField]
    GameObject MouthObj, facialHairObj;
    public void OpenSelectedScreen()
    {
            Toggle selectedToggle = ToggleGroup.ActiveToggles().FirstOrDefault();
        if (selectedToggle != null)
        { Debug.Log(selectedToggle.name);  }

        if (SelectedCharacter.CharacterName.Contains("Female"))
        { facialHairObj.SetActive(false); MouthObj.SetActive(true); }
        else
        { facialHairObj.SetActive(true); MouthObj.SetActive(false); }
        //selectedToggle.onValueChanged.AddListener = new ColorBlock();

        if (currentToggleScreen != null && currentToggleScreen.name.Contains(selectedToggle.name))
            return;
       
        for (int i=0;i<ToggleScreen.Length;i++)
        {
            if (ToggleScreen[i].name.Contains(selectedToggle.name))
            {
                currentToggleScreen = ToggleScreen[i];
                ToggleScreen[i].SetActive(true);
            }
            else
                ToggleScreen[i].SetActive(false);

        }
    }



    #endregion


    #region Public Variables


    public CharacterModals SelectedCharacter;

    public CharacterModals[] CharModals;
    public GameObject characterSelectionScreen, characterCustomizationScreen;

   
    [Space(10)]
    public Texture2D[] MaleEyes;
    public Texture2D[] MaleEyeBrows;
    public Texture2D[] MaleMouth;

    [Space(10)]
     public Texture2D[] FeMaleEyes;
    public Texture2D[] FeMaleEyeBrows;
    public Texture2D[] FeMaleMouth;

    [Header("--Assessories--")]
    public Color[] SkinTone;
  
    [Space(20)]
    [Header("ParentObj")]
    public GameObject TopObj;
    public GameObject BottomObj,HeadObj,ShoesObj,AccessoriesObj;

 
    public GameObject ImagePrefab;
    #region Select ModalType
    int a =0;
    public void SelectOther()
    {
        Debug.Log(".. here Other ..");
       /* PrevButton.onClick.RemoveAllListeners();
        NextButton.onClick.RemoveAllListeners();
        PrevButton.onClick.AddListener(()=>OnOtherPrevBtnClick());
        NextButton.onClick.AddListener(()=>OnOtherNextBtnClick());*/


        PrevButton.gameObject.SetActive(false);
        NextButton.gameObject.SetActive(false);
        OtherPrevButton.gameObject.SetActive(true);
        OtherNextButton.gameObject.SetActive(true);
        OtherPrevButton.interactable = false;
        a = 0; i = 0;
        SelectCharacter(0);


      
    }
    #endregion
    int k = 0;
    Transform[] modal;

    #region Select ModalType 
    public string[] modalsType;
    public void SetChar(int i)
    {
      
        string mName = "";

        if (SelectedCharacter.CharacterName.Contains("Male"))
            mName = "Male_" + modalsType[i];
        else
            mName = "Female_" + modalsType[i];


        Debug.Log(" i: " + i+ " .. Name : "+ mName);

        SelectVariantModal(mName);
    }


    public void SelectVariantModal(string ButtonName)
    {
        switch (ButtonName)
        {
            case "Male_Ecto":
                SelectCharacter(0);
                break;
            case "Male_Endo":
                SelectCharacter(2);
                break;
            case "Male_Meso":
                SelectCharacter(4);
                break;
            case "Female_Ecto":
                SelectCharacter(1);
                break;
            case "Female_Endo":
                SelectCharacter(3);
                break;
            case "Female_Meso":
                SelectCharacter(5);
                break;
            default: break;
        }
    }
    #endregion


     public void onResetBtn()
     {
        PrevButton.gameObject.SetActive(true);
         NextButton.gameObject.SetActive(true);
         OtherPrevButton.gameObject.SetActive(false);
         OtherNextButton.gameObject.SetActive(false);
     }
    public void SelectCharacter(int i)
    {

        OnBackBtn();
        characterSelectionScreen.SetActive(false);
        characterCustomizationScreen.SetActive(true);
        if (i == 0)
        {  SelectedCharacter = CharModals[0];
             GetMaleCloser();  }
        else if (i == 1)
        { SelectedCharacter = CharModals[1];
             GetFemaleCloser(); }
       else if (i == 2)
        {  SelectedCharacter = CharModals[2];
             GetMaleCloser();  }
        else if (i == 3)
        { SelectedCharacter = CharModals[3];
             GetFemaleCloser(); }
        else if (i ==4)
        {  
            SelectedCharacter = CharModals[4];
            GetMaleCloser(); }
        else if (i == 5)
        {  SelectedCharacter = CharModals[5];
             GetFemaleCloser(); 
        }

        PlayerPrefs.DeleteKey("top");
        PlayerPrefs.DeleteKey("bottom");
        PlayerPrefs.DeleteKey("hair");
        PlayerPrefs.DeleteKey("Footware");

        PlayerPrefs.DeleteKey("assessories");
        PlayerPrefs.DeleteKey("R");
        PlayerPrefs.DeleteKey("G");
        PlayerPrefs.DeleteKey("B");
        PlayerPrefs.DeleteKey("mouth");



        SelectedCharacter.CharModal.SetActive(true);
        PlayerPrefs.SetInt("SelectedCharacter", i);
        if (SelectedCharacter.CharacterName.Contains("Female"))
        { facialHairObj.SetActive(false); MouthObj.SetActive(true); }
        else
        { facialHairObj.SetActive(true); MouthObj.SetActive(false); }

        //GET current model top
        for (int j = 0; j < SelectedCharacter.TopModals.Length; j++)
        {
            Debug.Log("J" + j);
            if (SelectedCharacter.TopModals[j].gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("top", j);
                //Debug.Log("value of top :" + PlayerPrefs.GetInt("top"));

                for (int k = 0; k < SelectedCharacter.TopModals[j].GetComponent<SkinnedMeshRenderer>().materials.Length; k++)
                {
                    if (SelectedCharacter.TopModals[j].GetComponent<SkinnedMeshRenderer>().materials[k].name.Contains("skintone1")|| SelectedCharacter.TopModals[j].GetComponent<SkinnedMeshRenderer>().materials[k].name.Contains("skintone")|| SelectedCharacter.TopModals[j].GetComponent<SkinnedMeshRenderer>().materials[k].name.Contains("SkinTone"))
                    {
                        PlayerPrefs.SetFloat("R", SelectedCharacter.TopModals[j].GetComponent<SkinnedMeshRenderer>().materials[k].color.r);
                        PlayerPrefs.SetFloat("G", SelectedCharacter.TopModals[j].GetComponent<SkinnedMeshRenderer>().materials[k].color.g);
                        PlayerPrefs.SetFloat("B", SelectedCharacter.TopModals[j].GetComponent<SkinnedMeshRenderer>().materials[k].color.b);                                  
                    }
                }
                break;
            }
        }
       // Debug.Log("male top " + "Selectec Name"+SelectedCharacter.CharacterName+PlayerPrefs.GetInt("top"));
        //GET current model bottom
        for (int j = 0; j < SelectedCharacter.Bottoms.Length; j++)
        {
            if (SelectedCharacter.Bottoms[j].gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("bottom",j);
              //  Debug.Log("value of Botttom :" + PlayerPrefs.GetInt("bottom"));
                break;
            }
        }
        //GET current model Shoe
        for (int j = 0; j < SelectedCharacter.Footware.Length; j++)
        {
            if (SelectedCharacter.Footware[j].gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("Footware", j);
            //    Debug.Log("value of footware :" + PlayerPrefs.GetInt("Footware"));
                break;
               
            }
        }  //GET current model hairstyles
        for (int j = 0; j < SelectedCharacter.HairStyle.Length; j++)
        {
            if (SelectedCharacter.HairStyle[j].gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("hair", j);
          //     Debug.Log("value of Hair :" + PlayerPrefs.GetInt("hair"));
                break;
            }
        } //GET current model hairstyles
        for (int j = 0; j < SelectedCharacter.EyeWears.Length; j++)
        {
            if (SelectedCharacter.EyeWears[j].gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("assessories", j);
                break;             
            }
            else
            {
                PlayerPrefs.SetInt("assessories", -1);
            }
          //  Debug.Log("value of assessories :" + PlayerPrefs.GetInt("assessories"));

        }
       
        modal = SelectedCharacter.CharModal.GetComponentsInChildren<Transform>();
        bool istrue = true;

        SelectedEyes = SelectedCharacter.Eyes;
        SelectedEyebrows = SelectedCharacter.yebrows;

        if (SelectedCharacter.CharacterName.Contains("Female"))
            selectedMouth = SelectedCharacter.Mouth;
    }

    GameObject
        SelectedTop, 
        Selectedbottom, 
        SelectedHead, 
        SelectedShoes,
        SelectedAcccessories, 
        Selectedhair,
        SelectedEyes,
        SelectedEyebrows,
        selectedMouth;
    #endregion

    [Space(20)]
    [Header("Male Images")]
    [SerializeField] private List<Sprite> MaleTop;
    [SerializeField] private List<Sprite> MaleBottom;
    [SerializeField] private List<Sprite> MaleHairStyle;
    [SerializeField] private List<Sprite> MaleAccessories;
    [SerializeField] private List<Sprite> MaleShoes;


    [Space(20)]
    [Header("Female Images")]
    [SerializeField] private List<Sprite> FemaleTop;
    [SerializeField] private List<Sprite> FemaleBottom;
    [SerializeField] private List<Sprite> FemaleHairStyle;
    [SerializeField] private List<Sprite> FemaleAccessories;
    [SerializeField] private List<Sprite> FemaleShoes;

    
    List<GameObject> SelectedmaleHead, SelectedmaleEyes, SelectedmaleEyebrows, SelectedmaleMouth,SelectedMalehair;

    #region Male GetUp
    public void GetMaleCloser()
    {
        SelectedmaleMouth = new List<GameObject>();
        SelectedmaleEyes = new List<GameObject>();
        SelectedmaleEyebrows = new List<GameObject>();
        SelectedmaleHead = new List<GameObject>();
        SelectedMalehair = new List<GameObject>();
        //Male Top Wear
        for (int i=0;i<MaleTop.Count;i++)
        {
            GameObject Top = Instantiate(ImagePrefab, TopObj.transform);
            Top.name = MaleTop[i].name;
            Top.transform.GetChild(0).GetComponent<Image>().sprite = MaleTop[i];
            Toggle t = Top.GetComponent<Toggle>();
            t.group = TopObj.transform.GetComponent<ToggleGroup>();
            int k = i;
            t.onValueChanged.AddListener(delegate {
                ChangeTopforMale(k);
            });
        }

        //Male Bottom Wear
        for (int i = 0; i < MaleBottom.Count; i++)
        {
            GameObject Top = Instantiate(ImagePrefab, BottomObj.transform);
            Top.name = MaleBottom[i].name;
            Top.transform.GetChild(0).GetComponent<Image>().sprite = MaleBottom[i];
            Toggle t = Top.GetComponent<Toggle>();
            t.group = BottomObj.transform.GetComponent<ToggleGroup>();
            int k = i;
         //   Debug.Log("male botom" + k);
            t.onValueChanged.AddListener(delegate {
                ChangeBottomforMale(k);
            });
        }

        //Male Shoes Wear
        for (int i = 0; i < MaleShoes.Count; i++)
        {
            GameObject Top = Instantiate(ImagePrefab, ShoesObj.transform);
            Top.name = MaleShoes[i].name;
            Top.transform.GetChild(0).GetComponent<Image>().sprite = MaleShoes[i];
            Toggle t = Top.GetComponent<Toggle>();
            t.group = ShoesObj.transform.GetComponent<ToggleGroup>();
            int k = i;
            t.onValueChanged.AddListener(delegate {
                ChangeShoes(k);
            });
        }

        //Male Hair style change
       for (int i = 0; i < MaleHairStyle.Count; i++)
        {
            GameObject Top = Instantiate(ImagePrefab, HeadObj.transform);
            Top.name = MaleHairStyle[i].name;
            Top.transform.GetChild(0).GetComponent<Image>().sprite = MaleHairStyle[i];
            Toggle t = Top.GetComponent<Toggle>();
            t.group = HeadObj.transform.GetComponent<ToggleGroup>();
            int k = i;
            t.onValueChanged.AddListener(delegate {
                ChangeHairStyle(k);
            });
            SelectedmaleHead.Add(Top);


        }


        //Male assessories
        for (int i = 0; i < MaleAccessories.Count; i++)
        {
            GameObject Top = Instantiate(ImagePrefab, AccessoriesObj.transform);
            Top.name = MaleAccessories[i].name;
            Top.transform.GetChild(0).GetComponent<Image>().sprite = MaleAccessories[i];
            Toggle t = Top.GetComponent<Toggle>();
            t.group = AccessoriesObj.transform.GetComponent<ToggleGroup>();
            int k = i;
            t.onValueChanged.AddListener(delegate {
                ChangeAssessaries(k);
            });
        }

        //Male Eyes
        for(int i=0;i<MaleEyes.Length;i++)
        {
            GameObject Top = Instantiate(ImagePrefab, HeadObj.transform);
            Top.name = MaleEyes[i].name;
           Sprite mySprite = Sprite.Create(MaleEyes[i], new Rect(0.0f, 0.0f, MaleEyes[i].width, MaleEyes[i].height), new Vector2(0.5f, 0.5f), 100.0f);

            Top.transform.GetChild(0).GetComponent<Image>().sprite = mySprite;
            Toggle t = Top.GetComponent<Toggle>();
            t.group = HeadObj.transform.GetComponent<ToggleGroup>();
            int k = i;
            t.onValueChanged.AddListener(delegate {
                ChangeEyes(k);
                Debug.Log(k + " : k");
            });

            SelectedmaleEyes.Add(Top);
        }

        //Male EyeBrows
        for (int i = 0; i < MaleEyeBrows.Length; i++)
        {
            GameObject Top = Instantiate(ImagePrefab, HeadObj.transform);
            Top.name = MaleEyeBrows[i].name;
            Sprite mySprite = Sprite.Create(MaleEyeBrows[i], new Rect(0.0f, 0.0f, MaleEyeBrows[i].width, MaleEyeBrows[i].height), new Vector2(0.5f, 0.5f), 100.0f);

            Top.transform.GetChild(0).GetComponent<Image>().sprite = mySprite;
            Toggle t = Top.GetComponent<Toggle>();
            t.group = HeadObj.transform.GetComponent<ToggleGroup>();
            int k = i;
            t.onValueChanged.AddListener(delegate {
                ChangeEyeBrows(k);
            });

            SelectedmaleEyebrows.Add(Top);

        }

        //CHange Mouth
        for (int i = 0; i < MaleMouth.Length; i++)
        {
            GameObject Top = Instantiate(ImagePrefab, HeadObj.transform);
            Top.name = MaleMouth[i].name;
            Sprite mySprite = Sprite.Create(MaleMouth[i], new Rect(0.0f, 0.0f, MaleMouth[i].width, MaleMouth[i].height), new Vector2(0.5f, 0.5f), 100.0f);

            Top.transform.GetChild(0).GetComponent<Image>().sprite = mySprite;
            Toggle t = Top.GetComponent<Toggle>();
            t.group = HeadObj.transform.GetComponent<ToggleGroup>();
            int k = i;
            t.onValueChanged.AddListener(delegate {
                ChangeMaleMouth(k);
            });

            SelectedmaleMouth.Add(Top);

        }


        ChangeHeadStyle("Head");

    }
    #endregion

    #region Female GetUp
    public void GetFemaleCloser()
    {
        SelectedmaleMouth = new List<GameObject>();
        SelectedmaleEyes = new List<GameObject>();
        SelectedmaleEyebrows = new List<GameObject>();
        SelectedmaleHead = new List<GameObject>();
        //Female Top Wear
        for (int i = 0; i < FemaleTop.Count; i++)
        {
            GameObject Top = Instantiate(ImagePrefab, TopObj.transform);
            Top.name = FemaleTop[i].name;
            Top.transform.GetChild(0).GetComponent<Image>().sprite = FemaleTop[i];
            Toggle t = Top.GetComponent<Toggle>();
            t.group = TopObj.transform.GetComponent<ToggleGroup>();
            int k = i;
            t.onValueChanged.AddListener(delegate {
                ChangeTopforMale(k);
            });
        }

        //Female Bottom Wear
        for (int i = 0; i < FemaleBottom.Count; i++)
        {
            GameObject Top = Instantiate(ImagePrefab, BottomObj.transform);
            Top.name = FemaleBottom[i].name;
            Top.transform.GetChild(0).GetComponent<Image>().sprite = FemaleBottom[i];
            Toggle t = Top.GetComponent<Toggle>();
            t.group = BottomObj.transform.GetComponent<ToggleGroup>();
            int k = i;
            t.onValueChanged.AddListener(delegate {
                ChangeBottomforMale(k);
            });
        }

        //Female Shoes Wear
        for (int i = 0; i < FemaleShoes.Count; i++)
        {
            GameObject Top = Instantiate(ImagePrefab, ShoesObj.transform);
            Top.name = FemaleShoes[i].name;
            Top.transform.GetChild(0).GetComponent<Image>().sprite = FemaleShoes[i];
            Toggle t = Top.GetComponent<Toggle>();
            t.group = ShoesObj.transform.GetComponent<ToggleGroup>();
            int k = i;
            t.onValueChanged.AddListener(delegate {
                ChangeShoes(k);
            });
        }

        //Female Hair Style
        for (int i = 0; i < FemaleHairStyle.Count; i++)
        {
            GameObject Top = Instantiate(ImagePrefab, HeadObj.transform);
            Top.name = FemaleHairStyle[i].name;
            Top.transform.GetChild(0).GetComponent<Image>().sprite = FemaleHairStyle[i];
            Toggle t = Top.GetComponent<Toggle>();
            t.group = HeadObj.transform.GetComponent<ToggleGroup>();
            int k = i;
            t.onValueChanged.AddListener(delegate {
                ChangeHairStyle(k);
            });

            SelectedmaleHead.Add(Top);
        }

        //Female assessories
        for (int i = 0; i < FemaleAccessories.Count; i++)
        {
            GameObject Top = Instantiate(ImagePrefab, AccessoriesObj.transform);
            Top.name = FemaleAccessories[i].name;
            Top.transform.GetChild(0).GetComponent<Image>().sprite = FemaleAccessories[i];
            Toggle t = Top.GetComponent<Toggle>();
            t.group = AccessoriesObj.transform.GetComponent<ToggleGroup>();
            int k = i;
            t.onValueChanged.AddListener(delegate {
                ChangeAssessaries(k);
            });
        }

      
        //FeMale Eyes
        for (int i = 0; i < FeMaleEyes.Length; i++)
        {
            GameObject Top = Instantiate(ImagePrefab, HeadObj.transform);
            Top.name = FeMaleEyes[i].name;
            Sprite mySprite = Sprite.Create(FeMaleEyes[i], new Rect(0.0f, 0.0f, FeMaleEyes[i].width, FeMaleEyes[i].height), new Vector2(0.5f, 0.5f), 100.0f);

            Top.transform.GetChild(0).GetComponent<Image>().sprite = mySprite;
            Toggle t = Top.GetComponent<Toggle>();
            t.group = HeadObj.transform.GetComponent<ToggleGroup>();
            int k = i;
            t.onValueChanged.AddListener(delegate {
                ChangeEyes(k);
            });

            SelectedmaleEyes.Add(Top);
            Top.SetActive(false);
        }

        //FeMale EyeBrows
        for (int i = 0; i < FeMaleEyeBrows.Length; i++)
        {
            GameObject Top = Instantiate(ImagePrefab, HeadObj.transform);
            Top.name = FeMaleEyeBrows[i].name;
            Sprite mySprite = Sprite.Create(FeMaleEyeBrows[i], new Rect(0.0f, 0.0f, FeMaleEyeBrows[i].width, FeMaleEyeBrows[i].height), new Vector2(0.5f, 0.5f), 100.0f);

            Top.transform.GetChild(0).GetComponent<Image>().sprite = mySprite;
            Toggle t = Top.GetComponent<Toggle>();
            t.group = HeadObj.transform.GetComponent<ToggleGroup>();
            int k = i;
            t.onValueChanged.AddListener(delegate {
                ChangeEyeBrows(k);
            });

            SelectedmaleEyebrows.Add(Top);
            Top.SetActive(false);

        }


        //CHange Mouth
        for (int i = 0; i < FeMaleMouth.Length; i++)
        {
            GameObject Top = Instantiate(ImagePrefab, HeadObj.transform);
            Top.name = FeMaleMouth[i].name;
            Sprite mySprite = Sprite.Create(FeMaleMouth[i], new Rect(0.0f, 0.0f, FeMaleMouth[i].width, FeMaleMouth[i].height), new Vector2(0.5f, 0.5f), 100.0f);

            Top.transform.GetChild(0).GetComponent<Image>().sprite = mySprite;
            Toggle t = Top.GetComponent<Toggle>();
            t.group = HeadObj.transform.GetComponent<ToggleGroup>();
            int k = i;
            t.onValueChanged.AddListener(delegate {
                ChangeMaleMouth(k);
            });
            SelectedmaleMouth.Add(Top);
            Top.SetActive(false);

        }

        ChangeHeadStyle("Head");
    }
    #endregion

    #region change Head Style
    public void ChangeHeadStyle(string name)
    {
        switch(name)
        {
            case "Head":

               for(int i=0;i< SelectedmaleHead.Count;i++)
                {
                    SelectedmaleHead[i].SetActive(true);
                }
                for (int i = 0; i < SelectedmaleEyebrows.Count; i++)
                {
                    SelectedmaleEyebrows[i].SetActive(false);
                }
                for (int i = 0; i < SelectedmaleEyes.Count; i++)
                {
                    SelectedmaleEyes[i].SetActive(false);
                }
                for (int i = 0; i < SelectedmaleMouth.Count; i++)
                {
                    SelectedmaleMouth[i].SetActive(false);
                }
                break;
            case "Eye":
                for (int i = 0; i < SelectedmaleHead.Count; i++)
                {
                    SelectedmaleHead[i].SetActive(false);
                }
                for (int i = 0; i < SelectedmaleEyebrows.Count; i++)
                {
                    SelectedmaleEyebrows[i].SetActive(false);
                }
                for (int i = 0; i < SelectedmaleEyes.Count; i++)
                {
                    SelectedmaleEyes[i].SetActive(true);
                }
                for (int i = 0; i < SelectedmaleMouth.Count; i++)
                {
                    SelectedmaleMouth[i].SetActive(false);
                }
                break;
            case "Eyebrow":
                for (int i = 0; i < SelectedmaleHead.Count; i++)
                {
                    SelectedmaleHead[i].SetActive(false);
                }
                for (int i = 0; i < SelectedmaleEyebrows.Count; i++)
                {
                    SelectedmaleEyebrows[i].SetActive(true);
                }
                for (int i = 0; i < SelectedmaleEyes.Count; i++)
                {
                    SelectedmaleEyes[i].SetActive(false);
                }
                for (int i = 0; i < SelectedmaleMouth.Count; i++)
                {
                    SelectedmaleMouth[i].SetActive(false);
                }
                break;
            case "Mouth":
                for (int i = 0; i < SelectedmaleHead.Count; i++)
                {
                    SelectedmaleHead[i].SetActive(false);
                }
                for (int i = 0; i < SelectedmaleEyebrows.Count; i++)
                {
                    SelectedmaleEyebrows[i].SetActive(false);
                }
                for (int i = 0; i < SelectedmaleEyes.Count; i++)
                {
                    SelectedmaleEyes[i].SetActive(false);
                }
                for (int i = 0; i < SelectedmaleMouth.Count; i++)
                {
                    SelectedmaleMouth[i].SetActive(true);
                }
                break;
        }
    }
    #endregion

    #region change Appearence 
    // change Top
    GameObject newTop;

    public void ChangeTopforMale(int t)
    {
      
        for (int i = 0; i < SelectedCharacter.TopModals.Length; i++)
        {
            SelectedCharacter.TopModals[i].SetActive(false);
        }
        SelectedCharacter.TopModals[t].SetActive(true);
        SelectedTop = SelectedCharacter.TopModals[t];
         SelectedTop.SetActive(true);
        PlayerPrefs.SetInt("top", t);
    }

    //Change Bottom
    public void ChangeBottomforMale(int t)
    {
      

        for (int i = 0; i < SelectedCharacter.Bottoms.Length; i++)
        {
            SelectedCharacter.Bottoms[i].SetActive(false);
        }
        Selectedbottom = SelectedCharacter.Bottoms[t];
        Selectedbottom.SetActive(true);
        SelectedCharacter.Bottoms[t].SetActive(true); 
        PlayerPrefs.SetInt("bottom", t);

    }
    

    //Change Shoes
    public void ChangeShoes(int t)
    {
      
        for (int i = 0; i < SelectedCharacter.Footware.Length; i++)
        {
            SelectedCharacter.Footware[i].SetActive(false);
        }
        SelectedShoes = SelectedCharacter.Footware[t];
        SelectedShoes.SetActive(true);
        PlayerPrefs.SetInt("Footware", t);
    }

    void ChangeEyes(int t)
    {
        Debug.Log(SelectedCharacter.CharacterName + ".. .. ");
        if (SelectedCharacter.CharacterName.Contains("Female"))
        {
            SelectedEyes.GetComponent<Renderer>().material.mainTexture = FeMaleEyes[t];
            PlayerPrefs.SetInt("eyes", t);

            Debug.Log("Female "+PlayerPrefs.GetInt("eyes"));

        }
        else
        {
            SelectedEyes.GetComponent<Renderer>().material.mainTexture = MaleEyes[t];
            PlayerPrefs.SetInt("eyes", t);
            Debug.Log("Male .. "+PlayerPrefs.GetInt("eyes"));
        }

        Debug.Log("Male .. " + PlayerPrefs.GetInt("eyes"));

    }
    void ChangeMaleMouth(int i)
    {
        if (SelectedCharacter.CharacterName.Contains("Female"))
        {
            selectedMouth.GetComponent<Renderer>().material.mainTexture = FeMaleMouth[i];
            PlayerPrefs.SetInt("mouth", -1);
        }
        else
        {

            for (int j = 0; j < SelectedCharacter.FacialHairs.Length; j++)
            {
                SelectedCharacter.FacialHairs[j].SetActive(false);
            }
            selectedMouth = SelectedCharacter.FacialHairs[i];
            selectedMouth.SetActive(true);
            PlayerPrefs.SetInt("mouth", i);
            //}
        }
    }
    void ChangeEyeBrows(int i) {

        if (SelectedCharacter.CharacterName.Contains("Female"))
        {
            SelectedEyebrows.GetComponent<Renderer>().material.mainTexture = FeMaleEyeBrows[i];
            PlayerPrefs.SetInt("eyebrows", -1);
        }
        else
        {
            SelectedEyebrows.GetComponent<Renderer>().material.mainTexture = MaleEyeBrows[i];
            PlayerPrefs.SetInt("eyebrows", i);
        }
    }


    //Change Specs
    public void ChangeAssessaries(int t)
    {
         for(int i=0;i< SelectedCharacter.EyeWears.Length; i++)
        {
            SelectedCharacter.EyeWears[i].SetActive(false);
        }
        SelectedCharacter.EyeWears[t].SetActive(true);
        SelectedAcccessories = SelectedCharacter.EyeWears[t];
        SelectedAcccessories.SetActive(true);
        PlayerPrefs.SetInt("assessories", t + 1);
    }

    //Change HairStyle
    public void ChangeHairStyle(int t)
    {
        for (int i = 0; i < SelectedCharacter.HairStyle.Length; i++)
        {
            SelectedCharacter.HairStyle[i].SetActive(false);
        }
        SelectedCharacter.HairStyle[t].SetActive(true);
        Selectedhair = SelectedCharacter.HairStyle[t];
        Selectedhair.SetActive(true);
        PlayerPrefs.SetInt("hair", t);
    }

    //change SkinTone
    Renderer[] r;
    Material[] allMaterials;
    List<Material> myMaterials;
    public void ChangeSkinTone(Image img)
    {
        myMaterials = new List<Material>();
         r = SelectedCharacter.CharModal.GetComponentsInChildren<Renderer>();
      
        for (int i = 0; i < r.Length; i++)
        {
             myMaterials = r[i].materials.ToList();

            for(int j=0;j<myMaterials.Count;j++)
            {
                if (myMaterials[j].name.Contains("skintone")|| myMaterials[j].name.Contains("SkinTone")
                    || myMaterials[j].name.Contains("eye"))
                {
                    myMaterials[j].color = img.color;
                    PlayerPrefs.SetFloat("R", img.color.r);
                    PlayerPrefs.SetFloat("G", img.color.g);
                    PlayerPrefs.SetFloat("B", img.color.b);
                }
            }        
        }
    }

    #endregion

    public void OnBackBtn()
    {
    
        foreach (Transform t in TopObj.transform) Destroy(t.gameObject);
        foreach (Transform t in BottomObj.transform) Destroy(t.gameObject);
        foreach (Transform t in ShoesObj.transform) Destroy(t.gameObject);
        foreach (Transform t in HeadObj.transform) Destroy(t.gameObject);
        foreach (Transform t in AccessoriesObj.transform) Destroy(t.gameObject);
        foreach (CharacterModals g in CharModals) g.CharModal.SetActive(false);
        characterSelectionScreen.SetActive(true);
        characterCustomizationScreen.SetActive(false);
        SelectedCharacter = null;
        SelectedmaleMouth = new List<GameObject>();
        SelectedmaleEyes = new List<GameObject>();
        SelectedmaleEyebrows = new List<GameObject>();
        SelectedmaleHead = new List<GameObject>();

       
       
    }

    public void Newback()
    {
        
    }

    public void OnBackClick() // join panel back 
    {
        joinRoomScreen.SetActive(false);
        characterCustomizationScreen.SetActive(true);
        SelectedCharacter.CharModal.gameObject.SetActive(true);

    }

    //OK button click 
    public GameObject joinRoomScreen;
    GameObject CurrentParent;
    public void OnOKButtonClick()// go to join panel
    {
        joinRoomScreen.SetActive(true);
        characterCustomizationScreen.SetActive(false);
        SelectedCharacter.CharModal.gameObject.SetActive(false);

        int a = PlayerPrefs.GetInt("top");
        int b = PlayerPrefs.GetInt("bottom");
        int c = PlayerPrefs.GetInt("hair");
        int d = PlayerPrefs.GetInt("Footware"); 

        int e = PlayerPrefs.GetInt("assessories");
        float f = PlayerPrefs.GetFloat("R");
        float g = PlayerPrefs.GetFloat("G");
        float h = PlayerPrefs.GetFloat("B");
        int i = PlayerPrefs.GetInt("mouth");
        SetPlayerProperties(a,b,c,d,e,f,g,h,i);
    }

    [Space(20)]
    [Header("-- Next Prev--")]
    public Button NextButton;
    public Button PrevButton;
    public Button OtherNextButton;
    public Button OtherPrevButton;
    int i = 0;
    public void OnNextBtnClick()
    {
        i++;

        if (i >= 2) {  NextButton.interactable = false; }
        PrevButton.interactable = true;
        SetChar(i);
    }

    public void OnPrevBtnClick()
    {
        i--;
        if (i <= 0) { PrevButton.interactable = false; }
        NextButton.interactable = true;
        SetChar(i);

    }

    //for other
    public void OnOtherNextBtnClick()
    {
        a++;
        Debug.Log("Next a:" + a);
        if (a >= 5)
        { OtherNextButton.interactable = false; } OtherPrevButton.interactable = true; 
       // else
       //     a++;
        SelectCharacter(a);
    }

    public void OnOtherPrevBtnClick()
    {
        a--;
        Debug.Log("Prev  a:" + a);
        if (a <= 0)
        { OtherPrevButton.interactable = false; }
        OtherNextButton.interactable = true;

        // else
        //   a--;
        SelectCharacter(a);
    }

    public void SetPlayerProperties(int TOP, int BOTTOM, int HAIRSTYLE, int FOOTWARE, int EYEWARE,float color_R, float color_G,
     float color_B,int MOUTH)
    {
        _myCustomProperties["TOP"] = TOP;
        _myCustomProperties["BOTTOM"] = BOTTOM;
        _myCustomProperties["HAIRSTYLE"] = HAIRSTYLE;
        _myCustomProperties["FOOTWARE"] = FOOTWARE;
        _myCustomProperties["EYEWARE"] = EYEWARE;
        _myCustomProperties["R"] = color_R;
        _myCustomProperties["G"] = color_G;
        _myCustomProperties["B"] = color_B;
        _myCustomProperties["MOUTH"] = MOUTH;
        _myCustomProperties["PlayerProfile"] = PlayerPrefs.GetInt("virtualEventUserId");
        Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties = _myCustomProperties;
    }

private ExitGames.Client.Photon.Hashtable _myCustomProperties =
 new ExitGames.Client.Photon.Hashtable();


}


[System.Serializable]
public class CharacterModals
{
    public string CharacterName;
    public GameObject CharModal;
    public GameObject[] TopModals;
    public GameObject[] Bottoms;
    public GameObject[] EyeWears;
    public GameObject[] Footware;
    public GameObject[] HairStyle;
    public GameObject[] FacialHairs;
    public GameObject HeadObj;
    public GameObject Eyes;
    public GameObject yebrows;
    public GameObject Mouth;

}
