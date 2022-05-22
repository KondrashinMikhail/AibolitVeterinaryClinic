using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AibolitVeterinaryClinicContracts.BindingModels
{
    public class MailSendInfoBindingModel
    {
        public string MailAddress { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public string FileName { get; set; }
    }
}
