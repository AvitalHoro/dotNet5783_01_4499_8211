using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class DoesNotExistException : Exception
    {
        public int ID { get; private set; } // Entity ID, to specify which ID is referred to
        public DoesNotExistException(int id) : base() { ID = id; }
        public DoesNotExistException(int id, string message) : base(message) { ID = id; }
        public DoesNotExistException(int id, string message, Exception inner) : base(message, inner) { ID = id; }
        protected DoesNotExistException(int id, SerializationInfo info, StreamingContext context) : base(info, context) { ID = id; }
        override public string ToString() => "DoesNotExistException: The ID " + ID + " does not exist in the system.";
    }

    [Serializable]
    public class InvalidIDException : Exception
    {
        public int ID { get; private set; } // Entity ID, to specify which ID is referred to
        public InvalidIDException(int id) : base() { ID = id; }
        public InvalidIDException(int id, string message) : base(message) { ID = id; }
        public InvalidIDException(int id, string message, Exception inner) : base(message, inner) { ID = id; }
        protected InvalidIDException(int id, SerializationInfo info, StreamingContext context) : base(info, context) { ID = id; }
        override public string ToString() => "InvalidIDException: The ID " + ID + " invalid";
    }

    [Serializable]
    public class InvalidPriceException : Exception
    {
        public int ID { get; private set; } // Entity ID, to specify which ID is referred to
        public InvalidPriceException(int id) : base() { ID = id; }
        public InvalidPriceException(int id, string message) : base(message) { ID = id; }
        public InvalidPriceException(int id, string message, Exception inner) : base(message, inner) { ID = id; }
        protected InvalidPriceException(int id, SerializationInfo info, StreamingContext context) : base(info, context) { ID = id; }
        override public string ToString() => "InvalidPriceException: The price of the product with the ID " + ID + " invalid";
    }

    [Serializable]
    public class OutOfStockException : Exception
    {
        public int ID { get; private set; } // Entity ID, to specify which ID is referred to
        public OutOfStockException(int id) : base() { ID = id; }
        public OutOfStockException(int id, string message) : base(message) { ID = id; }
        public OutOfStockException(int id, string message, Exception inner) : base(message, inner) { ID = id; }
        protected OutOfStockException(int id, SerializationInfo info, StreamingContext context) : base(info, context) { ID = id; }
        override public string ToString() => "OutOfStockException: The product with the ID " + ID + " out of stock";
    }

    [Serializable]
    public class OrderAlreadyShippedExecption : Exception
    {
        public int ID { get; private set; } // Entity ID, to specify which ID is referred to
        public OrderAlreadyShippedExecption(int id) : base() { ID = id; }
        public OrderAlreadyShippedExecption(int id, string message) : base(message) { ID = id; }
        public OrderAlreadyShippedExecption(int id, string message, Exception inner) : base(message, inner) { ID = id; }
        protected OrderAlreadyShippedExecption(int id, SerializationInfo info, StreamingContext context) : base(info, context) { ID = id; }
        override public string ToString() => "OrderAlreadyShippedExecption: The order with the ID " + ID + " already shipped";
    }

    [Serializable]
    public class OrderAlreadyDeliveredExecption : Exception
    {
        public int ID { get; private set; } // Entity ID, to specify which ID is referred to
        public OrderAlreadyDeliveredExecption(int id) : base() { ID = id; }
        public OrderAlreadyDeliveredExecption(int id, string message) : base(message) { ID = id; }
        public OrderAlreadyDeliveredExecption(int id, string message, Exception inner) : base(message, inner) { ID = id; }
        protected OrderAlreadyDeliveredExecption(int id, SerializationInfo info, StreamingContext context) : base(info, context) { ID = id; }
        override public string ToString() => "OrderAlreadyDeliveredExecption: The order with the ID " + ID + " already delivered";
    }

    [Serializable]
    public class NoNameException : Exception
    {
        public int ID { get; private set; } // Entity ID, to specify which ID is referred to
        public NoNameException(int id) : base() { ID = id; }
        public NoNameException(int id, string message) : base(message) { ID = id; }
        public NoNameException(int id, string message, Exception inner) : base(message, inner) { ID = id; }
        protected NoNameException(int id, SerializationInfo info, StreamingContext context) : base(info, context) { ID = id; }
        override public string ToString() => "NoNameException: The name of the product with the ID " + ID + " doesn't exist";
    }

    [Serializable]
    public class ProductExistInOrderException : Exception
    {
        public int ID { get; private set; } // Entity ID, to specify which ID is referred to
        public ProductExistInOrderException(int id) : base() { ID = id; }
        public ProductExistInOrderException(int id, string message) : base(message) { ID = id; }
        public ProductExistInOrderException(int id, string message, Exception inner) : base(message, inner) { ID = id; }
        protected ProductExistInOrderException(int id, SerializationInfo info, StreamingContext context) : base(info, context) { ID = id; }
        override public string ToString() => "ProductExistInOrderException: The product with the ID " + ID + " cannot be deleted because it has been ordered";
    }

    [Serializable]
    public class AlreadyExistsException : Exception
    {
        public int ID { get; private set; } // Entity ID, to specify which ID is referred to
        public AlreadyExistsException(int id) : base() { ID = id; }
        public AlreadyExistsException(int id, string message) : base(message) { ID = id; }
        public AlreadyExistsException(int id, string message, Exception inner) : base(message, inner) { ID = id; }
        protected AlreadyExistsException(int id, SerializationInfo info, StreamingContext context) : base(info, context) { ID = id; }
        override public string ToString() => "AlreadyExistsException: The ID " + ID + " already exists in the system.";
    }

    [Serializable]
    public class ProductNotExistInCartException : Exception
    {
        public int ID { get; private set; } // Entity ID, to specify which ID is referred to
        public ProductNotExistInCartException(int id) : base() { ID = id; }
        public ProductNotExistInCartException(int id, string message) : base(message) { ID = id; }
        public ProductNotExistInCartException(int id, string message, Exception inner) : base(message, inner) { ID = id; }
        protected ProductNotExistInCartException(int id, SerializationInfo info, StreamingContext context) : base(info, context) { ID = id; }
        override public string ToString() => "ProductNotExistInCartException: The product with the ID " + ID + " does not exist in your cart.";
    }

    [Serializable]
    public class NoCostumerNameException : Exception
    {
        public NoCostumerNameException() : base() { }
        public NoCostumerNameException(string message) : base(message) { }
        public NoCostumerNameException(string message, Exception inner) : base(message, inner) { }
        protected NoCostumerNameException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString() => "NoCostumerNameException: We don't have your name";
    }

    [Serializable]
    public class NoCostumerAdressException : Exception
    {
        public NoCostumerAdressException() : base() { }
        public NoCostumerAdressException(string message) : base(message) { }
        public NoCostumerAdressException(string message, Exception inner) : base(message, inner) { }
        protected NoCostumerAdressException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString() => "NoCostumerAdressException: We don't have your address";
    }

    [Serializable]
    public class NoCostumerEmailException : Exception
    {
        public NoCostumerEmailException() : base() { }
        public NoCostumerEmailException(string message) : base(message) { }
        public NoCostumerEmailException(string message, Exception inner) : base(message, inner) { }
        protected NoCostumerEmailException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString() => "NoCostumerEmailException: We don't have your email";
    }

}

   
