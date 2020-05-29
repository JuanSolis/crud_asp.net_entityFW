using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Loginasp.Util;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Loginasp.Models
{
    public class Usuario
    {
        [Required]
        public string correo { get; set; }
        [Required]
        public int edad { get; set; }
        [Required]
        public string nombre_completo { get; set; }
        [Required]
        public string pais { get; set; }
        [Required]
        public string direccion { get; set; }

        public string crearHelper()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", "");
            return path;
        }

        public string crearHash(string password, string helper)
        {
            byte[] encodedPassword = new UTF8Encoding().GetBytes(password+helper); ;

            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

            string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

            return encoded;
        }

        public bool validarUsuario(User usuarioEncontrado, string password)
        {
            string hashPassword = crearHash(password, usuarioEncontrado.helper);

            if (usuarioEncontrado.hash == hashPassword)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }

    [MetadataType(typeof(Usuario))]
    partial class User
    {

    };
}