namespace Sequences.Data.Exceptions
{
#pragma warning disable S3925 // "ISerializable" should be implemented correctly

    [Serializable]
    public class EntityNotFoundException : Exception

    {
        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected EntityNotFoundException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }

#pragma warning restore S3925 // "ISerializable" should be implemented correctly
}