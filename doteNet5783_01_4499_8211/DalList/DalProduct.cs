using DalApi;
using DO;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Dal
{
    public class DalProduct : IProduct
    {
        readonly DataSource ds = DataSource.Instance;

        #region Add
        /// <summary>
        /// Adds a new product to the list of products.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <returns>The ID of the added product.</returns>
        /// <exception cref="AlreadyExistsException">Thrown when a product with the same ID already exists.</exception>
        public int Add(Product product)
        {
            Product newProduct = ds.ListProduct.FirstOrDefault(p => product.ID == p?.ID);
            if (newProduct != null)
            {
                if (newProduct.IsDeleted)
                    ds.ListProduct.RemoveAll(p => product.ID == p?.ID);
                else
                    throw new AlreadyExistsException(product.ID);
            }

            ds.ListProduct.Add(product);
            return product.ID;
        }
        #endregion

        #region GetById
        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product object.</returns>
        /// <exception cref="DoesNotExistException">Thrown when the product with the given ID does not exist.</exception>
        public Product GetById(int id)
        {
            Product product = ds.ListProduct.FirstOrDefault(p => p?.ID == id)
                ?? throw new DoesNotExistException(id);

            if (product.IsDeleted)
                throw new DoesNotExistException(id);

            return product;
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="product">The updated product object.</param>
        /// <exception cref="DoesNotExistException">Thrown when the product with the given ID does not exist.</exception>
        public void Update(Product product)
        {
            Product existingProduct = ds.ListProduct.FirstOrDefault(found => found?.ID == product.ID)
                ?? throw new DoesNotExistException(product.ID);

            if (existingProduct.IsDeleted)
                throw new DoesNotExistException(product.ID);

            ds.ListProduct.Remove(existingProduct);
            ds.ListProduct.Add(product);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Marks a product as deleted.
        /// </summary>
        /// <param name="id">The ID of the product to mark as deleted.</param>
        /// <exception cref="DoesNotExistException">Thrown when the product with the given ID does not exist.</exception>
        public void Delete(int id)
        {
            Product found = ds.ListProduct.FirstOrDefault(item => item?.ID == id)
                ?? throw new DoesNotExistException(id);

            if (found.IsDeleted)
                throw new DoesNotExistException(id);

            Product product = new()
            {
                ID = id,
                Name = found.Name,
                Category = found.Category,
                InStock = found.InStock,
                Price = found.Price,
                IsDeleted = true,
                Path = found.Path,
            };

            Update(product);
        }
        #endregion

        #region GetAll
        /// <summary>
        /// Retrieves all products optionally filtered by a predicate.
        /// </summary>
        /// <param name="filter">Optional filter predicate.</param>
        /// <returns>A collection of products.</returns>
        public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
        {
            if (filter == null)
                return ds.ListProduct;

            return (from Product? product in ds.ListProduct
                    where filter(product)
                    select product)
                    .ToList();
        }
        #endregion

        #region BackInStock
        /// <summary>
        /// Restores a product back to stock by marking it as not deleted.
        /// </summary>
        /// <param name="id">The ID of the product to restore.</param>
        public void BackInStock(int id)
        {
            Product found = ds.ListProduct.FirstOrDefault(item => item?.ID == id)
               ?? throw new DoesNotExistException(id);

            Product product = new()
            {
                ID = id,
                Name = found.Name,
                Category = found.Category,
                InStock = found.InStock,
                Price = found.Price,
                IsDeleted = false,
                Path = found.Path,
            };

            ds.ListProduct.Remove(found);
            ds.ListProduct.Add(product);
        }
        #endregion
    }
}
