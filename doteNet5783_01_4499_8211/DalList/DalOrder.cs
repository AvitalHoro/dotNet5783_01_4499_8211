using DalApi;
using DO;

namespace Dal
{
    public class DalOrder : IOrder
    {
        readonly DataSource ds = DataSource.Instance;

        #region Add
        /// <exception cref="AlreadyExistsException"></exception>
        /// <summary>
        /// Adds a new order to the list of orders.
        /// </summary>
        /// <param name="item">The order to add.</param>
        /// <returns>The ID of the added order.</returns>
        public int Add(Order item)
        {
            Order order = ds.ListOrder.FirstOrDefault(o => item.ID == o?.ID);
            if (order != null)
            {
                if (order.IsDeleted)
                    ds.ListOrder.RemoveAll(o => item.ID == o?.ID);
                else
                    throw new AlreadyExistsException(item.ID);
            }

            item.ID = DataSource.Config.NextOrderNumber;
            ds.ListOrder.Add(item);
            return item.ID;
        }
        #endregion

        #region GetById
        /// <exception cref="DoesNotExistException"></exception>
        /// <summary>
        /// Retrieves an order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>The order object.</returns>
        public Order GetById(int id)
        {
            Order order = ds.ListOrder.FirstOrDefault(item => item?.ID == id)
                ?? throw new DoesNotExistException(id);

            if (order.IsDeleted)
                throw new DoesNotExistException(id);

            return order;
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates an existing order identified by its ID.
        /// </summary>
        /// <param name="item">The updated order object.</param>
        public void Update(Order item)
        {
            Order order = ds.ListOrder.FirstOrDefault(found => found?.ID == item.ID)
                ?? throw new DoesNotExistException(item.ID);

            if (order.IsDeleted)
                throw new DoesNotExistException(item.ID);

            ds.ListOrder.Remove(order);
            ds.ListOrder.Add(item);
        }
        #endregion

        #region Delete
        /// <exception cref="DoesNotExistException"></exception>
        /// <summary>
        /// Deletes the order with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        public void Delete(int id)
        {
            Order found = ds.ListOrder.FirstOrDefault(item => item?.ID == id)
                ?? throw new DoesNotExistException(id);

            if (found.IsDeleted)
                throw new DoesNotExistException(id);

            Order order = new()
            {
                ID = id,
                CostumerName = found.CostumerName,
                CostumerEmail = found.CostumerEmail,
                CostumerAdress = found.CostumerAdress,
                OrderDate = found.OrderDate,
                DeliveryDate = found.DeliveryDate,
                ShipDate = found.ShipDate,
                IsDeleted = true
            };

            Update(order);
        }
        #endregion

        #region GetAll
        /// <summary>
        /// Retrieves all orders optionally filtered by a predicate.
        /// </summary>
        /// <param name="filter">Optional filter predicate.</param>
        /// <returns>A collection of orders.</returns>
        public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
        {
            if (filter == null)
                return ds.ListOrder;

            return (from Order? order in ds.ListOrder
                    where filter(order)
                    select order)
                    .ToList();
        }
        #endregion
    }
}
