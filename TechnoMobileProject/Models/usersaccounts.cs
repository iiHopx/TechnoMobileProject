using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using System.Drawing;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System;
using Microsoft.AspNetCore.Mvc;
using TechnoMobileProject.Models;

namespace TechnoMobileProject.Models
{
    public class usersaccounts
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string pass { get; set; }
        public string role { get; set; }
    }
}
