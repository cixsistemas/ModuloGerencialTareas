using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Presentacion.Utilitarios
{
	public class MailHelper
	{
		public void EnviarClientesDuplicados(string subject, List<EClientesDuplicados> lista)
		{
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

			using (MailMessage mailMessage = new MailMessage())
			{
				// Remitente desde configuración
				mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["MailAddress"]);
				mailMessage.Subject = subject;

				// 🔤 Codificación correcta
				mailMessage.SubjectEncoding = Encoding.UTF8;
				mailMessage.BodyEncoding = Encoding.UTF8;
				// (Opcional en .NET 4.5.2) mailMessage.HeadersEncoding = Encoding.UTF8;

				// 🔖 HTML con charset
				var sb = new StringBuilder();
				sb.AppendLine("<!DOCTYPE html>");
				sb.AppendLine("<html><head>");
				sb.AppendLine("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />");
				sb.AppendLine("<style>table, th, td {border:1px solid black; border-collapse:collapse;} th {background:#dcedc8;} th,td{padding:5px;}</style>");
				sb.AppendLine("</head><body>");
				sb.AppendLine("<h3>Buen día, se detectaron clientes duplicados en la programación:</h3>");
				sb.AppendLine("<table width='100%'>");
				sb.AppendLine("<thead><tr><th>DNI</th><th>Nombre</th><th>Ruta</th><th>Hora Viaje</th><th>Programación</th><th>Asientos</th><th>Usuario</th></tr></thead>");

				// Si quieres, puedes HtmlEncode los datos dinámicos:
				Func<string, string> enc = s => WebUtility.HtmlEncode(s ?? string.Empty);

				foreach (var item in lista)
				{
					sb.Append("<tr>");
					sb.AppendFormat("<td>{0}</td>", enc(item.DNI));
					sb.AppendFormat("<td>{0}</td>", enc(item.Nombre));
					sb.AppendFormat("<td>{0}</td>", enc(item.Ruta));
					sb.AppendFormat("<td>{0}</td>", enc(item.HoraViaje));
					sb.AppendFormat("<td>{0}</td>", enc(item.Programacion));
					sb.AppendFormat("<td>{0}</td>", enc(item.Asientos));
					sb.AppendFormat("<td>{0}</td>", enc(item.Usuario));
					sb.Append("</tr>");
				}

				sb.AppendLine("</table>");
				sb.AppendLine("<br/><h4 style='color:red;'>Atte.</h4>");
				sb.AppendLine("<h4 style='color:red;'>&Aacute;rea de Soporte &amp; Sistemas</h4>");
				sb.AppendLine("</body></html>");

				var htmlBody = sb.ToString();

				// Usa AlternateView para fijar charset y transferencia
				var htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, Encoding.UTF8, MediaTypeNames.Text.Html);
				htmlView.TransferEncoding = TransferEncoding.QuotedPrintable; // evita problemas con acentos en algunos servidores
				mailMessage.AlternateViews.Clear();
				mailMessage.AlternateViews.Add(htmlView);

				mailMessage.IsBodyHtml = true;

				// Destinatarios
				foreach (var correo in ConfigurationManager.AppSettings["To"]
							 .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
							 .Select(x => x.Trim()))
				{
					mailMessage.To.Add(correo);
				}

				using (var smtp = new SmtpClient(ConfigurationManager.AppSettings["Host"],
												 int.Parse(ConfigurationManager.AppSettings["Port"])))
				{
					// Config SMTP (según tu app.config actual)
					ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
					smtp.UseDefaultCredentials = false;
					smtp.Credentials = new NetworkCredential(
						ConfigurationManager.AppSettings["UserName"],
						ConfigurationManager.AppSettings["Password"]);
					smtp.EnableSsl = true;      // para 465/587
					smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
					smtp.Timeout = 60000;

					// (Opcional) smtp.DeliveryFormat = SmtpDeliveryFormat.International;

					smtp.Send(mailMessage);
				}
			}
		}
	}
}
