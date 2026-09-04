namespace Game.Scripts.Core
{
    public class SpriteFontConverter
    {
        public static string Parse(string input)
        {
            string output = "";
            
            foreach (char symbol in input)
                output += $"<sprite name=\"{symbol}\">";
            
            return output;
        } 
    }
}