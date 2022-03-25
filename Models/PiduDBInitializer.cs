using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Kutse_App.Models
{
    public class PiduDBInitializer : CreateDatabaseIfNotExists<PiduContext>
    {
        protected override void Seed(PiduContext dpb)
        {
            base.Seed(dpb);
        }
    }
}