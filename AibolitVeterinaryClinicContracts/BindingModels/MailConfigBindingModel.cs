using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AibolitVeterinaryClinicContracts.BindingModels
{
    public class MailConfigBindingModel
    {
        public string SmtpClientHost { get; set; }
        public int SmtpClientPort { get; set; }
        public string PopHost { get; set; }
        public int PopPort { get; set; }
        public string MailLogin { get; set; }
        public string MailPassword { get; set; }
        public string MailName { get; set; }
    }
}
