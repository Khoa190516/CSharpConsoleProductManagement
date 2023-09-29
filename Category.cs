using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpConsoleProductManagement
{
    internal class Category : BaseItem
    {
        public override void ShowInfo()
        {
            Console.WriteLine(
                $"ID: {this.Id}\n" +
                $"Name: {this.Name}\n");
        }
    }
}
