using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour{
	public List<Block> Blocks = new List<Block>();

    public float diffX = 3;
    public float diffY = 2.6f;

    public Block BlockLine;
    public Block Block120;
    public Block Block60;
    public Block BlockEnd;

    private void Start()
    {
        List<string> Datum = BlockDatum();
        Datum.ForEach(data => InstantiateBlock(data));
    }

    public static List<string> BlockDatum()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("TileData");
        string[] rowDataList = textAsset.text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        return new List<string>(rowDataList);
    }

    void InstantiateBlock(string BlockData)
    {
        StringParser parser = new StringParser(BlockData, '\t');
        string BlockType = parser.Consume();
        float posX = parser.ConsumeFloat();
        float posY = parser.ConsumeFloat();

        Block Item;

        if (BlockType == "Lin")
        {
            Item = Instantiate(BlockLine);
        }else if(BlockType == "60")
        {
            Item = Instantiate(Block60);
        }else if(BlockType == "120")
        {
            Item = Instantiate(Block120);
        }else if(BlockType == "End")
        {
            Item = Instantiate(BlockEnd);
        }
        else
        {
            Item = null;
            Debug.LogError("Invalid BlockType");
        }

        Item.Manager = this;
        Item.positionX = posX;
        Item.positionY = posY;
        Item.transform.position = new Vector3(posX * diffX, posY * diffY, -1);
        Blocks.Add(Item);
    }

    public Block NeighborBlock(Block origin, int dir)
    {
        if (dir == 0)
        {
            return FindWithPosition(origin.positionX + 1, origin.positionY);
        } else if(dir == 1)
        {
            return FindWithPosition(origin.positionX + 0.5f, origin.positionY + 1);
        }else if(dir == 2)
        {
            return FindWithPosition(origin.positionX - 0.5f, origin.positionY + 1);
        }else if(dir == 3)
        {
            return FindWithPosition(origin.positionX - 1, origin.positionY);
        }else if(dir == 4)
        {
            return FindWithPosition(origin.positionX - 0.5f, origin.positionY-1);
        }else if(dir == 5)
        {
            return FindWithPosition(origin.positionX + 0.5f, origin.positionY - 1);
        }
        else
        {
            Debug.LogError("Invalid Direction Number");
            return null;
        }
    }

	public Block FindWithPosition(float x, float y){
		return Blocks.Find(item => item.positionX == x && item.positionY == y);
	}
}