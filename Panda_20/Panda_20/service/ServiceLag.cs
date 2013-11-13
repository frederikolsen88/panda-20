using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Panda_20
{

    /// <summary>
    /// Service-klasse der tillader adgang mellem GUI og model-lag jævnført Model/View/Controller-modellen.
    /// 
    /// Implementeret som Singleton-pattern med 'Static Implementation' - se http://goo.gl/Yu5med
    /// </summary>
    public sealed class Service
    {
        
        //---------------<SINGLETON>-------------------- Author: Tobias R.

        private static readonly Service serviceInstance = new Service();

        private Service() { } 

        public static Service GetInstance
        {
            get
            {
                return serviceInstance;
            }
        }

        //--------------</SINGLETON>--------------------
        

    }
}
