using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;

namespace Sistema_Agricola.DAL
{
    public static class SeguridadContrasena
    {
        private const int Iteraciones = 150000;
        private const int TamanoSalt = 16;
        private const int TamanoHash = 32;

        public static DatosHashContrasena CrearHash(string contrasena)
        {
            if (string.IsNullOrWhiteSpace(contrasena))
            {
                throw new ArgumentException("La contrasena no puede estar vacia.");
            }

            byte[] saltBytes = RandomNumberGenerator.GetBytes(TamanoSalt);
            byte[] hash = CalcularHash(contrasena, saltBytes, Iteraciones);

            return new DatosHashContrasena
            {
                Hash = hash,
                Salt = Convert.ToBase64String(saltBytes),
                Iteraciones = Iteraciones
            };
        }

        public static bool Verificar(string contrasena, byte[] hashGuardado, string salt, int iteraciones)
        {
            if (string.IsNullOrWhiteSpace(contrasena) ||
                hashGuardado == null ||
                hashGuardado.Length == 0 ||
                string.IsNullOrWhiteSpace(salt) ||
                iteraciones <= 0)
            {
                return false;
            }

            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] hashIngresado = CalcularHash(contrasena, saltBytes, iteraciones);
            return CryptographicOperations.FixedTimeEquals(hashIngresado, hashGuardado);
        }

        public static void GuardarHash(
            SqlConnection conexion,
            int idUsuario,
            string contrasena,
            SqlTransaction? transaccion = null,
            bool debeCambiar = false)
        {
            DatosHashContrasena datos = CrearHash(contrasena);
            using SqlCommand cmd = new SqlCommand(
                "Actualizar_Contrasena_Segura",
                conexion,
                transaccion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Hash", SqlDbType.VarBinary, 64).Value = datos.Hash;
            cmd.Parameters.Add("@Salt", SqlDbType.VarChar, 64).Value = datos.Salt;
            cmd.Parameters.Add("@Iteraciones", SqlDbType.Int).Value = datos.Iteraciones;
            cmd.Parameters.Add("@ID_Usuario", SqlDbType.Int).Value = idUsuario;
            cmd.Parameters.Add("@Debe_Cambiar", SqlDbType.Bit).Value = debeCambiar;
            cmd.ExecuteNonQuery();
        }

        public static void EjecutarVerificacionSimulada(string contrasena)
        {
            byte[] salt = Convert.FromBase64String("MDEyMzQ1Njc4OUFCQ0RFRg==");
            byte[] hashEsperado = Convert.FromBase64String(
                "M9ji96p8ZRsW2wzj4ItxQGRxPzYQ6N4u0oXm9FJGdZ8=");
            byte[] hash = CalcularHash(contrasena ?? string.Empty, salt, Iteraciones);
            CryptographicOperations.FixedTimeEquals(hash, hashEsperado);
        }

        private static byte[] CalcularHash(string contrasena, byte[] salt, int iteraciones)
        {
            return Rfc2898DeriveBytes.Pbkdf2(
                contrasena,
                salt,
                iteraciones,
                HashAlgorithmName.SHA256,
                TamanoHash);
        }
    }

    public class DatosHashContrasena
    {
        public byte[] Hash { get; set; } = Array.Empty<byte>();
        public string Salt { get; set; } = string.Empty;
        public int Iteraciones { get; set; }
    }
}
