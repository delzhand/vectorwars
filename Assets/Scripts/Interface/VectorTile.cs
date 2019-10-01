using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public enum VectorTileDisplayMode
{
    name,
    level,
    rank
}

public class VectorTile : MonoBehaviour
{
    public static VectorTile selected;

    public VectorLocal VLocal;
    public VectorTileDisplayMode mode = VectorTileDisplayMode.name;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Populate(VectorLocal v)
    {
        VLocal = v;
        Image i = transform.Find("Image").GetComponent<Image>();
        i.sprite = getSprite();
        i.enabled = true;
        Text t = transform.Find("Bottom Text").GetComponent<Text>();
        t.enabled = true;
        switch (mode)
        {
            case VectorTileDisplayMode.name:
                t.text = DataManager.CoreLibrary[VLocal.Core].name;
                break;
            case VectorTileDisplayMode.level:
                t.text = "Lv " + VLocal.Level;
                break;
            case VectorTileDisplayMode.rank:
                t.text = "Rank " + VLocal.Rank;
                break;
        }
    }

    public void Depopulate()
    {
        transform.Find("Image").GetComponent<Image>().enabled = false;
        transform.Find("Bottom Text").GetComponent<Text>().enabled = false;
        VLocal = null;
    }

    private Sprite getSprite()
    {
        string path = Path.Combine(Application.persistentDataPath, "GameData/Sprites/" + DataManager.CoreLibrary[VLocal.Core].name + "_sprite.png");
        if (File.Exists(path) == true)
        {
            byte[] bytes = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(4, 4, TextureFormat.RGBA32, false);
            texture.filterMode = FilterMode.Point;
            texture.LoadImage(bytes);
            Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 32);
            return sp;
        }
        return null;
    }

    public static GameObject Create(VectorLocal v, Vector2 position, Transform parent)
    {
        GameObject g = (GameObject)Instantiate(Resources.Load("Interface/Vector Tile"), new Vector3(position.x, position.y, 0), Quaternion.identity);
        g.GetComponent<VectorTile>().Populate(v);
        g.transform.SetParent(parent, false);
        return g;
    }

    public void Click()
    {
        // Variables:
        // Is there a tile currently selected?
        bool selectionExists = selected;
        // Is the tile that's selected a slot?
        bool selectionIsSlot = selectionExists && selected.GetComponent<VectorSlot>();
        // Is the vectorLocal in the selection in a slot?
        bool selectionVLocalAssignedToSlot = selectionExists && VectorSlot.isVectorAssignedAnySlot(selected.VLocal);
        // Are any slots open?
        bool anySlotsOpen = VectorSlot.AnySlotsOpen();
        // Is the tile that's clicked a slot?
        bool clickedIsSlot = GetComponent<VectorSlot>();
        // Is the vectorLocal in the tile that's clicked in a slot?
        bool clickedVLocalAssignedToSlot = VectorSlot.isVectorAssignedAnySlot(this.VLocal);
        // Is the vectorLocal in the tile that's clicked a match for the selection vectorLocal?
        bool clickedVLocalMatchesSelectionVLocal = selectionExists && (this.VLocal == selected.VLocal);

        if (!selectionExists)
        {
            if (!clickedIsSlot)
            {
                if (anySlotsOpen && !clickedVLocalAssignedToSlot)
                {
                    VectorSlot.NextSlotOpen(true).GetComponent<VectorTile>().Populate(this.VLocal);
                    return;
                }
                else
                {
                    Select();
                    return;
                }
            }
            else
            {
                Select();
                return;
            }
        }
        else
        {
            if (clickedVLocalMatchesSelectionVLocal)
            {
                Deselect();
                return;
            }
            else
            {
                if (selectionIsSlot)
                {
                    if (clickedIsSlot)
                    {
                        Select();
                        return;
                    }
                    else
                    {
                        if (clickedVLocalAssignedToSlot)
                        {
                            VectorTile tileA = VectorSlot.whatSlotIsVectorAssigned(this.VLocal).GetComponent<VectorTile>();
                            VectorTile tileB = VectorSlot.whatSlotIsVectorAssigned(selected.VLocal).GetComponent<VectorTile>(); ;
                            VectorLocal tmp = tileA.VLocal;
                            tileA.Populate(tileB.VLocal);
                            tileB.Populate(tmp);
                            Deselect();
                            return;
                        }
                        else
                        {
                            selected.Populate(this.VLocal);
                            Deselect();
                            return;
                        }
                    }
                }
                else
                {
                    if (clickedIsSlot)
                    {
                        if (selectionVLocalAssignedToSlot)
                        {
                            VectorTile tileA = VectorSlot.whatSlotIsVectorAssigned(this.VLocal).GetComponent<VectorTile>();
                            VectorTile tileB = VectorSlot.whatSlotIsVectorAssigned(selected.VLocal).GetComponent<VectorTile>(); ;
                            VectorLocal tmp = tileA.VLocal;
                            tileA.Populate(tileB.VLocal);
                            tileB.Populate(tmp);
                            Deselect();
                            return;
                        }
                        else
                        {
                            VectorSlot.whatSlotIsVectorAssigned(this.VLocal).GetComponent<VectorTile>().Populate(selected.VLocal);
                            Deselect();
                            return;
                        }
                    }
                    else
                    {
                        Select();
                        return;
                    }
                }
            }
        }
    }



    public void Select()
    {
        if (selected == null)
        {
            selected = this;
            selected.transform.Find("Selected Border").GetComponent<Image>().enabled = true;
        }
        else if (selected == this)
        {
            selected.transform.Find("Selected Border").GetComponent<Image>().enabled = false;
            selected = null;
        }
        else
        {
            selected.transform.Find("Selected Border").GetComponent<Image>().enabled = false;
            selected = this;
            selected.transform.Find("Selected Border").GetComponent<Image>().enabled = true;
        }
    }

    public void Deselect()
    {
        selected.transform.Find("Selected Border").GetComponent<Image>().enabled = false;
        selected = null;
    }
}
