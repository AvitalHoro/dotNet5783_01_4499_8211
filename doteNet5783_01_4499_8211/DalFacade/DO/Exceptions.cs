using System;
using System.Runtime.Serialization;

// Exceptions of all objects

namespace DO;

[Serializable]
public class DontExistException : Exception
{
    public int ID { get; private set; }
public DontExistException(int id) : base() { ID = id; }
    public DontExistException(int id, string message) : base(message) { ID = id; }
    public DontExistException(int id, string message, Exception inner) : base(message, inner) { ID = id; }
    protected DontExistException(int id, SerializationInfo info, StreamingContext context) : base(info, context) { ID = id; }
    override public string ToString() => "DontExistException: The ID " + ID + " does not exist in the system.";
}

[Serializable]
public class AlreadyExistsException : Exception
{
    public int ID { get; private set; }
    public AlreadyExistsException(int id) : base() { ID = id; }
    public AlreadyExistsException(int id, string message) : base(message) { ID = id; }
    public AlreadyExistsException(int id, string message, Exception inner) : base(message, inner) { ID = id; }
    protected AlreadyExistsException(int id, SerializationInfo info, StreamingContext context) : base(info, context) { ID = id; }
    override public string ToString() => "AlreadyExistsException: The ID " + ID + " already exist in the system.";
}