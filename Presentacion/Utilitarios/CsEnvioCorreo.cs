using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Logica;

namespace Presentacion.Utilitarios
{
    public class CsEnvioCorreo
    {
        LBoletosWeb _LBoletosWeb = new LBoletosWeb();

        public void EnviarMensaje(string subject, List<EBoletosWeb> list)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["MailAddress"]);
                mailMessage.Subject = subject;
                mailMessage.Body = "<html><head><style>table, th, td {border: 1px solid black; border-collapse: collapse;} ";
                mailMessage.Body += "th {background-color: #dcedc8;}";
                mailMessage.Body += "</style></head><body>";
                mailMessage.Body += "<h3>Buen d&iacute;a, a continuaci&oacute;n se muestran los boletos duplicados por venta web.</h3>";
                mailMessage.Body += "<table width='100%'>";
                mailMessage.Body += "<thead><tr><th>Boleto</th>";
                mailMessage.Body += "<th>F. Compra</th>";
                mailMessage.Body += "<th>Prog.</th>";
                mailMessage.Body += "<th>Ruta</th>";
                mailMessage.Body += "<th>Itinerario</th>";
                mailMessage.Body += "<th>Asi.</th>";
                mailMessage.Body += "<th>Pasajero</th><th>Dni</th><th>Telefono</th>";
                mailMessage.Body += "<th>Precio</th>";
                //mailMessage.Body += "<th>Asiento</th>";
                mailMessage.Body += "<th>Estado</th>";
                mailMessage.Body += "</tr></thead>";
                foreach (var item in list)
                {
                    mailMessage.Body += "<tr>";
                    mailMessage.Body += "<td>" + item.BOLETO + "</td>"
                        + "<td>" + item.F_COMPRA + "</td>"
                        + "<td>" + item.PROGRAMACION + "</td>"
                        + "<td>" + item.RUTA + "</td>"
                        + "<td>" + item.ITINERARIO + "</td>"
                        + "<td>" + item.ASIENTO + "</td>"
                        + "<td>" + item.NOMBRE + "</td>" + "<td>" + item.DNI + "</td>" + "<td>" + item.TELEFONO + "</td>"
                        + "<td>" + item.PRECIO + "</td>"
                        //+ "<td>" + item.ASIENTO + "</td>"
                        + "<td>" + item.ESTADO + "</td>";
                    mailMessage.Body += "</tr>";
                }
                mailMessage.Body += "</table>";
                mailMessage.Body += "<h3 style='color:red;'>Atte.</h3>";
                mailMessage.Body += "<h3 style='color:red;'>&Aacute;rea de Soporte & Sistemas</h3>";
                mailMessage.Body += "</body>";
                mailMessage.Body += "</html>";
                //mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                char[] delimit = new char[] { ';' };
                string Destinatarios = ConfigurationManager.AppSettings.Get("To");
                // En caso de que sean multiples destinatarios recorro el Array
                foreach (string enviar_a in Destinatarios.Split(delimit))
                {
                    mailMessage.To.Add(new MailAddress(enviar_a));
                }
                //mailMessage.To.Add(new MailAddress(recepientEmail));

                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["Host"];
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
                NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);

                //enviamos el mail
                smtp.Send(mailMessage);

                //eliminamos el objeto
                smtp.Dispose();
            }

            _LBoletosWeb.MatenimientoBoletosAnuladosWeb(list, "N");
        }

        public void EnviarMensaje2()
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["Host"];
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
            NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
            smtp.SendAsync("soporte@transporteschiclayo.pe", "transchiclayo2018@gmail.com", "hola", "hola...", null);
        }
    }
}
