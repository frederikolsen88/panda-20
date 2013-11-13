using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panda_20
{
    class Service
    {

        private Service serviceInstance = null;

        private Service()
        {
        }

        public Service GetInstance()
        {
            if (serviceInstance == null)
            {
                this.serviceInstance = new Service();
            }

            return serviceInstance;
        }

        //Here be methods soon @author Roland
        // TEST 2
    }
}
