using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services.Builder
{
    class NumberDirector
    {
        public NumberBuilder Builder = new NumberBuilder();
        public NumberDirector(NumberBuilder builder)
        {
            Builder = builder;
        }
        public void Construct(string text)
        {
            foreach(char sym in text)
            {
                switch (sym)
                {
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '0':
                    case '-':
                        Builder.Convert(sym);
                        break;
                }
            }
        }
    }
}
