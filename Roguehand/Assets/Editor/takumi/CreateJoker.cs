using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class CreateJoker : EditorWindow
{
    /// <summary>
    /// クラスの生成位置
    /// </summary>
    private static readonly string _classFilePass = "/Script/takumi/Card/Joker/JokerBody/";




    [MenuItem("Assets/CreateJoker")]
    static void Open()
    {
        
        This = ScriptableObject.CreateInstance<CreateJoker>();
        jolerListObject = Resources.Load<JolerListObject>("takumi/Observer/JolerLists");

        stringList = Resources.Load<StringList>(ObserverJokerBase.filePath2);
        This.Show();

    }

    private static JolerListObject jolerListObject;

    static CreateJoker This;
    static JokerBaseEnum.JokerEnum jokerEnum = JokerBaseEnum.JokerEnum.MAX;
    static StringList stringList;

    static string className = string.Empty;

    static int num1 = 0;
    static int num2 = 0;
    static int num3 = 0;
    static float float1 = 0;

    static JokerActionUseEnum.JokerActionTarget target = JokerActionUseEnum.JokerActionTarget.max;
    static JokerActionUseEnum.Timing timing = JokerActionUseEnum.Timing.max;
    static JokerActionUseEnum.AddType addType = JokerActionUseEnum.AddType.addition;


    /// <Summary>
    /// ウィンドウのパーツを表示します。
    /// </Summary>
    void OnGUI()
    {
        EditorGUILayout.BeginVertical("Box");

        if (GUILayout.Button("生成する"))
        {
            //　ここにボタンを押した時の処理を書きます
            CreateCS();
        }


        EditorGUILayout.Space();

        EditorGUILayout.LabelField("ジョーカーのクラスの名前");
        className = EditorGUILayout.TextField(className);

        EditorGUILayout.Space();

        // どんなジョーカーを生成するかを決定
        jokerEnum = (JokerBaseEnum.JokerEnum)EditorGUILayout.EnumPopup((JokerBaseEnum.JokerEnum)jokerEnum);

        // 現在のジョーカーの仕様を見えるように変更
        string jokerEX = jokerEnum == JokerBaseEnum.JokerEnum.MAX ? "" : stringList._expansion[(int)jokerEnum];
        EditorGUILayout.LabelField(jokerEX + ":" + "現在のジョーカーの仕様");
        //EditorGUILayout.LabelField(jokerEX);

        EditorGUILayout.Space();

        SwitchJoker(jokerEnum);
        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// ジョーカーの種類ごとに必要な情報が違うからそれをうめていく
    /// </summary>
    /// <param name="jokerEnum"></param>
    static void SwitchJoker(JokerBaseEnum.JokerEnum jokerEnum)
    {
        string answer = string.Empty;
        switch (jokerEnum)
        {
            case JokerBaseEnum.JokerEnum._ProbabilityDestruction:


                EditorGUILayout.LabelField("確立の分子");
                num2 = EditorGUILayout.IntField(num2);

                EditorGUILayout.Space();

                EditorGUILayout.LabelField("確立の分母");
                num1 = EditorGUILayout.IntField(num1);
                if (num1 < num2) num2 = num1;

                answer = num1.ToString() + "分の" + num2.ToString() + "の確立で破壊されます";
                EditorGUILayout.Space();
                EditorGUILayout.LabelField(answer);
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("持っているだけで倍率に加算する値");
                num3 = EditorGUILayout.IntField(num3);

                answer = num3.ToString() + "を倍率に加算する";

                EditorGUILayout.Space();
                EditorGUILayout.LabelField(answer);



                break;
            case JokerBaseEnum.JokerEnum._AnyDoneWhen:

                EditorGUILayout.LabelField("何をした時");
                target = (JokerActionUseEnum.JokerActionTarget)EditorGUILayout.EnumPopup((JokerActionUseEnum.JokerActionTarget)target);
                EditorGUILayout.LabelField(JokerActionUseEnum.JokerActionTargetExplanation[(int)target]);

                EditorGUILayout.Space();

                EditorGUILayout.LabelField("どのタイミングで計算が入るか");
                timing = (JokerActionUseEnum.Timing)EditorGUILayout.EnumPopup((JokerActionUseEnum.Timing)timing);
                EditorGUILayout.LabelField(JokerActionUseEnum.JokerActionTimingExplanation[(int)timing]);

                EditorGUILayout.Space();

                EditorGUILayout.LabelField("加算か乗算か");
                addType = (JokerActionUseEnum.AddType)EditorGUILayout.EnumPopup((JokerActionUseEnum.AddType)addType);
                EditorGUILayout.LabelField(addType == JokerActionUseEnum.AddType.addition ? "加算" : "乗算");

                EditorGUILayout.Space();

                EditorGUILayout.LabelField("一回の発生でどれくらいの量か");
                float1 = EditorGUILayout.FloatField(float1);

                break;
            case JokerBaseEnum.JokerEnum.MAX:
                break;
        }


    }
    static void CreateCS()
    {

        if (jolerListObject._className.Contains(className)) return;

        StringBuilder builder = new StringBuilder();
        builder.Clear();
        builder.Append(Application.dataPath);
        builder.Append(_classFilePass);
        builder.Append(className);
        builder.Append(".cs");

        StreamWriter sw;

        string filePass = builder.ToString();
        sw = new StreamWriter(filePass, false);
        builder.Clear();
        builder.Append("using UnityEngine;");
        builder.AppendLine();
        builder.Append("public class ");
        builder.Append(className);
        builder.Append(": JokerBase");

        builder.Append("{");
        builder.AppendLine();

        switch (jokerEnum)
        {
            case JokerBaseEnum.JokerEnum._ProbabilityDestruction:
                CreateProbabilityDestruction(ref builder);
                break;
            case JokerBaseEnum.JokerEnum._AnyDoneWhen:
                break;
            case JokerBaseEnum.JokerEnum.MAX:
                break;
        }

        builder.AppendLine();
        builder.AppendLine();

        builder.Append("}");

        sw.Write(builder.ToString());

        sw.Close();

    }
    static void CreateProbabilityDestruction(ref StringBuilder builder)
    {

        builder.Append("public override int Trun(){");
        builder.AppendFormat("return {0};", num3);
        builder.Append("}");
        builder.AppendLine();

        builder.Append("public override void RoundStart(){");
        builder.AppendLine();

        builder.AppendFormat("if((Random.Range(0,10000)%{0})<{1})",num1,num2);
        builder.AppendLine();

        builder.Append("{");
        builder.AppendLine();

        builder.Append("JokerUtility.Remove(this);");

        builder.AppendLine();

        builder.Append("}");

        builder.Append("}");

    }

    static void CreateAddClass() 
    {
        StringBuilder builder = new StringBuilder();
        builder.Clear();


        builder.Append("private static readonly string filePath = \"/Assets/Script/takumi/Card/Joker/JokerBody\";");
        builder.AppendLine();
        builder.Append("    public static string FILR_EXTENSION = \".asset\";");
        builder.AppendLine();
        builder.AppendFormat("    public static readonly string filePath2 = \"{0} \"", className);
        builder.AppendLine();
        builder.Append("static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths){");
        builder.AppendLine();
        builder.Append("        string filename = filePath + filePath2 + FILR_EXTENSION;");
        builder.AppendLine();
        builder.AppendFormat("    public static readonly string filePath2 = \"{0} \"", className);
        builder.AppendLine();
        builder.AppendFormat("    public static readonly string filePath2 = \"{0} \"", className);
        builder.AppendLine();
        builder.AppendFormat("    public static readonly string filePath2 = \"{0} \"", className);
        builder.AppendLine();
        builder.AppendFormat("    public static readonly string filePath2 = \"{0} \"", className);
        builder.AppendLine();
        builder.AppendFormat("    public static readonly string filePath2 = \"{0} \"", className);
        builder.AppendLine();






    }


    //static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    //{
    //    string filename = filePath + filePath2 + FILR_EXTENSION;
    //    foreach (string asset in importedAssets)
    //    {
    //        if (!filename.Equals(asset))
    //            continue;
    //        Debug.Log("ジョーカーの列挙体の生成開始");


    //    }
    //}



}
