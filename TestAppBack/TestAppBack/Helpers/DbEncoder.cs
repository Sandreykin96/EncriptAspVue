using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppBack.DbHelpers
{
    class DbEncoder
    {
        private ApplicationContext db;
        public DbEncoder(ApplicationContext context)
        {
            db = context;
        }

        public string Encode(string inputMessage)
        {
            string outputMessage = "";
            foreach (var item in inputMessage)
            {
                var keyAndValue = db.Codes.FirstOrDefault(code => code.oldSymbol == item.ToString());
                var nextSymbol = keyAndValue == null ? item.ToString() : keyAndValue.newSymbol;
                outputMessage += nextSymbol;
            }
            return outputMessage;
        }
    }
}
