using System;

namespace code.utils.time
{
    public static class TimeUtils
    {
        public static string ObtenerHoraActual()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
    }
}
