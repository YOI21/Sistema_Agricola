using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Agricola.UI.Utilidades
{
    public static class ValidacionCedula
    {
        public static bool EsCedulaValida(string cedula)
        {
            // Verificar que la cédula tenga 11 dígitos
            if (string.IsNullOrEmpty(cedula) || cedula.Length != 11)
                return false;

            // Verificar que solo contenga dígitos
            foreach (char c in cedula)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            // Algoritmo de validación de cédula dominicana
            int suma = 0;
            int[] pesoDigito = { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };

            // Se toman los primeros 10 dígitos para calcular el dígito verificador
            for (int i = 0; i < 10; i++)
            {
                int digito = int.Parse(cedula[i].ToString());
                int valorPosicion = digito * pesoDigito[i];

                // Si el resultado es de dos dígitos, se suman entre sí
                suma += (valorPosicion >= 10) ? (valorPosicion - 9) : valorPosicion;
            }

            // El dígito verificador es el complemento a 10 del módulo 10 de la suma
            int digitoVerificador = (10 - (suma % 10)) % 10;

            // Comparar con el último dígito de la cédula
            int ultimoDigitoCedula = int.Parse(cedula[10].ToString());

            return digitoVerificador == ultimoDigitoCedula;
        }
    }
}
