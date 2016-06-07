namespace Data.AnotherClass
{
    public static class Strings
    {
        public static bool IsDigit(string inscription)
        {
            foreach (char c in inscription)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }

        public static bool IsDigitFloat(string inscription)
        {
            foreach (char c in inscription)
            {
                if ( !char.IsDigit(c) || (c =='.') || (c == ',') )
                    return false;
            }

            return true;
        }

        public static string ReplaceDotToComma(string inscription)
        {
            return inscription.Replace(".", ",");
        }
    }
}
