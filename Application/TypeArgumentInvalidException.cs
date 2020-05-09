using System;
using System.Runtime.Serialization;

namespace Abbey.Core.Application {
    public class TypeArgumentInvalidException : SystemException {
        public TypeArgumentInvalidException() {}
        public TypeArgumentInvalidException( SerializationInfo info, StreamingContext context ) : base( info, context ) {}
        public TypeArgumentInvalidException( string message ) : base( message ) {}
        public TypeArgumentInvalidException( string message, Exception innerException ) : base( message, innerException ) {}
    }
}