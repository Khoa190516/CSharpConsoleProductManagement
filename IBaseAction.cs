using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpConsoleProductManagement
{
    internal interface IBaseAction<T> where T : class
    {
        public void Show();
        public bool IsExist(int id);
        public bool Add(T item);
        public bool Remove(int id);
        public bool Update(T item);
        public T? GetById(int id);
        
    }

    internal interface IViewAction<T> where T: class
    {
        public void ViewProductsByCategory();
        public void ViewProductsByCategoryId(int categoryId);
    }
}
