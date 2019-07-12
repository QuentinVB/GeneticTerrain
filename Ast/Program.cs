using System;


namespace Ast
{
    class Program
    {
        static void Main(string[] args)
        {
            AstWrapper wrapper = new AstWrapper();
            wrapper.Parse("x+1");
            //AstTree.AstTrees();
        }
    }
}