using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalInformationSystem.Controller;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Model;

namespace HospitalInformationSystem.Service
{
    abstract class ReportGenerator
    {
        public void GenerateReport(DateTime startTime, DateTime endTime)
        {
            Document document = new Document(PageSize.A4, 10, 10, 10, 10);
            PdfPTable table = CreateTable();

            SpecificReport(ref document, startTime, endTime, ref table);

            document.Open();
            document.Add(table);
            document.Close();
        }

        abstract protected void SpecificReport(ref Document document, DateTime startTime, DateTime endTime,ref PdfPTable table);

        protected virtual PdfPTable CreateTable() { return null; }


        //public static void GenerateReportOriginal(DateTime startTime, DateTime endTime)
        //{
        //    Document document = new Document(PageSize.A4, 10, 10, 10, 10);
        //    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("Izvestaj - " + DateTime.Now.ToString() + ".pdf", FileMode.Create));
        //    document.Open();
        //    PdfPTable table = new PdfPTable(RoomController.GetInstance().GetRooms().Count + 1);
        //    table.AddCell("");

        //    foreach (Room room in RoomController.GetInstance().GetRooms())
        //    {
        //        table.AddCell(room.Name);
        //    }

        //    for (DateTime timeIterator = startTime; DateTime.Compare(timeIterator, endTime) < 0; SecretaryUtil.NextValidTime(ref timeIterator))
        //    {
        //        if (timeIterator.Hour <= 7 || timeIterator.Hour >= 20)
        //        {
        //            Console.WriteLine(timeIterator.ToString());
        //            continue;
        //        }

        //        table.AddCell(timeIterator.ToString());
        //        foreach (Room room in RoomController.GetInstance().GetRooms())
        //        {
        //            if (SecretaryUtil.IsRoomFree(room, timeIterator))
        //            {
        //                PdfPCell cell = new PdfPCell(new Phrase("Slobodno"))
        //                cell.BackgroundColor = BaseColor.GREEN;
        //                table.AddCell(cell);
        //            }
        //            else
        //            {
        //                PdfPCell cell = new PdfPCell(new Phrase("Zauzeto"))
        //                cell.BackgroundColor = BaseColor.RED;
        //                table.AddCell(cell);
        //            }
        //        }


        //    }
        //    document.Add(table);
        //    document.Close();
        //}
    }
}
