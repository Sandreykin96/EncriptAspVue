using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppBack.Helpers
{
    public class CaesarEncoderTableCreator : CeaserEncoder
    {
        private ApplicationContext db;
        public int KeyNumber { get; set; }
        public CaesarEncoderTableCreator(ApplicationContext context)
        {
            db = context;
            alfabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯABCDEFGHIJKLMNOPQRSTUVWXYZабвгдеёжзиклмнопрстуфхцчщъыьэюяabcdifghijklopqustuvwxuz";
            KeyNumber = 5;
        }

        public void CreateKeysTable()
        {
            if (db.Codes.Any())
                return;

            foreach (var item in alfabet)
            {
                Code pair = new Code();
                pair.newSymbol = Encrypt(item.ToString(), KeyNumber);
                pair.oldSymbol = item.ToString();
                db.Codes.Add(pair);
            }
            db.SaveChanges();
        }
    }
}
