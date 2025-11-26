using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RunDetailsManager : MonoBehaviour
{
    public enum RunDetailsType
    {
        none = -1,
        allDeck,
        role,
        buff,
        blind,
        max
    }

    public static RunDetailsManager instance;

    /// <summary>
    /// 現在のタイプの状態
    /// </summary>
    [SerializeField] private RunDetailsType _nowDetailsType = RunDetailsType.role;

    /// <summary>
    /// 詳細UIの基礎にある背景のRectTransform
    /// </summary>
    [SerializeField] private RectTransform _backImageRectTransform;

    /// <summary>
    /// ホライゾングループの付いているボタンをまとめた変数
    /// </summary>
    [SerializeField]private List<Button> _runDetailsTypeButtons=new List<Button>((int)RunDetailsType.max);

    /// <summary>
    /// 描画可能なオブジェクトをまとめた配列
    /// 最大数はタイプの種類
    /// </summary>
    [SerializeField] private List<Transform> _runDetailsTypeParents = new List<Transform>((int)RunDetailsType.max);

    /// <summary>
    /// 詳細な情報のUIを描画する関数を持たせるボタン
    /// </summary>
    [SerializeField, Header("このボタンはプレハブ内ではなくシーン上に存在している")]private Button _runInfo;

    /// <summary>
    /// 詳細情報のUIを閉じる関数を持たせるボタン
    /// </summary>
    [SerializeField]private Button _endrRunInfo;


    /// <summary>
    /// 内容ごとのUIの描画を行う関数
    /// </summary>
    private List<DetailsBase> _detailsTypeAction=new List<DetailsBase>((int)RunDetailsType.max);

    /// <summary>
    /// ランの詳細が見えるかどうかのフラグ
    /// </summary>
    private bool IsRunDetailsOpen = false;  

    #region 定数


    /// <summary>
    /// デフォルト時のボタンの横幅
    /// </summary>
    readonly private Vector2 _typeButtonDefaultSizeX = new Vector2(200, 100);
    /// <summary>
    /// デッキ一覧時のボタンの横幅
    /// </summary>
    readonly private Vector2 _typeButtonDeckSizeX = new Vector2(350, 100);

    /// <summary>
    /// デフォルト時のUIの横幅
    /// </summary>
    readonly private float _backImageDefaultSizeX = 920;
    /// <summary>
    /// デッキ一覧時のUIの横幅
    /// </summary>
    readonly private float _backImageDeckSizeX = 1520;

    #endregion

    public void Start()
    {
        Initializ();
    }

    // 初期化関数
    private void Initializ()
    {
        // 詳細UIを開くボダンに開く関数をわたす
        _runInfo.onClick.AddListener(Show);
        // 詳細UIを閉じるボダンに閉じる関数をわたす
        _endrRunInfo.onClick.AddListener(End);

        // 内容ごとのアクションを保存
        SetShows();
        // 描画内容を変える関数をわたす関数
        SetChengeType();

        // もしも現在のタイプが不正値だったらロールで上書きする
        if (_nowDetailsType == RunDetailsType.none) ChengeType(RunDetailsType.role);


        // 非アクティブ状態に移行
        End();
        instance = this;

    }

    /// <summary>
    ///  描画を開始する関数
    /// </summary>
    private void Show()
    {
        IsRunDetailsOpen = true;

        // アクティブ状態に移行
        gameObject.SetActive(true);


        // 現在選択中のタイプのオブジェクトだけアクティブ状態に移行
        _runDetailsTypeParents[(int)_nowDetailsType].gameObject.SetActive(true);

        _detailsTypeAction[(int)_nowDetailsType].Show();


    }
    /// <summary>
    ///  描画を終了する関数
    /// </summary>
    private void End()
    {
        IsRunDetailsOpen = false;
        // すべての要素のオブジェクトを非アクティブ状態に移行
        for (int i=0;i< _runDetailsTypeParents.Count; i++) 
        {
            _detailsTypeAction[i].Hide();
            _runDetailsTypeParents[i].gameObject.SetActive(false);
        }

        // 非アクティブ状態に移行
        gameObject.SetActive(false);
    }

    /// <summary>
    /// ボタンにChengeType関数を持たせるボタン
    /// </summary>
    private void SetChengeType()
    {
        // 子供の数だけ回す
        for (int i = 0; i < _runDetailsTypeButtons.Count; i++)
        {
            // キャッシュする必要有
            int dommyNumber = i;

            Button button = _runDetailsTypeButtons[i];

            if (button == null) continue;

            // ラムダ式  
            button.onClick.AddListener(() =>
            {
                ChengeType((RunDetailsType)dommyNumber);

            });
        }

    }

    /// <summary>
    /// 描画内容を変更する関数
    /// </summary>
    private void ChengeType(RunDetailsType type)
    {
        if (_nowDetailsType == type) return;

        // 前回選択中のタイプのオブジェクトだけ非アクティブ状態に移行
        // 例外として前回の選択がnoneだったら切り替えを行わない
        if(_nowDetailsType!=RunDetailsType.none) _runDetailsTypeParents[(int)_nowDetailsType].gameObject.SetActive(false);

         // 選択中を変更
        _nowDetailsType = type;

        // 現在選択中のタイプのオブジェクトだけアクティブ状態に移行
        _runDetailsTypeParents[(int)_nowDetailsType].gameObject.SetActive(true);



        Vector2 backImageSize = _backImageRectTransform.sizeDelta;

        Vector2 buttonSize = Vector2.zero;

        if (_nowDetailsType == RunDetailsType.allDeck)
        {
            backImageSize.x = _backImageDeckSizeX;
            buttonSize = _typeButtonDeckSizeX;
        }
        else
        {
            backImageSize.x = _backImageDefaultSizeX;

            buttonSize = _typeButtonDefaultSizeX;

        }

        // 背景の大きさを設定
        _backImageRectTransform.sizeDelta = backImageSize;

        for(int i=0;i< _runDetailsTypeButtons.Count; i++)
        {
            _detailsTypeAction[i].Hide();

            _runDetailsTypeButtons[i].GetComponent<RectTransform>().sizeDelta = buttonSize;
        }

        // 内容を描画
        _detailsTypeAction[(int)_nowDetailsType].Show();
    }


    /// <summary>
    ///  内容ごとの関数を代入する
    /// </summary>
    private void SetShows() 
    {
        for (int i = 0; i < _runDetailsTypeParents.Count; i++) 
        {
            _detailsTypeAction.Add( _runDetailsTypeParents[i].GetComponent<DetailsBase>());
            _detailsTypeAction[i].Initializ();

        }
    }

    public bool IsOpen() { return IsRunDetailsOpen; }
}
