using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreMaster : MonoBehaviour
{
    private Dictionary<int, score> scoreMap = new Dictionary<int, score>();

    [SerializeField] TextAsset scoreMaster;

    //instance
    public static ScoreMaster instance;
    public void Awake()
    {
        instance = this;
        Lood();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Lood()
    {

        //読み込んだCSVファイルを格納
        List<string[]> csvDatas = new List<string[]>();

        //CSVファイルの行数を格納
        int height = 0;


        //繋げたファイルパスを使いファイルのロードを行う
        TextAsset textAsset = scoreMaster;

        //読み込んだテキストをString型にして格納
        StringReader reader = new StringReader(textAsset.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            // ,で区切ってCSVに格納
            csvDatas.Add(line.Split(','));
            height++; // 行数加算
        }

        for (int j = 0; j < csvDatas.Count; j++)
        {
            score _score = new score();

            _score.BasicScore = int.Parse(csvDatas[j][2]);
            _score.BasicMagnification = int.Parse(csvDatas[j][3]);
            _score.AddBasicScore = int.Parse(csvDatas[j][4]);
            _score.AddBasicMagnification = int.Parse(csvDatas[j][5]);

            scoreMap.Add(int.Parse(csvDatas[j][0]), _score);

        }
    }

    public int GetBasicScore(int Key) { score score;   scoreMap.TryGetValue(Key, out score); return score.BasicScore; }
    public int GetBasicMagnification(int Key) { score score; scoreMap.TryGetValue(Key, out score); return score.BasicMagnification; }
    public int GetAddBasicScore(int Key) { score score; scoreMap.TryGetValue(Key, out score); return score.AddBasicScore; }
    public int GetAddBasicMagnification(int Key) { score score; scoreMap.TryGetValue(Key, out score); return score.AddBasicMagnification; }

    public score GetScore(int key) {return scoreMap[key]; }
    public void SetScore(int key,score score) {scoreMap[key]=score; }


    public class score
    {
        public int BasicScore = -1;
        public int BasicMagnification = -1;
        public int AddBasicScore = -1;
        public int AddBasicMagnification = -1;
    }
}
