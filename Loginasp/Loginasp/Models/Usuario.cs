using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Loginasp.Util;

namespace Loginasp.Models
{
    public class Usuario
    {
        public string nombreUsuario { get; set; }
        public string password { get; set; }


        public bool verificarUsuario()
        {
            if (Storage.Instance.listadoUsuarios.Exists( x => x.nombreUsuario.Equals(nombreUsuario)))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

    }
   
}