using System;
using System.Runtime.Serialization;

// Exceptions of all objects

namespace DO;

[Serializable]
public class DoesNotExistException : Exception
{
    public int ID { get; private set; }
    public DoesNotExistException(int id) : base() { ID = id; }
    public DoesNotExistException(int id, string message) : base(message) { ID = id; } 
    public DoesNotExistException(int id, string message, Exception inner) : base(message, inner) { ID = id; }
    protected DoesNotExistException(int id, SerializationInfo info, StreamingContext context) : base(info, context) { ID = id; }
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


[Serializable]
public class RuningNumberDoesNotExistException : Exception
{
    public string TypeNumber { get;  private set; } 
    public RuningNumberDoesNotExistException(string type) : base() { TypeNumber = type; }
    public RuningNumberDoesNotExistException(string type, string message) : base(message) { TypeNumber = type; }
    public RuningNumberDoesNotExistException(string type, string message, Exception inner) : base(message, inner) { TypeNumber = type; }
    protected RuningNumberDoesNotExistException(string type, SerializationInfo info, StreamingContext context) : base(info, context) { TypeNumber = type; }
    override public string ToString() => "RuningNumberDoesNotExistException: The " + TypeNumber + " does not exist in the system.";
}


[Serializable]
public class LoadingException : Exception
{
    string filePath;
    public LoadingException() : base() { filePath = ""; }
    public LoadingException(string message) : base(message) { filePath = ""; }
    public LoadingException(string message, Exception inner) : base(message, inner) { filePath = ""; }

    public LoadingException(string path, string messege, Exception inner) => filePath = path;
    protected LoadingException(SerializationInfo info, StreamingContext context) : base(info, context) { filePath = ""; }
}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}

