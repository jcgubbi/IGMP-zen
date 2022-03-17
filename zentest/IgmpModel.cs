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

        public static Zen<bool> IsValidHostState(Zen<Datagram> dtg, Zen<HostState> hs)
        {
            // host state
            var is_idle = hs.GetIdleMember();
            var is_nonmember = hs.GetNonMember();
            var is_delayingmember = hs.GetDelayingMember();

            var at_least_one_state = Or(is_idle == 1, is_nonmember == 1, is_delayingmember == 1);

            //var no_pairs = BitwiseAnd<bool>(BitwiseXor<bool>(is_idle, is_nonmember),
            //                    BitwiseXor<bool>(is_nonmember, is_delayingmember),
            //                    BitwiseXor<bool>(is_idle, is_nonmember));
            var no_pairs = Or(And(is_idle == 1, is_nonmember == 0, is_delayingmember == 0),
                                And(is_idle == 1, is_nonmember == 0, is_delayingmember ==0),
                                And(is_idle ==0, is_nonmember ==0, is_delayingmember==1)
                                );
            //return And(at_least_one_state, no_pairs);
            return no_pairs;
        }

        public static Zen<byte> HostQuery(Zen<Datagram> dtg, Zen<HostState> hs) {
            var version_type = dtg.GetIGMPPacket().GetVersionType();

            // specifies version 1, host query
            var zf = (Zen<byte>)0x1F;

            var group_addr = dtg.GetIGMPPacket().GetGroupAddr();

            var dest_addr = dtg.GetDstIp();
            // all_hosts_addr = 224.0.0.1 --> 3758096385 integer
            var all_hosts_addr = 3758096385;
            var equals_allhosts = dest_addr == all_hosts_addr;

            // host state
            var host_state = IsValidHostState(dtg, hs);

            return If<byte>(
                And((version_type & zf) == 1, group_addr == 0, equals_allhosts),
                1,
                0);
        }

        public static Zen<byte> HostReport(Zen<Datagram> dtg, Zen<HostState> hs) {
            var version_type = dtg.GetIGMPPacket().GetVersionType();

            var dest_addr = dtg.GetDstIp();
            // assumption: the only IP address connected to this router is:
            // 198.4.1.0 --> 3322151168
            var reported_addr = dest_addr == 3322151168;
            var ttl = dtg.GetTTL();
            var set_ttl = ttl == 1;

            var host_state = And(IsValidHostState(dtg, hs), Equals(hs.GetNonMember(), false));

            return If<byte>(
                And((version_type & 0x1F) == 2, reported_addr, set_ttl),
                1,
                0);
        }

        // IgmpMain() is the wrapper function that takes a datagram, and router state as input and returns all kinds of error which is present in it 

        public static Zen<byte> IgmpMain(Zen<Datagram> dtg, Zen<HostState> hs, Zen<byte> s1, Zen<byte> s2)
        {
            Zen<byte> v1 = HostQuery(dtg, hs);
            Zen<byte> v2 = HostReport(dtg, hs);
            
            // s1s2 --> 01 = idle member 1, 11 = delaying member 2, else nonmember 0
            var hostState = If<byte>(
                And(s1 == 0, s2 == 1),
                1,
                If<byte>(
                    And(s1 == 1, s2 == 1),
                    2,
                    0
                    )
                );

            
            var is_idle = If<byte>(hostState == 1,
                1,
                0);
            var idle = hs.GetIdleMember() == is_idle;
            var is_delayingmember = If<byte>(hostState == 2,
                1,
                0);
            var delaying = hs.GetDelayingMember() == is_delayingmember;
            var is_nonmember = If<byte>(hostState == 0,
                1,
                0);
            var nonmember = hs.GetNonMember() == is_nonmember;


            //Zen<IList<byte>> err = List(d, t);
            //var err = d * t;
            //var err = v1 * v2;
            var err = hostState * v1 * v2;
            //var err2 = idle * delaying * nonmember;
            //return a*b + a*(1-b);
            //return (r4 * r5 * r6 * r7 * r8);
            return If<byte>(
                Or(delaying, idle),
                hostState * v1 * v2,
                hostState * v1
                );
        }
    }

}