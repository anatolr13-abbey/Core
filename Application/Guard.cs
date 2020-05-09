// See http://ajdotnet.wordpress.com/2009/08/01/posting-guards-guard-classes-explained for background.

using System;
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace Abbey.Core.Application {
    /// <summary>
    /// Helper class to provide parameter checks.
    /// </summary>
    public static class Guard {
        private const string NULL_PARAM = "(no param)";
        private const string NULL_VALUE = "(null)";

        /// <summary>
        /// Checks if a given value is not zero and throws a respective exception otherwise.
        /// </summary>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        [DebuggerStepThrough]
        public static void Zero( int arg, string paramName, string message = null ) {
            if( arg == 0 ) {
                ThrowArgumentOutOfRangeException( paramName, arg, message );
            }
        }

        /// <summary>
        /// Checks if a given value is not negative and throws a respective exception otherwise.
        /// </summary>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        [DebuggerStepThrough]
        public static void Negative( int arg, string paramName, string message = null ) {
            if( arg < 0 ) {
                ThrowArgumentOutOfRangeException( paramName, arg, message );
            }
        }

        /// <summary>
        /// Checks if a given value is not negative or zero and throws a respective exception otherwise.
        /// </summary>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        [DebuggerStepThrough]
        public static void NegativeOrZero( int arg, string paramName, string message = null ) {
            if( arg <= 0 ) {
                ThrowArgumentOutOfRangeException( paramName, arg, message );
            }
        }

        /// <summary>
        /// Checks if a given value is not null and throws a respective exception otherwise.
        /// </summary>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        [DebuggerStepThrough]
        public static void Null( object arg, string paramName, string message = null ) {
            if( arg == null ) {
                ThrowArgumentNullException( paramName, message );
            }
        }

        /// <summary>
        /// Checks if a given value is neither null nor empty and throws a respective exception otherwise.
        /// </summary>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error. </param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is empty.</exception>
        [DebuggerStepThrough]
        public static void NullOrEmpty( string arg, string paramName, string message = null ) {
            if( arg == null ) {
                ThrowArgumentNullException( paramName, message );
            }
            if( arg == string.Empty ) {
                ThrowArgumentOutOfRangeException( paramName, arg, message );
            }
        }

        /// <summary>
        /// Checks if a given value is neither null nor empty and throws a respective exception otherwise.
        /// </summary>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error. </param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is empty.</exception>
        [DebuggerStepThrough]
        public static void Empty( ICollection arg, string paramName, string message = null ) {
            Null( arg, paramName, message );
            if( arg.Count == 0 ) {
                ThrowArgumentOutOfRangeException( paramName, arg, message );
            }
        }

        /// <summary>
        /// Checks if a given value is empty and throws an exception.
        /// </summary>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error. </param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is empty.</exception>
        [DebuggerStepThrough]
        public static void Empty( Guid arg, string paramName, string message = null ) {
            if( arg == Guid.Empty ) {
                ThrowArgumentOutOfRangeException( paramName, arg, message );
            }
        }

        /// <summary>
        /// Checks if a given condition is met and throws an exception.
        /// </summary>
        /// <param name="condition">The condition that has to be true, otherwise an exception is thrown.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <param name="message">A message that describes the error. </param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is empty.</exception>
        [DebuggerStepThrough]
        public static void Condition( bool condition, string paramName, object arg, string message = null ) {
            if( condition ) {
                ThrowArgumentOutOfRangeException( paramName, arg, message );
            }
        }

        /// <summary>
        /// Checks if a given condition is met and throws an exception.
        /// </summary>
        /// <param name="condition">The condition that has to be true, otherwise an exception is thrown.</param>
        /// <param name="typeParamName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error. </param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is empty.</exception>
        [DebuggerStepThrough]
        public static void Condition( bool condition, string typeParamName, string message = null ) {
            if( condition ) {
                ThrowTypeArgumentInvalidException( typeParamName, message );
            }
        }

        /// <summary>
        /// Checks if a given directory path exists and throws a respective exception otherwise.
        /// </summary>
        /// <param name="path">The path that has to exist, otherwise an exception is thrown.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error. </param>
        /// <exception cref="DirectoryNotFoundException">Thrown when the directory does not exist.</exception>
        [DebuggerStepThrough]
        public static void DirectoryExists( string path, string paramName, string message = null ) {
            if( !Directory.Exists( path ) ) {
                ThrowDirectoryNotFoundException( paramName, path, message );
            }
        }

        /// <summary>
        /// report invalid switch value.
        /// </summary>
        /// <param name="variable">The variable.</param>
        /// <param name="value">The value.</param>
        [DebuggerStepThrough]
        public static void InvalidSwitchValue( string variable, object value ) {
            var message = string.Format( "Invalid switch value '{1}' for '{0}'.", variable ?? NULL_PARAM, value ?? NULL_VALUE );
            throw new InvalidOperationException( message );
        }

        private static void ThrowArgumentNullException( string paramName, string message ) {
            if( message == null ) {
                message = "Argument '" + ( paramName ?? NULL_PARAM ) + "' should not be NULL.";
            }
            throw new ArgumentNullException( paramName, message );
        }

        private static void ThrowArgumentOutOfRangeException( string paramName, object value, string message ) {
            if( message == null ) {
                message = "Argument '" + ( paramName ?? NULL_PARAM ) + "' should not be empty.";
            }
#if SILVERLIGHT
            throw new ArgumentOutOfRangeException( paramName, message );
#else
            throw new ArgumentOutOfRangeException( paramName, value ?? NULL_VALUE, message );
#endif
        }

        private static void ThrowTypeArgumentInvalidException( string typeParamName, string message ) {
            if( message == null ) {
                message = $"Type argument '{typeParamName}' is invalid.";
            }
            throw new TypeArgumentInvalidException( message );
        }

        private static void ThrowDirectoryNotFoundException( string paramName, string value, string message ) {
            if( message == null ) {
                message = "Directory specified by argument ‘" + ( paramName ?? NULL_PARAM ) + "’ does not exist or could not be found" + ( value != null
                                                                                                                                               ? $": '{value}'"
                                                                                                                                               : string.Empty ) + ".";
            }
            throw new DirectoryNotFoundException( message );
        }

    }
}