using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook;

namespace Panda_20.model
{
    public sealed class Model
    {
        private static readonly Model ModelInstance = new Model();

        private Model()
        {
            
        } 

        public static Model Instance
        {
            get
            {
                return ModelInstance;
            }
        } 
    }
}
