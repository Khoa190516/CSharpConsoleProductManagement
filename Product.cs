using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpConsoleProductManagement
{
    internal class Product : BaseItem
    {
        public string Description { get; set; } = string.Empty;
        public float Price { get; set; }
        public int CategoryId { get; set; }

        public override void ShowInfo() => Console.WriteLine(
                $"ID: {this.Id}\n" +
                $"Name: {this.Name}\n" +
                $"Description: {this.Description}\n" +
                $"Price: {this.Price}\n");


        public void ShowInfo(string cateName) => Console.WriteLine(
                $"ID: {this.Id}\n" +
                $"Name: {this.Name}\n" +
                $"Description: {this.Description}\n" +
                $"Price: {this.Price}\n" +
                $"Category: {cateName}\n");
    }
}
