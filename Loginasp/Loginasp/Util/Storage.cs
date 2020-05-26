using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Loginasp.Models;

namespace Loginasp.Util
{
    public class Storage
    {
        private static Storage _instance = null;

        public static Storage Instance
        {
            
            get
            {
                if (_instance == null)
                {
                   return _instance = new Storage();
                }
                else
                {
                    return _instance;
                }
        
            }

        }

        public List<Usuario> listadoUsuarios = new List<Usuario>();

    }
}