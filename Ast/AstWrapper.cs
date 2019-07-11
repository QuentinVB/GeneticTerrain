using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ast
{
    class AstWrapper
    {

        private SimpleAnalyzer simpleAnalyzer;
        //compute in analyzer identifiernode 
        // class algorithm

        public AstWrapper()
        {
           simpleAnalyzer = new SimpleAnalyzer();
        }
        public Node Parse(string expression)
        {

            return simpleAnalyzer.Parse(expression);
        }

     }
}
