using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CSharpConsoleProductManagement
{
    internal class StoreApp
    {
        private static StoreApp? storeApp;

        protected readonly StoreData store;
        protected readonly IBaseAction<Category> categoryAction;
        protected readonly IViewAction<Category> categoryViewAction;
        protected readonly IBaseAction<Product> productAction;
        protected readonly CategoryAction categoryBaseAction;

        protected StoreApp()
        {
            store = new()
            {
                Products = new(),
                Categories = new()
            };

            categoryBaseAction = new(store);
            categoryAction = categoryBaseAction;
            productAction = new ProductAction(store);
            categoryViewAction = categoryBaseAction;
        }

        public static StoreApp Instance()
        {
            storeApp ??= new();

            return storeApp;
        }

        public void Run()
        {
            Console.WriteLine("- Store Management -");

            int option;

            do
            {
                ShowMenu();

                Console.Write("Option: ");
                var input = Console.ReadLine();
                Console.WriteLine();

                if (!int.TryParse(input, out option))
                {
                    Console.WriteLine("Incorret input");
                }

                switch (option)
                {
                    case 1:
                        {
                            categoryAction.Show();
                            break;
                        }
                    case 2:
                        {
                            productAction.Show();
                            break;
                        }
                    case 3:
                        {
                            bool isTryAgain;
                            do
                            {
                                Console.Write("ID: ");
                                var id = Console.ReadLine();
                                Console.Write("\nName: ");
                                var name = Console.ReadLine();

                                if(IsCategoryValid(id, name))
                                {
                                    int idAdd = int.Parse(id);

                                    if (categoryAction.IsExist(idAdd))
                                    {
                                        Console.WriteLine($"Category ID: {idAdd} already exists");
                                    }
                                    else
                                    {
                                        Category category = new()
                                        {
                                            Id = idAdd,
                                            Name = name,
                                        };

                                        if (categoryAction.Add(category))
                                        {
                                            Console.WriteLine("-- Category added --\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Category added fails");
                                        };
                                    }                 
                                }

                                ShowContinueOption(out isTryAgain);

                            } while (isTryAgain);
                            
                            break;
                        }
                    case 4:
                        {
                            bool isTryAgain;
                            do
                            {
                                Console.Write("ID: ");
                                var id = Console.ReadLine();

                                Console.Write("\nName: ");
                                var name = Console.ReadLine();

                                if (IsCategoryValid(id, name))
                                {
                                    int idCate = int.Parse(id);

                                    if (!categoryAction.IsExist(idCate))
                                    {
                                        Console.WriteLine($"Category ID: {idCate} is not found");
                                    }
                                    else
                                    {
                                        Category category = new()
                                        {
                                            Id = idCate,
                                            Name = name,
                                        };

                                        if (categoryAction.Update(category))
                                        {
                                            Console.WriteLine("-- Category updated --\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Category update failed, ID: " + category.Id + "\n");
                                        };
                                    }
                                }

                                ShowContinueOption(out isTryAgain);

                            } while (isTryAgain);
                            
                            break;
                        }
                    case 5:
                        {
                            bool isTryAgain;
                            do
                            {
                                Console.Write("ID: ");
                                var id = Console.ReadLine();

                                if (IsCategoryValid(id, "default" ))
                                {
                                    int idRemove = int.Parse(id);

                                    if (!categoryAction.IsExist(idRemove))
                                    {
                                        Console.WriteLine($"Category ID: {idRemove} is not found");
                                    }
                                    else
                                    {
                                        if (IsCategoryHasProducts(idRemove))
                                        {
                                            Console.WriteLine("Category still has products, can't remove right now.");
                                        }
                                        else
                                        {
                                            if (categoryAction.Remove(idRemove))
                                            {
                                                Console.WriteLine("-- Category removed --\n");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Category remove failed, ID: " + idRemove + "\n");
                                            };
                                        }          
                                    }       
                                }

                                ShowContinueOption(out isTryAgain);

                            } while (isTryAgain);
                            
                            break;
                        }
                    case 6:
                        {
                            bool isTryAgain;
                            do
                            {
                                Console.Write("ID: ");
                                var id = Console.ReadLine();
                                Console.Write("\nName: ");
                                var name = Console.ReadLine();
                                Console.Write("\nPrice: ");
                                var price = Console.ReadLine();
                                Console.Write("\nDescription: ");
                                var des = Console.ReadLine();
                                Console.Write("\nCategory ID: ");
                                var cateId = Console.ReadLine();

                                if(IsProductValid(id, name, des, price, cateId))
                                {
                                    int idAdd = int.Parse(id);

                                    if (productAction.IsExist(idAdd))
                                    {
                                        Console.WriteLine($"Product ID: {idAdd} already exists");
                                    }
                                    else
                                    {
                                        float priceAdd = float.Parse(price);
                                        int cateIdAdd = int.Parse(cateId);

                                        Product product = new()
                                        {
                                            Id = idAdd,
                                            Name = name,
                                            Description = des,
                                            Price = priceAdd,
                                            CategoryId = cateIdAdd
                                        };

                                        if (productAction.Add(product))
                                        {
                                            Console.WriteLine("-- Product added --\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Product add failed.\n");
                                        };
                                    }
                                }

                                ShowContinueOption(out isTryAgain);

                            } while (isTryAgain);
                            
                            break;
                        }
                    case 7:
                        {
                            bool isTryAgain;
                            do
                            {
                                Console.Write("ID: ");
                                var id = Console.ReadLine();

                                Console.Write("\nName: ");
                                var name = Console.ReadLine();
                                Console.Write("\nPrice: ");
                                var price = Console.ReadLine();
                                Console.Write("\nDescription: ");
                                var des = Console.ReadLine();
                                Console.Write("\nCategory ID: ");
                                var cateId = Console.ReadLine();

                                if(IsProductValid(id, name, des, price, cateId))
                                {
                                    int idAdd = int.Parse(id);
                                    if (!productAction.IsExist(idAdd))
                                    {
                                        Console.WriteLine($"Product ID: {idAdd} is not found.");
                                    }
                                    else
                                    {
                                        float priceAdd = float.Parse(price);
                                        int cateIdAdd = int.Parse(cateId);

                                        Product product = new()
                                        {
                                            Id = idAdd,
                                            Name = name,
                                            Description = des,
                                            Price = priceAdd,
                                            CategoryId = cateIdAdd
                                        };

                                        if (productAction.Update(product))
                                        {
                                            Console.WriteLine("-- Product updated --\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Product updat failed.\n");
                                        };
                                    }
                                }

                                ShowContinueOption(out isTryAgain);

                            } while (isTryAgain);
                            
                            break;
                        }
                    case 8:
                        {
                            bool isTryAgain;
                            do
                            {
                                Console.Write("ID: ");
                                var id = Console.ReadLine();

                                if (int.TryParse(id, out int idAdd))
                                {
                                    if (!productAction.IsExist(idAdd))
                                    {
                                        Console.WriteLine($"Product ID: {idAdd} is not found.");
                                    }
                                    else
                                    {
                                        if (productAction.Remove(idAdd))
                                        {
                                            Console.WriteLine("-- Product removed --\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Product remove failed, ID: " + idAdd + "\n");
                                        };
                                    }
                                    
                                }
                                else
                                {
                                    Console.WriteLine("Invalid ID, must be a number. Try again\n");
                                }

                                ShowContinueOption(out isTryAgain);

                            } while (isTryAgain);
                            
                            break;
                        }
                    case 9:
                        {
                            Console.WriteLine("View products by category.");
                            categoryViewAction.ViewProductsByCategory();
                            break;
                        }
                    case 10:
                        {
                            bool isTryAgain;
                            Console.WriteLine("View products by category ID.");
                            do
                            {
                                Console.Write("Enter Category ID: ");
                                var cateIdInput = Console.ReadLine();

                                if (IsCategoryValid(cateIdInput, Common.NOT_AVAILABLE))
                                {
                                    int cateId = int.Parse(cateIdInput);
                                    categoryViewAction.ViewProductsByCategoryId(cateId);
                                }

                                ShowContinueOption(out isTryAgain);
                            } while (isTryAgain);

                            break;
                        }
                    case 11:
                        {
                            bool isTryAgain;
                            Console.WriteLine("Find category by ID.");
                            do
                            {
                                Console.Write("Enter Category ID: ");
                                var cateIdInput = Console.ReadLine();

                                if (IsCategoryValid(cateIdInput, Common.NOT_AVAILABLE))
                                {
                                    int cateId = int.Parse(cateIdInput);
                                    var cateSearch = categoryAction.GetById(cateId);

                                    if (cateSearch != null) { cateSearch.ShowInfo(); } 
                                    else { Console.WriteLine($"Category ID: {cateId} is not found."); }
                                }

                                ShowContinueOption(out isTryAgain);
                            } while (isTryAgain);

                            break;
                        }
                    case 12:
                        {
                            bool isTryAgain;
                            Console.WriteLine("Find product by ID.");
                            do
                            {
                                Console.Write("Enter Product ID: ");
                                var procIdInput = Console.ReadLine();

                                if (int.TryParse(procIdInput, out int procId))
                                {
                                    var cateSearch = categoryAction.GetById(procId);

                                    if (cateSearch != null) { cateSearch.ShowInfo(); }
                                    else { Console.WriteLine($"Product ID: {procId} is not found."); }
                                }
                                else
                                {
                                    Console.WriteLine("Product ID is invalid, must be a number.");
                                }

                                ShowContinueOption(out isTryAgain);
                            } while (isTryAgain);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                Console.WriteLine();
            } while (option > 0 && option < 13);
        }

        private static void ShowMenu()
        {
            Console.WriteLine(
                    $"1. View Categories\n" +
                    $"2. View Products\n" +
                    $"3. Add Category\n" +
                    $"4. Update Category\n" +
                    $"5. Remove Category\n" +
                    $"6. Add Product\n" +
                    $"7. Update Product\n" +
                    $"8. Remove Product\n" +
                    $"9. View Products By Category\n" +
                    $"10. View Products By Category ID\n" +
                    $"11. Find Category By ID\n" +
                    $"12. Find Product By ID\n" +
                    $"Other. Exit");
        }

        private bool IsCategoryValid(string? id, string? name)
        {
            if (!int.TryParse(id, out _) || string.IsNullOrEmpty(name))
            {
                if (!int.TryParse(id, out _))
                {
                    Console.WriteLine("Category ID is invalid, must be a number.");
                }

                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Category name is required.");
                }

                return false;
            }
            return true;
        }

        private bool IsProductValid(string? id, string? name, string? des, string? price, string? cateId)
        {
            if (!int.TryParse(id, out _) ||
                string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(des) ||
                !float.TryParse(price, out _) ||
                !int.TryParse(cateId, out int cateIdAdd)||
                !categoryAction.IsExist(cateIdAdd))
            {
                
                if(!int.TryParse(id,out _))
                {
                    Console.WriteLine("Invalid ID, must be a number.");
                }

                if (!float.TryParse(price, out float priceProc))
                {
                    Console.WriteLine("Invalid price, must be a number.");
                }

                if(priceProc is < 0 or >= 999999999)
                {
                    Console.WriteLine("Invalid price, price >= 0");
                }

                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Product name is required");
                }

                if (string.IsNullOrEmpty(des))
                {
                    Console.WriteLine("Product description is required");
                }

                if (!int.TryParse(cateId, out cateIdAdd))
                {
                    Console.WriteLine("Invalid Category ID, must be a number.");
                }

                if(!categoryAction.IsExist(cateIdAdd))
                {
                    Console.WriteLine($"Category ID: {cateIdAdd} is not found.");
                }

                return false;
            }

            return true;
        }

        private static void ShowContinueOption(out bool isTryAgain)
        {
            Console.WriteLine("Continue ? [y/n]\n");
            var tryOption = Console.ReadLine();

            if (!string.IsNullOrEmpty(tryOption) &&
                tryOption.ToLower().Trim().Equals("y"))
            {
                isTryAgain = true;
            }
            else
            {
                isTryAgain = false;
            }

            Console.WriteLine();
        }

        private bool IsCategoryHasProducts(int categoryId)
        {
            return store.Products.Find(p => p.CategoryId == categoryId) != null;
        }
    }
}
