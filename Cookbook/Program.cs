using Cookbook.Views;
using System;

namespace Cookbook
{
    class Program
    {
        static void Main(string[] args)
        {
            Display display = new Display(new Data.CookbookContext());            
        }
    }
}
