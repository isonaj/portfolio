using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Model
{
    public class StockQuote
    {
        public int Id { get; set; }
        public string Code { get; private set; }
        public DateTime Date { get; private set; }
        public decimal? Open { get; private set; }
        public decimal? High { get; private set; }
        public decimal? Low { get; private set; }
        public decimal Close { get; private set; }
        public long? Volume { get; private set; }

        public StockQuote(string code, DateTime date, decimal? open, decimal? high, decimal? low, decimal close, long? volume)
        {
            Code = code;
            Date = date;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }
    }
}
