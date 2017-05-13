using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Waiver.Models
{
    public class YourWaiver
    {
        public string FileName { get; set; }
    }
    public class WaiverViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Guardian { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Club { get; set; }
        public string SignatureData { get; set; }
        public IEnumerable<SelectListItem> Clubs { get; set; }
        public string SignedWaiverFileName { get; set; }

        public static WaiverViewModel Create()
        {
            return new WaiverViewModel()
            {
                Clubs = GetClubs()
            };
        }

        public void InitializeClubs()
        {
            this.Clubs = GetClubs();
        }

        private static IEnumerable<SelectListItem> GetClubs()
        {
            return new List<SelectListItem>()
                                 {
                                     new SelectListItem() { Value = "Bel Air Cricket Club", Text = "Bel Air Cricket Club" },
                                     new SelectListItem() { Value = "Defence Cricket Club", Text = "Defence Cricket Club" },
                                     new SelectListItem() { Value = "Ottawa Cricket Club", Text = "Ottawa Cricket Club" },
                                     new SelectListItem() { Value = "Canterbury Cricket Club", Text = "Canterbury Cricket Club" },
                                     new SelectListItem() { Value = "Christ Church Cathedral Cricket Club", Text = "Christ Church Cathedral Cricket Club" },
                                     new SelectListItem() { Value = "Nepean Cricket Club", Text = "Nepean Cricket Club" },
                                     new SelectListItem() { Value = "Capital United Cricket Club", Text = "Capital United Cricket Club" },
                                     new SelectListItem() { Value = "New Edinburgh Cricket Club", Text = "New Edinburgh Cricket Club" },
                                     new SelectListItem() { Value = "Royal Canadian Cricket Club", Text = "Royal Canadian Cricket Club" },
                                     new SelectListItem() { Value = "Ahmadiyya Cricket Club", Text = "Ahmadiyya Cricket Club" },
                                     new SelectListItem() { Value = "Cumberland Cricket Club", Text = "Cumberland Cricket Club" },
                                     new SelectListItem() { Value = "Kingston Cricket Club", Text = "Kingston Cricket Club" },
                                     new SelectListItem() { Value = "OVCC Juniors Cricket Club", Text = "OVCC Juniors Cricket Club" }
                                 }.AsEnumerable().OrderBy(i => i.Text);
        }
    }
}