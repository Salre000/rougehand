using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
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
        jolerListObject = Resources.Load<JokerListObject>("takumi/Observer/JolerLists");

        stringList = Resources.Load<StringList>(ObserverJokerBase.filePath2);
        This.Show();

    }

    private static JokerListObject jolerListObject;

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
    static JokerActionUseEnum.JokerRarity rarity = JokerActionUseEnum.JokerRarity.Common;

    /// <Summary>
    /// ウィンドウのパーツを表示します。
    /// </Summary>
    void OnGUI()
    {
        EditorGUILayout.BeginVertical("Box");

        if (GUILayout.Button("生成する"))
        {
            // 同名スプリクトの生成を妨害
            if (jolerListObject._className.Contains(className)) return;

            // ここにボタンを押した時の処理を書きます
            CreateCS();

            CreateAddClass();
        }
        if (GUILayout.Button("リロード"))
        {
            className = string.Empty;

            CreateAddClass();
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

        rarity = (JokerActionUseEnum.JokerRarity)EditorGUILayout.EnumPopup((JokerActionUseEnum.JokerRarity)rarity);
        EditorGUILayout.LabelField("ジョーカーのレアリティ");


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


        builder.Append("    public override JokerActionUseEnum.JokerRarity GetRarity() { ");
        builder.AppendFormat("return JokerActionUseEnum.JokerRarity.{0};", rarity.ToString());
        builder.Append("}");
        builder.AppendLine();

        if (addType == JokerActionUseEnum.AddType.Multiplication)
        {

            builder.Append("    public override bool GetAddType() {return false; ");
            builder.Append("}");
            builder.AppendLine();


        }

        switch (jokerEnum)
        {
            case JokerBaseEnum.JokerEnum._ProbabilityDestruction:
                CreateProbabilityDestruction(ref builder);
                break;
            case JokerBaseEnum.JokerEnum._AnyDoneWhen:
                CreateAnyDoneWhen(ref builder);

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
    static void CreateAnyDoneWhen(ref StringBuilder builder)
    {

        switch (timing)
        {
            case JokerActionUseEnum.Timing.trun:
                builder.Append("float _magnification=0;");
                builder.AppendLine();



                builder.Append("public override void UpData(){");
                builder.AppendFormat("if(JokerUtility.GetTarget()!=JokerActionUseEnum.JokerActionTarget.{0})return;", target.ToString());
                builder.AppendLine();

                builder.AppendFormat("_magnification+={0};", float1.ToString());
                builder.Append("}");
                builder.AppendLine();

                builder.Append("public override float Trun(){");
                builder.Append("return _magnification;");
                builder.AppendLine();

                builder.Append("}");
                builder.AppendLine();
                builder.Append("public override void TrunReset(){");
                builder.Append(" _magnification=0;");
                builder.AppendLine();

                builder.Append("}");



                break;
            case JokerActionUseEnum.Timing.now:

                builder.Append("public override void UpData(){");
                builder.AppendFormat("if(JokerUtility.GetTarget()!=JokerActionUseEnum.JokerActionTarget.{0})return;", target.ToString());
                builder.AppendLine();

                builder.AppendFormat("JokerUtility.{0}({1});", addType == JokerActionUseEnum.AddType.addition ? "AddMagnification" : "", float1.ToString());
                builder.Append("}");


                break;
            case JokerActionUseEnum.Timing.never:
                builder.Append("float _magnification=0;");
                builder.AppendLine();



                builder.Append("public override void UpData(){");
                builder.AppendFormat("if(JokerUtility.GetTarget()!=JokerActionUseEnum.JokerActionTarget.{0})return;", target.ToString());
                builder.AppendLine();
                builder.AppendFormat("        JokerObjectUtility.CardAddAction(JokerUtility.GetIndex(),{0});", float1.ToString());
                builder.AppendLine();

                builder.AppendFormat("_magnification+={0};", float1.ToString());
                builder.Append("}");
                builder.AppendLine();

                builder.Append("public override float Trun(){");
                builder.Append("return _magnification;");
                builder.AppendLine();

                builder.Append("}");


                break;
        }

    }
    static void CreateProbabilityDestruction(ref StringBuilder builder)
    {

        builder.Append("public override float Trun(){");
        builder.AppendFormat("return {0};", num3);
        builder.Append("}");
        builder.AppendLine();

        builder.Append("public override void RoundStart(){");
        builder.AppendLine();

        builder.AppendFormat("if((Random.Range(0,10000)%{0})<{1})", num1, num2);
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

        if(className!=string.Empty)jolerListObject._className.Add(className);

        StringBuilder builder = new StringBuilder();


        builder.Clear();
        builder.Append(Application.dataPath);
        builder.Append("/Script/takumi/Generic/");
        builder.Append("ALLJoker");
        builder.Append(".cs");
        StreamWriter sw;
        string filePass = builder.ToString();

        Debug.Log(filePass);

        sw = new StreamWriter(filePass, false);


        builder.Clear();

        builder.Append("using System.IO;");
        builder.AppendLine();
        builder.Append("using System.Text;");
        builder.AppendLine();
        builder.Append("using UnityEditor;");
        builder.AppendLine();
        builder.Append("using UnityEngine;");
        builder.AppendLine();


        builder.AppendFormat("public static class ALLJoker");
        builder.AppendLine();
        builder.Append("{");

        builder.Append("public enum _allJokerEnum{");

        for (int i = 0; i < jolerListObject._className.Count; i++)
        {
            builder.AppendLine();
            builder.AppendFormat("_{0},", jolerListObject._className[i]);

        }

        builder.AppendLine();
        builder.Append("MAX");

        builder.AppendLine();
        builder.Append("}");

        builder.AppendLine();

        builder.Append("public static JokerBase GetJoker(_allJokerEnum joker){");
        builder.AppendLine();
        builder.Append("        switch (joker){");
        builder.AppendLine();
        for (int i = 0; i < jolerListObject._className.Count; i++)
        {
            builder.AppendLine();
            builder.AppendFormat("            case _allJokerEnum._{0}:return new {0}();", jolerListObject._className[i]);

        }
        builder.AppendLine();
        builder.Append("}");
        builder.Append("return null;");
        builder.Append("}");
        builder.AppendLine();

        builder.AppendLine();

        builder.AppendLine();


        builder.Append("}");

        sw.Write(builder.ToString());

        sw.Close();



    }

}
