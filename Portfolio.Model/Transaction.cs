using System;

namespace Portfolio.Model
{
    public class Transaction
    {
        public DateTime Date { get; private set; }
        public string Code { get; private set; }
        //public string Units { get; }
        //public string Amount { get; }

        public Transaction(DateTime date, string code)
        {
            Date = date;
            Code = code;
        }
    }
}