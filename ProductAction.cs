using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpConsoleProductManagement
{
    internal class ProductAction : IBaseAction<Product>
    {
        protected readonly StoreData store;

        public ProductAction(StoreData _store) 
        {
            this.store = _store;
        }

        public void Show()
        {
            foreach (var product in store.Products)
            {
                var cate = store.Categories.Find(c => c.Id == product.CategoryId);

                if (cate != null)
                {
                    product.ShowInfo(cate.Name);
                }
                else
                {
                    product.ShowInfo("N/A");
                }
            }
        }

        public bool Add(Product product)
        {
            if (IsExist(product.Id))
            {
                return false;
            }
            store.Products.Add(product);
            return true;
        }

        public bool Remove(int productId)
        {
            Product? proc = store.Products.Find(p => p.Id == productId);
            if (proc != null)
            {
                store.Products.Remove(proc);
                return true;
            }
            return false;
        }

        public bool Update(Product productUpdate)
        {
            Product? p = store.Products.Find(p => p.Id == productUpdate.Id);
            if (p != null)
            {
                p.Name = productUpdate.Name;
                p.Description = productUpdate.Description;
                p.Price = productUpdate.Price;
                p.CategoryId = productUpdate.CategoryId;

                return true;
            }
            return false;
        }

        public bool IsExist(int productId)
        {
            return store.Products.Find(p => p.Id == productId) != null;
        }

        public Product? GetById(int id)
        {
            return store.Products.Find(p => p.Id ==  id);   
        }
    }
}
