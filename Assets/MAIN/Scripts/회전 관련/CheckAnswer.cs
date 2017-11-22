using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CheckAnswer : MonoBehaviour {

    public GameObject[] DropBlocks = new GameObject[1];
    public string Filename = "Ans_CH.bin";
    public int NumberOfAnswerToView = -1;
    public bool SaveChanges = false;
    private int[,] AngleAnswer;
    private bool FirstTime = true;
    private bool Modified = false;
    private int ModAnsNum = -1;

    void Start()
    {
        SaveChanges = false;
        int retval = ReadAnsFile();
        if (retval == 1) return;

        if (DontDestroyOnLoadManager.Objects.Count != 0)
        {
            DontDestroyOnLoadManager.DestroyAll();
        }
    }

	void Update() {

        if (Modified == true)
        {
            int retval = ReadAnsFile();
            if (retval == 1) return;
        }

        if (NumberOfAnswerToView >= 0 && NumberOfAnswerToView < AngleAnswer.GetLength(0))
        {
            if(ModAnsNum != NumberOfAnswerToView)
            {
                FirstTime = true;
            }

            if (FirstTime == true)
            {
                FirstTime = false;
                SaveChanges = false;
                ModAnsNum = NumberOfAnswerToView;

                
                if(Modified != true) {
                    print(ModAnsNum + "번 정답을 로드했어!!");
                }

                Modified = false;

                for (int BlockNum = 0; BlockNum < AngleAnswer.GetLength(1); BlockNum++)
                {
                    if (AngleAnswer[NumberOfAnswerToView, BlockNum] == -1)
                    {
                        DropBlocks[BlockNum].GetComponent<RotateAnim>().EffectToAnswer = false;
                    }
                    else
                    {
                        DropBlocks[BlockNum].GetComponent<RotateAnim>().EffectToAnswer = true;
                        if (DropBlocks[BlockNum].GetComponent<RotateAnim>().FixedBlock == false) {
                            DropBlocks[BlockNum].GetComponent<RotateAnim>().state = AngleAnswer[NumberOfAnswerToView, BlockNum];
                            DropBlocks[BlockNum].transform.rotation = Quaternion.Euler(0, 0, AngleAnswer[NumberOfAnswerToView, BlockNum]);
                        }
                    }
                }
            }
            else
            {
                if (SaveChanges == true)
                {
                    int NoEffectNum = 0;
                    FirstTime = true;
                    SaveChanges = false;
                    string[] Storage = new string[AngleAnswer.GetLength(0) + 3];

                    using (StreamReader reader = new StreamReader(Filename)) {
                        int idx = 0;
                        while((Storage[idx] = reader.ReadLine()) != null)
                        {
                            if (idx == ModAnsNum + 2)
                            {
                                Storage[idx] = "";
                                RotateAnim[] Anim = new RotateAnim[DropBlocks.Length];
                                for (int i = 0; i < DropBlocks.Length; i++)
                                {
                                    Anim[i] = DropBlocks[i].GetComponent<RotateAnim>();
                                    if (Anim[i].EffectToAnswer == false)
                                    {
                                        Storage[idx] += -1 + "|";
                                        NoEffectNum++;
                                    }
                                    else
                                    {
                                        Storage[idx] += (int)Anim[i].state + "|";
                                    }
                                }
                            }
                            idx++;
                        }
                    }

                    string AllCombined = Storage[0];

                    if(NoEffectNum == DropBlocks.Length)
                    {
                        for(int n = ModAnsNum + 3; n < AngleAnswer.GetLength(0) + 1; n++)
                        {
                            Storage[n] = Storage[n + 1];
                        }
                        for (int i = 1; i < Storage.Length - 2; i++)
                        {
                            AllCombined += System.Environment.NewLine;
                            AllCombined += Storage[i];
                        }
                        print("모든 블럭의 Effect To Answer값이 false여서 기록 자체를 삭제할게~");
                    }
                    else
                    {
                        for (int i = 1; i < Storage.Length - 1; i++)
                        {
                            AllCombined += System.Environment.NewLine;
                            AllCombined += Storage[i];
                        }
                        print("이걸 정답으로 바꿨어!! " + NumberOfAnswerToView + "번 정답을 다시 로딩할게!!");
                    }
                                        
                    using (StreamWriter outputFile = new StreamWriter(Filename))
                    {
                        outputFile.WriteLine(AllCombined);
                    }
                    Modified = true;
                }
            }
        }
        else
        {
            FirstTime = true;
            SaveChanges = false;
            Debug.LogError("FATAL ERROR : " + NumberOfAnswerToView + "번 정답은 존재하지 않아!!");
        }
	}

    private int ReadAnsFile()
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
                    return 1;
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
                        return 1;
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
                                Debug.LogError("FATAL ERROR : \"" + Filename + "\"가 이상해!!");
                                return 1;
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
                            Debug.LogError("FATAL ERROR : \"" + Filename + "\"가 이상해!!");
                        }
                    }
                }
            }
        }
        else
        {
            Debug.LogError("FATAL ERROR : \"" + Filename + "\"가 존재하지 않아!!");
            return 1;
        }
        return 0;
    }
}
