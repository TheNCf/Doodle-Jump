namespace Game.Scripts.Core
{
    public class SpriteFontConverter
    {
        public static string Parse(string input)
        {
            var output = "";

            foreach (var symbol in input)
                output += $"<sprite name=\"{symbol}\">";

            return output;
        }
    }
}