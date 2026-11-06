using System;
using System.Linq;
using System.Security.Cryptography;

namespace Sistema_Agricola.BLL
{
    public static class BLL_PoliticaContrasena
    {
        public const int LongitudMinima = 12;
        public const int LongitudMaxima = 128;
        public const int LongitudTemporal = 16;

        private const string Mayusculas = "ABCDEFGHJKLMNPQRSTUVWXYZ";
        private const string Minusculas = "abcdefghijkmnopqrstuvwxyz";
        private const string Numeros = "23456789";
        private const string Simbolos = "!@$%*-_+=?";
        private const string Todos = Mayusculas + Minusculas + Numeros + Simbolos;

        public static string GenerarTemporal()
        {
            char[] caracteres = new char[LongitudTemporal];
            caracteres[0] = Elegir(Mayusculas);
            caracteres[1] = Elegir(Minusculas);
            caracteres[2] = Elegir(Numeros);
            caracteres[3] = Elegir(Simbolos);

            for (int i = 4; i < caracteres.Length; i++)
            {
                caracteres[i] = Elegir(Todos);
            }

            for (int i = caracteres.Length - 1; i > 0; i--)
            {
                int destino = RandomNumberGenerator.GetInt32(i + 1);
                (caracteres[i], caracteres[destino]) = (caracteres[destino], caracteres[i]);
            }

            return new string(caracteres);
        }

        public static void Validar(string? contrasena)
        {
            if (string.IsNullOrWhiteSpace(contrasena))
            {
                throw new ArgumentException("Ingrese una contrasena.");
            }

            if (contrasena.Length < LongitudMinima || contrasena.Length > LongitudMaxima)
            {
                throw new ArgumentException(
                    $"La contrasena debe tener entre {LongitudMinima} y {LongitudMaxima} caracteres.");
            }

            if (!contrasena.Any(char.IsUpper) ||
                !contrasena.Any(char.IsLower) ||
                !contrasena.Any(char.IsDigit) ||
                !contrasena.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c)))
            {
                throw new ArgumentException(
                    "La contrasena debe incluir mayuscula, minuscula, numero y simbolo.");
            }
        }

        public static void ValidarConfirmacion(string? contrasena, string? confirmacion)
        {
            Validar(contrasena);
            if (!string.Equals(contrasena, confirmacion, StringComparison.Ordinal))
            {
                throw new ArgumentException("La confirmacion de contrasena no coincide.");
            }
        }

        private static char Elegir(string conjunto)
        {
            return conjunto[RandomNumberGenerator.GetInt32(conjunto.Length)];
        }
    }
}
