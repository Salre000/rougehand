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
    /// �N���X�̐����ʒu
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
    /// �E�B���h�E�̃p�[�c��\�����܂��B
    /// </Summary>
    void OnGUI()
    {
        EditorGUILayout.BeginVertical("Box");

        if (GUILayout.Button("��������"))
        {
            //�@�����Ƀ{�^�������������̏����������܂�
            CreateCS();
        }


        EditorGUILayout.Space();

        EditorGUILayout.LabelField("�W���[�J�[�̃N���X�̖��O");
        className = EditorGUILayout.TextField(className);

        EditorGUILayout.Space();

        // �ǂ�ȃW���[�J�[�𐶐����邩������
        jokerEnum = (JokerBaseEnum.JokerEnum)EditorGUILayout.EnumPopup((JokerBaseEnum.JokerEnum)jokerEnum);

        // ���݂̃W���[�J�[�̎d�l��������悤�ɕύX
        string jokerEX = jokerEnum == JokerBaseEnum.JokerEnum.MAX ? "" : stringList._expansion[(int)jokerEnum];
        EditorGUILayout.LabelField(jokerEX + ":" + "���݂̃W���[�J�[�̎d�l");
        //EditorGUILayout.LabelField(jokerEX);

        EditorGUILayout.Space();

        SwitchJoker(jokerEnum);
        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// �W���[�J�[�̎�ނ��ƂɕK�v�ȏ�񂪈Ⴄ���炻������߂Ă���
    /// </summary>
    /// <param name="jokerEnum"></param>
    static void SwitchJoker(JokerBaseEnum.JokerEnum jokerEnum)
    {
        string answer = string.Empty;
        switch (jokerEnum)
        {
            case JokerBaseEnum.JokerEnum._ProbabilityDestruction:


                EditorGUILayout.LabelField("�m���̕��q");
                num2 = EditorGUILayout.IntField(num2);

                EditorGUILayout.Space();

                EditorGUILayout.LabelField("�m���̕���");
                num1 = EditorGUILayout.IntField(num1);
                if (num1 < num2) num2 = num1;

                answer = num1.ToString() + "����" + num2.ToString() + "�̊m���Ŕj�󂳂�܂�";
                EditorGUILayout.Space();
                EditorGUILayout.LabelField(answer);
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("�����Ă��邾���Ŕ{���ɉ��Z����l");
                num3 = EditorGUILayout.IntField(num3);

                answer = num3.ToString() + "��{���ɉ��Z����";

                EditorGUILayout.Space();
                EditorGUILayout.LabelField(answer);



                break;
            case JokerBaseEnum.JokerEnum._AnyDoneWhen:

                EditorGUILayout.LabelField("����������");
                target = (JokerActionUseEnum.JokerActionTarget)EditorGUILayout.EnumPopup((JokerActionUseEnum.JokerActionTarget)target);
                EditorGUILayout.LabelField(JokerActionUseEnum.JokerActionTargetExplanation[(int)target]);

                EditorGUILayout.Space();

                EditorGUILayout.LabelField("�ǂ̃^�C�~���O�Ōv�Z�����邩");
                timing = (JokerActionUseEnum.Timing)EditorGUILayout.EnumPopup((JokerActionUseEnum.Timing)timing);
                EditorGUILayout.LabelField(JokerActionUseEnum.JokerActionTimingExplanation[(int)timing]);

                EditorGUILayout.Space();

                EditorGUILayout.LabelField("���Z����Z��");
                addType = (JokerActionUseEnum.AddType)EditorGUILayout.EnumPopup((JokerActionUseEnum.AddType)addType);
                EditorGUILayout.LabelField(addType == JokerActionUseEnum.AddType.addition ? "���Z" : "��Z");

                EditorGUILayout.Space();

                EditorGUILayout.LabelField("���̔����łǂꂭ�炢�̗ʂ�");
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
    //        Debug.Log("�W���[�J�[�̗񋓑̂̐����J�n");


    //    }
    //}



}
