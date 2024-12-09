using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Jw.MeetingCountdown.Commands
{
    internal class TimeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var strTime = value.ToString();
            if (!Regex.IsMatch(strTime, "^([01][0-9]|2[0-3]):([0-5][0-9])$"))
                return new ValidationResult(false, "Formato de hora inválido. Informe uma hora no formato 24h: HH:mm.");

            var arrTime = strTime.Split(':');
            var meetingStartTimeSpan = new TimeSpan(Convert.ToInt32(arrTime[0]), Convert.ToInt32(arrTime[1]), 0);
            if (meetingStartTimeSpan < DateTime.Now.TimeOfDay)
                return new ValidationResult(false, "O horário de início da reunião deve ser maior do que a hota atual.");

            return ValidationResult.ValidResult;
        }
    }
}
