using System.Runtime.Serialization;

namespace Sequences.Data.Exceptions
{
#pragma warning disable S3925 // "ISerializable" should be implemented correctly

    public class EntityAlreadyExistException : Exception
    {
        public EntityAlreadyExistException()
        {
        }

        public EntityAlreadyExistException(string? message) : base(message)
        {
        }

        public EntityAlreadyExistException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EntityAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

#pragma warning restore S3925 // "ISerializable" should be implemented correctly
}