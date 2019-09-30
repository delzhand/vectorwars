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
        transform.Find("Image").GetComponent<Image>().sprite = getSprite();
        Text t = transform.Find("Bottom Text").GetComponent<Text>();
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

    // Update is called once per frame
    void Update()
    {
        
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
        g.GetComponent<VectorTile>().VLocal = v;
        g.transform.SetParent(parent, false);
        return g;
    }

    public void Click()
    {
        // Cases not handled:
        // Something not in slot but with assigned slot selected, clicked in slot

        VectorSlot open = null;
        VectorSlot thisAssignedSlot = null;
        foreach (VectorSlot vs in FindObjectsOfType<VectorSlot>())
        {
            open = vs.IsOpen() ? vs : open;
            if (vs.SlotTile && vs.SlotTile.VLocal.Acquired == this.VLocal.Acquired)
            {
                thisAssignedSlot = vs;
            }
        }
        VectorSlot selectionInSlot = selected ? selected.InSlot() : null;

        if (selected == null && open && thisAssignedSlot == null)
        {
            Console.Log("Nothing selected, " + open.name + " open, no assigned slot");
            open.Assign(this);
        }
        else if (selected == null && open && thisAssignedSlot)
        {
            Console.Log("Nothing selected, " + open.name + " open, already assigned slot");
            Select();
        }
        else if (selected == null && !open)
        {
            Console.Log("Nothing selected, no slots open");
            Select();
        }
        else if (selected == this)
        {
            Console.Log("Something selected, is this");
            Deselect();
        }
        else if (selectionInSlot && thisAssignedSlot)
        {
            Console.Log("Something in slot selected, clicked in slot");
            VectorTile temp = selectionInSlot.SlotTile;
            selectionInSlot.Assign(this);
            thisAssignedSlot.Assign(temp);
            Deselect();
        }
        else if (!selectionInSlot && !this.InSlot())
        {
            Console.Log("Something not in slot selected, clicked not in slot");
            Select();
        }
        else if (selected != null && selected != this && !selectionInSlot)
        {
            Console.Log("Something selected, not this, not in slot");
            Deselect();
        }
        else if (selected != null && selected != this && selectionInSlot)
        {
            Console.Log("Something selected, not this, in slot");
            selectionInSlot.Assign(this);
            Deselect();
        }
    }

    public VectorSlot InSlot()
    {
        return transform.parent.GetComponent<VectorSlot>();
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
