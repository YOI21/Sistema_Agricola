using Sistema_Agricola.BLL;
using Sistema_Agricola.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sistema_Agricola.UI
{
    public partial class FrmReporteInsumos : Form
    {
        private static string Texto(DataRow fila, params string[] columnas)
        {
            foreach (string columna in columnas)
            {
                if (fila.Table.Columns.Contains(columna) && fila[columna] != DBNull.Value)
                {
                    return Convert.ToString(fila[columna]) ?? string.Empty;
                }
            }

            return string.Empty;
        }

        private static int Entero(DataRow fila, params string[] columnas)
        {
            foreach (string columna in columnas)
            {
                if (fila.Table.Columns.Contains(columna) && fila[columna] != DBNull.Value)
                {
                    return Convert.ToInt32(fila[columna], CultureInfo.InvariantCulture);
                }
            }

            return 0;
        }

        private static decimal Decimal(DataRow fila, params string[] columnas)
        {
            foreach (string columna in columnas)
            {
                if (fila.Table.Columns.Contains(columna) && fila[columna] != DBNull.Value)
                {
                    return Convert.ToDecimal(fila[columna], CultureInfo.InvariantCulture);
                }
            }

            return 0;
        }

        private static string Normalizar(string valor)
        {
            return (valor ?? string.Empty).Trim().ToUpperInvariant();
        }

        private static string Moneda(decimal valor)
        {
            return $"RD$ {valor:N2}";
        }

        private static string Abreviar(string valor, int largo)
        {
            if (string.IsNullOrWhiteSpace(valor) || valor.Length <= largo)
            {
                return valor;
            }

            return valor.Substring(0, largo - 1) + ".";
        }

        private static string Csv(string valor)
        {
            return "\"" + (valor ?? string.Empty).Replace("\"", "\"\"") + "\"";
        }

        private static string FormatearValor(object valor)
        {
            if (valor == DBNull.Value || valor == null)
            {
                return string.Empty;
            }

            if (valor is DateTime fecha)
            {
                return fecha.ToString("dd/MM/yyyy");
            }

            if (valor is decimal dec)
            {
                return dec.ToString("N2");
            }

            return Convert.ToString(valor) ?? string.Empty;
        }

        private static Color ColorNivel(string nivel)
        {
            return nivel switch
            {
                "Agotado" => Color.FromArgb(190, 70, 58),
                "Bajo" => Color.FromArgb(220, 145, 45),
                "Normal" => Color.FromArgb(45, 125, 94),
                "Inactivo" => Color.FromArgb(116, 124, 132),
                _ => Color.FromArgb(52, 101, 164)
            };
        }

        private static void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Reporte de Insumos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
