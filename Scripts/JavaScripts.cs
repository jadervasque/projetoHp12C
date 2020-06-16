namespace HP12C.Scripts
{
    internal class JavaScripts
    {
        public static string DYS
        {
            get
            {
                return @"
                var dx;
                var mx;
                var yx;
                var dy;
                var my;
                var yy;

                d2 = new Date(yx, mx - 1, dx, 12, 0, 0);
                d1 = new Date(yy, my - 1, dy, 12, 0, 0);

                var xx1;
                var l1 = d1.getTimezoneOffset() * 60000;
                var l2 = d2.getTimezoneOffset() * 60000;
                xx1 = Math.round(((d2.getTime() - l2) - (d1.getTime() - l1)) / 86400000);

                var yy1;
                var dd1 = d1.getDate();
                var dd2 = d2.getDate();
                var z1 = dd1;
                var z2 = dd2;
                if (dd1 == 31) {
                    z1 = 30;
                }
                if (dd2 == 31) {
                    if (dd1 >= 30) {
                        z2 = 30;
                    }
                }
                var fdt1 = 360 * d1.getFullYear() + 30 * (d1.getMonth() + 1) + z1;
                var fdt2 = 360 * d2.getFullYear() + 30 * (d2.getMonth() + 1) + z2;
                yy1 = fdt2 - fdt1;
                ";
            }
        }

        public static string DATE
        {
            get
            {
                return @"
                var xx1;
                var base;
                var day;
                var month;
                var year;
                base = new Date(year, month - 1, day, 12, 0, 0);
                base.setTime(base.getTime() + Math.floor(x) * 86400000);
                if (dmy) {
                    xx1 = base.getDate() + (base.getMonth() + 1) / 100 + base.getFullYear() / 1000000;
                } else {
                    xx1 = (base.getMonth() + 1) + base.getDate() / 100 + base.getFullYear() / 1000000;
                }
                ";
            }
        }
    }
}
