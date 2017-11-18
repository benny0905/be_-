using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine;

public class BetaTest : MonoBehaviour {
    
    public GameObject[] DropBlocks = new GameObject[1];
    public string Filename = "Ans_CH.bin";
    public bool IsCorrect = false;

    private void Start() {
        IsCorrect = false;
    }

    private void Update() {
        if (IsCorrect == true)
        {
            IsCorrect = false;
            string AngleWriter = "";
            
            if (DropBlocks.Length < 2)
            {
                Debug.LogError("FATAL ERROR : " + DropBlocks.Length + "개의 블럭으로 게임을 진행할 수 없어!!");
                return;
            }

            for (int i = 0; i < DropBlocks.Length; i++)
            {
                if (DropBlocks[i] == null)
                {
                    Debug.LogError("FATAL ERROR : 육각블럭들이 모두 주어지지는 않았어!! 넣고 다시 와!!");
                    return;
                }
            }

            if (File.Exists(Filename))
            {
                using (StreamReader reader = new StreamReader(Filename))
                {
                    int sizeofblocks = 0;
                    string DataString = reader.ReadLine();
                    for (int i = 0; i < DataString.Length; i++)
                    {
                        sizeofblocks *= 10;
                        sizeofblocks += DataString[i] - '0';
                    }
                    if (sizeofblocks != DropBlocks.Length)
                    {
                        Debug.LogError("FATAL ERROR : 육각블럭들의 개수가 맞지 않아!!");
                        return;
                    }

                    int WrittenNameidx = 0;
                    string[] WrittenName = new string[DropBlocks.Length];
                    DataString = reader.ReadLine();

                    for (int idx = 0; idx < DataString.Length; idx++)
                    {
                        if (DataString[idx] == '|') WrittenNameidx++;
                        else
                        {
                            WrittenName[WrittenNameidx] += DataString[idx];
                        }
                    }

                    for(int idx = 0; idx < DropBlocks.Length; idx++)
                    {
                        if(DropBlocks[idx].name != WrittenName[idx])
                        {
                            Debug.LogError("FATAL ERROR : 육각블럭들이 제대로 세팅되지 않았어!!");
                            return;
                        }
                    }
                }
            }
            else
            {
                AngleWriter += DropBlocks.Length + System.Environment.NewLine;
                for(int i = 0; i < DropBlocks.Length; i++)
                {
                    AngleWriter += DropBlocks[i].name + "|";
                }
                AngleWriter += System.Environment.NewLine;
            }

            print("알았어!! 이게 정답이라는 것을 기록해 둘게!!");
            RotateAnim[] Anim = new RotateAnim[DropBlocks.Length];

            for (int i = 0; i < DropBlocks.Length; i++)
            {
                Anim[i] = DropBlocks[i].GetComponent<RotateAnim>();
                if (Anim[i].EffectToAnswer == false)
                {
                    AngleWriter += -1 + "|";
                }
                else
                {
                    AngleWriter += (int)Anim[i].state + "|";
                }
            }

            using (StreamWriter outputFile = new StreamWriter(Filename, true))
            {
                outputFile.WriteLine(AngleWriter);
            }
        }
    }
}
