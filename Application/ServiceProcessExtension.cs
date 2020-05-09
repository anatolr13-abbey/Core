using System;
using System.Collections;
using System.Reflection;
using System.ServiceProcess;

namespace Abbey.Core.Application {
    // Based on http://social.technet.microsoft.com/wiki/contents/articles/30957.c-windows-service-in-console-mode-for-debug-and-admin.aspx.
    public static class ServiceProcessHelper {
        /// <summary>
        /// Runs <see cref="service"/> in 1 of 3 modes.
        /// Mode 1: The service .exe file is run from SCM. The .exe functions as a regular Windows service.
        /// Mode 2: The service .exe file is run from command line. The .exe functions as a console.
        /// Mode 3: The service .exe file is run from VS. The .exe simulates Windows service by invoking OnStart and OnStop methods.
        /// </summary>
        /// <param name="service">The service to run.</param>
        /// <param name="args">The command-line arguments to process.</param>
        /// <param name="argsProcessor">The callback to process command-line arguments.</param>
        public static void Run( ServiceBase service, string[] args, Action<string[]> argsProcessor = null ) {
            Guard.Null( service, "service" );

            Run( new[] {service}, args, argsProcessor );
        }

        /// <summary>
        /// Runs <see cref="services"/>.
        /// Mode 1: The service .exe file is run from SCM. The .exe functions as a regular Windows service.
        /// Mode 2: The service .exe file is run from command line. The .exe functions as a console.
        /// Mode 3: The service .exe file is run from VS. The .exe simulates Windows service by invoking OnStart and OnStop methods.
        /// </summary>
        /// <param name="services">The services to run.</param>
        /// <param name="args">The command-line arguments to process.</param>
        /// <param name="argsProcessor">The callback to process command-line arguments.</param>
        public static void Run( ServiceBase[] services, string[] args, Action<string[]> argsProcessor = null ) {
            Guard.Null( services, "services" );

            // In interactive mode?
            if( Environment.UserInteractive ) {
                // In debug mode?
                if( System.Diagnostics.Debugger.IsAttached ) {
                    // Simulate the services execution.
                    RunInteractive( services, args );
                }
                else {
                    argsProcessor?.Invoke(args);
                }
            }
            else {
                // Normal service execution.
                ServiceBase.Run( services );
            }
        }

        /// <summary>
        /// Run services in interactive mode
        /// </summary>
        private static void RunInteractive( ServiceBase[] servicesToRun, IEnumerable args ) {
            Console.WriteLine();
            Console.WriteLine( "Start the services in interactive mode." );
            Console.WriteLine();

            // Start services.
            var onStartMethod = typeof( ServiceBase ).GetMethod( "OnStart", BindingFlags.Instance | BindingFlags.NonPublic );
            if (onStartMethod == null)
            {
                throw new ApplicationException("Failed to find OnStart method.");
            }

            foreach( var service in servicesToRun ) {
                Console.Write( "Starting {0} ... ", service.ServiceName );
                onStartMethod.Invoke( service, new object[] {args} );
                Console.WriteLine( "done." );
            }

            // Waiting...
            Console.WriteLine();
            Console.WriteLine( "Press a key to stop services and finish process..." );
            Console.ReadKey();
            Console.WriteLine();

            // Stop services.
            var onStopMethod = typeof( ServiceBase ).GetMethod( "OnStop", BindingFlags.Instance | BindingFlags.NonPublic );
            if (onStopMethod == null)
            {
                throw new ApplicationException("Failed to find OnStop method.");
            }

            foreach ( var service in servicesToRun ) {
                Console.Write( "Stopping {0} ... ", service.ServiceName );
                onStopMethod.Invoke( service, null );
                Console.WriteLine( "done." );
            }

            Console.WriteLine();
            Console.WriteLine( "All services have been stopped." );

            // Waiting for a key press not to return to VS directly.
            if( System.Diagnostics.Debugger.IsAttached ) {
                Console.WriteLine();
                Console.Write( "=== Press any key to quit ===" );
                Console.ReadKey();
            }
        }
    }
}