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
    }

}