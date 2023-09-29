using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpConsoleProductManagement
{
    internal class CategoryAction : IBaseAction<Category>, IViewAction<Category>
    {
        protected readonly StoreData store;

        public CategoryAction(StoreData _store)
        {
            this.store = _store;
        }

        public void Show()
        {
            foreach (var category in store.Categories)
            {
                category.ShowInfo();
            }
        }

        public bool Add(Category category)
        {
            if (IsExist(category.Id))
            {
                return false;
            }
            store.Categories.Add(category);
            return true;
        }

        public bool Remove(int categoryId)
        {
            Category? cate = store.Categories.Find(p => p.Id == categoryId);
            if (cate != null)
            {
                store.Categories.Remove(cate);
                return true;
            }
            return false;
        }

        public bool Update(Category categoryUpdate)
        {
            Category? cate = store.Categories.Find(c => c.Id.Equals(categoryUpdate.Id));
            if (cate != null)
            {
                cate.Name = categoryUpdate.Name;
                return true;
            }
            return false;
        }

        public bool IsExist(int categoryId)
        {
            return store.Categories.Find(c => c.Id == categoryId) != null;
        }

        public void ViewProductsByCategoryId(int categoryId)
        {
            var category = store.Categories.Find(c => c.Id == categoryId);
            var cateName = "N/A";

            if (category != null) { cateName = category.Name; }

            var result = store.Products.Where(p => p.CategoryId == categoryId).ToList();

            Console.WriteLine($"Category: {cateName}\n");

            foreach( var product in result)
            {
                product.ShowInfo();
            }
        }

        public void ViewProductsByCategory()
        {
            foreach( var cate in store.Categories)
            {
                Console.WriteLine($"Category: {cate.Name}\n");

                var result = store.Products.Where(p => p.CategoryId == cate.Id).ToList();

                foreach (var product in result)
                {
                    product.ShowInfo();
                }
            }
        }

        public Category? GetById(int id)
        {
            return store.Categories.Find(c => c.Id == id);
        }
    }
}
