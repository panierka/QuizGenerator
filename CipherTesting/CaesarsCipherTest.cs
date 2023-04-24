using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizGenerator.Model.Ciphers;

namespace CipherTesting
{
    [TestClass]
    public class CaesarsCipherTest
    {
        [TestMethod]
        public void BasicTest()
        {
            CaesarsCipher cipher = new(1);
            string text = "ABC 123";
            string encryptedText = cipher.Encrypt(text);

            Assert.AreEqual("¥CÆ 123", encryptedText);
        }

        [TestMethod]
        public void LowercaseTest()
        {
            CaesarsCipher cipher = new(1);
            string text = "abc 123";
            string encryptedText = cipher.Encrypt(text);

            Assert.AreEqual("¹cæ 123", encryptedText);
        }

        [TestMethod]
        public void LoopTest()
        {
            CaesarsCipher c = CaesarsCipher.ROT13;
            string text1 = "Politechnika.Œl¹ska 123";
            string text2 = c.Encrypt(text1);
            string text3 = c.Decrypt(text2);

            Assert.AreEqual(text1, text3);
        }

        [TestMethod]
        public void FirstAndLastCharacter()
        {
            CaesarsCipher cipher = new(1);
            string text = "aaa";
            string encryptedText = cipher.Decrypt(text);

            Assert.AreEqual("¿¿¿", encryptedText);

            string text1 = "¿¿¿";
            string encryptedText1 = cipher.Encrypt(text1);

            Assert.AreEqual("aaa", encryptedText1);
        }
    }
}
