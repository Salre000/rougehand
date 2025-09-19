using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ObserverJokerBase : AssetPostprocessor
{
    private static readonly string filePath = "Assets/Resources/";
    public static readonly string filePath2 = "takumi/Observer/JokerList";
    public static string FILR_EXTENSION = ".asset";

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        string filename = filePath + filePath2 + FILR_EXTENSION;
        foreach (string asset in importedAssets)
        {
            if (!filename.Equals(asset))
                continue;
            Debug.Log("ジョーカーの列挙体の生成開始");

            CreateCS(filePath + filePath2);

        }
    }
    static Encoding encoding;
    private static void CreateCS(string filename)
    {
        StringList achievementsAll = Resources.Load<StringList>(filePath2);

        StringBuilder builder = new StringBuilder();
        builder.Clear();
        builder.Append(Application.dataPath);
        builder.Append("/Script/takumi/Generic/JokerBaseEnum");
        builder.Append(".cs");

        StreamWriter sw;

        string filePass = builder.ToString();
        sw = new StreamWriter(filePass, false);
        builder.Clear();
        builder.Append("public  static class JokerBaseEnum {");
        builder.AppendLine();


        builder.Append("public enum JokerEnum {");
        builder.AppendLine();

        for (int i = 0; i < achievementsAll._enumName.Count; i++)
        {
            builder.AppendFormat("/// <summary>");

            builder.AppendLine();
            builder.AppendFormat("///{0}", achievementsAll._expansion[i]);

            builder.AppendLine();
            builder.AppendFormat("/// </summary>");

            builder.AppendLine();

            builder.AppendFormat("_{0}", achievementsAll._enumName[i]);
            builder.Append(",");
            builder.AppendLine();

        }

        builder.Append("MAX");
        builder.AppendLine();


        builder.Append("}");

        builder.AppendLine();

        builder.Append("}");

        sw.Write(builder.ToString());

        sw.Close();
        Debug.Log("ジョーカーの列挙体の生成完了");
    }



}
