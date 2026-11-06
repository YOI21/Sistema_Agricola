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
        private void ExportarCsv()
        {
            DataTable tabla = tabControl.SelectedIndex == 1 ? movimientosFiltrados : productosFiltrados;
            if (tabla.Rows.Count == 0)
            {
                MostrarError("No hay datos para exportar.");
                return;
            }

            using SaveFileDialog dialogo = new SaveFileDialog
            {
                Filter = "Archivo CSV (*.csv)|*.csv",
                FileName = $"Reporte_Insumos_{DateTime.Now:yyyyMMdd_HHmm}.csv"
            };

            if (dialogo.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            StringBuilder contenido = new StringBuilder();
            contenido.AppendLine(string.Join(",", tabla.Columns.Cast<DataColumn>()
                .Select(c => Csv(c.ColumnName))));

            foreach (DataRow fila in tabla.Rows)
            {
                contenido.AppendLine(string.Join(",", tabla.Columns.Cast<DataColumn>()
                    .Select(c => Csv(FormatearValor(fila[c])))));
            }

            File.WriteAllText(dialogo.FileName, contenido.ToString(), Encoding.UTF8);
            Process.Start(new ProcessStartInfo(dialogo.FileName) { UseShellExecute = true });
        }

        private void MostrarVistaImpresion()
        {
            if (productosFiltrados.Rows.Count == 0)
            {
                MostrarError("No hay insumos para imprimir.");
                return;
            }

            filaImpresion = 0;
            using PrintDocument documento = new PrintDocument
            {
                DocumentName = "Reporte de Insumos"
            };
            documento.DefaultPageSettings.Landscape = true;
            documento.PrintPage += ImprimirPagina;

            using PrintPreviewDialog vista = new PrintPreviewDialog
            {
                Document = documento,
                Width = 1100,
                Height = 720
            };
            vista.ShowDialog(this);
        }

        private void ImprimirPagina(object sender, PrintPageEventArgs e)
        {
            Graphics? graphics = e.Graphics;
            if (graphics == null)
            {
                e.HasMorePages = false;
                return;
            }

            using Font titulo = new Font("Segoe UI", 15F, FontStyle.Bold);
            using Font subtitulo = new Font("Segoe UI", 9F);
            using Font encabezado = new Font("Segoe UI", 8F, FontStyle.Bold);
            using Font normal = new Font("Segoe UI", 7.5F);
            using Brush tintaTitulo = new SolidBrush(Color.FromArgb(44, 76, 94));
            using Brush tinta = new SolidBrush(Color.Black);
            using Brush fondoEncabezado = new SolidBrush(Color.FromArgb(224, 234, 240));
            using Pen borde = new Pen(Color.FromArgb(185, 193, 199));

            Rectangle margen = e.MarginBounds;
            int y = margen.Top;
            graphics.DrawString("Reporte de Insumos", titulo, tintaTitulo, margen.Left, y);
            y += 30;
            graphics.DrawString(
                $"Generado: {DateTime.Now:dd/MM/yyyy HH:mm} | Registros: {productosFiltrados.Rows.Count:N0}",
                subtitulo,
                tinta,
                margen.Left,
                y);
            y += 28;

            string[] columnas = { "Código", "Nombre", "Tipo", "Cantidad", "Unidad", "Stock mínimo", "Valor stock", "Nivel" };
            int[] anchos = { 80, 210, 115, 82, 80, 92, 100, 78 };
            int altoFila = 24;
            int x = margen.Left;

            for (int i = 0; i < columnas.Length; i++)
            {
                graphics.FillRectangle(fondoEncabezado, x, y, anchos[i], altoFila);
                graphics.DrawRectangle(borde, x, y, anchos[i], altoFila);
                graphics.DrawString(columnas[i], encabezado, tinta, new RectangleF(x + 3, y + 5, anchos[i] - 6, altoFila));
                x += anchos[i];
            }

            y += altoFila;
            while (filaImpresion < productosFiltrados.Rows.Count && y + altoFila < margen.Bottom)
            {
                DataRow fila = productosFiltrados.Rows[filaImpresion];
                string[] valores =
                {
                    Texto(fila, "Código", "CÃ³digo", "Codigo"),
                    Texto(fila, "Nombre"),
                    Texto(fila, "Tipo"),
                    Decimal(fila, "Cantidad").ToString("N2"),
                    Texto(fila, "Unidad"),
                    Decimal(fila, "Stock mínimo", "Stock mÃ­nimo").ToString("N2"),
                    Moneda(Decimal(fila, "Valor stock")),
                    Texto(fila, "Nivel")
                };

                x = margen.Left;
                for (int i = 0; i < columnas.Length; i++)
                {
                    graphics.DrawRectangle(borde, x, y, anchos[i], altoFila);
                    graphics.DrawString(
                        valores[i],
                        normal,
                        tinta,
                        new RectangleF(x + 3, y + 5, anchos[i] - 6, altoFila));
                    x += anchos[i];
                }

                y += altoFila;
                filaImpresion++;
            }

            e.HasMorePages = filaImpresion < productosFiltrados.Rows.Count;
        }

    }
}
