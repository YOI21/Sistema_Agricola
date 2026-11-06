using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using ClosedXML.Excel;

namespace Sistema_Agricola.UI
{
    internal sealed class ResumenReporteSiembras
    {
        public int TotalSiembras { get; init; }
        public int EnCrecimiento { get; init; }
        public int Cosechadas { get; init; }
        public decimal CantidadSembrada { get; init; }
        public decimal HectareasSembradas { get; init; }
        public decimal CostoSiembra { get; init; }
        public decimal CostoFertilizacion { get; init; }
        public double PromedioDiasCiclo { get; init; }
        public IReadOnlyList<string> TiposCultivo { get; init; } = Array.Empty<string>();
    }

    internal static class CalculosReporteSiembras
    {
        public static ResumenReporteSiembras Calcular(DataTable datos)
        {
            ArgumentNullException.ThrowIfNull(datos);

            List<DataRow> filasUnicas = ObtenerFilasUnicas(datos);
            int cosechadas = filasUnicas.Count(fila =>
                LeerTexto(fila, "Estado_Cultivo").Equals(
                    "Cosechado", StringComparison.OrdinalIgnoreCase));

            List<double> ciclos = filasUnicas
                .Select(CalcularDiasCiclo)
                .Where(dias => dias > 0)
                .ToList();

            return new ResumenReporteSiembras
            {
                TotalSiembras = filasUnicas.Count,
                Cosechadas = cosechadas,
                EnCrecimiento = filasUnicas.Count - cosechadas,
                CantidadSembrada = Sumar(filasUnicas, "Cantidad"),
                HectareasSembradas = Sumar(filasUnicas, "Hectareas_Sembradas"),
                CostoSiembra = Sumar(filasUnicas, "Costo_Siembra"),
                CostoFertilizacion = Sumar(datos.AsEnumerable(), "Costo_Fertilizacion"),
                PromedioDiasCiclo = ciclos.Count == 0 ? 0 : ciclos.Average(),
                TiposCultivo = filasUnicas
                    .Select(fila => LeerTexto(fila, "Tipo_Cultivo"))
                    .Where(valor => !string.IsNullOrWhiteSpace(valor))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .OrderBy(valor => valor)
                    .ToList()
            };
        }

        public static List<DataRow> ObtenerFilasUnicas(DataTable datos)
        {
            IEnumerable<DataRow> filas = datos.AsEnumerable();
            if (!datos.Columns.Contains("ID_Cultivo"))
            {
                return filas.ToList();
            }

            return filas
                .GroupBy(fila => LeerTexto(fila, "ID_Cultivo"), StringComparer.OrdinalIgnoreCase)
                .Select(grupo => grupo.First())
                .ToList();
        }

        private static decimal Sumar(IEnumerable<DataRow> filas, string columna)
        {
            decimal total = 0;
            foreach (DataRow fila in filas)
            {
                if (!fila.Table.Columns.Contains(columna) || fila.IsNull(columna))
                {
                    continue;
                }

                if (decimal.TryParse(Convert.ToString(fila[columna], CultureInfo.InvariantCulture),
                    NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor))
                {
                    total += valor;
                }
            }

            return total;
        }

        private static double CalcularDiasCiclo(DataRow fila)
        {
            if (!fila.Table.Columns.Contains("Fecha_Plantacion") ||
                !fila.Table.Columns.Contains("Fecha_Cosecha") ||
                fila.IsNull("Fecha_Plantacion") || fila.IsNull("Fecha_Cosecha"))
            {
                return 0;
            }

            DateTime inicio = Convert.ToDateTime(fila["Fecha_Plantacion"], CultureInfo.CurrentCulture);
            DateTime fin = Convert.ToDateTime(fila["Fecha_Cosecha"], CultureInfo.CurrentCulture);
            return Math.Max(0, (fin - inicio).TotalDays);
        }

        private static string LeerTexto(DataRow fila, string columna)
        {
            return fila.Table.Columns.Contains(columna) && !fila.IsNull(columna)
                ? Convert.ToString(fila[columna], CultureInfo.CurrentCulture) ?? string.Empty
                : string.Empty;
        }
    }

    internal static class ExportadorReporteSiembrasExcel
    {
        private static readonly XLColor VerdePrincipal = XLColor.FromArgb(46, 125, 50);
        private static readonly XLColor VerdeClaro = XLColor.FromArgb(226, 239, 218);

        public static void Exportar(DataTable datos, string rutaArchivo, string filtrosAplicados)
        {
            ArgumentNullException.ThrowIfNull(datos);
            if (string.IsNullOrWhiteSpace(rutaArchivo))
            {
                throw new ArgumentException("La ruta del archivo es obligatoria.", nameof(rutaArchivo));
            }

            string? directorio = Path.GetDirectoryName(rutaArchivo);
            if (!string.IsNullOrWhiteSpace(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            ResumenReporteSiembras resumen = CalculosReporteSiembras.Calcular(datos);
            using XLWorkbook libro = new XLWorkbook();
            libro.Properties.Title = "Reporte de siembras";
            libro.Properties.Subject = "Siembras filtradas del Sistema Agricola";
            libro.Properties.Created = DateTime.Now;

            CrearHojaResumen(libro, resumen, filtrosAplicados);
            CrearHojaDetalle(libro, datos);
            CrearHojaControl(libro, datos);
            libro.SaveAs(rutaArchivo);
        }

        private static void CrearHojaResumen(
            XLWorkbook libro,
            ResumenReporteSiembras resumen,
            string filtrosAplicados)
        {
            IXLWorksheet hoja = libro.Worksheets.Add("Resumen");
            hoja.Cell("A1").Value = "REPORTE DE SIEMBRAS";
            hoja.Range("A1:B1").Merge();
            hoja.Range("A1:B1").Style
                .Font.SetBold()
                .Font.SetFontSize(16)
                .Font.SetFontColor(XLColor.White)
                .Fill.SetBackgroundColor(VerdePrincipal);

            hoja.Cell("A3").Value = "Generado";
            hoja.Cell("B3").Value = DateTime.Now;
            hoja.Cell("B3").Style.DateFormat.Format = "dd/MM/yyyy HH:mm";
            hoja.Cell("A4").Value = "Filtros";
            hoja.Cell("B4").Value = filtrosAplicados;

            (string Etiqueta, object Valor)[] indicadores =
            {
                ("Siembras únicas", resumen.TotalSiembras),
                ("En crecimiento", resumen.EnCrecimiento),
                ("Cosechadas", resumen.Cosechadas),
                ("Cantidad sembrada", resumen.CantidadSembrada),
                ("Hectáreas sembradas", resumen.HectareasSembradas),
                ("Costo de siembra", resumen.CostoSiembra),
                ("Costo de fertilización", resumen.CostoFertilizacion),
                ("Ciclo promedio (días)", resumen.PromedioDiasCiclo),
                ("Tipos de cultivo", string.Join(", ", resumen.TiposCultivo))
            };

            int fila = 6;
            foreach ((string etiqueta, object valor) in indicadores)
            {
                hoja.Cell(fila, 1).Value = etiqueta;
                AsignarValor(hoja.Cell(fila, 2), valor);
                fila++;
            }

            hoja.Range(6, 1, fila - 1, 1).Style.Font.SetBold();
            hoja.Range(6, 1, fila - 1, 2).Style.Border
                .OutsideBorder = XLBorderStyleValues.Thin;
            hoja.Range(6, 1, fila - 1, 2).Style.Border
                .InsideBorder = XLBorderStyleValues.Thin;
            hoja.Range(6, 1, fila - 1, 1).Style.Fill.SetBackgroundColor(VerdeClaro);
            hoja.Cell(fila + 1, 1).Value =
                "Control: los totales de siembra se calculan por ID de cultivo único para evitar duplicados por fertilización.";
            hoja.Range(fila + 1, 1, fila + 1, 2).Merge();
            hoja.Range(fila + 1, 1, fila + 1, 2).Style.Alignment.WrapText = true;
            hoja.Range(fila + 1, 1, fila + 1, 2).Style.Font.Italic = true;
            hoja.Column(1).Width = 28;
            hoja.Column(2).Width = 48;
            hoja.SheetView.FreezeRows(1);
        }

        private static void CrearHojaDetalle(XLWorkbook libro, DataTable datos)
        {
            IXLWorksheet hoja = libro.Worksheets.Add("Detalle");
            EscribirTabla(hoja, datos, "TablaDetalleSiembras", agregarTotalesDeFilas: true);
        }

        private static void CrearHojaControl(XLWorkbook libro, DataTable datos)
        {
            string[] columnasControl =
            {
                "ID_Cultivo", "Nombre_Cultivo", "Cantidad", "Hectareas_Sembradas",
                "Costo_Siembra", "Estado_Cultivo"
            };

            DataTable control = new DataTable("ControlTotales");
            foreach (string nombre in columnasControl.Where(datos.Columns.Contains))
            {
                control.Columns.Add(nombre, datos.Columns[nombre]!.DataType);
            }

            foreach (DataRow fila in CalculosReporteSiembras.ObtenerFilasUnicas(datos))
            {
                DataRow nueva = control.NewRow();
                foreach (DataColumn columna in control.Columns)
                {
                    nueva[columna.ColumnName] = fila[columna.ColumnName];
                }
                control.Rows.Add(nueva);
            }

            IXLWorksheet hoja = libro.Worksheets.Add("Control de totales");
            EscribirTabla(hoja, control, "TablaControlSiembras");

            if (control.Rows.Count == 0 || control.Columns.Count == 0)
            {
                return;
            }

            int filaTotal = control.Rows.Count + 3;
            hoja.Cell(filaTotal, 1).Value = "SIEMBRAS ÚNICAS";
            hoja.Cell(filaTotal, 1).Style.Font.SetBold();
            if (control.Columns.Count > 1)
            {
                hoja.Cell(filaTotal, 2).FormulaA1 =
                    $"COUNTA(A2:A{control.Rows.Count + 1})";
                hoja.Cell(filaTotal, 2).Style.Font.SetBold();
                hoja.Cell(filaTotal, 2).Style.Fill.SetBackgroundColor(VerdeClaro);
            }

            for (int columna = 1; columna <= control.Columns.Count; columna++)
            {
                DataColumn dato = control.Columns[columna - 1];
                if (!EsNumerica(dato.DataType) ||
                    dato.ColumnName.Equals("ID_Cultivo", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                string letra = XLHelper.GetColumnLetterFromNumber(columna);
                hoja.Cell(filaTotal, columna).FormulaA1 =
                    $"SUM({letra}2:{letra}{control.Rows.Count + 1})";
                hoja.Cell(filaTotal, columna).Style.Font.SetBold();
                hoja.Cell(filaTotal, columna).Style.Fill.SetBackgroundColor(VerdeClaro);
            }
        }

        private static void EscribirTabla(
            IXLWorksheet hoja,
            DataTable datos,
            string nombreTabla,
            bool agregarTotalesDeFilas = false)
        {
            if (datos.Columns.Count == 0)
            {
                hoja.Cell("A1").Value = "Sin columnas disponibles";
                return;
            }

            for (int columna = 0; columna < datos.Columns.Count; columna++)
            {
                DataColumn dato = datos.Columns[columna];
                hoja.Cell(1, columna + 1).Value = ObtenerTitulo(dato.ColumnName);
            }

            for (int fila = 0; fila < datos.Rows.Count; fila++)
            {
                for (int columna = 0; columna < datos.Columns.Count; columna++)
                {
                    AsignarValor(hoja.Cell(fila + 2, columna + 1), datos.Rows[fila][columna]);
                }
            }

            int ultimaFila = Math.Max(1, datos.Rows.Count + 1);
            IXLRange rango = hoja.Range(1, 1, ultimaFila, datos.Columns.Count);
            rango.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            rango.Style.Border.InsideBorder = XLBorderStyleValues.Hair;

            if (datos.Rows.Count > 0)
            {
                IXLTable tabla = rango.CreateTable(nombreTabla);
                tabla.Theme = XLTableTheme.TableStyleMedium4;
                tabla.ShowAutoFilter = true;

                if (agregarTotalesDeFilas)
                {
                    int filaTotal = datos.Rows.Count + 3;
                    hoja.Cell(filaTotal, 1).Value = "TOTALES DE FILAS";
                    hoja.Cell(filaTotal, 1).Style.Font.SetBold();
                    for (int columna = 1; columna <= datos.Columns.Count; columna++)
                    {
                        DataColumn dato = datos.Columns[columna - 1];
                        if (!EsNumerica(dato.DataType) ||
                            dato.ColumnName.StartsWith("ID_", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        string letra = XLHelper.GetColumnLetterFromNumber(columna);
                        hoja.Cell(filaTotal, columna).FormulaA1 =
                            $"SUM({letra}2:{letra}{datos.Rows.Count + 1})";
                        hoja.Cell(filaTotal, columna).Style.Font.SetBold();
                        hoja.Cell(filaTotal, columna).Style.Fill.SetBackgroundColor(VerdeClaro);
                    }
                }
            }
            else
            {
                rango.Style.Font.SetBold().Font.SetFontColor(XLColor.White);
                rango.Style.Fill.SetBackgroundColor(VerdePrincipal);
            }

            for (int columna = 0; columna < datos.Columns.Count; columna++)
            {
                DataColumn dato = datos.Columns[columna];
                IXLColumn columnaExcel = hoja.Column(columna + 1);
                if (dato.DataType == typeof(DateTime))
                {
                    columnaExcel.Style.DateFormat.Format = "dd/MM/yyyy";
                }
                else if (EsNumerica(dato.DataType))
                {
                    columnaExcel.Style.NumberFormat.Format = "#,##0.00";
                }
            }

            hoja.Columns().AdjustToContents();
            foreach (IXLColumn columna in hoja.ColumnsUsed())
            {
                columna.Width = Math.Min(columna.Width + 2, 38);
            }
            hoja.SheetView.FreezeRows(1);
        }

        private static void AsignarValor(IXLCell celda, object? valor)
        {
            if (valor == null || valor == DBNull.Value)
            {
                celda.Clear();
                return;
            }

            switch (valor)
            {
                case DateTime fecha:
                    celda.Value = fecha;
                    break;
                case decimal numeroDecimal:
                    celda.Value = numeroDecimal;
                    break;
                case double numeroDouble:
                    celda.Value = numeroDouble;
                    break;
                case float numeroFloat:
                    celda.Value = numeroFloat;
                    break;
                case int numeroEntero:
                    celda.Value = numeroEntero;
                    break;
                case long numeroLargo:
                    celda.Value = numeroLargo;
                    break;
                case short numeroCorto:
                    celda.Value = numeroCorto;
                    break;
                case bool booleano:
                    celda.Value = booleano;
                    break;
                default:
                    celda.Value = Convert.ToString(valor, CultureInfo.CurrentCulture) ?? string.Empty;
                    break;
            }
        }

        private static bool EsNumerica(Type tipo)
        {
            TypeCode codigo = Type.GetTypeCode(Nullable.GetUnderlyingType(tipo) ?? tipo);
            return codigo is TypeCode.Byte or TypeCode.SByte or TypeCode.UInt16 or TypeCode.UInt32
                or TypeCode.UInt64 or TypeCode.Int16 or TypeCode.Int32 or TypeCode.Int64
                or TypeCode.Decimal or TypeCode.Double or TypeCode.Single;
        }

        private static string ObtenerTitulo(string nombreColumna)
        {
            Dictionary<string, string> titulos = new(StringComparer.OrdinalIgnoreCase)
            {
                ["ID_Cultivo"] = "ID",
                ["Nombre_Cultivo"] = "Cultivo",
                ["Fecha_Plantacion"] = "Fecha de siembra",
                ["Fecha_Cosecha"] = "Fecha de cosecha",
                ["Hectareas_Sembradas"] = "Hectáreas sembradas",
                ["Costo_Siembra"] = "Costo de siembra",
                ["Costo_Fertilizacion"] = "Costo de fertilización"
            };

            return titulos.TryGetValue(nombreColumna, out string? titulo)
                ? titulo
                : nombreColumna.Replace('_', ' ');
        }
    }
}
