using System;
using System.Runtime.Serialization;

// Exceptions of all objects

namespace DO;

[Serializable]
public class DontExitException : Exception
{
    public int ID { get; private set; }
public DontExitException(int id) : base() { ID = id; }
    public DontExitException(int id, string message) : base(message) { ID = id; }
    public DontExitException(int id, string message, Exception inner) : base(message, inner) { ID = id; }
    protected DontExitException(int id, SerializationInfo info, StreamingContext context) : base(info, context) { ID = id; }
    override public string ToString() => "DontExitException: The ID " + ID + " does not exist in the system.";
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