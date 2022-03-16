namespace ICMP_test
{
    using System.Collections.Generic;
    using System.Linq;
    using ZenLib;
    using System;
    using static ZenLib.Language;

    /// <summary>
    /// Class representing the model for IP server.
    /// </summary>
    public sealed class IgmpModel
    {

        //These are only relevant to multicast router
        //public static Zen<byte> JoinLocalGroup(Zen<Datagram> dtg, Zen<RouterState> rs) {
        //    return If<byte>(
        //        true,
        //        1,
        //        1);
        //}

        //public static Zen<byte> LeaveLocalGroup(Zen<Datagram> dtg, Zen<RouterState> rs) {
        //    return If<byte>(
        //        true,
        //        1,
        //        1);
        //}

        //public static Zen<byte> ReceiveIGMPPacket(Zen<Datagram> dtg, Zen<RouterState> rs) {}

        public static Zen<byte> HostQuery(Zen<Datagram> dtg, Zen<HostState> hs) {
            var version_type = dtg.GetIGMPPacket().GetVersionType();
            var zf = (Zen<byte>)0xF0;
            return If<byte>(
                (version_type & zf) == 1,
                1,
                0);
        }

        public static Zen<byte> HostReport(Zen<Datagram> dtg, Zen<HostState> hs) {
            var version_type = dtg.GetIGMPPacket().GetVersionType();
            return If<byte>(
                (version_type & 0xF0) == 2,
                1,
                0);
        }

        // IgmpMain() is the wrapper function that takes a datagram, and router state as input and returns all kinds of error which is present in it 

        public static Zen<IList<byte>> IgmpMain(Zen<Datagram> dtg, Zen<HostState> hs)
        {
            Zen<byte> v1 = HostQuery(dtg, hs);
            Zen<byte> v2 = HostReport(dtg, hs);
   

            var d = If<byte>(
                v1 != 0,
                1,
                0);
            var t = If<byte>(
                v2 != 0,
                1,
                0);
            

            //byte r9 = 5;

            Zen<IList<byte>> err = List(d, t);
            //return a*b + a*(1-b);
            //return (r4 * r5 * r6 * r7 * r8);
            return err;
            //Console.WriteLine(v1+v2+v3+v4+v5+v6+v7+v8);
        }
    }

}