using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Travel_Portal
{
    public class Auth
    {
        public string key = "4g5tft6";
        public string ComputeStringToSha256Hash(string plainText)
        {
            // Create a SHA256 hash from string   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Computing Hash - returns here byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));

                // now convert byte array to a string   
                StringBuilder stringbuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringbuilder.Append(bytes[i].ToString("x2"));
                }
                return stringbuilder.ToString();
            }
        }
        public int AuthorizeAdmin(string adminid, string cookie_hash)
        {

            if (ComputeStringToSha256Hash(adminid + key) == cookie_hash)
            {
                //authorized
                return Convert.ToInt32(adminid);
            }
            else
            {
                return 0;
            }
        }
        public string AuthorizeUser(string email, string cookie_hash)
        {

            if (ComputeStringToSha256Hash(email + key) == cookie_hash)
            {
                //authorized
                return email;
            }
            else
            {
                return null;
            }
        }
        public void send_email(string code, string email)
        {
            var senderEmail = new MailAddress("aishmarch28@gmail.com", "Travel Portal");
            var receiverEmail = new MailAddress(email, "Receiver");
            var password = "*123#545";
            var sub = "OTP to Login";
            var body = ("Here is your otp to login: " + code);
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = sub,
                Body = body
            })
            {
                smtp.Send(mess);
            }

        }
    }
}
