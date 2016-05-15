using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Waiver.Business;
using Waiver.Models;

namespace Waiver.Controllers
{
    public class WaiverController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(WaiverViewModel.Create());
        }

        [HttpPost]
        public ActionResult Index(WaiverViewModel model)
        {
            model.InitializeClubs();

            var waiverTemplate = Server.MapPath("/pdf/20150501.pdf");
            var signedWaiver = Server.MapPath(string.Format("/pdf/{0}_{1}_{2}.pdf", model.FirstName, model.LastName, model.Club.Replace(" " , "_")));
            var signatureFile = Server.MapPath(string.Format("/pdf/{0}_{1}_{2}.png", model.FirstName, model.LastName, model.Club.Replace(" " , "_")));

            var pdfService = new WaiverService(waiverTemplate, signedWaiver);

            Image signatureImage = null;
            byte[] data = Convert.FromBase64String(model.SignatureData.Split(',')[1]);
            using (var stream = new MemoryStream(data, 0, data.Length))
            {
                signatureImage = Image.FromStream(stream);
                signatureImage.Save(signatureFile);
            }

            pdfService.GenerateWaiver(model.FirstName.ToUpper(), model.LastName.ToUpper(), model.Guardian.ToUpper(), model.DateOfBirth.ToUpper(), model.PhoneNumber.ToUpper(), model.Club, signatureFile);

            model.SignedWaiverFileName = string.Format("{0}_{1}_{2}.pdf", model.FirstName, model.LastName, model.Club.Replace(" ", "_"));

            FileInfo signedWaiverFile = new FileInfo(signedWaiver);
            FileInfo fileOfSignature = new FileInfo(signatureFile);

            Response.AddHeader("Content-Disposition", "attachment;filename="+ model.SignedWaiverFileName );
            Response.WriteFile(signedWaiver);
            Response.Flush();

            signedWaiverFile.Delete();
            fileOfSignature.Delete();
            Response.End();
            return null;
        }
    }
}