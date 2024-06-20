namespace DataKeeper.Extensions
{
    public static class StringExtension
    {
        public static string ToTitleCase(this string str)
        {
            if (string.IsNullOrEmpty(str) || str.Length < 2)
                return str;

            str = str.ToLower();
            return char.ToUpper(str[0]) + str.Substring(1);
        }
    }
}
