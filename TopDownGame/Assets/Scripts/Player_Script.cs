using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Script : MonoBehaviour {

    #region Declaration
    float speed = 0.08f;
    float speed2 = 0.06f;

    public Animator animator;
    public GameObject CastleInside;
    public GameObject shopMenu;
    public GameObject Inventory;
    public GameObject armourInventory;

    bool canEnterShop = false;

    public Canvas myCanvas;

    bool isInvetoryOpen = false;

    public static int looking = 1;
    int isWalking = 0;
    int numWalking = 0;

    public int WaitToChange;

    int brWaitToChange = 0;

    int timesClicked = 0;

    bool isShopOpen;

    int Money = 0;
    string strMoney;
    public Text MoneyText;

    int ItemClicked = -1;

    public Image imageToFollow;

    public GameObject imageToFollowObject;

    public Button buyApple;

    public Image[] Slots;
    public Image Img;
    Image OldImg;

    public Sprite NoHelmet;
    public Sprite NoChestplate;
    public Sprite NoBelt;
    public Sprite NoBoots;
    public Sprite NoWeapon;
    public Sprite NoRing;

    public Sprite EmptySlot;
    public Sprite Egg;
    public Sprite Apple;
    public Sprite HelmetLevel1;
    public Sprite ChestLevel1;

    public Texture2D cursorTexture;
    public Texture2D cursorTextureClicked;

    public GameObject DropItem;

    GameObject DropedItem;
    public SpriteRenderer DropedItemImage;

    bool CanPickUp = false;

    public string pickUp = "f";
    public string openInventory = "b";
    public string dropItems = "q";
    public string openShops = "e";

    #endregion

    void Start() {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);

        shopMenu.SetActive(false);
        animator = gameObject.GetComponentInChildren(typeof(Animator)) as Animator;
        MoneyText.text = "4321";

        OldImg = Img;
        //imageToFollow = Img;

        for (int i = 0; i < 12; i++)
        {
            Slots[i].sprite = EmptySlot;
        }
        Slots[12].sprite = NoHelmet;
        Slots[13].sprite = NoChestplate;
        Slots[14].sprite = NoBelt;
        Slots[15].sprite = NoBoots;
        Slots[16].sprite = NoWeapon;
        Slots[17].sprite = NoRing;
    }
    
    void Update() {

        if (ItemClicked != -1)
        {
            imageToFollowObject.SetActive(true);
            //imageToFollow2 = imageToFollow;
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
            imageToFollow.transform.position = myCanvas.transform.TransformPoint(pos);
            Cursor.SetCursor(cursorTextureClicked, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }
    }

    void setMoney()
    {
        MoneyText.text = Money.ToString();
    }

    void FixedUpdate()
    {
        isWalking = 0;
        numWalking = 0;
        if(brWaitToChange == 0)
        {
            imageToFollow.sprite = EmptySlot;
            brWaitToChange--;
        }
        else if (brWaitToChange > 0)
        {
            brWaitToChange--;
        }
        if(Input.GetKeyDown(pickUp) && CanPickUp == true)
        {
            CanPickUp = false;

            DropedItemImage = DropedItem.gameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;

            for (int i = 0; i < 12; i++)
            {
                if (Slots[i].sprite == EmptySlot)
                {
                    Slots[i].sprite = DropedItemImage.sprite;
                    break;
                }
            }
            Destroy(DropedItem);
        }
        if (Input.GetKeyDown(dropItems) && imageToFollow != EmptySlot)
        {
            Instantiate(DropItem);
            ItemClicked = -1;

            brWaitToChange = WaitToChange;
        }
        if (Input.GetKeyDown(openInventory) && isShopOpen == false)
        {
            isInvetoryOpen = !isInvetoryOpen;
            Inventory.SetActive(isInvetoryOpen);
            armourInventory.SetActive(isInvetoryOpen);
        }
        if ((Input.GetKeyDown(openShops) && isShopOpen == true) || (canEnterShop == false && isShopOpen == true))
        {
            shopMenu.SetActive(false);
            isInvetoryOpen = false;
            Inventory.SetActive(isInvetoryOpen);
            armourInventory.SetActive(false);
            isShopOpen = false;
        }
        else if ((Input.GetKeyDown(openShops) && canEnterShop == true))
        {
            shopMenu.SetActive(true);
            isInvetoryOpen = true;
            Inventory.SetActive(isInvetoryOpen);
            armourInventory.SetActive(false);
            isShopOpen = true;
        }
        else
        {

            if (Input.GetKey("w") && Input.GetKey("d"))
            {
                isWalking = 1;
                transform.position = new Vector3(transform.position.x + speed2, transform.position.y, transform.position.z);
                transform.position = new Vector3(transform.position.x, transform.position.y + speed2, transform.position.z);

                if (looking != 3)
                {
                    isWalking = 0;
                }

                looking = 3;
            }
            else if (Input.GetKey("s") && Input.GetKey("d"))
            {
                isWalking = 1;
                transform.position = new Vector3(transform.position.x + speed2, transform.position.y, transform.position.z);
                transform.position = new Vector3(transform.position.x, transform.position.y - speed2, transform.position.z);

                if (looking != 1)
                {
                    isWalking = 0;
                }

                looking = 1;
            }
            else if (Input.GetKey("s") && Input.GetKey("a"))
            {
                isWalking = 1;
                transform.position = new Vector3(transform.position.x - speed2, transform.position.y, transform.position.z);
                transform.position = new Vector3(transform.position.x, transform.position.y - speed2, transform.position.z);

                if (looking != 1)
                {
                    isWalking = 0;
                }

                looking = 1;
            }
            else if (Input.GetKey("w") && Input.GetKey("a"))
            {
                isWalking = 1;
                transform.position = new Vector3(transform.position.x - speed2, transform.position.y, transform.position.z);
                transform.position = new Vector3(transform.position.x, transform.position.y + speed2, transform.position.z);

                if (looking != 3)
                {
                    isWalking = 0;
                }

                looking = 3;
            }
            else
            {
                if (Input.GetKey("d"))
                {
                    isWalking = 1;
                    transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
                    if (looking != 2)
                    {
                        isWalking = 0;
                    }
                    numWalking++;
                    looking = 2;
                }
                if (Input.GetKey("a"))
                {
                    isWalking = 1;
                    transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
                    if (looking != 4)
                    {
                        isWalking = 0;
                    }
                    numWalking++;
                    looking = 4;
                }
                if (Input.GetKey("w"))
                {
                    isWalking = 1;
                    transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
                    if (looking != 3)
                    {
                        isWalking = 0;
                    }
                    numWalking++;
                    looking = 3;
                }
                if (Input.GetKey("s"))
                {
                    isWalking = 1;
                    transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
                    if (looking != 1)
                    {
                        isWalking = 0;
                    }
                    numWalking++;
                    looking = 1;
                }
            }
        }
        animatorInts();
    }

    void animatorInts()
    {

        animator.SetInteger("LookingPos", looking);
        animator.SetInteger("isWalking", isWalking);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Castle enter")
        {
            transform.position = new Vector3(CastleInside.transform.position.x, CastleInside.transform.position.y, CastleInside.transform.position.z);
        }
        if (coll.gameObject.tag == "ShopEnter")
        {
            canEnterShop = true;
        }

        if(coll.gameObject.tag == "DropedItem")
        {
            CanPickUp = true;
            DropedItem = coll.gameObject;
        }

    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "ShopEnter")
        {
            canEnterShop = false;
        }

        if (coll.gameObject.tag == "DropedItem")
        {
            CanPickUp = false;
        }
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    IEnumerator Wait2Secs()
    {
        yield return new WaitForSeconds(2);
    }

    #region Buy Stuff
    public void buy_Apple()
    {
        for (int i = 0; i < 12; i++)
        {
            if (Slots[i].sprite == EmptySlot)
            {
                Slots[i].sprite = Apple;
                break;
            }
        }
    }

    public void buy_Egg()
    {
        for (int i = 0; i < 12; i++)
        {
            if (Slots[i].sprite == EmptySlot)
            {
                Slots[i].sprite = Egg;
                break;
            }
        }
    }

    public void buy_HelmetLevel1()
    {
        for (int i = 0; i < 12; i++)
        {
            if (Slots[i].sprite == EmptySlot)
            {
                Slots[i].sprite = HelmetLevel1;
                break;
            }
        }
    }

    public void buy_ChestLevel1()
    {
        for (int i = 0; i < 12; i++)
        {
            if (Slots[i].sprite == EmptySlot)
            {
                Slots[i].sprite = ChestLevel1;
                break;
            }
        }
    }
    #endregion

    #region Slots  
    public void Slot1_button()
    {
        if (ItemClicked == -1 && Slots[0].sprite != EmptySlot)
        {
            ItemClicked = 0;
            imageToFollow.sprite = Slots[ItemClicked].sprite;
            OldImg.sprite = Slots[ItemClicked].sprite;
            Slots[ItemClicked].sprite = EmptySlot;
        }
        else if (ItemClicked != -1)
        {
            int ItemClickedNow = 0;
            if (Slots[ItemClickedNow].sprite == EmptySlot)
            {
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = EmptySlot;

                ItemClicked = -1;
                timesClicked = 0;
            }
            else
            {
                timesClicked++;
                Img.sprite = Slots[ItemClickedNow].sprite;
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = Img.sprite;

                ItemClicked = ItemClickedNow;
            }
        }
    }

    public void Slot2_button()
    {
        if (ItemClicked == -1 && Slots[1].sprite != EmptySlot)
        {
            ItemClicked = 1;
            imageToFollow.sprite = Slots[ItemClicked].sprite;
            OldImg.sprite = Slots[ItemClicked].sprite;
            Slots[ItemClicked].sprite = EmptySlot;
        }
        else if (ItemClicked != -1)
        {
            int ItemClickedNow = 1;
            if (Slots[ItemClickedNow].sprite == EmptySlot)
            {
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = EmptySlot;

                ItemClicked = -1;
                timesClicked = 0;
            }
            else
            {
                timesClicked++;
                Img.sprite = Slots[ItemClickedNow].sprite;
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = Img.sprite;

                ItemClicked = ItemClickedNow;
            }
        }
    }

    public void Slot3_button()
    {
        if (ItemClicked == -1 && Slots[2].sprite != EmptySlot)
        {
            ItemClicked = 2;
            imageToFollow.sprite = Slots[ItemClicked].sprite;
            OldImg.sprite = Slots[ItemClicked].sprite;
            Slots[ItemClicked].sprite = EmptySlot;
        }
        else if (ItemClicked != -1)
        {
            int ItemClickedNow = 2;
            if (Slots[ItemClickedNow].sprite == EmptySlot)
            {
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = EmptySlot;

                ItemClicked = -1;
                timesClicked = 0;
            }
            else
            {
                timesClicked++;
                Img.sprite = Slots[ItemClickedNow].sprite;
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = Img.sprite;

                ItemClicked = ItemClickedNow;
            }
        }
    }

    public void Slot4_button()
    {
        if (ItemClicked == -1 && Slots[3].sprite != EmptySlot)
        {
            ItemClicked = 3;
            imageToFollow.sprite = Slots[ItemClicked].sprite;
            OldImg.sprite = Slots[ItemClicked].sprite;
            Slots[ItemClicked].sprite = EmptySlot;
        }
        else if (ItemClicked != -1)
        {
            int ItemClickedNow = 3;
            if (Slots[ItemClickedNow].sprite == EmptySlot)
            {
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = EmptySlot;

                ItemClicked = -1;
                timesClicked = 0;
            }
            else
            {
                timesClicked++;
                Img.sprite = Slots[ItemClickedNow].sprite;
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = Img.sprite;

                ItemClicked = ItemClickedNow;
            }
        }
    }

    public void Slot5_button()
    {
        if (ItemClicked == -1 && Slots[4].sprite != EmptySlot)
        {
            ItemClicked = 4;
            imageToFollow.sprite = Slots[ItemClicked].sprite;
            OldImg.sprite = Slots[ItemClicked].sprite;
            Slots[ItemClicked].sprite = EmptySlot;
        }
        else if (ItemClicked != -1)
        {
            int ItemClickedNow = 4;
            if (Slots[ItemClickedNow].sprite == EmptySlot)
            {
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = EmptySlot;

                ItemClicked = -1;
                timesClicked = 0;
            }
            else
            {
                timesClicked++;
                Img.sprite = Slots[ItemClickedNow].sprite;
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = Img.sprite;

                ItemClicked = ItemClickedNow;
            }
        }
    }

    public void Slot6_button()
    {
        if (ItemClicked == -1 && Slots[5].sprite != EmptySlot)
        {
            ItemClicked = 5;
            imageToFollow.sprite = Slots[ItemClicked].sprite;
            OldImg.sprite = Slots[ItemClicked].sprite;
            Slots[ItemClicked].sprite = EmptySlot;
        }
        else if (ItemClicked != -1)
        {
            int ItemClickedNow = 5;
            if (Slots[ItemClickedNow].sprite == EmptySlot)
            {
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = EmptySlot;

                ItemClicked = -1;
                timesClicked = 0;
            }
            else
            {
                timesClicked++;
                Img.sprite = Slots[ItemClickedNow].sprite;
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = Img.sprite;

                ItemClicked = ItemClickedNow;
            }
        }
    }

    public void Slot7_button()
    {
        if (ItemClicked == -1 && Slots[6].sprite != EmptySlot)
        {
            ItemClicked = 6;
            imageToFollow.sprite = Slots[ItemClicked].sprite;
            OldImg.sprite = Slots[ItemClicked].sprite;
            Slots[ItemClicked].sprite = EmptySlot;
        }
        else if (ItemClicked != -1)
        {
            int ItemClickedNow = 6;
            if (Slots[ItemClickedNow].sprite == EmptySlot)
            {
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = EmptySlot;

                ItemClicked = -1;
                timesClicked = 0;
            }
            else
            {
                timesClicked++;
                Img.sprite = Slots[ItemClickedNow].sprite;
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = Img.sprite;

                ItemClicked = ItemClickedNow;
            }
        }
    }

    public void Slot8_button()
    {
        if (ItemClicked == -1 && Slots[7].sprite != EmptySlot)
        {
            ItemClicked = 7;
            imageToFollow.sprite = Slots[ItemClicked].sprite;
            OldImg.sprite = Slots[ItemClicked].sprite;
            Slots[ItemClicked].sprite = EmptySlot;
        }
        else if (ItemClicked != -1)
        {
            int ItemClickedNow = 7;
            if (Slots[ItemClickedNow].sprite == EmptySlot)
            {
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = EmptySlot;

                ItemClicked = -1;
                timesClicked = 0;
            }
            else
            {
                timesClicked++;
                Img.sprite = Slots[ItemClickedNow].sprite;
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = Img.sprite;

                ItemClicked = ItemClickedNow;
            }
        }
    }

    public void Slot9_button()
    {
        if (ItemClicked == -1 && Slots[8].sprite != EmptySlot)
        {
            ItemClicked = 8;
            imageToFollow.sprite = Slots[ItemClicked].sprite;
            OldImg.sprite = Slots[ItemClicked].sprite;
            Slots[ItemClicked].sprite = EmptySlot;
        }
        else if (ItemClicked != -1)
        {
            int ItemClickedNow = 8;
            if (Slots[ItemClickedNow].sprite == EmptySlot)
            {
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = EmptySlot;

                ItemClicked = -1;
                timesClicked = 0;
            }
            else
            {
                timesClicked++;
                Img.sprite = Slots[ItemClickedNow].sprite;
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = Img.sprite;

                ItemClicked = ItemClickedNow;
            }
        }
    }

    public void Slot10_button()
    {
        if (ItemClicked == -1 && Slots[9].sprite != EmptySlot)
        {
            ItemClicked = 9;
            imageToFollow.sprite = Slots[ItemClicked].sprite;
            OldImg.sprite = Slots[ItemClicked].sprite;
            Slots[ItemClicked].sprite = EmptySlot;
        }
        else if (ItemClicked != -1)
        {
            int ItemClickedNow = 9;
            if (Slots[ItemClickedNow].sprite == EmptySlot)
            {
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = EmptySlot;

                ItemClicked = -1;
                timesClicked = 0;
            }
            else
            {
                timesClicked++;
                Img.sprite = Slots[ItemClickedNow].sprite;
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = Img.sprite;

                ItemClicked = ItemClickedNow;
            }
        }
    }

    public void Slot11_button()
    {
        if (ItemClicked == -1 && Slots[10].sprite != EmptySlot)
        {
            ItemClicked = 10;
            imageToFollow.sprite = Slots[ItemClicked].sprite;
            OldImg.sprite = Slots[ItemClicked].sprite;
            Slots[ItemClicked].sprite = EmptySlot;
        }
        else if (ItemClicked != -1)
        {
            int ItemClickedNow = 10;
            if (Slots[ItemClickedNow].sprite == EmptySlot)
            {
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = EmptySlot;

                ItemClicked = -1;
                timesClicked = 0;
            }
            else
            {
                timesClicked++;
                Img.sprite = Slots[ItemClickedNow].sprite;
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = Img.sprite;

                ItemClicked = ItemClickedNow;
            }
        }
    }

    public void Slot12_button()
    {
        if (ItemClicked == -1 && Slots[11].sprite != EmptySlot)
        {
            ItemClicked = 11;
            imageToFollow.sprite = Slots[ItemClicked].sprite;
            OldImg.sprite = Slots[ItemClicked].sprite;
            Slots[ItemClicked].sprite = EmptySlot;
        }
        else if (ItemClicked != -1)
        {
            int ItemClickedNow = 11;
            if (Slots[ItemClickedNow].sprite == EmptySlot)
            {
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = EmptySlot;

                ItemClicked = -1;
                timesClicked = 0;
            }
            else
            {
                timesClicked++;
                Img.sprite = Slots[ItemClickedNow].sprite;
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = Img.sprite;

                ItemClicked = ItemClickedNow;
            }
        }
    }

    public void HelmetSlot_button()
    {
        if (ItemClicked == -1 && Slots[12].sprite != NoHelmet)
        {
            ItemClicked = 12;
            imageToFollow.sprite = Slots[ItemClicked].sprite;
            OldImg.sprite = Slots[ItemClicked].sprite;
            Slots[ItemClicked].sprite = NoHelmet;
        }
        else if (ItemClicked != -1 && IsHelmet(imageToFollow.sprite))
        {
            int ItemClickedNow = 12;
            if (Slots[ItemClickedNow].sprite == NoHelmet)
            {
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = EmptySlot;

                ItemClicked = -1;
                timesClicked = 0;
            }
            else
            {
                timesClicked++;
                Img.sprite = Slots[ItemClickedNow].sprite;
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = Img.sprite;

                ItemClicked = ItemClickedNow;
            }
        }
    }

    public void ChestPlateSlot_button()
    {
        if (ItemClicked == -1 && Slots[13].sprite != NoChestplate)
        {
            ItemClicked = 13;
            imageToFollow.sprite = Slots[ItemClicked].sprite;
            OldImg.sprite = Slots[ItemClicked].sprite;
            Slots[ItemClicked].sprite = NoChestplate;
        }
        else if (ItemClicked != -1 && IsChestPlate(imageToFollow.sprite))
        {
            int ItemClickedNow = 13;
            if (Slots[ItemClickedNow].sprite == NoChestplate)
            {
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = EmptySlot;

                ItemClicked = -1;
                timesClicked = 0;
            }
            else
            {
                timesClicked++;
                Img.sprite = Slots[ItemClickedNow].sprite;
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = Img.sprite;

                ItemClicked = ItemClickedNow;
            }
        }
    }

    public void BeltSlot_button()
    {
        if (ItemClicked == -1 && Slots[14].sprite != NoBelt)
        {
            ItemClicked = 14;
            imageToFollow.sprite = Slots[ItemClicked].sprite;
            OldImg.sprite = Slots[ItemClicked].sprite;
            Slots[ItemClicked].sprite = NoBelt;
        }
        else if (ItemClicked != -1 && IsBelt(imageToFollow.sprite))
        {
            int ItemClickedNow = 14;
            if (Slots[ItemClickedNow].sprite == NoBelt)
            {
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = EmptySlot;

                ItemClicked = -1;
                timesClicked = 0;
            }
            else
            {
                timesClicked++;
                Img.sprite = Slots[ItemClickedNow].sprite;
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = Img.sprite;

                ItemClicked = ItemClickedNow;
            }
        }
    }

    public void BootsSlot_button()
    {
        if (ItemClicked == -1 && Slots[15].sprite != NoBoots)
        {
            ItemClicked = 15;
            imageToFollow.sprite = Slots[ItemClicked].sprite;
            OldImg.sprite = Slots[ItemClicked].sprite;
            Slots[ItemClicked].sprite = NoBoots;
        }
        else if (ItemClicked != -1 && IsBoots(imageToFollow.sprite))
        {
            int ItemClickedNow = 15;
            if (Slots[ItemClickedNow].sprite == NoBoots)
            {
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = EmptySlot;

                ItemClicked = -1;
                timesClicked = 0;
            }
            else
            {
                timesClicked++;
                Img.sprite = Slots[ItemClickedNow].sprite;
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = Img.sprite;

                ItemClicked = ItemClickedNow;
            }
        }
    }

    public void WeaponSlot_button()
    {
        if (ItemClicked == -1 && Slots[16].sprite != NoWeapon)
        {
            ItemClicked = 16;
            imageToFollow.sprite = Slots[ItemClicked].sprite;
            OldImg.sprite = Slots[ItemClicked].sprite;
            Slots[ItemClicked].sprite = NoWeapon;
        }
        else if (ItemClicked != -1 && IsWeapon(imageToFollow.sprite))
        {
            int ItemClickedNow = 16;
            if (Slots[ItemClickedNow].sprite == NoWeapon)
            {
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = EmptySlot;

                ItemClicked = -1;
                timesClicked = 0;
            }
            else
            {
                timesClicked++;
                Img.sprite = Slots[ItemClickedNow].sprite;
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = Img.sprite;

                ItemClicked = ItemClickedNow;
            }
        }
    }

    public void RingSlot_button()
    {
        if (ItemClicked == -1 && Slots[17].sprite != NoRing)
        {
            ItemClicked = 17;
            imageToFollow.sprite = Slots[ItemClicked].sprite;
            OldImg.sprite = Slots[ItemClicked].sprite;
            Slots[ItemClicked].sprite = NoRing;
        }
        else if (ItemClicked != -1 && IsRing(imageToFollow.sprite))
        {
            int ItemClickedNow = 17;
            if (Slots[ItemClickedNow].sprite == NoRing)
            {
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = EmptySlot;

                ItemClicked = -1;
                timesClicked = 0;
            }
            else
            {
                timesClicked++;
                Img.sprite = Slots[ItemClickedNow].sprite;
                Slots[ItemClickedNow].sprite = imageToFollow.sprite;

                imageToFollow.sprite = Img.sprite;

                ItemClicked = ItemClickedNow;
            }
        }
    }



    bool IsHelmet(Sprite sprite)
    {
        if (sprite == HelmetLevel1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool IsChestPlate(Sprite sprite)
    {
        if (sprite == ChestLevel1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool IsBelt(Sprite sprite)
    {
        return false;
    }

    bool IsBoots(Sprite sprite)
    {
        return false;
    }

    bool IsWeapon(Sprite sprite)
    {
        return false;
    }

    bool IsRing(Sprite sprite)
    {
        return false;
    }

    #endregion  
}
