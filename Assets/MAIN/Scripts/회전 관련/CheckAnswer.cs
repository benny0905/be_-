using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CheckAnswer : MonoBehaviour {

    public GameObject[] DropBlocks = new GameObject[1];
    public string Filename = "Ans_CH.bin";
    private int[,] AngleAnswer;
    public int NumberOfAnswerToView = 100;

    void Start()
    {
        if (File.Exists(Filename))
        {
            using (StreamReader reader = new StreamReader(Filename))
            {
                int sizeofblocks = 0;
                string DataString = reader.ReadLine();
                for (int i = 0; i < DataString.Length; i++)
                {
                    if (i != 0) sizeofblocks *= 10;
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

                for (int idx = 0; idx < DropBlocks.Length; idx++)
                {
                    if (DropBlocks[idx].name != WrittenName[idx])
                    {
                        Debug.LogError("FATAL ERROR : 육각블럭들이 제대로 세팅되지 않았어!!");
                        return;
                    }
                }
                for (int lineidx = 0; ; lineidx++)
                {
                    string AngleData = reader.ReadLine();

                    if (AngleData == null) break;

                    if (lineidx == 0)
                    {
                        AngleAnswer = new int[1, DropBlocks.Length];
                        for (int init = 0; init < DropBlocks.Length; init++)
                        {
                            AngleAnswer[0, init] = 0;
                        }
                    }
                    else
                    {
                        int[,] Temp = new int[lineidx, DropBlocks.Length];
                        for (int templineidx = 0; templineidx < lineidx; templineidx++)
                        {
                            for (int tempidx = 0; tempidx < DropBlocks.Length; tempidx++)
                            {
                                Temp[templineidx, tempidx] = AngleAnswer[templineidx, tempidx];
                            }
                        }
                        AngleAnswer = new int[lineidx + 1, DropBlocks.Length];
                        for (int templineidx = 0; templineidx < lineidx; templineidx++)
                        {
                            for (int tempidx = 0; tempidx < DropBlocks.Length; tempidx++)
                            {
                                AngleAnswer[templineidx, tempidx] = Temp[templineidx, tempidx];
                            }
                        }
                        for (int init = 0; init < DropBlocks.Length; init++)
                        {
                            AngleAnswer[lineidx, init] = 0;
                        }
                    }

                    int Ansidx = 0;
                    bool minus = false;
                    for (int idx = 0; idx < AngleData.Length; idx++)
                    {
                        if (AngleData[idx] == '-')
                        {
                            if (minus == true)
                            {
                                Debug.LogError("FATAL ERROR : \"" + Filename + "\"가 이상합니다!!");
                                return;
                            }
                            else minus = true;
                        }
                        else if (AngleData[idx] == '|')
                        {
                            Ansidx++;
                            minus = false;
                        }
                        else if (AngleData[idx] >= '0' && AngleData[idx] <= '9')
                        {
                            int letter = AngleData[idx] - '0';
                            if (letter != 0 || AngleAnswer[lineidx, Ansidx] != 0)
                            {
                                AngleAnswer[lineidx, Ansidx] *= 10;
                                AngleAnswer[lineidx, Ansidx] += letter;
                                if (minus == true) AngleAnswer[lineidx, Ansidx] = -AngleAnswer[lineidx, Ansidx];
                            }
                        }
                        else
                        {
                            Debug.LogError("FATAL ERROR : \"" + Filename + "\"가 이상합니다!!");
                        }
                    }
                }
            }
        }
        else
        {
            Debug.LogError("FATAL ERROR : \"" + Filename + "\"가 존재하지 않아!!");
            return;
        }
    }
	void Update() {
        if (NumberOfAnswerToView >= 0 && NumberOfAnswerToView < AngleAnswer.GetLength(0))
        {
            for(int BlockNum = 0; BlockNum < AngleAnswer.GetLength(1); BlockNum++)
            {
                if(AngleAnswer[NumberOfAnswerToView, BlockNum] == -1)
                {
                    DropBlocks[BlockNum].GetComponent<SpriteRenderer>().color = Color.gray;
                }
                else
                {
                    DropBlocks[BlockNum].transform.rotation = Quaternion.Euler(0, 0, AngleAnswer[NumberOfAnswerToView, BlockNum]);
                }
            }
        }
        else
        {
            Debug.LogError("FATAL ERROR : " + NumberOfAnswerToView + "번 정답은 존재하지 않아!!");
        }
	}
}
