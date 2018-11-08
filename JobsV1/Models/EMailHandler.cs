using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

namespace JobsV1.Models
{
    public class EMailHandler
    {

        private JobDBContainer db = new JobDBContainer();

        public string SendMail(int jobId, string renterMail, string mailType, string renterName) {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.realwheelsdavao.com"); //smtp server
                
                MailDefinition md = new MailDefinition();
                md.From = "admin@realwheelsdavao.com";      //sender mail
                md.IsBodyHtml = true;                       //set true to enable use of html tags 
                md.Subject = "RealWheels Reservation";   //mail title

                ListDictionary replacements = new ListDictionary();
                replacements.Add("{name}", "Martin");
                replacements.Add("{unit}", "Honda City");
                replacements.Add("{tour}", "City Tour");
                replacements.Add("{type}", "w/ Driver");
                replacements.Add("{days}", "2");
                replacements.Add("{total}", "5500");

                string body,message;

                //get job details

                md.Subject = renterName + ": NEW RealWheels Reservation";   //mail title

                if (mailType == "ADMIN")
                {
                    //mail content for admin
                    //id = carReservation Id

                    CarReservation reserve = db.CarReservations.Find(jobId);

                    message = "A NEW Reservation Inquiry has been made. Please follow the link for the reservation details. <a href='http://localhost:50382/reservation/" + jobId + "/" + reserve.DtTrx.Month + "/" + reserve.DtTrx.Day + "/" + reserve.DtTrx.Year + "/" + reserve.RenterName + "' " +
                    " style='display:block;background-color:dodgerblue;margin:20px;padding:20px;text-decoration:none;font-weight:bolder;font-size:300;color:white;border-radius:3px;'> Reservation Details  </a> ";
                }
                else if(mailType == "PAYMENT-SUCCESS")
                {
                    md.Subject = renterName + ": Paypal Reservation SUCCESS";   //mail title

                    CarReservation reserve = db.CarReservations.Find(jobId);

                    //mail content for successful payment
                    //id = carReservation Id
                    message = "Paypal Payment is SUCCESS. Please follow the link for the reservation details. <a href='https://realwheelsdavao.com/reservation/" + jobId + "/" + reserve.DtTrx.Month + "/" + reserve.DtTrx.Day + "/" + reserve.DtTrx.Year + "/" + reserve.RenterName + "' " +
                    " style='display:block;background-color:dodgerblue;margin:20px;padding:20px;text-decoration:none;font-weight:bolder;font-size:300;color:white;border-radius:3px;'> Reservation Details </a> ";
                }
                else if (mailType == "PAYMENT-DENIED")
                {
                    md.Subject = renterName + ": Paypal Reservation DENIED";   //mail title

                    CarReservation reserve = db.CarReservations.Find(jobId);

                    //mail content for denied payment
                    //id = carReservation Id
                    message = "Paypal Payment have been DENIED. Please follow the link for the reservation details. <a href='https://realwheelsdavao.com/reservation/" + jobId + "/" + reserve.DtTrx.Month + "/" + reserve.DtTrx.Day + "/" + reserve.DtTrx.Year + "/" + reserve.RenterName + "' " +
                    " style='display:block;background-color:dodgerblue;margin:20px;padding:20px;text-decoration:none;font-weight:bolder;font-size:300;color:white;border-radius:3px;'> Reservation Details </a> ";
                }
                else if (mailType == "PAYMENT-PENDING")
                {
                    md.Subject = renterName + ": Paypal Reservation PENDING";   //mail title

                    CarReservation reserve = db.CarReservations.Find(jobId);

                    //mail content for pending payment
                    //id = carReservation Id
                    message = "Paypal Payment has been sent. Please follow the link for the reservation details. <a href='http://localhost:50382/reservation/" + jobId + "/" + reserve.DtTrx.Month + "/" + reserve.DtTrx.Day + "/" + reserve.DtTrx.Year + "/" + reserve.RenterName + "' " +
                    " style='display:block;background-color:dodgerblue;margin:20px;padding:20px;text-decoration:none;font-weight:bolder;font-size:300;color:white;border-radius:3px;'> Reservation Details </a> ";
                }
                else if (mailType == "CLIENT-PENDING")
                {
                    md.Subject = "Realwheels Reservation";   //mail title

                    CarReservation reserve = db.CarReservations.Find(jobId);

                    //mail content for pending payment
                    message = "We are happy to recieved your inquiry. We will contact you after we have processed your reservation. Please click the link below for your reservation details, Thank you. <a href='https://realwheelsdavao.com/reservation/" + jobId + "/" + reserve.DtTrx.Month + "/" + reserve.DtTrx.Day + "/" + reserve.DtTrx.Year + "/" + reserve.RenterName + "' " +
                    " style='display:block;background-color:dodgerblue;margin:20px;padding:20px;text-decoration:none;font-weight:bolder;font-size:300;color:white;border-radius:3px;'> View Booking Details </a> ";
                }
                else
                {
                    //send email in /joborder
                    //id = jobMain Id

                    JobMain job = db.JobMains.Find(jobId);

                    md.Subject = "Realwheels Reservation";   //mail title

                    //mail content for client inquiries
                    //message = " Your inquiry have been processed to confirm your reservation, please follow the link for the invoice and payment. <a href='https://realwheelsdavao.com/JobOrder/BookingDetails/" + jobId.ToString() + "?iType=1' " +
                    //" style='display:block;background-color:dodgerblue;margin:20px;padding:20px;text-decoration:none;font-weight:bolder;font-size:300;color:white;border-radius:3px;'> View Booking Details </a> ";
                    message = " Your inquiry have been processed to confirm your reservation, please follow the link for the invoice and payment. <a href='https://realwheelsdavao.com/invoice/" + jobId + "/" + job.JobDate.Month + "/" + job.JobDate.Day + "/" + job.JobDate.Year + "/" + job.Description + "' " +
                    " style='display:block;background-color:dodgerblue;margin:20px;padding:20px;text-decoration:none;font-weight:bolder;font-size:300;color:white;border-radius:3px;'> View Booking Details </a> ";
                }

                body =
                    "" +
                    // " <p>Hello {name}, You have booked a {tour} and  {unit} {type} for {days} day(s). The total cost of the package is {total}. </p>" +
                    " <div style='background-color:#f4f4f4;padding:50px' align='center'>" +
                    " <div style='background-color:white;margin:50px;padding:50px;text-align:center;color:#555555;font:normal 300 16px/21px 'Helvetica Neue',Arial'>  <h1> RealWheels Car Reservation </h1>" +
                    message +
                    " <p> This is an auto-generated email. DO NOT REPLY TO THIS MESSAGE </p> " +
                    " </div></div>" +
                    "";

                MailMessage msg = md.CreateMailMessage(renterMail, replacements, body, new System.Web.UI.Control());

                SmtpServer.Port = 587;          //default smtp port
                SmtpServer.Credentials = new System.Net.NetworkCredential("admin@realwheelsdavao.com", "Real123!");
                SmtpServer.EnableSsl = false;   //enable for gmail smtp server
                System.Net.ServicePointManager.Expect100Continue = false;
                SmtpServer.Send(msg);           //send message
                return "success";
            }
            catch (Exception ex)
            {
                return "error: " + ex;
            }
        }
    }
}