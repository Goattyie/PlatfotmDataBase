using DynamicExpresso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services.Builder
{
    class NumberBuilder
    {
        private string _value;
        private Interpreter _interpreter = new Interpreter();
        public void Convert(char symbol)
        {
            _value += symbol;
        }
        public double ToValue() 
        {
            double val;
            try
            {
                val = Math.Abs(double.Parse(_interpreter.Eval(_value).ToString()));
            }
            catch { val = 0; }
            return val;
        }
        public string ToText()
        {
            return _value;
        }
    }
}
