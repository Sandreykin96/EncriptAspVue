using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppBack.Helpers
{
    public class CeaserEncoder
    {
        public string alfabet { get; set; }

        public string Encrypt(string plainMessage, int key)
        {
            return CodeEncode(plainMessage, key);
        }

        public string Decrypt(string encryptedMessage, int key)
        {
            return CodeEncode(encryptedMessage, -key);
        }

        private string CodeEncode(string text, int k)
        {
            var letterQty = alfabet.Length;
            var retVal = "";
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                var index = alfabet.IndexOf(c);
                if (index < 0)
                {
                    retVal += c.ToString();
                }
                else
                {
                    var codeIndex = (letterQty + index + k) % letterQty;
                    retVal += alfabet[codeIndex];
                }
            }
            return retVal;
        }
    }
}
