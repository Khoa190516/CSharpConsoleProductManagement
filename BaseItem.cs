using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpConsoleProductManagement
{
    internal abstract class BaseItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public abstract void ShowInfo();
    }
}
