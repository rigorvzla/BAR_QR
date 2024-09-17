using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;

namespace BAR_QR
{
    public class Impresor
    {
        public static string[] ImpresorasActivas()
        {
            List<string> impr = new List<string>();

            foreach (string impresoras in PrinterSettings.InstalledPrinters)
            {
                impr.Add(impresoras);
            }

            return impr.ToArray();
        }

        public static void Ticket(string NombreImpresora, string imagen, string datos)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += delegate (object sender, PrintPageEventArgs e)
            {
                // Configuración de la fuente y otros estilos
                Font font = new Font("Arial", 11);
                Brush brush = Brushes.Black;

                // Área de impresión más grande
                float leftMargin = 10;
                float topMargin = 10;
                float printWidth = e.PageBounds.Width - 2 * leftMargin;
                float printHeight = e.PageBounds.Height - 2 * topMargin;

                // Texto a imprimir
                string empresaName = datos;

                // Ajustar el área de impresión
                RectangleF printArea = new RectangleF(leftMargin, topMargin, printWidth, printHeight);

                // Dibujar el texto en el área de impresión
                e.Graphics.DrawString(empresaName, font, brush, printArea);

                // Ajuste de la posición para la imagen del código QR
                float y = 100; // Dejar un espacio entre el texto y la imagen

                // Agregar imagen de código QR
                Image qrImage = Image.FromFile(imagen); // Reemplaza con la ruta correcta

                // Dimensiones manuales
                float manualQrWidth = 150;
                float manualQrHeight = 150;

                e.Graphics.DrawImage(qrImage, new RectangleF(leftMargin, y, manualQrWidth, manualQrHeight));
            };

            printDocument.PrinterSettings.PrinterName = NombreImpresora;
            printDocument.Print();
        }
    }
}
