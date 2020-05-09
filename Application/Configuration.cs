using System;
using System.Configuration;

namespace Abbey.Core.Application {
    /// <summary>
    /// Facilitates getting settings from a configuration file.
    /// </summary>
    public static class Configuration {

        /// <summary>
        /// Gets a string value from the configuration file corresponding to the <paramref name="key"/>. If the key is not present in the configuration file, a <exception cref="ConfigurationErrorsException"></exception> is thrown.
        /// </summary>
        /// <param name="key">The key corresponding to the value being obtained.</param>
        /// <returns>A value corresponding to the <paramref name="key"/>.</returns>
        public static string GetRequiredValue( string key ) {
            return GetRequiredValue( key, value => value );
        }

        /// <summary>
        /// Gets a value of <typeparamref name="T"/> type from the configuration file corresponding to the <paramref name="key"/>. If the key is not present in the configuration file, a <exception cref="ConfigurationErrorsException"></exception> is thrown.
        /// </summary>
        /// <param name="key">The key corresponding to the value being obtained.</param>
        /// <returns>A value corresponding to the <paramref name="key"/>.</returns>
        public static T GetRequiredValue<T>( string key ) where T : IConvertible {
            return GetRequiredValue( key, value => ( T )Convert.ChangeType( value, typeof( T ) ) );
        }

        /// <summary>
        /// Gets a value of <typeparamref name="T"/> type from the configuration file corresponding to the <paramref name="key"/>. If the key is not present in the configuration file, a <exception cref="ConfigurationErrorsException"></exception> is thrown.
        /// </summary>
        /// <param name="key">The key corresponding to the value being obtained.</param>
        /// <param name="parseFunc">A method which converts string to <typeparamref name="T"/> type.</param>
        /// <returns>A value corresponding to the <paramref name="key"/>.</returns>
        public static T GetRequiredValue<T>( string key, Func<string, T> parseFunc ) {
            var setting = ConfigurationManager.AppSettings[key];
            if( setting == null ) {
                throw new ConfigurationErrorsException($"Requred configuration entry is missing: {key}.");
            }
            return parseFunc( setting );
        }

        /// <summary>
        /// Gets a string value from the configuration file corresponding to the <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The key corresponding to the value being obtained.</param>
        /// <param name="defaultValue">The value to return if the key is not present in the configuration file.</param>
        /// <returns>A value corresponding to the <paramref name="key"/>.</returns>
        public static string GetOptionalValue( string key, string defaultValue = default ) {
            return GetOptionalValue( key, value => value, defaultValue );
        }

        /// <summary>
        /// Gets a value from the configuration file corresponding to the <paramref name="key"/>. If the key is not present in the configuration file, a value equal to <paramref name="defaultValue"/>, if specified, or default value for the <typeparamref name="T"/> type, if not specified, is returned.
        /// </summary>
        /// <param name="key">The key corresponding to the value being obtained.</param>
        /// <param name="defaultValue">The value to return if the key is not present in the configuration file.</param>
        /// <returns>A value corresponding to the <paramref name="key"/>. If the key is not present in the configuration file, a value equal to <paramref name="defaultValue"/>, if specified, or default value for the <typeparamref name="T"/> type, if not specified, is returned.</returns>
        public static T GetOptionalValue<T>( string key, T defaultValue = default ) where T : IConvertible {
            return GetOptionalValue( key, value => ( T )Convert.ChangeType( value, typeof( T ) ), defaultValue );
        }

        /// <summary>
        /// Gets a value from the configuration file corresponding to the <paramref name="key"/>. If the key is not present in the configuration file, a value equal to <paramref name="defaultValue"/>, if specified, or default value for the <typeparamref name="T"/> type, if not specified, is returned.
        /// </summary>
        /// <param name="key">The key corresponding to the value being obtained.</param>
        /// <param name="parseFunc">A method which converts string to <typeparamref name="T"/> type.</param>
        /// <param name="defaultValue">The value to return if the key is not present in the configuration file.</param>
        /// <returns>A value corresponding to the <paramref name="key"/>. If the key is not present in the configuration file, a value equal to <paramref name="defaultValue"/>, if specified, or default value for the <typeparamref name="T"/> type, if not specified, is returned.</returns>
        public static T GetOptionalValue<T>( string key, Func<string, T> parseFunc, T defaultValue = default ) {
            var setting = ConfigurationManager.AppSettings[key];
            return setting == null
                ? defaultValue
                : parseFunc( setting );
        }
    }
}