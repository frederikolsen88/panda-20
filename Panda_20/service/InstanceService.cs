using System;
using System.Threading;

namespace Panda_20.service
{
    class InstanceService
    {
        // Mutex for allowing checking for a single isntance.
        // A named mutex allows for stack synchronization across threads and processes - a lock, basically..
        // The name of it is just a random generated GUID from createguid.com, it could be "BatmanRules" if we wanted it to, but this should be unique (hopefully).
        private static Mutex singleInstanceMutex = new Mutex(true, "PANDA : {F8830A8E-8081-48CD-A280-B3C9BF7E7F5F}");

        // Property to tell if it is in deed single instance.
        public static bool IsSingleInstance 
        {
            get
            {
                return isSingleInstance();
            }
        }

       

        /// <summary>
        /// Checks if this is the only instance of the application, and returns a bool to indicate whether this is so.
        /// Releases the mutex in case it is abandoned.
        /// </summary>
        private static bool isSingleInstance()
        {
            Boolean result = false;

            try
            {
                // First parameter - Timespan.Zero = mutex waits 0 milliseconds to see if it can gain access to the mutex (so immideately returns false if mutex is locked).
                // Second parameter - exitContext set to true, meaning we can escape the synchronization context before we try to aquire a lock on it (in case we don't get it right away, ie. when it's in use) 
                if (singleInstanceMutex.WaitOne(TimeSpan.Zero, true))
                {
                    // System.Windows.Forms.MessageBox.Show("Aquired mutex sucessfully");
                    result = true;
                }
            }
            // In case the mutex was abandoned; in case the previous instance of the program terminated unexpectedly.
            // Calls the method itself afterwards, to aquire the mutex (our lock, so to speak) again.
            catch (AbandonedMutexException)
            {
                // System.Windows.Forms.MessageBox.Show("Abandoned mutex - now released!");
                singleInstanceMutex.ReleaseMutex();
                result = isSingleInstance();
            }

            
            return result;
        }

        /// <summary>
        /// Releases the mutex, allowing a new instance of the application.
        /// The very last thing the application should do before shutting down!
        /// </summary>
        public static void releaseMutex()
        {
                singleInstanceMutex.ReleaseMutex();
        }
    }
}
