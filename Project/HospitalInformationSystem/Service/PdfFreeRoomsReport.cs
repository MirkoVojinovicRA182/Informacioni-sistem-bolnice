using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using HospitalInformationSystem.Controller;
using Model;
using System.IO;

namespace HospitalInformationSystem.Service
{
    class PdfFreeRoomsReport : ReportGenerator
    {
        protected override void SpecificReport(ref Document document, DateTime startTime, DateTime endTime,ref PdfPTable table)
        {
            string name = "Izvestaj.pdf";
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(name, FileMode.Create));
            table = CreateTable();
            table.AddCell("");

            GenerateRoomsHeader(ref table);

            for(DateTime timeIterator = startTime; DateTime.Compare(timeIterator, endTime) < 0; SecretaryUtil.NextValidTime(ref timeIterator))
            {
                if (timeIterator.Hour <= 7 || timeIterator.Hour >= 20)
                {
                    Console.WriteLine(timeIterator.ToString());
                    continue;
                }
                GenerateTimeStampRow(ref table,ref timeIterator);
            }
        }

        protected override PdfPTable CreateTable()
        {
            PdfPTable table = new PdfPTable(RoomController.GetInstance().GetRooms().Count + 1);
            return table;
        }

        public void GenerateRoomsHeader(ref PdfPTable table)
        {
            foreach (Room room in RoomController.GetInstance().GetRooms())
            {
                PdfPCell cell = new PdfPCell(new Phrase(room.Name, FontFactory.GetFont("Arial", 13, Font.BOLD)));
                table.AddCell(cell);
            }
        }

        public void GenerateTimeStampRow(ref PdfPTable table, ref DateTime timeIterator)
        {
            table.AddCell(timeIterator.ToString());
            foreach (Room room in RoomController.GetInstance().GetRooms())
            {
                if (SecretaryUtil.IsRoomFree(room, timeIterator))
                {
                    PdfPCell cell = new PdfPCell(new Phrase("Slobodno"));
                    cell.BackgroundColor = BaseColor.GREEN;
                    table.AddCell(cell);
                }
                else
                {
                    PdfPCell cell = new PdfPCell(new Phrase("Zauzeto"));
                    cell.BackgroundColor = BaseColor.RED;
                    table.AddCell(cell);
                }
            }
        }
    }
}
