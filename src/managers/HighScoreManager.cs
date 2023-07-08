using System;
using System.IO;
using System.Text;

namespace SpaceArcade;

public static class HighScoreManager{
    private static string FileName;
    public static int Score = -1;
    public static void Init(){
        FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "highScore.txt");
        if(!File.Exists(FileName))
            using(StreamWriter sw = File.CreateText(FileName));
    }

    public static void Load(){
        var text = File.ReadAllText(FileName);
        Score = int.Parse(text);
    }

    public static void Save(int score){
        File.WriteAllText(FileName, ""+score);
    }
}