using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DressUpScripts : MonoBehaviour
{
    //public EventSystem eventSystem;
    //UI
    public GameObject MainFrame;
    public GameObject LoginScreen; public InputField pwInputField;
    public GameObject MainScreen;
    public GameObject Step123;
    public GameObject Step1;
    public GameObject Step2;
    public GameObject Step3;

    //buttons
    public GameObject button_Step1;
    public GameObject button_Step2;
    public GameObject button_Step3;
    public Sprite step1_enabled;
    public Sprite step1_disabled;
    public Sprite step2_enabled;
    public Sprite step2_disabled;
    public Sprite step3_enabled;
    public Sprite step3_disabled;

    //catalogue groups 
    //step 1
    public GameObject GroupColor_Skin;
    public GameObject Group_Hair; public GameObject GroupColor_Hair;
    public GameObject Group_Eyes; public GameObject GroupColor_Eyes;
    public GameObject Group_Lips; public GameObject GroupColor_Lips;
    //step 2
    public GameObject Group_Tops; public GameObject GroupColor_Tops;
    public GameObject Group_Sleeves; public GameObject GroupColor_Sleeves;
    public GameObject Group_Bottoms; public GameObject GroupColor_Bottoms;
    //step 3
    public GameObject Group_Shoes; public GameObject GroupColor_Shoes;
    public GameObject Group_Purse; public GameObject GroupColor_Purse;
    public GameObject Group_Jewelry; public GameObject GroupColor_Jewelry;
    public GameObject Group_MoreAccessories; public GameObject GroupColor_MoreAccessories;


    //avatars dress up placeholders
    public GameObject Layers_Hair_Front;
    public GameObject Layers_Hair_Back; 
    public GameObject Layers_Eyes;
    public GameObject Layers_Lips; 
    public GameObject Layers_BaseBody; 
    public GameObject Layers_Head; 
    public GameObject Layers_Fist; 
    public GameObject Layers_Tops;
    public GameObject Layers_Sleeves; 
    public GameObject Layers_Bottoms; 
    public GameObject Layers_Shoes; 
    public GameObject Layers_Purse; 
    public GameObject Layers_Jewelry; 
    public GameObject Layers_More_Accessories; 

    //for storing assets
    public class AssetResources
    {
        public GameObject GroupUI;
        public GameObject prev_button;
        public GameObject next_button;
        public Object[] spriteList;
        public Object[] colorableList;
        public Object[] textureList;
        public Object[] thumbsList;
        int placeholderCount;
        public int[] current_showing_indexes;
        public int lastIndex;
        int currentSelectedThumb;
        
        public AssetResources(GameObject gui, Object[] sL, Object[] cL, Object[] txL, Object[] tL, int holderCount)
        {
            spriteList = sL;
            colorableList = cL;
            textureList = txL;
            if(gui==null) return; //no thumbs list
            GroupUI = gui;
            prev_button = GroupUI.transform.Find("buttons/prev").gameObject;
            next_button = GroupUI.transform.Find("buttons/next").gameObject;
            
            thumbsList = tL;
            placeholderCount = holderCount;
            current_showing_indexes = new int[placeholderCount];
            currentSelectedThumb = -2;

            initThumbs();
            initThumbSelection();
            initPrevNextClicks();            
        }

        void initThumbs()
        {
            int totalAvailableThumbnails = thumbsList.Length;
            for(int i=0; i<placeholderCount; i++){
                if(totalAvailableThumbnails > i) {
                    GameObject currentThumb = GroupUI.transform.Find("placeholders/"+i.ToString()).gameObject;
                    currentThumb.transform.Find("asset").GetComponent<Image>().sprite = (Sprite) thumbsList[i];
                    current_showing_indexes[i] = i;
                }
                else{
                    current_showing_indexes[i] = -1; 
                }
            }
            //hide next button if loaded assets not more than number of placeholders
            if(totalAvailableThumbnails > placeholderCount) next_button.SetActive(true);
            else next_button.SetActive(false);

            lastIndex = placeholderCount-1;
            prev_button.SetActive(false);
            updateSelectedThumb(); //remove all highlights
        }

        void initThumbSelection()
        {
            int totalAvailableThumbnails = thumbsList.Length;
            for(int i=0; i<placeholderCount; i++){
                if(totalAvailableThumbnails > i) {
                    GameObject currentThumb = GroupUI.transform.Find("placeholders/"+i.ToString()).gameObject;
                    if(currentThumb==null) continue;
                    int x = new int();
                    x = i;
                    currentThumb.GetComponent<Button> ().onClick.AddListener ( delegate { 
                        if(currentSelectedThumb == current_showing_indexes[x])
                            currentSelectedThumb = -2; //removes highlight
                        else
                            currentSelectedThumb = current_showing_indexes[x];
                        updateSelectedThumb();
                    });
                }
            }

        }

        void initPrevNextClicks()
        {
            prev_button.GetComponent<Button> ().onClick.AddListener ( delegate { prevThumbnail(); });
            next_button.GetComponent<Button> ().onClick.AddListener ( delegate { nextThumbnail(); });
        }

        void nextThumbnail()
        {
            if(thumbsList.Length-1 == lastIndex) initThumbs(); //showing the last thumbnail pic > return to first thumbnail
            else { //must have next thumbnail available
                for(int i=0; i<placeholderCount; i++){
                    int indexForThumb = current_showing_indexes[i]+1;
                    GroupUI.transform.Find("placeholders/"+i.ToString()).gameObject.transform.Find("asset").GetComponent<Image>().sprite = (Sprite) thumbsList[indexForThumb];
                    current_showing_indexes[i] = indexForThumb;
                }
                lastIndex = lastIndex + 1;
                prev_button.SetActive(true);
                updateSelectedThumb();
            }
        }

        void prevThumbnail()
        {
            for(int i=0; i<placeholderCount; i++){
                int indexForThumb = current_showing_indexes[i]-1;
                GroupUI.transform.Find("placeholders/"+i.ToString()).gameObject.transform.Find("asset").GetComponent<Image>().sprite = (Sprite) thumbsList[indexForThumb];
                current_showing_indexes[i] = indexForThumb;
            }
            lastIndex = lastIndex - 1;
            if(current_showing_indexes[0] == 0) prev_button.SetActive(false); //already to left limit
            updateSelectedThumb();
        }

        void updateSelectedThumb() //to show the dashes
        {
            for(int i=0; i<placeholderCount; i++){
                GameObject currentThumb = GroupUI.transform.Find("placeholders/"+i.ToString()).gameObject;
                if(currentThumb==null) continue;
                GameObject dashes = currentThumb.transform.Find("thumbnail_dashes").gameObject;
                if(current_showing_indexes[i]==currentSelectedThumb) dashes.SetActive(true);
                else dashes.SetActive(false);
            }
        }

    }
    //step 1
    AssetResources HairAssets;
    AssetResources HairBackAssets;
    AssetResources EyesAssets;
    AssetResources LipsAssets;
    //step 2
    AssetResources TopsAssets;
    AssetResources SleevesAssets;
    AssetResources BottomsAssets;
    //step 3
    AssetResources ShoesAssets;
    AssetResources PurseAssets;
    AssetResources JewelryAssets;
    AssetResources MoreAssets;

    Color32 white = new Color32(255,255,225,255);
    Color32 transparent = new Color32(0,0,0,0);
    public Sprite transparent_png;

    Color32[] colorGroup_Skin = { //11 placeholders
        new Color32(255, 243, 230, 255),
        new Color32(253, 220, 210, 255),
        new Color32(246, 211, 176, 255),
        new Color32(223, 176, 118, 255),
        new Color32(204, 125, 54, 255),
        new Color32(165, 105, 51, 255),
        new Color32(133, 71, 14, 255),
        new Color32(105, 51, 11, 255),
        new Color32(138, 102, 68, 255),
        new Color32(80, 40, 10, 255),
        new Color32(116, 67, 48, 255),
        new Color32(124, 57, 49, 255),
        new Color32(191, 130, 85, 255),
        new Color32(251, 191, 255, 255),
        new Color32(254, 143, 195, 255),
        new Color32(241, 103, 103, 255),
        new Color32(253, 243, 104, 255),
        new Color32(102, 223, 92, 255),
        new Color32(93, 243, 165, 255),
        new Color32(173, 240, 252, 255),
        new Color32(59, 103, 214, 255),
        new Color32(141, 96, 244, 255)
    };

    Color32[] colorGroup_Hair = { //17 placeholders
        new Color32(117, 47, 22, 255),
        new Color32(147, 167, 176, 255),
        new Color32(146, 66, 151, 255),
        new Color32(42, 55, 107, 255),
        new Color32(219, 43, 113, 255),
        new Color32(179, 131, 85, 255),
        new Color32(0, 0, 0, 255),
        new Color32(77, 77, 77, 255),
        new Color32(114, 191, 68, 255),
        new Color32(14, 20, 14, 255),
        new Color32(243, 229, 201, 255),
        new Color32(181, 128, 82, 255),
        new Color32(239, 217, 177, 255),
        new Color32(204, 175, 89, 255),
        new Color32(138, 102, 68, 255),
        new Color32(233, 199, 101, 255),
        new Color32(128, 87, 59, 255),
        new Color32(247, 238, 205, 255),
        new Color32(35, 43, 34, 255),
        new Color32(234, 232, 222, 255),
        new Color32(237, 199, 89, 255),
        new Color32(144, 97, 64, 255),
        new Color32(113, 72, 42, 255),
        new Color32(185, 65, 18, 255),
        new Color32(84, 65, 43, 255),
        new Color32(229, 213, 186, 255),
        new Color32(231, 216, 192, 255),
        new Color32(188, 75, 163, 255)
    };

    Color32[] colorGroup_Eyes = { //4 placeholders
        new Color32(0, 113, 143, 255),
        new Color32(124, 144, 44, 255),
        new Color32(102, 80, 30, 255),
        new Color32(188, 143, 217, 255),
        new Color32(68, 214, 248, 255),
        new Color32(37, 27, 1, 255),
        new Color32(105, 175, 0, 255),
        new Color32(146, 164, 174, 255),
        new Color32(121, 163, 221, 255),
        new Color32(173, 141, 72, 255),
        new Color32(166, 170, 133, 255),
        new Color32(170, 182, 203, 255),
        new Color32(94, 185, 202, 255),
        new Color32(89, 55, 34, 255),
        new Color32(123, 62, 51, 255),
        new Color32(71, 102, 178, 255),
        new Color32(165, 132, 84, 255),
        new Color32(99, 133, 177, 255),
        new Color32(115, 101, 105, 255),
        new Color32(44, 101, 56, 255),
        new Color32(89, 101, 104, 255),
        new Color32(89, 84, 101, 255),
        new Color32(74, 92, 110, 255),
        new Color32(62, 117, 143, 255),
        new Color32(118, 197, 176, 255),
        new Color32(160, 195, 188, 255),
        new Color32(193, 235, 230, 255),
        new Color32(203, 229, 235, 255),
        new Color32(200, 236, 206, 255),
        new Color32(210, 209, 180, 255),
        new Color32(202, 188, 129, 255),
        new Color32(223, 204, 174, 255),
        new Color32(222, 177, 163, 255),
        new Color32(216, 109, 111, 255),
        new Color32(86, 138, 98, 255),
        new Color32(101, 79, 115, 255)
    };

    Color32[] colorGroup_Lips = { //4 placeholders
        new Color32(197, 18, 76, 255),
        new Color32(181, 56, 26, 255),
        new Color32(198, 68, 75, 255),
        new Color32(229, 56, 147, 255),
        new Color32(250, 135, 178, 255),
        new Color32(254, 211, 234, 255),
        new Color32(114, 10, 99, 255),
        new Color32(158, 10, 23, 255),
        new Color32(219, 40, 114, 255),
        new Color32(222, 177, 163, 255),
        new Color32(216, 109, 111, 255),
        new Color32(248, 188, 166, 255),
        new Color32(167, 219, 225, 255),
        new Color32(253, 227, 112, 255),
        new Color32(255, 128, 52, 255),
        new Color32(222, 225, 231, 255),
        new Color32(247, 239, 193, 255),
        new Color32(205, 177, 34, 255),
        new Color32(250, 134, 241, 255),
        new Color32(141, 61, 152, 255),
        new Color32(131, 171, 173, 255),
        new Color32(202, 170, 147, 255),
        new Color32(203, 38, 41, 255),
        new Color32(248, 233, 202, 255),
        new Color32(255, 255, 255, 255),
        new Color32(160, 157, 204, 255),
        new Color32(251, 199, 215, 255),
        new Color32(34, 26, 2, 255),
        new Color32(177, 214, 137, 255),
        new Color32(224, 151, 198, 255),
        new Color32(118, 50, 37, 255),
        new Color32(41, 52, 104, 255),
        new Color32(37, 154, 150, 255),
        new Color32(236, 110, 154, 255),
        new Color32(37, 154, 150, 255)
    };

    Color32[] colorGroup_General = {
        new Color32(241, 122, 163, 255),
        new Color32(224, 151, 198, 255),
        new Color32(236, 110, 110, 255),
        new Color32(252, 44, 169, 255),
        new Color32(219, 40, 114, 255),
        new Color32(243, 13, 105, 255),
        new Color32(203, 38, 41, 255),
        new Color32(255, 0, 0, 255),
        new Color32(255, 128, 52, 255),
        new Color32(255, 190, 56, 255),
        new Color32(255, 245, 0, 255),
        new Color32(253, 227, 112, 255),
        new Color32(247, 239, 193, 255),
        new Color32(205, 177, 34, 255),
        new Color32(207, 255, 104, 255),
        new Color32(161, 205, 140, 255),
        new Color32(88, 148, 60, 255),
        new Color32(31, 118, 56, 255),
        new Color32(161, 246, 216, 255),
        new Color32(108, 182, 186, 255),
        new Color32(13, 38, 125, 255),
        new Color32(0, 18, 178, 255),
        new Color32(64, 116, 223, 255),
        new Color32(136, 163, 218, 255),
        new Color32(18, 185, 239, 255),
        new Color32(167, 219, 225, 255),
        new Color32(148, 255, 249, 255),
        new Color32(141, 61, 152, 255),
        new Color32(164, 109, 162, 255),
        new Color32(125, 44, 146, 255),
        new Color32(65, 41, 104, 255),
        new Color32(203, 147, 227, 255),
        new Color32(250, 134, 241, 255),
        new Color32(222, 225, 231, 255),
        new Color32(181, 176, 172, 255),
        new Color32(34, 26, 2, 255),
        new Color32(255, 255, 255, 255),
        new Color32(118, 50, 37, 255),
        new Color32(144, 97, 64, 255),
        new Color32(202, 170, 147, 255)
    };


    public class AssetColors
    {
        public GameObject GroupUI;
        public GameObject prev_button;
        public GameObject next_button;
        public Color32[] colorsList;
        int placeholderCount;
        public int[] current_showing_indexes;
        public int lastIndex;

        public AssetColors(GameObject gui, Color32[] cL, int holderCount)
        {
            colorsList = cL;
            if(gui==null) return; //no color group in UI
            GroupUI = gui;
            prev_button = GroupUI.transform.Find("prev").gameObject;
            next_button = GroupUI.transform.Find("next").gameObject;
            placeholderCount = holderCount;
            current_showing_indexes = new int[placeholderCount];

            initThumbs();
            initPrevNextClicks();   
        }

        void initThumbs() //fill in colors from index 0
        {
            int totalAvailableColors = colorsList.Length;
            for(int i=0; i<placeholderCount; i++){
                if(totalAvailableColors > i) {
                    GroupUI.transform.Find("colors/"+i.ToString()).gameObject.GetComponent<Image>().color = colorsList[i];
                    current_showing_indexes[i] = i;
                }
                else{
                    current_showing_indexes[i] = -1; 
                }
            }
            //hide next button if loaded assets not more than number of placeholders
            if(totalAvailableColors > placeholderCount) next_button.SetActive(true);
            else next_button.SetActive(false);

            lastIndex = placeholderCount-1;
            prev_button.SetActive(false);
        }

        void initPrevNextClicks()
        {
            prev_button.GetComponent<Button> ().onClick.AddListener ( delegate { prevColor(); });
            next_button.GetComponent<Button> ().onClick.AddListener ( delegate { nextColor(); });
        }

        void nextColor()
        {
            if(colorsList.Length-1 == lastIndex) initThumbs(); //showing the last thumbnail pic > return to first thumbnail
            else { //must have next thumbnail available
                for(int i=0; i<placeholderCount; i++){
                    int indexForThumb = current_showing_indexes[i]+1;
                    GroupUI.transform.Find("colors/"+i.ToString()).gameObject.GetComponent<Image>().color = colorsList[indexForThumb];
                    current_showing_indexes[i] = indexForThumb;
                }
                lastIndex = lastIndex + 1;
                prev_button.SetActive(true);
            }
        }

        void prevColor()
        {
            for(int i=0; i<placeholderCount; i++){
                int indexForThumb = current_showing_indexes[i]-1;
                GroupUI.transform.Find("colors/"+i.ToString()).gameObject.GetComponent<Image>().color = colorsList[indexForThumb];
                current_showing_indexes[i] = indexForThumb;
            }
            lastIndex = lastIndex - 1;
            if(current_showing_indexes[0] == 0) prev_button.SetActive(false); //already to left limit
        }
    }

    //part 1
    AssetColors AssetColors_Skin;
    AssetColors AssetColors_Hair;
    AssetColors AssetColors_Eyes;
    AssetColors AssetColors_Lips;
    //part 2
    AssetColors AssetColors_Tops;
    AssetColors AssetColors_Sleeves;
    AssetColors AssetColors_Bottoms;
    //part 4
    AssetColors AssetColors_Purse;
    AssetColors AssetColors_Shoes;
    AssetColors AssetColors_Jewelry;
    AssetColors AssetColors_More;


    // Start is called before the first frame update
    void Start()
    {
        LoginScreen.SetActive(true);
        MainFrame.SetActive(false);
        MainScreen.SetActive(false);
        Step123.SetActive(false);
        Step1.SetActive(false);
        Step2.SetActive(false);
        Step3.SetActive(false);
    }

    public void login()
    {
        string password = pwInputField.text;
        if(password == "devteam123") initGame();
    }

    void initGame()
    {
        pwInputField.DeactivateInputField();
        foreach(Transform child in LoginScreen.transform)
            Destroy(child.gameObject);
        LoginScreen.SetActive(false);
        MainFrame.SetActive(true);
        MainScreen.SetActive(true);
        Step123.SetActive(false);
        Step1.SetActive(false);
        Step2.SetActive(false);
        Step3.SetActive(false);
        
        //init blank costumes -> in future will load saved costumes
        nullAndHideCostumeLayers(Layers_Hair_Front);
        nullAndHideCostumeLayers(Layers_Hair_Back); 
        nullAndHideCostumeLayers(Layers_Eyes);
        nullAndHideCostumeLayers(Layers_Lips); 
        //nullAndHideCostumeLayers(Layers_BaseBody); 
        //nullAndHideCostumeLayers(Layers_Head); 
        //nullAndHideCostumeLayers(Layers_Fist); 
        nullAndHideCostumeLayers(Layers_Tops);
        nullAndHideCostumeLayers(Layers_Sleeves); 
        nullAndHideCostumeLayers(Layers_Bottoms); 
        nullAndHideCostumeLayers(Layers_Shoes); 
        nullAndHideCostumeLayers(Layers_Purse); 
        nullAndHideCostumeLayers(Layers_Jewelry); 
        nullAndHideCostumeLayers(Layers_More_Accessories); 

        loadAllAssets();
        initColorAssets();
    }

    void nullAndHideCostumeLayers(GameObject AvatarPartGroup)
    {
        GameObject AvatarDefaultLayer = AvatarPartGroup.transform.Find("default").gameObject;
        GameObject AvatarColorableLayer = AvatarPartGroup.transform.Find("colorable").gameObject;
        GameObject AvatarTextureLayer = AvatarPartGroup.transform.Find("texture").gameObject;
        AvatarDefaultLayer.GetComponent<Image>().sprite = null; AvatarDefaultLayer.SetActive(false);
        AvatarColorableLayer.GetComponent<Image>().sprite = null; AvatarColorableLayer.SetActive(false);
        AvatarTextureLayer.GetComponent<Image>().sprite = null; AvatarTextureLayer.SetActive(false);
        AvatarPartGroup.SetActive(false);
    }

    void loadAllAssets() //load assets from Assets/Resources folder
    {
        //step 1
        HairAssets = new AssetResources(Group_Hair, loadRes("hair"), loadRes("hair_colorable"), loadRes("hair_texture"), loadRes("thumbnails/hair"), 4); 
        HairBackAssets = new AssetResources(null, loadRes("hairback"), loadRes("hairback_colorable"), loadRes("hairback_texture"), null, 0); 
        EyesAssets = new AssetResources(Group_Eyes, loadRes("eyes"), loadRes("eyes_colorable"), loadRes("eyes_texture"), loadRes("thumbnails/eyes"), 2);             
        LipsAssets = new AssetResources(Group_Lips, loadRes("lips"), loadRes("lips_colorable"), loadRes("lips_texture"), loadRes("thumbnails/lips"), 2);
        //step 2
        TopsAssets = new AssetResources(Group_Tops, loadRes("tops"), loadRes("tops_colorable"), loadRes("tops_texture"), loadRes("thumbnails/tops"), 4);
        SleevesAssets = new AssetResources(Group_Sleeves, loadRes("sleeves"), loadRes("sleeves_colorable"), loadRes("sleeves_texture"), loadRes("thumbnails/sleeves"), 1);
        BottomsAssets = new AssetResources(Group_Bottoms, loadRes("bottoms"), loadRes("bottoms_colorable"), loadRes("bottoms_texture"), loadRes("thumbnails/bottoms"), 6);
        //step 3
        ShoesAssets = new AssetResources(Group_Shoes, loadRes("shoes"), loadRes("shoes_colorable"), loadRes("shoes_texture"), loadRes("thumbnails/shoes"), 2);
        PurseAssets = new AssetResources(Group_Purse, loadRes("purse"), loadRes("purse_colorable"), loadRes("purse_texture"), loadRes("thumbnails/purse"), 2);
        JewelryAssets = new AssetResources(Group_Jewelry, loadRes("jewelry"), loadRes("jewelry_colorable"), loadRes("jewelry_texture"), loadRes("thumbnails/jewelry"), 2);
        MoreAssets = new AssetResources(Group_MoreAccessories, loadRes("more"), loadRes("more_colorable"), loadRes("more_texture"), loadRes("thumbnails/more"), 2);
    }

    Object[] loadRes(string subfolder) {return Resources.LoadAll("dress up assets/"+subfolder, typeof(Sprite));}

    void initColorAssets()
    {
        AssetColors_Skin = new AssetColors(GroupColor_Skin, colorGroup_Skin, 11);
        AssetColors_Hair = new AssetColors(GroupColor_Hair, colorGroup_Hair, 17);
        AssetColors_Eyes = new AssetColors(GroupColor_Eyes, colorGroup_Eyes, 4);
        AssetColors_Lips = new AssetColors(GroupColor_Lips, colorGroup_Lips, 4);
        //part 2
        AssetColors_Tops = new AssetColors(GroupColor_Tops, colorGroup_General, 14);
        AssetColors_Sleeves = new AssetColors(GroupColor_Sleeves, colorGroup_General, 3);
        AssetColors_Bottoms = new AssetColors(GroupColor_Bottoms, colorGroup_General, 20);
        //part 4
        AssetColors_Purse = new AssetColors(GroupColor_Purse, colorGroup_General, 6);
        AssetColors_Shoes = new AssetColors(GroupColor_Shoes, colorGroup_General, 6);
        AssetColors_Jewelry = new AssetColors(GroupColor_Jewelry, colorGroup_General, 6);
        AssetColors_More = new AssetColors(GroupColor_MoreAccessories, colorGroup_General, 6);

    }
    

    // Update is called once per frame
    void Update(){}

    public void step_1_Clicked()
    {
        MainScreen.SetActive(false);
        Step123.SetActive(true);
        Step1.SetActive(true);
        Step2.SetActive(false);
        Step3.SetActive(false);
        button_Step1.GetComponent<Image>().sprite = step1_enabled;
        button_Step2.GetComponent<Image>().sprite = step2_disabled;
        button_Step3.GetComponent<Image>().sprite = step3_disabled;
    }

    public void step_2_Clicked()
    {
        MainScreen.SetActive(false);
        Step123.SetActive(true);
        Step1.SetActive(false);
        Step2.SetActive(true);
        Step3.SetActive(false);
        button_Step1.GetComponent<Image>().sprite = step1_disabled;
        button_Step2.GetComponent<Image>().sprite = step2_enabled;
        button_Step3.GetComponent<Image>().sprite = step3_disabled;
    }

    public void step_3_Clicked()
    {
        MainScreen.SetActive(false);
        Step123.SetActive(true);
        Step1.SetActive(false);
        Step2.SetActive(false);
        Step3.SetActive(true);
        button_Step1.GetComponent<Image>().sprite = step1_disabled;
        button_Step2.GetComponent<Image>().sprite = step2_disabled;
        button_Step3.GetComponent<Image>().sprite = step3_enabled;
    }

    public void goto_mainScreen() 
    {
        MainScreen.SetActive(true);
        Step123.SetActive(false);
        Step1.SetActive(false);
        Step2.SetActive(false);
        Step3.SetActive(false);
        button_Step1.GetComponent<Image>().sprite = step1_disabled;
        button_Step2.GetComponent<Image>().sprite = step2_disabled;
        button_Step3.GetComponent<Image>().sprite = step3_disabled;
    }


///////////////////////////////////////dress up functions///////////////

    public void setSkin(GameObject selectedSkin)
    {
        wearColorOnAvatar(selectedSkin, Layers_BaseBody);
        wearColorOnAvatar(selectedSkin, Layers_Fist);
        wearColorOnAvatar(selectedSkin, Layers_Head);
    }

    public void setHair(int index)
    {
        int indexToWear = HairAssets.current_showing_indexes[index];
        if(indexToWear<0) return;
        Sprite selectedHairSprite_def; try{ selectedHairSprite_def = (Sprite) HairAssets.spriteList[indexToWear]; } catch{selectedHairSprite_def = transparent_png;}
        Sprite selectedHairSprite_col; selectedHairSprite_col = getMatchingSpriteAsset(HairAssets.colorableList, selectedHairSprite_def);
        Sprite selectedHairSprite_tex; selectedHairSprite_tex = getMatchingSpriteAsset(HairAssets.textureList, selectedHairSprite_def);
        Sprite selectedHairBackSprite_def; try{ selectedHairBackSprite_def = (Sprite) HairBackAssets.spriteList[indexToWear]; } catch{selectedHairBackSprite_def = transparent_png;}
        Sprite selectedHairBackSprite_col; selectedHairBackSprite_col = getMatchingSpriteAsset(HairBackAssets.colorableList, selectedHairBackSprite_def);
        Sprite selectedHairBackSprite_tex; selectedHairBackSprite_tex = getMatchingSpriteAsset(HairBackAssets.textureList, selectedHairBackSprite_def);
        GameObject Hair_Front_DefaultLayer = Layers_Hair_Front.transform.Find("default").gameObject;
        GameObject Hair_Front_ColorableLayer = Layers_Hair_Front.transform.Find("colorable").gameObject;
        GameObject Hair_Front_TextureLayer = Layers_Hair_Front.transform.Find("texture").gameObject;
        GameObject Hair_Back_DefaultLayer = Layers_Hair_Back.transform.Find("default").gameObject;
        GameObject Hair_Back_ColorableLayer = Layers_Hair_Back.transform.Find("colorable").gameObject;
        GameObject Hair_Back_TextureLayer = Layers_Hair_Back.transform.Find("texture").gameObject;
        if(selectedHairSprite_def != Hair_Front_DefaultLayer.GetComponent<Image>().sprite) //user selected different hair -> use default hair color, hide colorable layer
        {
            //front hair
            Layers_Hair_Front.SetActive(true);
            Hair_Front_DefaultLayer.GetComponent<Image>().sprite =  selectedHairSprite_def;
            Hair_Front_DefaultLayer.SetActive(true);
            Hair_Front_ColorableLayer.GetComponent<Image>().sprite =  selectedHairSprite_col;
            Hair_Front_ColorableLayer.SetActive(false); //hide but just set sprite for coloring later
            Hair_Front_TextureLayer.GetComponent<Image>().sprite =  selectedHairSprite_tex;
            Hair_Front_TextureLayer.SetActive(true);
            //back hair
            Layers_Hair_Back.SetActive(true);
            Hair_Back_DefaultLayer.GetComponent<Image>().sprite =  selectedHairBackSprite_def;
            Hair_Back_DefaultLayer.SetActive(true);
            Hair_Back_ColorableLayer.GetComponent<Image>().sprite =  selectedHairBackSprite_col;
            Hair_Back_ColorableLayer.SetActive(false); //hide but just set sprite for coloring later
            Hair_Back_TextureLayer.GetComponent<Image>().sprite =  selectedHairBackSprite_tex;
            Hair_Back_TextureLayer.SetActive(true);
            //Hair_Template.SetActive(false);
        }
        else //same hair -> use hair template, make bald
        {
            Hair_Front_DefaultLayer.GetComponent<Image>().sprite = null;
            Hair_Front_ColorableLayer.GetComponent<Image>().sprite = null;
            Hair_Front_TextureLayer.GetComponent<Image>().sprite = null;
            Hair_Back_DefaultLayer.GetComponent<Image>().sprite = null;
            Hair_Back_ColorableLayer.GetComponent<Image>().sprite = null;
            Hair_Back_TextureLayer.GetComponent<Image>().sprite = null;
            //Hair_Template.SetActive(true);
            Layers_Hair_Front.SetActive(false);
            Layers_Hair_Back.SetActive(false);
        }

    }

    public void setEyes(int index) {wearDefaultOnAvatar(index, EyesAssets, Layers_Eyes);}
    public void setLips(int index) {wearDefaultOnAvatar(index, LipsAssets, Layers_Lips);}
    public void setTops(int index) {wearDefaultOnAvatar(index, TopsAssets, Layers_Tops);}
    public void setSleeves(int index) {wearDefaultOnAvatar(index, SleevesAssets, Layers_Sleeves);}
    public void setBottoms(int index) {wearDefaultOnAvatar(index, BottomsAssets, Layers_Bottoms);}
    public void setShoes(int index) {wearDefaultOnAvatar(index, ShoesAssets, Layers_Shoes);}
    public void setPurse(int index) {wearDefaultOnAvatar(index, PurseAssets, Layers_Purse);}
    public void setJewelry(int index) {wearDefaultOnAvatar(index, JewelryAssets, Layers_Jewelry);}
    public void setMoreAccessories(int index) {wearDefaultOnAvatar(index, MoreAssets, Layers_More_Accessories);}

    public void wearDefaultOnAvatar(int index, AssetResources AssetGroup, GameObject AvatarPartGroup)
    {
        int indexToWear = AssetGroup.current_showing_indexes[index];
        if(indexToWear<0) return;
        Sprite selectedDefaultLayer; try{ selectedDefaultLayer = (Sprite) AssetGroup.spriteList[indexToWear]; } catch{selectedDefaultLayer = transparent_png;}
        Sprite selectedColorableLayer; selectedColorableLayer = getMatchingSpriteAsset(AssetGroup.colorableList, selectedDefaultLayer);  
        Sprite selectedTextureLayer; selectedTextureLayer = getMatchingSpriteAsset(AssetGroup.textureList, selectedDefaultLayer);
        GameObject AvatarDefaultLayer = AvatarPartGroup.transform.Find("default").gameObject;
        GameObject AvatarColorableLayer = AvatarPartGroup.transform.Find("colorable").gameObject;
        GameObject AvatarTextureLayer = AvatarPartGroup.transform.Find("texture").gameObject;
        //check if same asset
        if(selectedDefaultLayer != AvatarDefaultLayer.GetComponent<Image>().sprite) //clicked on not same asset as previous, wear default layer, hide colorable layer
        {
            AvatarDefaultLayer.GetComponent<Image>().sprite =  selectedDefaultLayer;
            AvatarColorableLayer.GetComponent<Image>().sprite =  selectedColorableLayer; AvatarColorableLayer.GetComponent<Image>().color = transparent;
            AvatarTextureLayer.GetComponent<Image>().sprite =  selectedTextureLayer;
            AvatarPartGroup.SetActive(true);
            AvatarDefaultLayer.SetActive(true);
            AvatarTextureLayer.SetActive(true);
            AvatarColorableLayer.SetActive(false);
        }
        else //same asset -> remove it from avatar
        {
            AvatarDefaultLayer.GetComponent<Image>().sprite = null;
            AvatarColorableLayer.GetComponent<Image>().sprite = null;
            AvatarTextureLayer.GetComponent<Image>().sprite = null;
            AvatarPartGroup.SetActive(false);
        }
    }

    Sprite getMatchingSpriteAsset(Object[] resourceList, Sprite defaultSprite)
    {
        for(int a=0; a<resourceList.Length; a++){
            Sprite curSprite = (Sprite) resourceList[a];
            if(curSprite.name == defaultSprite.name)
                return curSprite;
        }
        return transparent_png;
    }


    public void setHairColor(GameObject selectedColor) {wearColorOnAvatar(selectedColor, Layers_Hair_Front); wearColorOnAvatar(selectedColor, Layers_Hair_Back);} //for front & back hair
    public void setEyesColor(GameObject selectedColor) {wearColorOnAvatar(selectedColor, Layers_Eyes);}
    public void setLipsColor(GameObject selectedColor) {wearColorOnAvatar(selectedColor, Layers_Lips);}
    public void setTopsColor(GameObject selectedColor) {wearColorOnAvatar(selectedColor, Layers_Tops);}
    public void setSleevesColor(GameObject selectedColor) {wearColorOnAvatar(selectedColor, Layers_Sleeves);}
    public void setBottomsColor(GameObject selectedColor) {wearColorOnAvatar(selectedColor, Layers_Bottoms);}
    public void setShoeColor(GameObject selectedColor) {wearColorOnAvatar(selectedColor, Layers_Shoes);}
    public void setPurseColor(GameObject selectedColor) {wearColorOnAvatar(selectedColor, Layers_Purse);}
    public void setJewelryColor(GameObject selectedColor) {wearColorOnAvatar(selectedColor, Layers_Jewelry);}
    public void setMoreAccessoriesColor(GameObject selectedColor) {wearColorOnAvatar(selectedColor, Layers_More_Accessories);}

    public void wearColorOnAvatar(GameObject selectedColor, GameObject AvatarPartGroup) //no effect on texture for now
    {
        if(AvatarPartGroup.activeSelf == false) return; //no effect on colors if layers are hidden
        GameObject AvatarDefaultLayer = AvatarPartGroup.transform.Find("default").gameObject;
        GameObject AvatarColorableLayer = AvatarPartGroup.transform.Find("colorable").gameObject;
        GameObject AvatarTextureLayer = AvatarPartGroup.transform.Find("texture").gameObject;
        //check if same color
        if(selectedColor.GetComponent<Image>().color != AvatarColorableLayer.GetComponent<Image>().color) //clicked on not same color as previous, wear colorable layer
        {
            AvatarColorableLayer.GetComponent<Image>().color =  selectedColor.GetComponent<Image>().color;
            AvatarTextureLayer.SetActive(true);
            AvatarColorableLayer.SetActive(true);
            AvatarDefaultLayer.SetActive(true);
        }
        else //same color, wear default layer, hide colorable layer
        {
            AvatarColorableLayer.GetComponent<Image>().color = transparent;
            AvatarTextureLayer.SetActive(true);
            AvatarColorableLayer.SetActive(false);
            AvatarDefaultLayer.SetActive(true);
        }
    }

    
}
