using DalApi;
using DO;
using System.Data;

namespace Dal
{
    public class DalOrderItem : IOrderItem
    {
        readonly DataSource ds = DataSource.Instance;

        #region Add
        /// <summary>
        /// Adds a new order item to the list.
        /// </summary>
        /// <param name="item">The order item to add.</param>
        /// <returns>The ID of the added order item.</returns>
        public int Add(OrderItem item)
        {
            OrderItem it = ds.ListOrderItem.FirstOrDefault(i => item.ID == i?.ID);
            if (it != null)
                ds.ListOrderItem.RemoveAll(i => item.ID == i?.ID);

            item.ID = DataSource.Config.NextOrderItemNumber;
            ds.ListOrderItem.Add(item);
            return item.ID;
        }
        #endregion

        #region GetById
        /// <exception cref="DoesNotExistException"></exception>
        /// <summary>
        /// Retrieves an order item by its ID.
        /// </summary>
        /// <param name="id">The ID of the order item to retrieve.</param>
        /// <returns>The order item object.</returns>
        public OrderItem GetById(int id)
        {
            OrderItem item = ds.ListOrderItem.FirstOrDefault(item => item?.ID == id)
                ?? throw new DoesNotExistException(id);
            return item;
        }
        #endregion

        #region Update
        /// <exception cref="DoesNotExistException"></exception>
        /// <summary>
        /// Updates an existing order item.
        /// </summary>
        /// <param name="item">The updated order item object.</param>
        public void Update(OrderItem item)
        {
            OrderItem temp = ds.ListOrderItem.FirstOrDefault(found => found?.ID == item.ID)
                ?? throw new DoesNotExistException(item.ID);
            ds.ListOrderItem.Remove(temp);
            ds.ListOrderItem.Add(item);
        }
        #endregion

        #region Delete
        /// <exception cref="DoesNotExistException"></exception>
        /// <summary>
        /// Deletes an order item by its ID.
        /// </summary>
        /// <param name="id">The ID of the order item to delete.</param>
        public void Delete(int id)
        {
            OrderItem item = ds.ListOrderItem.FirstOrDefault(item => item?.ID == id)
                ?? throw new DoesNotExistException(id);
            ds.ListOrderItem.Remove(item);
        }
        #endregion

        #region GetAll
        /// <summary>
        /// Retrieves all order items optionally filtered by a predicate.
        /// </summary>
        /// <param name="filter">Optional filter predicate.</param>
        /// <returns>A collection of order items.</returns>
        public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)
        {
            if (filter == null)
                return ds.ListOrderItem;

            return (from OrderItem? orderItem in ds.ListOrderItem
                    where filter(orderItem)
                    select orderItem)
                    .ToList();
        }
        #endregion

        #region GetItem
        /// <exception cref="DoesNotExistException"></exception>
        /// <summary>
        /// Retrieves an order item by order ID and product ID.
        /// </summary>
        /// <param name="IdOrder">The ID of the order.</param>
        /// <param name="IdProduct">The ID of the product.</param>
        /// <returns>The order item matching the IDs.</returns>
        public OrderItem GetItem(int IdOrder, int IdProduct)
        {
            return ds.ListOrderItem.FirstOrDefault(item => (item?.OrderID == IdOrder) &&
                                                   (item?.ProductID == IdProduct))
                ?? throw new DoesNotExistException(IdOrder);
        }
        #endregion
    }
}
