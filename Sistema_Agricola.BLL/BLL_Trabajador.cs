using System;
using System.Data;
using System.Linq;
using System.Net.Mail;
using Sistema_Agricola.DAL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.BLL
{
    public class BLL_Trabajador
    {
        public static DataTable Listar_Trabajadores()
        {
            return new DAL_Trabajador().Listar_Trabajadores();
        }

        public static DataTable Buscar_Trabajador(string valor)
        {
            return new DAL_Trabajador().Buscar_Trabajador(valor?.Trim() ?? string.Empty);
        }

        public static string Insertar_Trabajador(
            string nombre,
            string apellido,
            string cedula,
            string sexo,
            string email,
            DateTime fechaNac,
            string direccion,
            string telefono,
            string ciudad,
            string sector,
            string posicion,
            decimal sueldo,
            DateTime fechaIngreso)
        {
            BLL_Seguridad.ExigirPermiso(PermisosSistema.TrabajadoresCrear);
            string validacion = ValidarDatos(
                nombre, apellido, cedula, sexo, email, fechaNac, telefono, posicion, sueldo, fechaIngreso);

            if (!string.IsNullOrEmpty(validacion))
            {
                return validacion;
            }

            Entitie_Trabajador trabajador = CrearEntidad(
                0, nombre, apellido, cedula, sexo, email, fechaNac, direccion,
                telefono, ciudad, sector, posicion, sueldo, fechaIngreso);

            string resultado = new DAL_Trabajador().Insertar_Trabajador(trabajador);
            BLL_Seguridad.Auditar(
                PermisosSistema.TrabajadoresCrear,
                "Trabajador",
                "Crear",
                $"Cedula={trabajador.Cedula}; Nombre={trabajador.Nombre} {trabajador.Apellido}; Resultado={resultado}");
            return resultado;
        }

        public static string Actualizar_Trabajador(
            int idTrabajador,
            string nombre,
            string apellido,
            string cedula,
            string sexo,
            string email,
            DateTime fechaNac,
            string direccion,
            string telefono,
            string ciudad,
            string sector,
            string posicion,
            decimal sueldo,
            DateTime fechaIngreso)
        {
            BLL_Seguridad.ExigirPermiso(PermisosSistema.TrabajadoresActualizar);
            if (idTrabajador <= 0)
            {
                return "Debe seleccionar un trabajador válido para actualizar.";
            }

            string validacion = ValidarDatos(
                nombre, apellido, cedula, sexo, email, fechaNac, telefono, posicion, sueldo, fechaIngreso);

            if (!string.IsNullOrEmpty(validacion))
            {
                return validacion;
            }

            Entitie_Trabajador trabajador = CrearEntidad(
                idTrabajador, nombre, apellido, cedula, sexo, email, fechaNac, direccion,
                telefono, ciudad, sector, posicion, sueldo, fechaIngreso);

            string resultado = new DAL_Trabajador().Actualizar_Trabajador(trabajador);
            BLL_Seguridad.Auditar(
                PermisosSistema.TrabajadoresActualizar,
                "Trabajador",
                "Actualizar",
                $"ID={idTrabajador}; Cedula={trabajador.Cedula}; Resultado={resultado}");
            return resultado;
        }

        public static string Eliminar_Trabajador(int idTrabajador)
        {
            BLL_Seguridad.ExigirPermiso(PermisosSistema.TrabajadoresEliminar);
            if (idTrabajador <= 0)
            {
                return "Debe seleccionar un trabajador válido para eliminar.";
            }

            string resultado = new DAL_Trabajador().Eliminar_Trabajador(idTrabajador);
            BLL_Seguridad.Auditar(
                PermisosSistema.TrabajadoresEliminar,
                "Trabajador",
                "Eliminar",
                $"ID={idTrabajador}; Resultado={resultado}");
            return resultado;
        }

        public static Entitie_Trabajador Obtener_Trabajador(int idTrabajador)
        {
            return new DAL_Trabajador().Obtener_Trabajador(idTrabajador);
        }

        private static Entitie_Trabajador CrearEntidad(
            int idTrabajador,
            string nombre,
            string apellido,
            string cedula,
            string sexo,
            string email,
            DateTime fechaNac,
            string direccion,
            string telefono,
            string ciudad,
            string sector,
            string posicion,
            decimal sueldo,
            DateTime fechaIngreso)
        {
            return new Entitie_Trabajador
            {
                ID_Trabajador = idTrabajador,
                Nombre = nombre.Trim(),
                Apellido = apellido.Trim(),
                Cedula = cedula.Trim(),
                Sexo = sexo.Trim(),
                Email = email.Trim().ToLowerInvariant(),
                Fecha_Nac = fechaNac.Date,
                Direccion = direccion?.Trim() ?? string.Empty,
                Telefono = telefono.Trim(),
                Ciudad = ciudad?.Trim() ?? string.Empty,
                Sector = sector?.Trim() ?? string.Empty,
                Posicion = posicion.Trim(),
                Sueldo = sueldo,
                Fecha_Ingreso = fechaIngreso.Date
            };
        }

        private static string ValidarDatos(
            string nombre,
            string apellido,
            string cedula,
            string sexo,
            string email,
            DateTime fechaNac,
            string telefono,
            string posicion,
            decimal sueldo,
            DateTime fechaIngreso)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
            {
                return "El nombre y el apellido son obligatorios.";
            }

            if (nombre.Trim().Length > 50 || apellido.Trim().Length > 50)
            {
                return "El nombre y el apellido no pueden superar los 50 caracteres.";
            }

            cedula = cedula?.Trim() ?? string.Empty;
            if (!EsCedulaValida(cedula))
            {
                return "La cédula no es válida. Debe contener 11 dígitos y un dígito verificador correcto.";
            }

            if (string.IsNullOrWhiteSpace(sexo))
            {
                return "Debe seleccionar el sexo del trabajador.";
            }

            email = email?.Trim() ?? string.Empty;
            if (!EsCorreoValido(email) || email.Length > 100)
            {
                return "Ingrese un correo electrónico válido de hasta 100 caracteres.";
            }

            telefono = telefono?.Trim() ?? string.Empty;
            if (telefono.Length < 10 || telefono.Length > 11 || !telefono.All(char.IsDigit))
            {
                return "El teléfono debe contener entre 10 y 11 dígitos.";
            }

            if (string.IsNullOrWhiteSpace(posicion))
            {
                return "Debe seleccionar el rol laboral del trabajador.";
            }

            if (sueldo <= 0)
            {
                return "El sueldo debe ser mayor que cero.";
            }

            int edad = DateTime.Today.Year - fechaNac.Year;
            if (fechaNac.Date > DateTime.Today.AddYears(-edad))
            {
                edad--;
            }

            if (edad < 18)
            {
                return "El trabajador debe tener al menos 18 años.";
            }

            if (fechaIngreso.Date > DateTime.Today)
            {
                return "La fecha de ingreso no puede ser futura.";
            }

            return string.Empty;
        }

        private static bool EsCorreoValido(string email)
        {
            try
            {
                MailAddress direccion = new MailAddress(email);
                return direccion.Address.Equals(email, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        private static bool EsCedulaValida(string cedula)
        {
            if (cedula.Length != 11 || !cedula.All(char.IsDigit))
            {
                return false;
            }

            int[] pesos = { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };
            int suma = 0;

            for (int i = 0; i < 10; i++)
            {
                int producto = (cedula[i] - '0') * pesos[i];
                suma += producto >= 10 ? producto - 9 : producto;
            }

            int digitoVerificador = (10 - (suma % 10)) % 10;
            return digitoVerificador == cedula[10] - '0';
        }
    }
}
