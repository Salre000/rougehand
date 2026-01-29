using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ResultUIManager : MonoBehaviour
{
    public static ResultUIManager Instance { get; private set; }
    [SerializeField, Header("勝利か敗北かのもじ")] TextMeshProUGUI[] ResultAnswer = new TextMeshProUGUI[3];
    [SerializeField, Header("一ラウンドの最高スコア")] TextMeshProUGUI highScoreText;
    [SerializeField, Header("一番プレイした役")] TextMeshProUGUI highRoleText;
    [SerializeField, Header("プレイしたカードの枚数")] TextMeshProUGUI playCardCountText;
    [SerializeField, Header("ディスカードしたカードの枚数")] TextMeshProUGUI discardCardCountText;
    [SerializeField, Header("購入したカードの枚数")] TextMeshProUGUI buyCardCountText;
    [SerializeField, Header("リロールした回数")] TextMeshProUGUI reroolCountText;
    [SerializeField, Header("新しく見つけた")] TextMeshProUGUI newDiscoveryCountText;
    [SerializeField, Header("シード値")] TextMeshProUGUI seedText;
    [SerializeField, Header("アンティの値")] TextMeshProUGUI anteText;
    [SerializeField, Header("ラウンドの値")] TextMeshProUGUI roundText;
    [SerializeField, Header("シード値をコピーするボタン")] Button seedCopy;
    [SerializeField, Header("エンドレスモードを起動するボタン")] Button endless;
    [SerializeField, Header("新しいラン")] Button newRun;
    [SerializeField, Header("メインメニュー")] Button mainMene;

    [SerializeField, Header("ショップから出たときのボタン")] Button shopEndButton;
    public bool resultFlag { private set; get; }

    public void Awake()
    {
        Instance = this;

        gameObject.SetActive(false);
        resultFlag = false;

        newRun.onClick.AddListener(ReLoodScene);
        mainMene.onClick.AddListener(TitelScene);
        endless.onClick.AddListener(StartEndless);

    }
    public void Update()
    {
        


    }


    public void Active(string text = "")
    {
        MemoryManager.Keep();

        gameObject.SetActive(true);
        resultFlag = true;

        Memory resultMemory = MemoryManager.instantMemory;
        SetResultAnswer(text);
        BestHand();
        RoleCount();
        PlayCardCount();
        DiscardCardCount();
        BuyCardCount();
        ReroolCount();
        AnteCount();
        RoundCount();
        SeedCount();
    }

    private void SetResultAnswer(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            if (i > ResultAnswer.Length) return;

            ResultAnswer[i].text = text[i].ToString();

        }




    }

    private void BestHand()
    {
        Memory resultMemory = MemoryManager.instantMemory;
        highScoreText.text = resultMemory._highScore.ToString();

    }
    private readonly int COLORID = 7;

    private void RoleCount()
    {
        int index = 0;
        int value = 0;
        Memory resultMemory = MemoryManager.instantMemory;
        for (int i = 0; i < resultMemory._roleCount.Count; i++)
        {
            if (value > resultMemory._roleCount[i]) continue;
            value = resultMemory._roleCount[i];
            index = i;
        }
        StringBuilder sb = new StringBuilder();
        sb.Clear();
        sb.Append(MasterData.instance.GetStringMaster(index + IDUtility.ROLE_ID, true));
        sb.Append(MasterData.instance.GetStringMaster(IDUtility.RICHTEXT_ID));
        sb.Append(MasterData.instance.GetStringMaster(IDUtility.RICHTEXT_ID + COLORID));
        sb.Append("[");
        sb.Append(resultMemory._roleCount[index].ToString());
        sb.Append("]");

        highRoleText.text = sb.ToString();
    }

    private void PlayCardCount()
    {
        Memory resultMemory = MemoryManager.instantMemory;
        playCardCountText.text = resultMemory._playCardCount.ToString();
    }

    private void DiscardCardCount()
    {
        Memory resultMemory = MemoryManager.instantMemory;
        discardCardCountText.text = resultMemory._discardCardCount.ToString();
    }

    private void BuyCardCount()
    {
        Memory resultMemory = MemoryManager.instantMemory;
        buyCardCountText.text = resultMemory._buyCardCount.ToString();
    }
    private void ReroolCount()
    {
        Memory resultMemory = MemoryManager.instantMemory;
        reroolCountText.text = resultMemory._reroolCount.ToString();
    }
    private void AnteCount()
    {
        Memory resultMemory = MemoryManager.instantMemory;
        anteText.text = resultMemory._ante.ToString();
    }
    private void RoundCount()
    {
        Memory resultMemory = MemoryManager.instantMemory;
        roundText.text = resultMemory._round.ToString();
    }

    private void SeedCount()
    {
        Memory resultMemory = MemoryManager.instantMemory;
        seedText.text = resultMemory.theSeed.ToString();
    }

    private void ReLoodScene() 
    {
        GameSceneManager.LoadScene(GameSceneManager.mainScene);
    }
    private void TitelScene() 
    {
        GameSceneManager.LoadScene(GameSceneManager.titleScene);
    }

    private void StartEndless() 
    {
        gameObject.SetActive(false);
        resultFlag = false;

        shopEndButton.onClick.AddListener(() => 
        {
            int baseScore = MasterData.instance.GetIntMaster(IDUtility.TARGET_SCORE_ID + GameUtility.GetAllRoundCount());
            
            float reta=1+(Random.Range(0,5)/10f);


            MasterData.instance.AddStringMaster(IDUtility.TARGET_SCORE_ID + GameUtility.GetAllRoundCount()+1,
                ((int)(baseScore * reta)).ToString());


        });

        int baseScore = MasterData.instance.GetIntMaster(IDUtility.TARGET_SCORE_ID + GameUtility.GetAllRoundCount());

        float reta = 1 + (Random.Range(0, 5) / 10f);

        MasterData.instance.AddStringMaster(IDUtility.TARGET_SCORE_ID + GameUtility.GetAllRoundCount() + 1,
            ((int)(baseScore * reta)).ToString());







    }

}
