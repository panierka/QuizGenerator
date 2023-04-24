using System.Text;

namespace QuizGenerator.Model.Ciphers
{
    public class CaesarsCipher : ICipher
    {
        private const string ALPHABET = "AĄBCĆDEĘFGHIJKLŁMNŃOÓPQRSŚTUVWXYZŹŻ";

        private readonly int shiftValue;

        public CaesarsCipher(int shiftValue = 1)
        {
            this.shiftValue = shiftValue;
        }

        public string Encrypt(string text)
        {
            return ShiftText(text, +shiftValue);
        }

        public string Decrypt(string text)
        {
            return ShiftText(text, -shiftValue);
        }

        private static string ShiftText(string text, int shift)
        {
            StringBuilder messageBuilder = new();

            foreach (char character in text)
            {
                char shiftedCharacter = ShiftCharacter(character, shift);
                _ = messageBuilder.Append(shiftedCharacter);
            }

            return messageBuilder.ToString();
        }

        private static char ShiftCharacter(char c, int shift)
        {
            bool isLower = char.IsLower(c);
            if (isLower)
            {
                c = char.ToUpper(c, System.Globalization.CultureInfo.InvariantCulture);
            }

            int n = ALPHABET.Length;
            int index = ALPHABET.IndexOf(c);
            
            if (index == -1)
            {
                return !isLower ? c : char.ToLower(c, System.Globalization.CultureInfo.InvariantCulture);
            }

            int shiftedIndex = Modulus(index + shift, n);
            char shiftedCharacter = ALPHABET[shiftedIndex];

            if (isLower)
            {
                shiftedCharacter = char.ToLower(shiftedCharacter, System.Globalization.CultureInfo.InvariantCulture);
            }

            return shiftedCharacter;
        }

        private static int Modulus(int x, int n)
        {
            int r = x % n;
            return r >= 0 ? r : r + n;
        }

        public static CaesarsCipher ROT13 { get; } = new(13);
    }
}
